using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
namespace Navalmod
{
    public class torpedo : BlockScript
    {
        private MKey fire;
        private MSlider range;
        private MSlider initialvelocity;
        private MSlider unlucktime;
        private MSlider velocity;
        private MToggle magnetism;
        private MSlider tntmass;
        public MMenu type;
        private MSlider errorsize;
        private Navalhe navalhe;
        private Quaternion startrolation;
        private float high;
        private bool fireing;
        private float runtime;
        private float runtimemax;
        private bool first;
        private bool running;
        private void Awake()
        {
            try
            {
                

            }
            catch
            {

            }
                fire = AddKey("发射", "fire", KeyCode.F);
            range = AddSlider("射程(km)", "range", 10f, 1f, 20f);
            initialvelocity = AddSlider("初速度(kt)", "initialvelocity", 100f, -150f, 150f);
            unlucktime = AddSlider("发射后启动引擎时间(s)", "unlucktime", 1f, 0.1f, 5f);
            velocity = AddSlider("航速(kt)", "velocity", 30f, 10f, 50f);
            errorsize = AddSlider("故障率", "errorsize", 0.3f, 0f, 1f);
            tntmass = AddSlider("鱼雷装药(kg)", "tntmass", 300f, 50f, 1000f);
            type = AddMenu("torpedotype", 0,new List<string>{"蒸汽鱼雷","电动鱼雷", "酸素鱼雷"});
            fireing = false;
            navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            runtime = 0f;
            runtimemax = 0f;
            startrolation = new Quaternion();
            running = false;
        }
        private void Update()
        {
            if (base.BlockBehaviour.isSimulating)
            {
                if ((fire.IsPressed||fire.EmulationPressed()) && fireing == false)
                {
                    high = navalhe.high;
                    fireing = true;
                    try
                    {
                        base.BlockBehaviour.blockJoint.breakForce = -100;
                        foreach (Collider collider in Physics.OverlapSphere(base.transform.position, 1f))
                        {
                            Physics.IgnoreCollision(base.GetComponentInChildren<Collider>(), collider);
                        }
                    }
                    catch
                    {

                    }
                    
                    
                    base.Rigidbody.velocity += base.transform.right.normalized * initialvelocity.Value * 0.5114f;
                    runtimemax = ((range.Value * 1000f) / (velocity.Value * 0.5114f*3f));
                    startrolation = base.transform.rotation;
                    startrolation.x=0f;
                    startrolation.z = 0f;
                }
            }
            
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (running)
            {
                if (type.Value == 2)
                {
                    if (error)
                    {
                        erroring = true;
                    }
                    if (erroring == false)
                    {

                        base.StartCoroutine(this.explodeM());
                    }
                }
                else
                {
                    if (collision.gameObject.GetComponent<torpedo>() == null)
                    {
                        if (error)
                        {
                            erroring = true;
                        }
                        if (erroring == false)
                        {

                            base.StartCoroutine(this.explodeM());
                        }
                    }
                }
                
            }
        }
        private bool error;
        public Material boommat;
        private bool erroring;
        public ParticleSystem boompar;
        public ModTexture boomtex;
        public GameObject boomobj = new GameObject();
        public static ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        public ParticleSystem.EmissionModule emission;
        public float timeE;
        public Vector3 vvv;     
        private void FixedUpdate()
        {
            if (base.BlockBehaviour.isSimulating)
            {

                if (fireing)
                {
                    if (first == false&&runtime>=0.1f)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < errorsize.Value)
                        {
                            error = true;
                        }
                        base.Rigidbody.velocity += base.transform.right.normalized * initialvelocity.Value * 0.5114f;
                        first = true;
                        if (type.Value == 0)
                        {

                            boommat = new Material(Shader.Find("Particles/Additive"));
                            boomtex = ModResource.GetTexture("water_1");
                            boommat.mainTexture = boomtex;
                            boomobj = new GameObject(); 
                            boomobj.transform.SetParent(base.transform);
                            boomobj.transform.position = base.transform.position;
                            boompar = boomobj.AddComponent<ParticleSystem>();
                            boompar.startLifetime = 0.5f;
                            boompar.gravityModifier = 0.00001f;
                            boompar.startSize = 3f;
                            Color color;
                            color.r = 10f;
                            color.b = 10f;
                            color.g = 10f;
                            color.a = 10f;
                            boompar.startColor = color;
                            boompar.maxParticles = 100;
                            boompar.startSpeed = 0f;
                            boompar.loop = true;
                            ParticleSystem.ShapeModule shape = boompar.shape;
                            shape.shapeType = ParticleSystemShapeType.Hemisphere;
                            shape.radius = 1f;
                            boompar.simulationSpace = ParticleSystemSimulationSpace.World;
                            ParticleSystemRenderer particleSystemRenderer = boompar.GetComponent<ParticleSystemRenderer>();
                            particleSystemRenderer.material = boommat;
                            particleSystemRenderer.sortingOrder = 2;
                            Quaternion quaternion = Quaternion.identity;
                            quaternion.eulerAngles = new Vector3(-90f, 0f, 0f);
                            boomobj.transform.rotation = quaternion;
                            ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = boompar.sizeOverLifetime;
                            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(0.05f, 1f);
                            sizeOverLifetime.enabled = true;

                            emission = boompar.emission;
                            emission.enabled = false;
                            ParticleSystem.TextureSheetAnimationModule texture = boompar.textureSheetAnimation;
                            texture.enabled = true;
                            texture.numTilesX = 8;
                            texture.numTilesY = 8;
                        }
                    }

                    runtime += Time.fixedDeltaTime;
                    if (runtime >= unlucktime.Value && running == false)
                    {
                        running = true;
                        base.Rigidbody.angularDrag = 10f;
                    }
                    if (runtime >= runtimemax)
                    {
                        erroring = true;
                    }
                }
                if (vvv.magnitude!=0f)
                {
                    vvv.y = base.Rigidbody.velocity.y;
                    base.Rigidbody.velocity = vvv;
                }
                if (running)
                {
                    timeE += Time.fixedDeltaTime;
                    if (timeE >= 0.05f)
                    {
                        timeE = 0f;
                        if (erroring == false)
                        {
                            if (base.transform.position.y <= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
                            {
                                try
                                {
                                    if (type.Value == 0)
                                    {
                                        boompar.Emit(1);
                                    }
                                }
                                catch
                                {

                                }
                                base.Rigidbody.drag = 0.001f;
                                Vector3 vvvE = base.Rigidbody.velocity;
                                vvvE.y = 0f;
                                
                                Vector3 vv = base.transform.right.normalized * Mathf.Pow (velocity.Value,1.01f) * 0.5114f*3f;

                                vv.y = base.Rigidbody.velocity.y;
                                base.Rigidbody.velocity = vv;
                                vvv = vv;
                                base.transform.rotation = startrolation;
                            }   
                            if (base.transform.position.y < (SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - high))
                            {
                                if (Mathf.Abs(base.transform.position.y - (SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - high)) < 0.5f)
                                {
                                    Vector3 n = base.transform.position;
                                    n.y = SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - high;
                                    base.transform.position = n;
                                    Vector3 d = base.Rigidbody.velocity;
                                    d.y = 0f;
                                    Rigidbody.velocity = d;
                                }
                                else
                                {
                                    Vector3 n = base.Rigidbody.velocity;
                                    if (n.y <= 10) { 
                                    n.y += 10f;
                                        }
                                    Rigidbody.velocity = n;
                                }


                            }
                        }
                    }
                }
            }
            

        }
        private RaycastHit[] hitsort(RaycastHit[] hits)
        {
            List<RaycastHit> hitsE = new List<RaycastHit>(hits);
            /*foreach (RaycastHit hit in hits)
            {
                hitsE.Add(hit);
            }*/
            hitsE.Sort(delegate (RaycastHit a, RaycastHit b)
            {


                return (this.gameObject.transform.position - a.point).magnitude.CompareTo((this.gameObject.transform.position - b.point).magnitude);
            });

            return hitsE.ToArray();
        }
        public bool Breakdown(Vector3 startpos, Collider collider, float cjsdE, float r)
        {

            if (collider.gameObject.transform.position.y < SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
            {
                r *= 0.7f;
            }
            RaycastHit[] hitsE;
            
            hitsE = Physics.RaycastAll(startpos, (collider.attachedRigidbody.gameObject.transform.position - startpos).normalized, r);
            
            RaycastHit[] hits = hitsort(hitsE);

            float cjsd = cjsdE;

            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].collider.attachedRigidbody.gameObject == collider.attachedRigidbody.gameObject)
                {

                    return true;

                }
                float num4 = hits[i].transform.GetComponent<blockset>().thickness.Value;

                try
                {

                    int type = hits[i].transform.gameObject.GetComponent<blockset>().menu.Value;
                    switch (type)
                    {
                        case 0:
                            num4 *= SingleInstance<Armorlistat>.Instance.armorattributesZJ[hits[i].transform.gameObject.GetComponent<blockset>().ZJG.Value].Brinell / 200f;
                            break;
                        case 1:
                            num4 *= SingleInstance<Armorlistat>.Instance.armorattributesJG[hits[i].transform.gameObject.GetComponent<blockset>().JGG.Value].Brinell / 200f;
                            break;
                        case 2:
                            num4 *= SingleInstance<Armorlistat>.Instance.armorattributesZX[hits[i].transform.gameObject.GetComponent<blockset>().ZX.Value].Brinell / 200f;
                            break;


                    }

                }
                catch
                {

                }
                num4 /= 1000f;



                bool noblock = false;
                try
                {
                    int id = hits[i].transform.gameObject.GetComponent<BlockBehaviour>().BlockID;
                    if (id == 63 || id == 22 || id == 73)
                    {
                        noblock = true;
                    }
                }
                catch
                {

                }

                if (!noblock)

                {
                    if (cjsd < num4)
                    {

                        return false;
                    }
                    else
                    {

                        cjsd -= num4;

                    }
                }



            }

            return false;


        }
        private Rigidbody rigidbody;
        private new List<Rigidbody> rigidbodies;
        private IEnumerator Explodeforce()
        {
            float morefloat = 0f;
            this.rigidbodies = new List<Rigidbody>();
            float ra = (float)(Math.Pow(tntmassE, 0.5f) * 2 + 5f);
            bool attack = true;

            foreach (Collider collider in Physics.OverlapSphere(base.gameObject.transform.position, ra))

            {


                if (collider.attachedRigidbody && collider.attachedRigidbody.gameObject.layer != 20 && !this.rigidbodies.Contains(collider.attachedRigidbody) && collider.attachedRigidbody.gameObject.layer != 22 && collider.attachedRigidbody.tag != "KeepConstraintsAlways" && collider.attachedRigidbody.gameObject.name != "CanonBallHeavy" && collider.attachedRigidbody.gameObject.name != "CanonBallHeavy(Clone)")
                {


                    this.rigidbody = collider.attachedRigidbody;


                    if (Breakdown(this.gameObject.transform.position, collider, (float)Math.Pow(tntmassE, 0.276f) * 0.02762912f, ra))
                    {

                        this.rigidbody.WakeUp();
                        this.rigidbody.constraints = RigidbodyConstraints.None;
                        float k = (float)Math.Pow((ra - (collider.transform.position - this.transform.position).magnitude) / ra, 3f);
                        if ((collider.transform.position - this.transform.position).magnitude < 1f)
                        {
                            k = 1f;
                        }

                        try
                        {

                            BlockBehaviour bb = collider.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>();

                            try
                            {

                                int typeE = bb.GetComponent<blockset>().menu.Value;
                                //bb.BlockID == 63 || bb.BlockID == 1 || bb.BlockID == 15 || bb.BlockID == 34 || bb.BlockID == 14 || bb.BlockID == 26 || bb.BlockID == 55
                                if (bb.BlockID == 74)
                                {
                                    if (morefloat > 0)
                                    {
                                        morefloat = bb.GetComponent<sqrballoonfloat>().floatingEE(morefloat);
                                    }
                                    if (attack == true)
                                    {
                                        attack = false;

                                        morefloat += bb.GetComponent<sqrballoonfloat>().floating(tntmassE*1.6f, tntmassE);

                                    }

                                }
                                if (bb.gameObject.GetComponent<boiler>() != null)
                                {
                                    bb.gameObject.GetComponent<boiler>().HPdown(tntmassE * k * 100f);
                                }
                                else
                                {
                                    float damage = 0f;
                                    armorattribute armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesZJ[bb.GetComponent<blockset>().ZJG.Value];
                                    if (typeE == 1)
                                    {
                                        armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesJG[bb.GetComponent<blockset>().JGG.Value];
                                    }
                                    if (typeE == 2)
                                    {
                                        armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesZX[bb.GetComponent<blockset>().ZX.Value];
                                    }
                                    
                                    damage = Mathf.Max(0f, ((Mathf.Pow(tntmassE, 0.5f) + tntmassE*2f / 50f) * 25f - armorattribute.Tensile * bb.GetComponent<blockset>().thickness.Value / 100f) * 3000000000f / (armorattribute.EL * armorattribute.RA));
                                    bb.GetComponent<blockset>().thickness.Value -= Mathf.Abs((damage / bb.GetComponent<blockset>().br) * bb.GetComponent<blockset>().thickness.Value / 10f);
                                    /*
                                    ConfigurableJoint[] configurableJoints = bb.GetComponentsInChildren<ConfigurableJoint>();
                                    for (int i = 0; i < configurableJoints.Length; i++)
                                    {
                                        configurableJoints[i].breakForce -= damage;
                                        configurableJoints[i].breakTorque -= damage;
                                    }*/
                                    
                                    bb.blockJoint.breakForce -= damage;
                                    bb.blockJoint.breakTorque -= damage;

                                    foreach (Joint joint in bb.jointsToMe)
                                    {
                                        joint.breakForce -= damage;
                                        joint.breakTorque -= damage;
                                    }
                                }
                            }
                            catch
                            {

                            }

                        }
                        catch

                        {

                        }
                            
                        this.rigidbody.AddExplosionForce(this.tntmassE * 800f * k, base.gameObject.transform.position, ra, this.tntmassE * k);
                        if (navalhe.wood)
                        {
                            this.rigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * this.tntmassE * 2f);
                        }
                        else
                        {
                            this.rigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * this.tntmassE * 4000f * k);
                        }
                        this.rigidbodies.Add(this.rigidbody);




                    }

                }


            }



            yield break;
        }
        private void exf()
        {
            base.StartCoroutine(this.Explodeforce());

        }
        private void ExplodeE()
        {
            
            exf();
        }
        private bool canexplo = true;
        public void special()
        {
            try
            {
                soundplay.Instance.explosionsound(tntmassE, base.transform.position);
            }
            catch
            {

            }

            try
            {


                waterE.Instance.EmitSparks(base.transform.position, tntmassE / 10f,true);
                waterE2.Instance.EmitSparks(base.transform.position, tntmassE / 2f);

            }
            catch
            {

            }
            Destroy(boomobj);
            Destroy(base.gameObject);
        }
        public IEnumerator explodeM()
        {

                if (!StatMaster.isClient && this.canexplo)
                {
                    this.canexplo = false;
                tntmassE = tntmass.Value * 2f;
                try
                    {
                        soundplay.Instance.explosionsound(tntmassE, base.transform.position);
                    }
                    catch
                    {

                    }

                    try
                    {


                    waterE.Instance.EmitSparks(base.transform.position, tntmassE / 10f, true);
                    waterE2.Instance.EmitSparks(base.transform.position, tntmassE / 2f);
                    base.StartCoroutine(this.ExplodeES());

                    }
                    catch
                    {

                    }
                    try
                    {
                        this.ExplodeE();
                    }
                    catch
                    {

                    }
                }
            Destroy(boomobj);
            Destroy(base.gameObject);
            yield break;
        }
        public float tntmassE;
        private IEnumerator ExplodeES()
        {

            this.SendExplosionPositionToAll2();
            yield break;

        }
        private void SendExplosionPositionToAll2()
        {
            if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting)
            {
                ModNetworking.SendToAll(Messages.torpedo.CreateMessage(new object[]
                {
                    base.BlockBehaviour.BuildingBlock.Guid.ToString(),
                    (int)base.BlockBehaviour.ParentMachine.PlayerID
                }));
            }
        }
    }
}