using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
using UnityEngine.Rendering;
using System.Threading;
using SRF.UI.Layout;

namespace Navalmod
{
	// Token: 0x02000009 RID: 9
	public class NAbolt : MonoBehaviour
	{
        // Token: 0x0600001E RID: 30
        Thread thread;
        private void exf()
        {
            base.StartCoroutine(this.Explodeforce());

        }
		private void ExplodeE()
		{
			if (base.gameObject.transform.position.y < SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
			{
				this.tntmass *= 2f;
			}
			this.damage = 0f;
            exf();
            
			blotlist blotlist = new blotlist();
			blotlist.type = this.type;
			blotlist.scale = this.scale;
			blotlist.networkids = this.networkid;
			blotlist.state = 1;
			blotlist.damage = this.damage;
            if (simplehe == 0)
            {
                this.navalhe.addblot(blotlist);
            }
            
		}

		// Token: 0x0600001F RID: 31
		public IEnumerator explodeM()
		{
			if (!StatMaster.isClient && this.canexplo)
			{
				this.canexplo = false;
                try
                {
                    soundplay.Instance.explosionsound(tntmass, base.transform.position);
                }
                catch
                {

                }
                
                try
                {
                    if (simplehe==1)
                    {
                        waterE.Instance.EmitSparksR(base.transform.position, tntmass);
                    }
                    else
                    {
                        waterE.Instance.EmitSparks(base.transform.position, tntmass,false);
                    }
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
			yield break;
		}
        //炮弹弹头属性
        public float[] hetntmass = new float[2] {1f, 0.95f};
        public float[] hepenetration = new float[2] { 0f, 0.1f };
        public float[] saptntmass = new float[5] {0.85f,0.75f,0.7f,0.6f,0.5f};
        public float[] sappenetration = new float[5] {0.17f, 0.22f, 0.25f, 0.4f, 0.5f};
        public float[] aptntmass = new float[4] { 0.34f, 0.28f, 0.27f, 0.255f};
        public float[] appenetration = new float[4] { 0.73f, 0.8f, 0.94f, 1f};
        public float[] apfuzetimeE = new float[4] { 0.4f, 0.6f, 0.8f, 1f };
        public float[] sapanglereturn = new float[5] { 0f, 0f, 0f, 0f,0f };
        public float[] apanglereturn = new float[4] { 0f, 0f, 0f, 0f };
        // Token: 0x06000020 RID: 32
        public bool hit2;
        public void expl()
        {
            
                    if (this.type == 1)
                    {
                        tntmass *= hetntmass[hefuse];
                        base.StartCoroutine(this.explodeM());
                        this.destoryball();
                    }
                    if (this.type == 2)
                    {

                        tntmass *= aptntmass[aptype];
                        base.StartCoroutine(this.explodeM());
                        this.destoryball();
                    }
                    if (this.type == 3)
                    {
                        tntmass *= saptntmass[saptype];
                        base.StartCoroutine(this.explodeM());
                        this.destoryball();
                    }
            if (type == 4)
            {
                base.StartCoroutine(this.explodeM());
                this.destoryball();
            }
            if (type == 5)
            {
                base.StartCoroutine(this.explodeM());
                this.destoryball();
            }
             
        }
		private void OnCollisionEnter(Collision collision)
		{
    if (collision.gameObject.GetComponent<NAbolt>() == null)
    {
        if (hit3 == true || (blockBehaviour != collision.gameObject.GetComponent<BlockBehaviour>()))
        {
                    if (simplehe == 1)
                    {
                        target = collision;
                    }
            expl();
        }
    }
		}

		// Token: 0x06000021 RID: 33
        private TrailRenderer TrailRenderer;


        // Token: 0x06000022 RID: 34
        public bool hit3;
        public int simplehe;
        private BlockBehaviour hitbb;
        public Collision target;
        public NavalCannoBlockE NavalCannoBlockE;
        public ArrowTurretBlock arrowTurretBlock;
        public List<BlockBehaviour> blocks;
        public float time2;
        public int hefuse;
        public int saptype;
        public int aptype;
        public int apfuzetime;
        public bool waterin=false;
        public bool close;
        public List<BlockBehaviour> penetrationblock=new List<BlockBehaviour>(); 
        private Vector3 thisvector3;
        private Vector3 lastvector3;
        private float booml=114f;
        public bool inwater;
        public bool isboom;
        public Vector3 originFirePos;
        public float exploreRange;
        public bool haveView;
        public bool hasView;
        private void Update()
        {
            this.timer += Time.deltaTime;
            time2 -= Time.fixedDeltaTime;
        }
            private void FixedUpdate()
            {




            try
            {


                lastvector3 = thisvector3;
                thisvector3 = rigidbody2.velocity;
            }
            catch
            {

            }
            
                    bool flag = base.transform.position.y < SingleInstance<Sea>.Instance.Getseahigh (base.transform.position) && this.waterin;
                    if (flag)
                    {
                        this.waterin = false;
                        SingleInstance<waterE2>.Instance.EmitSparks(base.transform.position, this.scale);
                        base.StartCoroutine(this.ExplodeEX());
                    }

            try
            {
                if (hasView == true)
                {
                    if ((view.gameObject.transform.position-base.gameObject.transform.position).magnitude<1000f)
                    {
                        hasView = false;
                        view.errorMulti += thisvector3.magnitude / 50000f;
                    }
                    
                }
            }
            catch
            {

            }
            
            if ((time2 < 0f&&(!StatMaster.isClient|| simplehe == 1)) || hit2)
            {
                time2 = 0.05f;
                // this.rigidbody2.AddTorque(Vector3.Cross(-this.transform.forward, rigidbody.velocity) * 50f);


                this.transform.up = this.rigidbody2.velocity.normalized;

                if (this.timer < 0f && this.timer > -1f)
                    {
                        this.timer = 0f;
                        expl();
                    }
                    if (this.rigidbody2.velocity.magnitude < 2f && this.timer > 0.05f)
                    {
                        expl();
                    }
                if (this.transform.position.y < SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
                { 

                    if (!inwater)
                    {   
                        Vector3 vector = rigidbody2.velocity;

                        Vector3 noyv = vector;
                        noyv.y = 0;

                        
                        vector.y += noyv.magnitude*0.2f;

                        inwater = true;

                    }
                    if (!isboom)
                    {
                        Vector3 vector3 = rigidbody2.velocity;
                        rigidbody2.drag = scale * 20f / mass / apfuzetimeE[apfuzetime];

                        vector3.y += 250f;
                        if (vector3.y > -10 && SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - this.transform.position.y > 1f)
                        {
                            vector3.y = -10;
                        }
                        rigidbody2.velocity = vector3;
                    }
                    else
                    {
                        Vector3 vector3 = rigidbody2.velocity;
                        rigidbody2.drag = 1f;

                        Vector3 noyv = vector3;
                        noyv.y = 0;


                        vector3.y += noyv.magnitude * 0.2f;
                        if (vector3.y > -10 && SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - this.transform.position.y > 1f)
                        {
                            vector3.y = -10;
                        }
                        rigidbody2.velocity = vector3;
                    }
                    if (isboom)
                    {
                        if (booml == 114f)
                        {
                            booml = 2f;
                        }
                    }
                    else
                    {
                        if (booml == 114f)
                        {
                            booml = 0.1f * apfuzetimeE[apfuzetime];
                        }
                    }
                    


                }
                else
                {
                    if (inwater)
                    {
                        rigidbody2.drag = 0.05f;
                        inwater = false;
                        waterin = true;
                    }

                }
                    
                /*if (this.transform.position.y < (this.seahigh - (scale / 60f)))
                {
                    if (!this.wood)
                    {
                        expl();
                    }
                    else if (this.rigidbody2.velocity.magnitude < 20f)
                    {
                        expl();
                    }
                }*/

                if (booml != 114f)
                {
                    booml -= Time.fixedDeltaTime;
                    if (booml <= 0f)
                    {
                        expl();
                    }
                }
                if (simplehe == 1)
                {
                    if (Physics.Raycast(new Ray(this.gameObject.transform.position, this.rigidbody2.velocity), out RaycastHit raycastHit, this.rigidbody2.velocity.magnitude / 20f))
                    {
                        if (raycastHit.collider.transform.gameObject.GetComponent<NAbolt>() == null)
                        {
                            base.transform.position = raycastHit.point;
                        }
                        expl();
                    }
                }
                    if (this.type == 4 && this.timer > 0.1f)
                    {
                        Collider[] array2 = Physics.OverlapSphere(this.gameObject.transform.position, (this.tntmass * 1.8f) + 16f);
                        for (int j = 0; j < array2.Length; j++)
                        {
                            if (array2[j].transform.gameObject.GetComponent<NAbolt>() == null && array2[j].transform.gameObject.GetComponent<ArrowController>() == null)
                            {

                                expl();
                                break;
                            }
                        }
                    }

                    if (this.type == 5 )
                    {
                    if (haveView) {
                       if((originFirePos - base.transform.position).magnitude > exploreRange)
                        {
                            expl();
                        }
                    }
                    else
                    {
                        if (this.timer > 0.15f)
                        {
                            Collider[] array3 = Physics.OverlapSphere(this.gameObject.transform.position, this.tntmass * UnityEngine.Random.Range(0.1f, 4.5f) + 16f);
                            for (int k = 0; k < array3.Length; k++)
                            {
                                if (array3[k].transform.gameObject.GetComponent<NAbolt>() == null && array3[k].transform.gameObject.GetComponent<ArrowController>() == null)
                                {
                                    expl();
                                    break;
                                }
                            }
                        }
                    }
                    }
                    if ((this.type == 1 && hefuse == 1) || type == 2 || type == 3)
                    {

                        RaycastHit raycastHit = default(RaycastHit);
                        Physics.Raycast(this.gameObject.transform.position, this.rigidbody2.velocity, out raycastHit, this.rigidbody2.velocity.magnitude / 15f + 2f);
                        BlockBehaviour component2 = raycastHit.transform.gameObject.GetComponent<BlockBehaviour>();
                        if (component2 != null && this.blockBehaviour != component2)    
                        {

                            RaycastHit[] Hits = Physics.RaycastAll(this.gameObject.transform.position, this.rigidbody2.velocity, this.rigidbody2.velocity.magnitude / 10f + 3f);

                            RaycastHit[] raycastHits = hitsort(Hits);
                        for (int i = 0; i < raycastHits.Length; i++)
                            {
                                
                                BlockBehaviour component3 = raycastHits[i].transform.gameObject.GetComponent<BlockBehaviour>();
                                if (this.cjxs == 0f)
                                {

                                    float v3 = this.rigidbody2.velocity.magnitude;
                                    if (!this.wood)
                                    {
                                        
                                            v3 /=3f;
                                        
                                    }
                                float normalpe = v3 * v3 * this.mass / Mathf.Pow(scale, 2f) / 4.5f;
                                    if (type == 1)
                                    {

                                        if (this.wood)
                                        {
                                            this.cjxs = v3 * v3 * this.mass / base.transform.localScale.z / 3000000f * 3.4f;
                                        }
                                        else
                                        {
                                            this.cjxs = normalpe * hepenetration[hefuse];
                                        }
                                    }
                                    if (type == 2)
                                    {
                                        if (this.wood)
                                        {
                                            this.cjxs = v3 * v3 * this.mass / base.transform.localScale.z / 107000f * 3.4f;
                                        }
                                        else
                                        {
                                            this.cjxs = normalpe * appenetration[aptype];
                                        }
                                    }
                                    if (type == 3)
                                    {
                                        if (this.wood)
                                        {
                                            this.cjxs = v3 * v3 * this.mass / base.transform.localScale.z / 750000f * 3.4f;
                                        }
                                        else
                                        {
                                            this.cjxs = normalpe * sappenetration[saptype];
                                        }
                                    }
                                    this.blockBehaviour = new BlockBehaviour();
                                }
                               
                                float num2 = component3.GetComponent<blockset>().thickness.Value;
                                
                                double angle = new double();
                                try
                                {
                                if (component3.GetComponent<boiler>() != null)
                                {
                                    component3.GetComponent<boiler>().HPdown(mass * 200f);
                                }
                                if (component3.BlockID == 74)
                                {
                                    component3.GetComponent<sqrballoonfloat>().floating(scale, tntmass);
                                    break;
                                }
                                int type = component3.GetComponent<blockset>().menu.Value;
                                    switch (type)
                                    {
                                        case 0:
                                            num2 *= SingleInstance<Armorlistat>.Instance.armorattributesZJ[component3.GetComponent<blockset>().ZJG.Value].Brinell / 200f;
                                            break;
                                        case 1:
                                            num2 *= SingleInstance<Armorlistat>.Instance.armorattributesJG[component3.GetComponent<blockset>().JGG.Value].Brinell / 200f;
                                        break;
                                        case 2:
                                            num2 *= SingleInstance<Armorlistat>.Instance.armorattributesZX[component3.GetComponent<blockset>().ZX.Value].Brinell / 200f;
                                        break;


                                    }

                                }
                                catch   
                                {

                                }
                            float xxx = Mathf.Min(component3.transform.localScale.x, Mathf.Min(component3.transform.localScale.y, component3.transform.localScale.z));
                                if (xxx == component3.transform.localScale.y)
                                {
                                    
                                    angle = Math.Abs((double)Vector3.Angle(component3.transform.up, this.rigidbody2.transform.up));
                                    num2 += Math.Abs((float)Math.Tan(angle * (Math.PI * 2f / 360f))) * num2;

                                }
                                if (xxx == component3.transform.localScale.x)
                                {
                                    angle = Math.Abs((double)Vector3.Angle(component3.transform.right, this.rigidbody2.transform.up));
                                    num2 += Math.Abs((float)Math.Tan(angle * (Math.PI * 2f / 360f))) * num2;
                                }
                                if (xxx == component3.transform.localScale.z)
                                {
                                    angle = Math.Abs((double)Vector3.Angle(component3.transform.forward, this.rigidbody2.transform.up));
                                    num2 += Math.Abs((float)Math.Tan(angle * (Math.PI * 2f / 360f))) * num2;
                                }
                                num2 = Math.Abs(num2);

                                bool noblock = false;
                                try
                                {
                                if (penetrationblock.Find(b => b == component3) != null)
                                {
                                    noblock = true;
                                }
                                    int id = component3.BlockID;
                                    if (id == 63 || id == 22 || id == 73)
                                    {
                                        noblock = true;
                                    }
                                }
                                catch
                                {

                                }


                                if ((this.cjxs > num2 && this.blockBehaviour != component3) || noblock)
                                {
                                    if (!noblock)
                                    {
                                        blotlist blotlist = new blotlist();
                                        blotlist.type = this.type;
                                        blotlist.scale = this.scale;
                                        blotlist.networkids = this.networkid;
                                        blotlist.state = 3;
                                        blotlist.penetrating = cjxs;
                                        blotlist.penetratingE = num2;
                                        this.navalhe.addblot(blotlist);
                                    penetrationblock.Add(component3);
                                    }
                                    this.blockBehaviour = component3;
                                    try
                                    {
                                        hit3 = false;
                                        hit2 = true;
                                        Physics.IgnoreCollision(this.GetComponent<Collider>(), raycastHits[i].collider);
                                    if (!noblock)
                                    {
                                        raycastHits[i].collider.attachedRigidbody.AddExplosionForce(cjxs * scale*0.1f, this.gameObject.transform.position, mass, 0f);
                                    }
                                    else
                                    {

                                        raycastHits[i].collider.attachedRigidbody.AddExplosionForce(tntmass * 100f, this.gameObject.transform.position, mass, 0f);
                                    }
                                    if (component3.BlockID != 74)
                                    {
                                        int typeE = component3.GetComponent<blockset>().menu.Value;
                                        float damage = 0f;
                                        armorattribute armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesZJ[component3.GetComponent<blockset>().ZJG.Value];
                                        if (typeE == 1)
                                        {
                                            armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesJG[component3.GetComponent<blockset>().JGG.Value];
                                        }
                                        if (typeE == 2)
                                        {
                                            armorattribute = SingleInstance<Armorlistat>.Instance.armorattributesZX[component3.GetComponent<blockset>().ZX.Value];
                                        }

                                        damage = Mathf.Max(0f, (mass / 2f - armorattribute.Tensile * component3.GetComponent<blockset>().thickness.Value / 100f) * 3000000000f / (armorattribute.EL * armorattribute.RA));
                                        component3.GetComponent<blockset>().thickness.Value -= Mathf.Abs((damage / component3.GetComponent<blockset>().br) * component3.GetComponent<blockset>().thickness.Value / 10f);
                                        //1145141919810
                                        /*
                                        ConfigurableJoint[] configurableJoints = component3.GetComponentsInChildren<ConfigurableJoint>();
                                        for (int n = 0; n < configurableJoints.Length; n++)
                                        {
                                            configurableJoints[n].breakForce -= damage;
                                            configurableJoints[n].breakTorque -= damage;
                                        }*/
                                        
                                        try
                                        {
                                            component3.blockJoint.breakForce -= damage;
                                            component3.blockJoint.breakTorque -= damage;
                                        }
                                        catch
                                        {

                                        }

                                        foreach (Joint joint in component3.jointsToMe)
                                        {
                                            try
                                            {
                                                joint.breakForce -= damage;
                                                joint.breakTorque -= damage;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                }
                                    catch
                                    {
                                        ModConsole.Log("出错了啊啊啊啊啊啊");
                                    }
                                    if ((cjxs/5f)<num2&& booml==114f)
                                    {
                                    booml = 0.3f* (63f / (this.rigidbody2.velocity.magnitude / 10f + 3f)) * Mathf.Pow(apfuzetimeE[apfuzetime], 0.8f)+0.02f;
                                    }
                                    if (!noblock)
                                    {
                                        this.cjxs -= num2;
                                        
                                    }
 
                                }
                                if (this.cjxs < num2 && this.blockBehaviour != component3)
                                {
                                    i = raycastHits.Length;
                                    blotlist blotlist = new blotlist();
                                    if (type == 1)
                                    {
                                        if (angle > 90f)
                                        {
                                            angle = angle - 90f;
                                        }
                                        if (angle < 85f)
                                        {
                                            hit3 = true;
                                            blotlist.state = 2;
                                        }
                                        else
                                        {
                                            hit3 = false;
                                            blotlist.state = 4;
                                        }
                                    }
                                    if (type == 2)
                                    {
                                        if (angle > 90f)
                                        {
                                            angle = angle - 90f;
                                        }
                                        if (angle < 70f)
                                        {
                                            hit3 = true;
                                            blotlist.state = 2;
                                        }
                                        else
                                        {
                                            hit3 = false;
                                            blotlist.state = 4;
                                        }
                                    }
                                    if (type == 3)
                                    {
                                        if (angle > 90f)
                                        {
                                            angle = angle - 90f;
                                        }
                                        if (angle < 70f)
                                        {
                                            hit3 = true;
                                            blotlist.state = 2;
                                        }
                                        else
                                        {
                                            hit3 = false;
                                            blotlist.state = 4;
                                        }
                                    }


                                    blotlist.type = this.type;
                                    blotlist.scale = this.scale;
                                    blotlist.networkids = this.networkid;

                                    blotlist.penetrating = cjxs;
                                    blotlist.penetratingE = num2;
                                    this.blockBehaviour = component3;
                                    this.navalhe.addblot(blotlist);
                                }
                            }
                        }
                    
                }




            }
            
            
		}

		// Token: 0x06000023 RID: 35
		private void SendExplosionPositionToAll(Vector3 position)
		{
			if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting)
			{
				ModNetworking.SendToAll(Messages.bultboomtype.CreateMessage(new object[]
				{
					position,
					this.scale
				}));
			}
		}

		// Token: 0x06000024 RID: 36
		private IEnumerator ExplodeEX()
		{


            
            this.SendExplosionPositionToAll(base.gameObject.transform.position);
            
            yield break;
		}

        public Material boommat;
        public ParticleSystem boompar;
        public GameObject boomobj;
        public static ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        public ModTexture boomtex = ModResource.GetTexture("boomtex");


		// Token: 0x06000025 RID: 37
		private IEnumerator ExplodeES()
		{
           
            this.SendExplosionPositionToAll2(base.gameObject.transform.position);
            
            /*
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(PrefabMaster.LevelPrefabs[this.levelBombCategory].GetValue(this.levelBomb2ID).gameObject, base.gameObject.transform.position, base.gameObject.transform.rotation, base.transform);
			
			ExplodeOnCollide component = gameObject.GetComponent<ExplodeOnCollide>();
			gameObject.transform.localScale = Vector3.one * (float)Math.Pow((double)(this.tntmass * 2f), 0.5) / 1.5f;
			component.radius = 0f;
			component.power = 0f;
			component.torquePower = 0f;
			component.upPower = 0f;
			component.Explodey();
            
            UnityEngine.Object.Destroy(gameObject);*/
            yield break;

		}

		// Token: 0x06000026 RID: 38
		private void SendExplosionPositionToAll2(Vector3 position)
		{
			if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting)
			{
				ModNetworking.SendToAll(Messages.bultboom2type.CreateMessage(new object[]
				{
					position,
					this.tntmass
				}));
			}
		}
        public string MyGUID;
        public int ccount;
		// Token: 0x06000027 RID: 39
        public void desrotyballclient()
        {
            this.NavalCannoBlockE.nAbolts.Remove(this);
            UnityEngine.Object.Destroy(base.gameObject);
        }

		public void destoryball()
		{
            if (simplehe == 2)
            {
                Destroy(base.gameObject.GetComponent<H3NetworkBlock>());
                
            }
            UnityEngine.Object.Destroy(gameObject);
            if (simplehe == 1)
            {  
                

            }
            else
            {

                if (StatMaster.isHosting)
                {

                    /*
                    try
                    {



                        Message balldestroy = Messages.balldestroy.CreateMessage(new object[]
                        {
                    this.MyGUID,
                    ccount,
                    player,
                    simplehe
                        });
                        ModNetworking.SendToAll(balldestroy);
                    }
                    catch
                    {

                    }
                    this.NavalCannoBlockE.nAbolts.Remove(this);*/
                    UnityEngine.Object.Destroy(gameObject);
                }
            }
            
        }
        
        public int player;
        // Token: 0x06000028 RID: 40
        public view view;

		// Token: 0x06000029 RID: 41
		private IEnumerator delay()
		{
			yield return new WaitForFixedUpdate();
			yield break;
		}

		// Token: 0x06000454 RID: 1108
		private IEnumerator Explodeforce()
		{
                float morefloat=0f;
                this.rigidbodies = new List<Rigidbody>();
                float ra = (float)(Math.Pow(tntmass, 0.5f) * 2 + 5f);
                bool attack = true;

                foreach (Collider collider in Physics.OverlapSphere(base.gameObject.transform.position, ra))

                {
                

                    if (collider.attachedRigidbody && collider.attachedRigidbody.gameObject.layer != 20 && !this.rigidbodies.Contains(collider.attachedRigidbody) && collider.attachedRigidbody.gameObject.layer != 22  && collider.attachedRigidbody.gameObject.name != "h3shell")
                    {


                        this.rigidbody = collider.attachedRigidbody;
                    /*
                    try
                    {
                        int type = collider.attachedRigidbody.gameObject.GetComponent<blockset>().menu.Value;
                        if (type == 4)
                        {
                            BlockBehaviour bc = collider.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>();
                            bc.blockJoint.breakForce -= 10000f * (tntmass / (this.gameObject.transform.position - bc.transform.position).magnitude);
                                if (bc.BlockHealth.health > 0f)
                                {
                                bc.BlockHealth.DamageBlock((tntmass / (this.gameObject.transform.position - bc.transform.position).magnitude) * 2f);
                                }
                            
                        }
                    }
                    catch
                    {

                    }*/



                    if (Breakdown(this.gameObject.transform.position, collider, (float)Math.Pow(tntmass, 0.276f) * 0.02762912f, ra))
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
                                if (bb.GetComponent<torpedo>() != null)
                                {

                                    if (bb.GetComponent<torpedo>().type.Value == 2)
                                    {
                                        if (UnityEngine.Random.Range(0f, 10f) <= tntmass * k)
                                        {
                                            base.StartCoroutine(bb.GetComponent<torpedo>().explodeM());
                                        }
                                    }
                                    else
                                    {
                                        if (UnityEngine.Random.Range(0f, 100f) <= tntmass * k)
                                        {
                                            base.StartCoroutine(bb.GetComponent<torpedo>().explodeM());
                                        }
                                    }

                                }
                                if (bb.gameObject.GetComponent<boiler>() != null)
                                {
                                    bb.gameObject.GetComponent<boiler>().HPdown(tntmass * k*100f);
                                }
                                if (bb.BlockID == 74)
                                {
                                    if (morefloat > 0)
                                    {
                                        morefloat = bb.GetComponent<sqrballoonfloat>().floatingEE(morefloat);
                                    }
                                    if (attack == true)
                                    {
                                        attack = false;
                                        if ((type == 1 & hefuse != 1) || type == 4 || type == 5)
                                        {

                                        }
                                        else
                                        {
                                            morefloat = bb.GetComponent<sqrballoonfloat>().floatingE(scale, tntmass);

                                        }

                                        morefloat += bb.GetComponent<sqrballoonfloat>().floating(scale, tntmass);

                                    }

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
                                    //1145141919810
                                    damage = Mathf.Max(0f, ((Mathf.Pow(tntmass, 0.5f) + mass / 50f) * 25f - armorattribute.Tensile * bb.GetComponent<blockset>().thickness.Value / 100f) * 3000000000f / (armorattribute.EL * armorattribute.RA));
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

                            this.rigidbody.AddExplosionForce(this.tntmass *800f * k, base.gameObject.transform.position, ra, this.tntmass * k);
                            if (this.wood)
                            {
                                this.rigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * this.tntmass * 2f);
                            }
                            else
                            {
                                this.rigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * this.tntmass * 4000f * k);
                            }
                            this.damage += this.tntmass;
                            this.rigidbodies.Add(this.rigidbody);




                        }

                    }

                
                }

            
			
			yield break;
		}
        public RaycastHit[] raycasts(Vector3 startpos, Vector3 dis, float length)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(startpos, dis, length);

            return hits;
        }
        private static int HitComparison(RaycastHit a, RaycastHit b)
        {
            
            if (a.distance <= b.distance)
            {
                return -1;
            }
            return 1;
        }
        private static int HitComparisonE(Vector3 a, Vector3 b)
        {
            if (a.magnitude <= b.magnitude )
            {
                return -1;
            }
            return 1;
        }
        private RaycastHit[] hitsort(RaycastHit[] hits)
        {
            List<RaycastHit> hitsE = new List<RaycastHit>(hits);
            /*foreach (RaycastHit hit in hits)
            {
                hitsE.Add(hit);
            }*/
            hitsE.Sort(delegate(RaycastHit a, RaycastHit b)
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
            if (lastvector3 != null)
            {
                hitsE = Physics.RaycastAll(startpos - (lastvector3.normalized * (scale / 1000f)), (collider.attachedRigidbody.gameObject.transform.position - startpos + lastvector3.normalized * (scale / 1000f)).normalized, r);
            }
            else
            {
                hitsE = Physics.RaycastAll(startpos, (collider.attachedRigidbody.gameObject.transform.position - startpos).normalized, r);
            }
            RaycastHit[] hits = hitsort(hitsE);
             
            float cjsd = cjsdE; 
          
            for (int i = 0 ; i < hits.Length; i++)
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
                try {
                    int id = hits[i].transform.gameObject.GetComponent<BlockBehaviour>().BlockID;
                    if (id == 63|| id == 22|| id == 73)
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
        public void Awake()
        {
            base.name = "h3shell";
        }
        // Token: 0x0400001A RID: 26
        public float force_radius;

		// Token: 0x0400001B RID: 27
		public float mass;

		// Token: 0x0400001C RID: 28
		public float tntmass;

		// Token: 0x0400001D RID: 29
		public float damping;

		// Token: 0x0400001E RID: 30
		public int type;

		// Token: 0x0400001F RID: 31
		public CanonBlock parentsCB;

		// Token: 0x04000020 RID: 32
		public GameObject blot;

		// Token: 0x04000021 RID: 33
		private Rigidbody rigidbody;

		// Token: 0x04000022 RID: 34
		private List<Rigidbody> rigidbodies;

		// Token: 0x04000023 RID: 35
		public Rigidbody rigidbody2;

		// Token: 0x04000024 RID: 36
		public float timer;

		// Token: 0x04000025 RID: 37
		public float cjxs;

		// Token: 0x04000026 RID: 38
		public BlockBehaviour blockBehaviour;

		// Token: 0x04000027 RID: 39
		public bool boom;

		// Token: 0x04000028 RID: 40
		public bool asd;

		// Token: 0x04000029 RID: 41
		private readonly int levelBombCategory = 4;

		// Token: 0x0400002A RID: 42
		private readonly int levelBombID = 5001;

		// Token: 0x0400002B RID: 43
		private readonly int levelBomb2ID = 5003;

		// Token: 0x0400002C RID: 44
		public bool ycyx;

		// Token: 0x0400002D RID: 45
		public bool wood;
        
		// Token: 0x0400002E RID: 46
		public float seahigh;

		// Token: 0x0400002F RID: 47
		private AudioSource explosionvoice;

		// Token: 0x04000030 RID: 48
		public bool canexplo;

		// Token: 0x04000031 RID: 49
		private bool massright;

		// Token: 0x04000032 RID: 50
		public Navalhe navalhe;

		// Token: 0x0400015E RID: 350
		public float scale;

		// Token: 0x0400015F RID: 351
		public int networkid;

		// Token: 0x0400050F RID: 1295
		private float damage;
	}
}
