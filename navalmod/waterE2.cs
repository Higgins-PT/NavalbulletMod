using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
namespace Navalmod
{
    public class waterE2 : SingleInstance<waterE2>
    {

        public Navalhe wood;
        public Material boommat;
        public ParticleSystem boompar;
        public GameObject boomobj;
        public static ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        public ModTexture boomtex;

        public Material boommat2;
        public ParticleSystem boompar2;
        public GameObject boomobj2;
        public static ParticleSystem.EmitParams emitParams2 = default(ParticleSystem.EmitParams);
        public ModTexture boomtex2;
        public Material boommat3;
        public ParticleSystem boompar3;
        public GameObject boomobj3;
        public static ParticleSystem.EmitParams emitParams3 = default(ParticleSystem.EmitParams);
        public ModTexture boomtex3;
        public Material boommat4;
        public ParticleSystem boompar4;
        public GameObject boomobj4;
        public static ParticleSystem.EmitParams emitParams4 = default(ParticleSystem.EmitParams);
        public ModTexture boomtex4;
        public GameObject waterhit1;
        public GameObject waterhit2;

        public void Awake()
        {


            waterhit1 = ModResource.GetAssetBundle("WaterHit AB").LoadAsset<GameObject>("waterHit1");
            waterhit2 = ModResource.GetAssetBundle("WaterHit AB").LoadAsset<GameObject>("waterHit2");
            UnityEngine.Object.DontDestroyOnLoad(waterhit1);
            UnityEngine.Object.DontDestroyOnLoad(waterhit2);

            ModConsole.Log("aaaaaaaaaaaaaaaaaaaaaassssaaaaaaaaaaaaaaaaaaaaaaaa");
            boomtex = ModResource.GetTexture("boomtex");
            /*gameObject_1 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(gameObject_1);
            gameObject_2 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(gameObject_2);
            gameObject_3 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(gameObject_3);
            water_1 = new Material(Shader.Find("NeatWolf/Additive(Soft)HDR"));

            particleSystem_1 = gameObject_1.AddComponent<ParticleSystem>();
            particleSystem_2 = gameObject_1.AddComponent<ParticleSystem>();
            particleSystem_3 = gameObject_1.AddComponent<ParticleSystem>();
            ParticleSystem particle = particleSystem_1;
            particle.loop = false;
            particle.startLifetime = 4f;
            particle.startSpeed = 37f;
            particle.gravityModifier = 3f;
            particle.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[1] { new ParticleSystem.Burst() };

            bursts[1].minCount = 20;
            bursts[1].maxCount = 20;
            bursts[1].time = 0f;
            particle.emission.SetBursts(bursts);

            ParticleSystem.ShapeModule shape = particle.shape;
            ParticleSystem.RotationOverLifetimeModule rotationOverLifetime = particle.rotationOverLifetime;
            ParticleSystem.CollisionModule collisionModule = particle.collision;*/
            //---------------------------------
            boommat4 = new Material(Shader.Find("Particles/Alpha Blended"));
            boomtex4 = ModResource.GetTexture("water_1");
            boommat4.mainTexture = boomtex4;
            boomobj4 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar4 = boomobj4.AddComponent<ParticleSystem>();
            boompar4.startLifetime = 0.5f;
            boompar4.startSpeed = 0f;
            boompar4.gravityModifier = 0.01f;
            boompar4.startSize = 10f;
            Color color4;
            color4.r = 225f;
            color4.b = 225f;
            color4.g = 225f;
            color4.a = 100f;
            boompar4.startColor = color4;

            boompar4.loop = false;
            ParticleSystem.ShapeModule shape4 = boompar4.shape;
            shape4.shapeType = ParticleSystemShapeType.Sphere;
            shape4.radius = 0.001f;
            
            boompar4.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer4 = boompar4.GetComponent<ParticleSystemRenderer>();

            particleSystemRenderer4.material = boommat4;
            particleSystemRenderer4.sortingOrder = 1;
            ParticleSystem.ColorOverLifetimeModule colorOverLifetime4 = boompar4.colorOverLifetime;
            colorOverLifetime4.color = new ParticleSystem.MinMaxGradient(Color.white,new Color(255f,255f,255f,0f));
            colorOverLifetime4.enabled = true;
            ParticleSystem.SizeOverLifetimeModule sizeOverLifetime4 = boompar4.sizeOverLifetime;
            sizeOverLifetime4.size = new ParticleSystem.MinMaxCurve(1f, 0f);
            sizeOverLifetime4.enabled = true;
            ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity4 = boompar4.limitVelocityOverLifetime;
            limitVelocity4.enabled = true;
            limitVelocity4.dampen = 0.2f;
            ParticleSystem.EmissionModule emission4 = boompar4.emission;
            emission4.enabled = false;
            //----------------------------------
            boommat = new Material(Shader.Find("Particles/Alpha Blended"));
            boomtex = ModResource.GetTexture("water_1");
            boommat.mainTexture = boomtex;
            boomobj = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar = boomobj.AddComponent<ParticleSystem>();
            boompar.startLifetime = 4f;
            boompar.startSpeed = 37f;
            boompar.gravityModifier = 3f;
            boompar.startSize = 15f;
            ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity = boompar.limitVelocityOverLifetime;
            limitVelocity.enabled = true;
            limitVelocity.dampen = 0.01f;
            Color color;
            color.r = 225f;
            color.b = 225f;
            color.g = 225f;
            color.a = 100f;
            boompar.startColor = color;

            boompar.loop = false;
            ParticleSystem.ShapeModule shape = boompar.shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.radius = 5f;
            shape.angle = 3f;
            boompar.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer = boompar.GetComponent<ParticleSystemRenderer>();
            particleSystemRenderer.material = boommat;
            ParticleSystem.SubEmittersModule subEmittersModule = boompar.subEmitters;
            subEmittersModule.enabled = true;
            subEmittersModule.birth0 = boompar4;
            
   
            ParticleSystem.EmissionModule emission = boompar.emission;
            emission.enabled = false;
            
            //-------------------------------------
            boommat2 = new Material(Shader.Find("Particles/Alpha Blended"));
            boomtex2 = ModResource.GetTexture("water_1");
            boommat2.mainTexture = boomtex2;
            boomobj2 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar2 = boomobj2.AddComponent<ParticleSystem>();
            boompar2.startLifetime = 2.3f;
            boompar2.startSpeed = 25f;

            boompar2.startSize = 10f;
            Color color2;
            color2.r = 255f;
            color2.b = 255f;
            color2.g = 255f;
            color2.a = 100f;
            boompar2.startColor = color2;

            boompar2.loop = false;
            ParticleSystem.ShapeModule shape2 = boompar2.shape;
            shape2.shapeType = ParticleSystemShapeType.Circle;
            shape2.radius = 2f;
            boompar2.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer2 = boompar2.GetComponent<ParticleSystemRenderer>();
           
            particleSystemRenderer2.material = boommat2;
            particleSystemRenderer2.sortingOrder = 1;
         
            ParticleSystem.EmissionModule emission2 = boompar2.emission;
            emission2.enabled = false;
            
            //------------------------------------------------------------
            boommat3 = new Material(Shader.Find("Particles/Alpha Blended"));
            boomtex3 = ModResource.GetTexture("water_1");
            boommat3.mainTexture = boomtex3;
            boomobj3 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar3 = boomobj3.AddComponent<ParticleSystem>();
            boompar3.startLifetime = 2.3f;
            boompar3.startSpeed = 40f;
            boompar3.gravityModifier = 5f;
            boompar3.startSize = 40f;
            Color color3;
            color3.r = 255f;
            color3.b = 255f;
            color3.g = 255f;
            color3.a = 100f;
            boompar3.startColor = color3;

            boompar3.loop = false;
            ParticleSystem.ShapeModule shape3 = boompar3.shape;
            shape3.shapeType = ParticleSystemShapeType.Cone;
            shape3.radius = 2f;
            shape3.angle = 45f;
            

            boompar3.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer3 = boompar3.GetComponent<ParticleSystemRenderer>();

            particleSystemRenderer3.material = boommat3;
            particleSystemRenderer3.sortingOrder = 1;
          
            ParticleSystem.EmissionModule emission3 = boompar3.emission;
            emission3.enabled = false;
            /*ParticleSystem.SubEmittersModule subEmittersModule3 = boompar3.subEmitters;
            subEmittersModule3.enabled = true;
            subEmittersModule3.birth0 = boompar4;*/
            //-------------------------
            DontDestroyOnLoad(boomobj);
            DontDestroyOnLoad(boomobj);
            DontDestroyOnLoad(boomobj2);
            DontDestroyOnLoad(boomobj3);
            DontDestroyOnLoad(waterhit1);
            DontDestroyOnLoad(waterhit2);

        }
        public override string Name { get; } = "water2";
        public void Creatwaterspray(Vector3 pos, float scale)
        {
            
        }
        public void EmitSparks(Vector3 contact, float size)
        {
            try
            {
                contact.y = SingleInstance<Sea>.Instance.Getseahigh(contact);
                if (this.wood.wood)
                {
                    size = size / 5f;
                }
                soundplay.Instance.explosionsound(size*0.15f, contact);
                Quaternion quaternion = Quaternion.identity;
                quaternion.eulerAngles = new Vector3(-90f, 0f, 0f);
                boompar.transform.rotation = quaternion;
                boompar2.transform.rotation = quaternion;
                boompar3.transform.rotation = quaternion;
                boompar.transform.position = contact;
                boompar.startSpeed = (37f * size / 203f);
                boompar.startSize = (15f * size / 203f);
                boompar.Emit(6);
                boompar2.transform.position = contact;
                boompar2.startSpeed = (25f * size / 203f);
                boompar2.startSize = (10f * size / 203f);
                boompar2.Emit((int)(size/5.8f));
                boompar3.transform.position = contact;
                boompar3.startSpeed = (40f * size / 203f);
                boompar3.startSize = (10f * size / 203f);
                boompar3.Emit((int)(size / 5.8f));
                try
                {
                    GameObject waterhit;

                    if (size > 130f)
                    {
                        waterhit = (GameObject)UnityEngine.Object.Instantiate(waterhit1, contact, Quaternion.identity);
                        waterhit.transform.localScale = size / 76.2f * Vector3.one;
                        UnityEngine.Object.Destroy(waterhit, 3f);
                    }
                    else
                    {
                        waterhit = (GameObject)UnityEngine.Object.Instantiate(waterhit2, contact, Quaternion.identity);
                        waterhit.transform.localScale = size / 76.2f * Vector3.one;
                        UnityEngine.Object.Destroy(waterhit, 3f);
                    }
                    
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
