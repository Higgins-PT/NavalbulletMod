using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
using UnityEngine.SocialPlatforms;
using System.Runtime.InteropServices;

namespace Navalmod
{
	// Token: 0x02000014 RID: 20
	public class NavalCannoBlockE : MonoBehaviour
	{
		// Token: 0x06000072 RID: 114
        public bool fireture;
        public bool fireing;

        private bool fire;
        public Material boommat4;
        public ParticleSystem boompar4;
        public GameObject boomobj4;
        public static ParticleSystem.EmitParams emitParams4 = default(ParticleSystem.EmitParams);
        public ModTexture boomtex4;
        public MMenu trail;
        public MSlider autofireangle;
        public void Effect()
        {
            boomobj.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos);
            boomobj2.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos - new Vector3(0f, 25f * scale.Value / 406f / this.transform.localScale.y, 0f));
            boomobj.transform.forward = -base.transform.up;
            boomobj2.transform.forward = -base.transform.up;

            fire = false;
            emitParams.startSize = 20f * scale.Value / 406f;
            Color color;
            color.r = 1f;
            color.b = 1f;
            color.g = 1f;
            color.a = 10f;
            emitParams.startColor = color;
            emitParams.applyShapeToPosition = true;
            emitParams.startLifetime = 10f * scale.Value / 406f;
            boompar.Emit(emitParams, 8);
            waterE.Instance.EmitSparksE2(boomobj.transform.position, scale.Value / 4f);
            emitParams.startSize = 20f * scale.Value / 406f;
            boompar2.Emit(emitParams, 20);
        }
        private void FixedUpdate()
        {
            if (CB.isSimulating)
            {
                boomobj.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos);
                boomobj2.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos - new Vector3(0f, 25f * scale.Value / 406f / this.transform.localScale.y, 0f));
                boomobj.transform.forward = -base.transform.up;
                boomobj2.transform.forward = -base.transform.up;
                if (fire)
                {
                    
                }
            }
        }
        public view view;
        public void fireE()
        {
            try
            {
                if (CB.blockJoint.breakForce > 0f)
                {

                    if(salfefirerange.Value == 0)
                    {
                        fire = true;
                        this.shootsound();
                        base.StartCoroutine(this.shoot());
                    }
                    else
                    {
                        if (!Physics.Raycast(this.CB.transform.TransformPoint(this.CB.boltSpawnPos), -CB.transform.up,salfefirerange.Value))
                        {
                            fire = true;
                            this.shootsound();
                            base.StartCoroutine(this.shoot());
                        }
                    }
                    
                }
            }
            catch
            {

            }
        }
        private void Update()
		{
            relodingtime -= Time.deltaTime;
            if ((StatMaster.InGlobalPlayMode || StatMaster.InLocalPlayMode || this.navalhe.open))
			{
                
				
                if(relodingtime < 0f&& fireture==false)
                {
                    
                    fireture = true;
                }
                
                try
                {
                    if ((this.CB.ShootKey.IsHeld || this.CB.ShootKey.EmulationHeld(false)) && fireture)
                    {


                        if (holdfire.IsActive)
                        {



                            this.canshoot = false;
                            this.timer = 0f;
                            relodingtime = (relodtime.Value + UnityEngine.Random.Range(0f, ranrelod.Value)) / navalhe.retime;
                            fireture = false;


                            
                            fireE();



                        }
                        else
                        {
                            if (this.canshoot)
                            {
                                this.canshoot = false;
                                this.timer = 0f;
                                relodingtime = (relodtime.Value + UnityEngine.Random.Range(0f, ranrelod.Value)) / navalhe.retime;
                                fireture = false;
                                
                                fireE();


                            }
                        }



                    }

                    if (this.CB.ShootKey.IsReleased || this.CB.ShootKey.EmulationReleased())
                    {
                        this.canshoot = true;
                    }
                    if (this.HEkey.IsPressed)
                    {
                        this.type = 1;

                    }
                    if (this.APkey.IsPressed)
                    {
                        this.type = 2;

                    }
                    if (this.SAPkey.IsPressed)
                    {
                        this.type = 3;

                    }
                    if (this.VTkey.IsPressed)
                    {
                        this.type = 4;

                    }
                    if (this.timekey.IsPressed)
                    {
                        this.type = 5;

                    }
                    if (ass == 1)
                    {

                        ass = 2;
                    }
                    if (ass == 2)
                    {
                        ass = 3;
                        try
                        {
                            if (CB.isSimulating && flaktext.Value != "")
                            {

                                try
                                {
                                    foreach (flakN flakN in AIflak.Instance.flakNs)
                                    {
                                        if (flakN.name == flaktext.Value && flakN.play == CB.BuildingBlock.ParentMachine.PlayerID)
                                        {
                                            view = flakN.view;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {

                        }
                        
                    }
                }
                catch
                {

                }

                try
                {
                    if (Vector3.Angle((-CB.transform.up.normalized * view.rangelast).normalized, ((view.lockg.transform.position - view.flakdpos) - CB.transform.position).normalized) < autofireangle.Value && fireture && flaktext.Value != ""&& view.autofire.IsActive&& view.locktype!=0)
                    {
                        boomobj.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos);
                        boomobj2.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos - new Vector3(0f, 25f * scale.Value / 406f / this.transform.localScale.y, 0f));
                        boomobj.transform.forward = -base.transform.up;
                        boomobj2.transform.forward = -base.transform.up;
                        this.timer = 0f;
                        relodingtime = (relodtime.Value + UnityEngine.Random.Range(0f, ranrelod.Value)) / navalhe.retime;
                        fireture = false;


                        fireE();

                    }
                }
                catch
                {

                }

                if (this.ass == 0)
				{
                    try
                    {
                        this.ass = 1;

                        SafeAwakeE();
                        boommat4 = new Material(Shader.Find("Particles/Alpha Blended"));
                        boomtex4 = ModResource.GetTexture("boomtex");
                        boommat4.mainTexture = boomtex4;
                        try
                        {
                            UnityEngine.Object.Destroy(this.boomobj4);
                        }
                        catch
                        {

                        }
                        boomobj4 = new GameObject();
                        UnityEngine.Object.DontDestroyOnLoad(boomobj);
                        boompar4 = boomobj4.AddComponent<ParticleSystem>();
                        boompar4.startLifetime = 3f * scale.Value / 406f;
                        boompar4.startSpeed = 0f;
                        boompar4.gravityModifier = -0.03f;
                        boompar4.startSize = 25f * scale.Value / 406f;
                        Color color4;
                        color4.r = 225f;
                        color4.b = 225f;
                        color4.g = 225f;
                        color4.a = 100f;
                        boompar4.startColor = color4;
                        ParticleSystem.EmissionModule emissionModule = boompar4.emission;
                        boompar4.loop = false;
                        ParticleSystem.ShapeModule shape4 = boompar4.shape;
                        shape4.shapeType = ParticleSystemShapeType.Sphere;
                        shape4.radius = 0.001f;

                        boompar4.simulationSpace = ParticleSystemSimulationSpace.World;
                        ParticleSystemRenderer particleSystemRenderer4 = boompar4.GetComponent<ParticleSystemRenderer>();

                        particleSystemRenderer4.material = boommat4;
                        particleSystemRenderer4.sortingOrder = 1;
                        ParticleSystem.ColorOverLifetimeModule colorOverLifetime4 = boompar4.colorOverLifetime;
                        colorOverLifetime4.color = new ParticleSystem.MinMaxGradient(Color.white, new Color(255f, 255f, 255f, 0f));
                        ParticleSystem.SizeOverLifetimeModule sizeOverLifetime4 = boompar4.sizeOverLifetime;
                        sizeOverLifetime4.size = new ParticleSystem.MinMaxCurve(0.5f, 1f);
                        sizeOverLifetime4.enabled = true;
                        ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity4 = boompar4.limitVelocityOverLifetime;
                        limitVelocity4.enabled = true;
                        limitVelocity4.dampen = 0.2f;
                        ParticleSystem.EmissionModule emission4 = boompar4.emission;
                        emission4.enabled = false;
                        ParticleSystem.TextureSheetAnimationModule texture4 = boompar4.textureSheetAnimation;
                        texture4.enabled = true;
                        texture4.numTilesX = 8;
                        texture4.numTilesY = 8;
                        //---------------------------------


                        boommat = new Material(Shader.Find("Particles/Alpha Blended"));
                        boomtex = ModResource.GetTexture("boomtex");
                        boommat.mainTexture = boomtex;
                        try
                        {
                            UnityEngine.Object.Destroy(this.boomobj);
                            UnityEngine.Object.Destroy(this.boomobj2);
                        }
                        catch
                        {

                        }
                        boomobj = new GameObject();
                        boomobj2 = new GameObject();
                        //--------------------------------
                        UnityEngine.Object.DontDestroyOnLoad(boomobj);
                        UnityEngine.Object.DontDestroyOnLoad(boomobj2);
                        boompar = boomobj.AddComponent<ParticleSystem>();
                        boompar.startLifetime = 3f * scale.Value / 406f;
                        boompar.startSpeed = 400f * scale.Value / 406f;
                        boompar.gravityModifier = -0.05f;
                        boompar.startSize = 20f * scale.Value / 406f;
                        Color color;
                        color.r = 10f;
                        color.b = 10f;
                        color.g = 10f;
                        color.a = 10f;
                        boompar.startColor = color;

                        boompar.loop = false;
                        ParticleSystem.ShapeModule shape = boompar.shape;
                        shape.shapeType = ParticleSystemShapeType.Cone;
                        shape.radius = 5f * scale.Value / 406f;
                        shape.angle = 5f * scale.Value / 406f;
                        ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = boompar.sizeOverLifetime;
                        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(0.8f, 1f);
                        ParticleSystemRenderer particleSystemRenderer = boompar.GetComponent<ParticleSystemRenderer>();
                        particleSystemRenderer.material = boommat;
                        particleSystemRenderer.renderMode = ParticleSystemRenderMode.Billboard;
                        particleSystemRenderer.velocityScale = 0f;
                        particleSystemRenderer.lengthScale = 9.5f;
                        particleSystemRenderer.sortingOrder = 1;
                        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = boompar.colorOverLifetime;
                        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(color, new Color(255f, 255f, 255f, 0f));
                        colorOverLifetime.enabled = true;
                        ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity = boompar.limitVelocityOverLifetime;
                        limitVelocity.enabled = true;
                        limitVelocity.dampen = 0.15f;
                        ParticleSystem.SubEmittersModule subEmittersModule = boompar.subEmitters;
                        subEmittersModule.enabled = true;
                        subEmittersModule.birth0 = boompar4;
                        //-----------------------
                        boompar2 = boomobj2.AddComponent<ParticleSystem>();
                        boompar2.startLifetime = 3f * scale.Value / 406f;
                        boompar2.startSpeed = 20f * scale.Value / 406f;

                        boompar2.startSize = 15f * scale.Value / 406f;
                        color.r = 10f;
                        color.b = 10f;
                        color.g = 10f;
                        color.a = 10f;
                        boompar2.startColor = color;
                        boompar.simulationSpace = ParticleSystemSimulationSpace.World;
                        boompar2.simulationSpace = ParticleSystemSimulationSpace.World;
                        boompar2.loop = false;
                        ParticleSystem.ShapeModule shape2 = boompar2.shape;
                        shape2.shapeType = ParticleSystemShapeType.Cone;
                        shape2.radius = 4f * scale.Value / 406f;
                        shape2.angle = 10f * scale.Value / 406f;
                        ParticleSystemRenderer particleSystemRenderer2 = boompar2.GetComponent<ParticleSystemRenderer>();
                        particleSystemRenderer2.material = boommat;
                        particleSystemRenderer2.renderMode = ParticleSystemRenderMode.Stretch;
                        particleSystemRenderer2.velocityScale = 0.2f;
                        particleSystemRenderer2.lengthScale = 2f;
                        particleSystemRenderer2.sortingOrder = 1;
                        ParticleSystem.ColorOverLifetimeModule colorOverLifetime2 = boompar2.colorOverLifetime;
                        colorOverLifetime2.enabled = true;
                        colorOverLifetime2.color = new ParticleSystem.MinMaxGradient(color, new Color(255f, 255f, 255f, 0f));
                        ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity2 = boompar2.limitVelocityOverLifetime;
                        limitVelocity2.enabled = true;
                        limitVelocity2.dampen = 0.07f;



                        ParticleSystem.EmissionModule emission2 = boompar2.emission;
                        emission2.enabled = false;

                        ParticleSystem.EmissionModule emission = boompar.emission;
                        emission.enabled = false;
                        ParticleSystem.TextureSheetAnimationModule texture = boompar.textureSheetAnimation;
                        texture.enabled = true;
                        texture.numTilesX = 8;
                        texture.numTilesY = 8;
                        ParticleSystem.TextureSheetAnimationModule texture2 = boompar2.textureSheetAnimation;
                        texture2.enabled = true;
                        texture2.numTilesX = 8;
                        texture2.numTilesY = 8;
                        /*ParticleSystem.Burst[] bursts1 = new ParticleSystem.Burst[1] { new ParticleSystem.Burst() };
                        bursts1[1].minCount = 50;
                        bursts1[1].maxCount = 50;
                        bursts1[1].time = 0f;
                        boompar.emission.SetBursts(bursts1);*/

                        try
                        {
                            this.CB.randomDelay = 0f;
                            Rigidbody component = this.CB.boltObject.gameObject.GetComponent<Rigidbody>();
                            component.mass = this.mass.Value / 1000f;
                            component.drag = this.velocityattenuation.Value;
                            Vector3 a = default(Vector3);
                            a.x = this.scale.Value;
                            a.y = this.scale.Value;
                            a.z = this.scale.Value;
                            this.CB.boltObject.transform.localScale = a / 500f;
                            this.CB.knockbackSpeed = this.backlash.Value * 10000f;
                            NAbolt component2 = this.gameObject.GetComponent<NAbolt>();
                            component2.tntmass = this.tntmass.Value;
                            component2.mass = this.mass.Value;

                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }


				}
                
            }
           
		}
        public MText flaktext;
        // Token: 0x06000073 RID: 115
        private void OnCollisionEnter(Collision collision)
		{
		}

		// Token: 0x06000074 RID: 116
		private void OnCollisionExit(Collision collision)
		{
		}

		// Token: 0x06000075 RID: 117
		private void OnCollisionStay(Collision collision)
		{
		}

		// Token: 0x06000076 RID: 118
		private void OnTriggerEnter(Collision collision)
		{
		}

		// Token: 0x06000077 RID: 119
		public virtual void ChangedProperties()
		{
		}

		// Token: 0x06000078 RID: 120
		public virtual void OnSimulateStartClient()
		{
		}

		// Token: 0x06000079 RID: 121
		public virtual void DisplayInMapper(bool value)
		{
		}

		// Token: 0x0600007A RID: 122
		public virtual void SimulateUpdateAlways()
		{
		}
       
        // Token: 0x17000017 RID: 23
        // (get) Token: 0x0600007B RID: 123
        // (set) Token: 0x0600007C RID: 124
        public BlockBehaviour BB { get; internal set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007D RID: 125
		// (set) Token: 0x0600007E RID: 126
		public bool EnhancementEnabled { get; set; }

        // Token: 0x0600007F RID: 127
        public MToggle yeguang;
        public MColourSlider colourSlider;
        public MSlider timer2;
        public MSlider relodtime;
        public float relodingtime;
        public MMenu hefuse;
        public MToggle holdfire;
        public List<NAbolt> nAbolts;

        public Material boommat;
        public ParticleSystem boompar;
        public GameObject boomobj;
        public static ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        public ModTexture boomtex = ModResource.GetTexture("boomtex");
        public GameObject cannonball;
        public Material boommat2;
        public ParticleSystem boompar2;
        public GameObject boomobj2;
        public void SafeAwakeE()
        {
            try
            {
                
                this.CB = base.GetComponent<CanonBlock>();
                this.MyGUID = CB.BuildingBlock.Guid.ToString();
                this.InitBullet();

                this.CB.knockbackSpeed = this.backlash.Value * 10000f;
                this.BB.name = "Changed Cannon";
                int n = 0;
                CB.OnReloadAmmo(ref n, AmmoType.Cannon, true, true);
            }
            catch
            {

            }
        }
        public void InitBullet()
        {
            try
            {
                UnityEngine.Object.Destroy(this.cannonball);
            }
            catch
            {
            }
        
            cannonball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cannonball.SetActive(false);
            
            NAbolt nabolt = cannonball.AddComponent<NAbolt>();
            nabolt.blot = cannonball;
            nabolt.canexplo = true;
            nabolt.NavalCannoBlockE = this;
            nabolt.cjxs = 0f;
            nabolt.seahigh = SingleInstance<Sea>.Instance.Getseahigh(base.transform.position);
            nabolt.asd = false;
            nabolt.tntmass = this.tntmass.Value;
            nabolt.mass = this.mass.Value;
            nabolt.type = this.type;
            nabolt.timer = 0f;
            nabolt.navalhe = navalhe;
            nabolt.ycyx = false;
            nabolt.hefuse = hefuse.Value;
            nabolt.saptype = saptype.Value;
            nabolt.aptype = aptype.Value;
            nabolt.apfuzetime = apfuzetime.Value;
            nabolt.wood = this.navalhe.wood;
            nabolt.waterin = true;
            nabolt.time2 = -1f;
            


            Rigidbody component = cannonball.AddComponent<Rigidbody>();
            nabolt.rigidbody2 = component;
            component.freezeRotation = true;
            component.drag = velocityattenuation.Value;
            component.mass = this.mass.Value;
            component.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            cannonball.GetComponent<MeshFilter>().mesh = ModResource.GetMesh("cannonballobj").Mesh;
            component.freezeRotation = true;
            Renderer renderer = this.cannonball.GetComponent<Renderer>();
            renderer.material.mainTexture = ModResource.GetTexture("cannonball").Texture;
            nabolt.rigidbody2 = component;
            component.mass = this.mass.Value / 1000f;
            component.drag = this.velocityattenuation.Value;
            nabolt.hit3 = true;
            Vector3 a2 = default(Vector3);
            a2.x = this.scale.Value;
            a2.y = this.scale.Value;
            a2.z = this.scale.Value;
            if (this.navalhe.wood)
            {
                this.cannonball.transform.localScale = a2 / 2000f;
            }
            else
            {
                this.cannonball.transform.localScale = a2 / 1000f;
            }
            this.CB.knockbackSpeed = this.backlash.Value * 10000f;
            this.ass = 1;
            nabolt.networkid = this.networkid;
            nabolt.scale = this.scale.Value;
            Light light2 = nabolt.gameObject.AddComponent<Light>();
            light2.range = scale.Value / 10f;
            light2.color = colourSlider.Value;
           
            TrailRenderer trailRenderer1 = nabolt.gameObject.AddComponent<TrailRenderer>();
            if (!trailRenderer1)
            {
                trailRenderer1 = nabolt.gameObject.AddComponent<TrailRenderer>();
            }
            trailRenderer1.autodestruct = false;
            trailRenderer1.receiveShadows = false;
            trailRenderer1.shadowCastingMode = ShadowCastingMode.Off;
            trailRenderer1.startWidth = this.scale.Value / 200f;
            trailRenderer1.endWidth = 0.01f;
            trailRenderer1.material = new Material(Shader.Find("Particles/Additive"));
            trailRenderer1.material.SetColor("_TintColor", colourSlider.Value);
            trailRenderer1.time = timer2.Value;
            trailRenderer1.enabled = yeguang.IsActive;
            switch (trail.Value)
            {
                case 0:
                    trailRenderer1.material.mainTexture = ModResource.GetTexture("yg");
                    break;
                case 1:
                    trailRenderer1.material.mainTexture = ModResource.GetTexture("boomtexE");
                    break;
                case 2:
                    trailRenderer1.material.mainTexture = ModResource.GetTexture("water_1");
                    break;

            }
           
            nabolt.hit2 = false;
            try
            {
                nabolt.player = (int)CB.ParentMachine.PlayerID;
            }
            catch
            {

            }


        }
        public MSlider ranrelod;
        public MMenu saptype;
        public MMenu aptype;
        public MMenu apfuzetime;
        public MSlider salfefirerange;
        private new void Awake()
		{
			this.CB = base.GetComponent<CanonBlock>();
			this.HEkey = this.CB.AddKey("HE", "HE", KeyCode.Alpha1);
            holdfire= CB.AddToggle("按住开火", "holdfire", false);
            this.APkey = this.CB.AddKey("AP", "AP", KeyCode.Alpha2);
			this.SAPkey = this.CB.AddKey("SAP", "SAP", KeyCode.Alpha3);
			this.VTkey = this.CB.AddKey("VT", "VT", KeyCode.Alpha4);
            flaktext = CB.AddText("火控接口编号", "flaktext", "");
            this.timekey = this.CB.AddKey("time", "时限引信弹", KeyCode.Alpha5);
            shootspeed= this.CB.AddSlider("弹速（m/s）", "shootspeed", 800f, 0f, 1000f, "", "x");
            ranrelod=this.CB.AddSlider("随机延迟(s)", "relodtimeS", 0f, 0f, 2f, "", "x");
            this.mass = this.CB.AddSlider("弹重（kg）", "mass", 5f, 0f, 2000f, "", "x");
			this.scale = this.CB.AddSlider("口径", "scaleE", 76f, 40f, 460f, "", "x");
			this.tntmass = this.CB.AddSlider("装药量（kg）(HE)", "tntmass", 0.5f, 0f, 70f, "", "x");
            relodtime= this.CB.AddSlider("装填时间(s)", "relodtime", 20f, 0.5f, 40f, "", "x");
            this.velocityattenuation = this.CB.AddSlider("速度衰减", "velocityattenuation", 0.2f, 0.01f, 0.5f, "", "x");
            hefuse=CB.AddMenu("hefuse", 0, new List<string>() {"HEI","CNF"}, false);
            saptype = CB.AddMenu("saptype", 0, new List<string>() { "CP", "CPC", "CPBC", "SAP", "SAPBC" }, false);
            aptype = CB.AddMenu("aptype", 0, new List<string>() { "AP", "APC", "APBC", "改进APBC" }, false);
            apfuzetime = CB.AddMenu("apfuzetime", 0, new List<string>() { "短引信", "中引信", "长引信", "91彻甲弹" }, false);
            trail = CB.AddMenu("trailfuse", 0, new List<string>() { "曳光尾迹", "曳光尾迹2", "曳光尾迹3" }, false);
            this.backlash = this.CB.AddSlider("后座力", "backlash", 1f, 0f, 1f, "", "x");
            yeguang = CB.AddToggle("曳光特效", "ygv", true);
            timer2= this.CB.AddSlider("曳光轨迹存在时间", "timer2", 0.04f, 0.01f, 0.5f, "", "x");
            colourSlider = CB.AddColourSlider("曳光弹颜色", "color", Color.blue, false);
            salfefirerange = CB.AddSlider("安全开火检测距离", "salfefirerange", 0f, 0f, 100f, "", "x");
            autofireangle = CB.AddSlider("自动开火角度阈值", "autofireangle", 1f, 0f, 5f, "", "x");
            this.type = 1;
			this.ass = 0;
            relodingtime = 0f;
            fireture = true;
			this.canshoot = true;
			this.timer = -1f;
			this.navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
			this.seahigh = SingleInstance<Sea>.Instance.Getseahigh(base.transform.position);
            if ((this.cannonshoot = base.gameObject.GetComponent<AudioSource>()) == null)
			{
				this.cannonshoot = base.gameObject.AddComponent<AudioSource>();
			}
            

            nAbolts = new List<NAbolt>();
            int n = 0;
            CB.OnReloadAmmo(ref n, AmmoType.Cannon, true, true);

        }
        
            // Token: 0x06000080 RID: 128
            private IEnumerator Delay()
		{
			yield return new WaitForFixedUpdate();
			yield break;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		public bool ShootEnabled { get; set; } = true;

		// Token: 0x06000083 RID: 131
		private IEnumerator shoot()
		{   
            if (StatMaster.isHosting)
            {
                GameObject canball = (GameObject)UnityEngine.Object.Instantiate(cannonball, this.CB.transform.TransformPoint(this.CB.boltSpawnPos), CB.transform.rotation);
                try
                {

                    Rigidbody rigidbody = canball.GetComponent<Rigidbody>();
                    rigidbody.velocity = -CB.transform.up.normalized * shootspeed.Value * 3f;

                    cannonball.transform.position = this.CB.transform.TransformPoint(this.CB.boltSpawnPos);
                    NAbolt nAbolt = canball.GetComponent<NAbolt>();

                    try
                    {
                        foreach (flakN flakN in AIflak.Instance.flakNs)
                        {
                            if (flakN.name == flaktext.Value && flakN.play == CB.BuildingBlock.ParentMachine.PlayerID)
                            {
                                view = flakN.view;

                                view.cantfire += 1;
                                nAbolt.view = view;
                                nAbolt.hasView = true;
                                break;

                            }
                        }
                    }
                    catch
                    {

                    }
                    nAbolt.MyGUID = MyGUID;
                    nAbolt.type = this.type;
                    if (type == 5 && view!=null)
                    {
                        nAbolt.haveView= true;
                        nAbolt.originFirePos = CB.transform.position;
                        nAbolt.exploreRange = view.rangelast;
                    }
                    //waterE.Instance.EmitSparks(this.CB.transform.TransformPoint(this.CB.boltSpawnPos), scale.Value/40f, true);
                    CB.gameObject.GetComponent<Rigidbody>().AddForce(shootspeed.Value * base.transform.up * backlash.Value);
                    ModConsole.Log("开始发送");
                    if (StatMaster.isHosting)
                    {

                        Message cannonfire = Messages.ballshoot.CreateMessage(new object[]
                            {
                    this.MyGUID,
                    1,
                    canball.transform.position,
                    rigidbody.velocity,
                    (int)CB.BuildingBlock.ParentMachine.PlayerID
                            });
                        ModNetworking.SendToAll(cannonfire);
                    }
                    ModConsole.Log("发送成功");
                }
                catch
                {

                }
                canball.SetActive(true);
                canball.isStatic = false;
            }
            yield break;



        }

		// Token: 0x06000084 RID: 132
		private bool V3(Vector3 vector3, Vector3 vector4, float wc)
		{
			ModConsole.Log((vector3 - vector4).magnitude.ToString());
			return (vector3 - vector4).magnitude < wc;
		}

		// Token: 0x06000085 RID: 133
		private new void Start()
		{
		}

		// Token: 0x06000086 RID: 134
		public void shootsound()
		{
            Effect();
            if (this.navalhe.boomvoice)
			{
				this.cannonshoot.spatialBlend = 1f;
				this.cannonshoot.maxDistance = 5000f;
				this.cannonshoot.rolloffMode = AudioRolloffMode.Logarithmic;
				this.cannonshoot.playOnAwake = false;
				this.cannonshoot.volume = this.navalhe.volume * this.scale.Value / 460f;
				this.cannonshoot.loop = false;
                cannonshoot.pitch = UnityEngine.Random.Range(0.7f, 1.5f); 
                if (this.scale.Value >= 406f)
				{
					this.cannonshoot.PlayOneShot(Soundfiles.explosionbig[UnityEngine.Random.Range(1, 4)]);
					this.cannonshoot.PlayOneShot(Soundfiles.shootexplosion[UnityEngine.Random.Range(1, 12)]);
					return;
				}
				if (this.scale.Value >= 305f)
				{
					this.cannonshoot.PlayOneShot(Soundfiles.explosionbig[UnityEngine.Random.Range(1, 4)]);
					this.cannonshoot.PlayOneShot(Soundfiles.shootexplosion[UnityEngine.Random.Range(1, 12)]);
					return;
				}
				if (this.scale.Value >= 180f)
				{
					this.cannonshoot.PlayOneShot(Soundfiles.shootbig[UnityEngine.Random.Range(1, 8)]);
					this.cannonshoot.PlayOneShot(Soundfiles.shootexplosion[UnityEngine.Random.Range(1, 12)]);
					return;
				}
				this.cannonshoot.PlayOneShot(Soundfiles.shootmid[UnityEngine.Random.Range(1, 4)]);
				this.cannonshoot.PlayOneShot(Soundfiles.shootexplosion[UnityEngine.Random.Range(1, 12)]);
			}
		}

		// Token: 0x04000063 RID: 99
		public CanonBlock CB;
    
        // Token: 0x04000064 RID: 100
        public MKey HEkey;

		// Token: 0x04000065 RID: 101
		public MKey APkey;

		// Token: 0x04000066 RID: 102
		public MKey SAPkey;

		// Token: 0x04000067 RID: 103
		public MKey VTkey;

		// Token: 0x04000068 RID: 104
		public MKey timekey;

		// Token: 0x04000069 RID: 105
		private Controller controller;

		// Token: 0x0400006A RID: 106
		public List<CanonBlock> CBL;

		// Token: 0x0400006B RID: 107
		public Transform targetSavedInController;

		// Token: 0x0400006C RID: 108
		[Obsolete]
		internal PlayerMachineInfo PMI;

		// Token: 0x0400006D RID: 109
		public Dictionary<int, Type> dic_EnhancementBlock = new Dictionary<int, Type>();

		// Token: 0x0400006E RID: 110
		private List<GameObject> GO = new List<GameObject>();

		// Token: 0x0400006F RID: 111
		public List<CanonBlock> CBNA;

		// Token: 0x04000070 RID: 112
		public Action PropertiseChangedEvent;

		// Token: 0x04000071 RID: 113
		public int type = 1;

		// Token: 0x04000072 RID: 114
		public MSlider mass;
        public string MyGUID;
        // Token: 0x04000073 RID: 115
        public MSlider velocity;

		// Token: 0x04000074 RID: 116
		public MSlider tntmass;
        public MSlider shootspeed;
        // Token: 0x04000075 RID: 117
        public MSlider velocityattenuation;

		// Token: 0x04000076 RID: 118
		public MSlider backlash;

		// Token: 0x04000077 RID: 119
		public int ass;

		// Token: 0x04000078 RID: 120
		public MSlider scale;

		// Token: 0x04000079 RID: 121
		private new GameObject gameObject;

		// Token: 0x0400007A RID: 122
		[HideInInspector]
		public Vector3 boltSpawnPos;

		// Token: 0x0400007B RID: 123
		[HideInInspector]
		public Quaternion boltSpawnRot;

		// Token: 0x0400007C RID: 124
		public MToggle boom;

		// Token: 0x0400007D RID: 125
		private bool canshoot;

		// Token: 0x0400007E RID: 126
		public float timer;

		// Token: 0x0400007F RID: 127
		public float seahigh;

		// Token: 0x04000080 RID: 128
		public AudioSource firebig;

		// Token: 0x04000081 RID: 129
		private AudioSource cannonshoot;

		// Token: 0x04000082 RID: 130
		private Navalhe navalhe;

		// Token: 0x0400042D RID: 1069
		public int networkid;
	}
}
