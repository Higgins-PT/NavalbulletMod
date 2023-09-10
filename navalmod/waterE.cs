using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
using System.Linq;
using ObjectTypes;

namespace Navalmod
{
    public class waterE:SingleInstance<waterE>
    {
        public ParticleSystem particleSystem_1;
        public ParticleSystem particleSystem_2;
        public ParticleSystem particleSystem_3;
        public GameObject gameObject_1;
        public GameObject gameObject_2;
        public GameObject gameObject_3;
        public Material water_1;
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
        public GameObject exp1;
        public GameObject exp2;
        public GameObject exp3;
        public GameObject fire1;
        public GameObject water;
        private GameObject navalhe;
        public GameObject matelshaderC;
        public Shader matelshader;
        public void Awake()
        {
            water = ModResource.GetAssetBundle("WaterAB").LoadAsset<GameObject>("Sea");
            fire1 = ModResource.GetAssetBundle("FireAB").LoadAsset<GameObject>("Fire");
            matelshaderC = ModResource.GetAssetBundle("ShaderAB").LoadAsset<GameObject>("Cube");
            matelshader = matelshaderC.GetComponent<MeshRenderer>().material.shader;
            /*exp1 = ModResource.GetAssetBundle("FireAB").LoadAsset<GameObject>("Exp1");
            exp2 = ModResource.GetAssetBundle("FireAB").LoadAsset<GameObject>("Exp2");
            exp3 = ModResource.GetAssetBundle("FireAB").LoadAsset<GameObject>("Exp3");
            UnityEngine.Object.DontDestroyOnLoad(exp1);
            UnityEngine.Object.DontDestroyOnLoad(exp2);
            UnityEngine.Object.DontDestroyOnLoad(exp3);*/
            UnityEngine.Object.DontDestroyOnLoad(fire1);
            UnityEngine.Object.DontDestroyOnLoad(water);
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
            //----------------------------------
            boommat = new Material(Shader.Find("Particles/Alpha Blended"));
            boomtex = ModResource.GetTexture("boomtex");
            boommat.mainTexture = boomtex;
            boomobj = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar = boomobj.AddComponent<ParticleSystem>();
            boompar.startLifetime = 10f;
            boompar.startSpeed = 50f;
            boompar.gravityModifier = -0.1f;
            boompar.startSize = 40f ;
            Color color;
            color.r = 10f;
            color.b = 10f;
            color.g = 10f;
            color.a = 10f;
            boompar.startColor = color;

            boompar.loop = false;
            ParticleSystem.ShapeModule shape = boompar.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 1f;
            boompar.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer = boompar.GetComponent<ParticleSystemRenderer>();
            particleSystemRenderer.material = boommat;
            
            ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = boompar.sizeOverLifetime;
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(0.8f,1f);
            sizeOverLifetime.enabled = true;
            ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity = boompar.limitVelocityOverLifetime;
            limitVelocity.enabled = true;
            limitVelocity.dampen = 0.2f;
            ParticleSystem.EmissionModule emission = boompar.emission;
            emission.enabled = false;
            ParticleSystem.TextureSheetAnimationModule texture = boompar.textureSheetAnimation;
            texture.enabled = true;
            texture.numTilesX = 8;
            texture.numTilesY = 8;
            //-------------------------------------
            boommat2 = new Material(Shader.Find("Particles/Additive"));
            boomtex2 = ModResource.GetTexture("boomtex");
            boommat2.mainTexture = boomtex2;
            boomobj2 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj);
            boompar2 = boomobj2.AddComponent<ParticleSystem>();
            boompar2.startLifetime = 10f;
            boompar2.startSpeed = 50f;

            boompar2.startSize = 40f;
            Color color2;
            color2.r = 10f;
            color2.b = 10f;
            color2.g = 10f;
            color2.a = 10f;
            boompar2.startColor = color2;
            
            boompar2.loop = false;
            ParticleSystem.ShapeModule shape2 = boompar2.shape;
            shape2.shapeType = ParticleSystemShapeType.Sphere;
            shape2.radius = 1f;
            boompar2.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer2 = boompar2.GetComponent<ParticleSystemRenderer>();
            
            particleSystemRenderer2.material = boommat2;
            particleSystemRenderer2.sortingOrder = 1;
            ParticleSystem.ColorOverLifetimeModule colorOverLifetime = boompar2.colorOverLifetime;

            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(Color.white,new Color(20f,20f,0f,10f));
            colorOverLifetime.enabled = true;
            ParticleSystem.SizeOverLifetimeModule sizeOverLifetime2 = boompar2.sizeOverLifetime;
            sizeOverLifetime2.size = new ParticleSystem.MinMaxCurve(0.9f, 1f);

            ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity2 = boompar2.limitVelocityOverLifetime;
            limitVelocity2.enabled = true;
            limitVelocity2.dampen = 0.2f;
            ParticleSystem.EmissionModule emission2 = boompar2.emission;
            emission2.enabled = false;
            ParticleSystem.TextureSheetAnimationModule texture2 = boompar2.textureSheetAnimation;
            texture2.enabled = true;
            texture2.numTilesX = 8;
            texture2.numTilesY = 8;
            //---------------------------------
            boommat3 = new Material(Shader.Find("Particles/Additive"));
            boomtex3 = ModResource.GetTexture("yg");
            boommat3.mainTexture = boomtex3;
            boommat3.SetColor("_TintColor", Color.yellow);
            boomobj3 = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(boomobj3);
            boompar3 = boomobj3.AddComponent<ParticleSystem>();
            boompar3.startLifetime = 10f;
            boompar3.startSpeed = 300f;
            boompar3.gravityModifier = 2f;
            boompar3.startSize = 0.2f;
            Color color3;
            color3.r = 10f;
            color3.b = 10f;
            color3.g = 10f;
            color3.a = 10f;
            boompar3.startColor = color3;

            boompar3.loop = false;
            ParticleSystem.ShapeModule shape3 = boompar3.shape;
            shape3.shapeType = ParticleSystemShapeType.Sphere;
            shape3.radius = 0.1f;
            boompar3.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystemRenderer particleSystemRenderer3 = boompar3.GetComponent<ParticleSystemRenderer>();

            particleSystemRenderer3.material = boommat3;
            particleSystemRenderer3.sortingOrder = 1;
            particleSystemRenderer3.renderMode = ParticleSystemRenderMode.Stretch;
            particleSystemRenderer3.velocityScale = 0.08f;
            particleSystemRenderer3.lengthScale = 3f;
            ParticleSystem.ColorOverLifetimeModule colorOverLifetime3 = boompar3.colorOverLifetime;
            ParticleSystem.CollisionModule collision = boompar3.collision;
            collision.enabled = false;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision3D;
            collision.dampen = 0.1f;
            collision.bounce = 0f;
            collision.radiusScale = 2f;
            collision.maxCollisionShapes = 200;
            colorOverLifetime3.color = new ParticleSystem.MinMaxGradient(Color.yellow, new Color(0f, 0f, 0f, 1f));
            colorOverLifetime3.enabled = true;
            ParticleSystem.SizeOverLifetimeModule sizeOverLifetime3 = boompar3.sizeOverLifetime;
            sizeOverLifetime3.size = new ParticleSystem.MinMaxCurve(0.9f, 1f);

            ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity3 = boompar3.limitVelocityOverLifetime;
            limitVelocity3.enabled = true;
            limitVelocity3.dampen = 0.08f;
            ParticleSystem.EmissionModule emission3 = boompar3.emission;
            emission3.enabled = false;
            DontDestroyOnLoad(gameObject_1);
            DontDestroyOnLoad(gameObject_2);
            DontDestroyOnLoad(gameObject_3);
            DontDestroyOnLoad(boomobj);
            DontDestroyOnLoad(boomobj2);
            DontDestroyOnLoad(boomobj3);
        }
        public override string Name { get; } = "water";
        public void Creatwaterspray(Vector3 pos,float scale)
        {

        }
        public void EmitSparksR(Vector3 contact, float size)
        {
            try
            {
                GameObject expl;
                GameObject fire;
                if (this.wood.wood)
                {
                    size = size / 5f;
                }
                boompar.transform.position = contact;
                boompar.startSpeed = (40f * (float)Math.Pow(size, 0.7f) / 30f) + 3f;
                boompar.startSize = (30f * (float)Math.Pow(size, 0.7f) / 30f) + 3f;
                boompar.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 30f) + 2f;
                boompar.Emit(2);
                boompar2.transform.position = contact;
                boompar2.startSpeed = (40f * (float)Math.Pow(size, 0.8f) / 30f) + 2f;
                boompar2.startSize = (40f * (float)Math.Pow(size, 0.8f) / 30f) + 5f;
                boompar2.startLifetime = (0.1f * (float)Math.Pow(size, 0.7f) / 30f) + 0.05f;
                boompar2.Emit(2);
                boompar3.transform.position = contact;
                boompar3.startSpeed = (100f * (float)Math.Pow(size, 0.7f) / 30f) + 50f;
                boompar3.startSize = (0.4f * (float)Math.Pow(size, 0.7f) / 30f) + 0.2f;
                boompar3.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 30f) + 1f;
                boompar3.Emit(1);
                if (StatMaster.isHosting)
                {
                    if (UnityEngine.Random.Range(0f, 1f) > 0.8f)
                    {
                        bool fireE = false;
                        fire = (GameObject)UnityEngine.Object.Instantiate(fire1, contact, Quaternion.identity);

                        try
                        {
                            Collider[] array2 = Physics.OverlapSphere(fire.transform.position, size);
                            for (int j = 0; j < array2.Length; j++)
                            {
                                if (array2[j].transform.gameObject.GetComponent<NAbolt>() == null && array2[j].transform.gameObject.GetComponent<ArrowController>() == null)
                                {
                                    fire.transform.SetParent(array2[j].transform);
                                    fireE = true;
                                    SendMsg(array2[j].attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().BuildingBlock.Guid, array2[j].attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().BuildingBlock.ParentMachine.PlayerID, size, contact);
                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }
                        fire.transform.localScale = (100f * (float)Math.Pow(size, 0.333f) / 20f) * Vector3.one;

                        UnityEngine.Object.Destroy(fire, 7f);
                        if (fireE == false)
                        {
                            UnityEngine.Object.Destroy(fire);
                        }
                    }
                }

            }
            catch
            {

            }
            }

        public void EmitSparks(Vector3 contact,float size,bool torpedo)
        {
            try
            {
                GameObject expl;
                GameObject fire;
                if (this.wood.wood)
                {
                    size = size / 5f;
                }
                boompar.transform.position = contact;
                boompar.startSpeed = (40f * (float)Math.Pow(size, 0.7f) / 30f) + 3f;
                boompar.startSize = (30f * (float)Math.Pow(size, 0.7f) / 30f) + 3f;
                boompar.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 30f) + 2f;
                boompar.Emit(10);
                boompar2.transform.position = contact;
                boompar2.startSpeed = (40f * (float)Math.Pow(size, 0.8f) / 30f) + 2f;
                boompar2.startSize = (40f * (float)Math.Pow(size, 0.8f) / 30f) + 5f;
                boompar2.startLifetime = (0.1f * (float)Math.Pow(size, 0.7f) / 30f) + 0.05f;
                boompar2.Emit(20);
                boompar3.transform.position = contact;
                boompar3.startSpeed = (100f * (float)Math.Pow(size, 0.7f) / 30f) + 50f;
                boompar3.startSize = (0.4f * (float)Math.Pow(size, 0.7f) / 30f) + 0.2f;
                boompar3.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 30f) + 1f;
                boompar3.Emit(3);
                if (StatMaster.isHosting && !torpedo)
                {
                    if (UnityEngine.Random.Range(0f, 1f) > 0f)
                    {
                        bool fireE = false;
                        fire = (GameObject)UnityEngine.Object.Instantiate(fire1, contact, Quaternion.identity);

                        try
                        {
                            Collider[] array2 = Physics.OverlapSphere(fire.transform.position, size);
                            for (int j = 0; j < array2.Length; j++)
                            {
                                if (array2[j].transform.gameObject.GetComponent<NAbolt>() == null && array2[j].transform.gameObject.GetComponent<ArrowController>() == null)
                                {
                                    fire.transform.SetParent(array2[j].transform);
                                    fireE = true;
                                    SendMsg(array2[j].attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().BuildingBlock.Guid, array2[j].attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().BuildingBlock.ParentMachine.PlayerID, size, contact);
                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }
                        fire.transform.localScale = (100f * (float)Math.Pow(size, 0.333f) / 20f) * Vector3.one;

                        UnityEngine.Object.Destroy(fire, 7f);
                        if (fireE == false)
                        {
                            UnityEngine.Object.Destroy(fire);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public void SendMsg(Guid block, int playid, float size, Vector3 pos)
        {
            ModNetworking.SendToAll(Messages.fire.CreateMessage(new object[]
                {
                    block.ToString(),
                    playid,
                    size,
                    pos
                }));
        }
        public void EmitSparksE(Vector3 contact, float size)
        {
            try
            {
                if (this.wood.wood)
                {
                    size = size / 5f;
                }
                boompar.transform.position = contact;
                boompar.startSpeed = (40f * (float)Math.Pow(size, 0.7f) / 406f);
                boompar.startSize = (30f * (float)Math.Pow(size, 0.7f) / 406f);
                boompar.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 406f);
                boompar.Emit(5);
                boompar2.transform.position = contact;
                boompar2.startSpeed = (40f * (float)Math.Pow(size, 0.8f) / 406f);
                boompar2.startSize = (40f * (float)Math.Pow(size, 0.8f) / 406f);
                boompar2.startLifetime = (0.1f * (float)Math.Pow(size, 0.8f) / 406f);
                boompar2.Emit(5);


            }
            catch
            {

            }
        }
        public void EmitSparksE2(Vector3 contact, float size)
        {
            try
            {
                if (this.wood.wood)
                {
                    size = size / 5f;
                }
                boompar.transform.position = contact;
                boompar.startSpeed = (40f * (float)Math.Pow(size, 0.7f) / 406f);
                boompar.startSize = (30f * (float)Math.Pow(size, 0.7f) / 406f);
                boompar.startLifetime = (2f * (float)Math.Pow(size, 0.7f) / 406f) + 0.1f;
                boompar.Emit(5);
                boompar2.transform.position = contact;
                boompar2.startSpeed = (40f * (float)Math.Pow(size, 0.8f) / 406f);
                boompar2.startSize = (40f * (float)Math.Pow(size, 0.8f) / 406f);
                boompar2.startLifetime = (0.1f * (float)Math.Pow(size, 0.8f) / 406f) + 0.1f;
                boompar2.Emit(5);
            }
            catch
            {

            }
        }
        public void firein(Guid block, int playid , float size, Vector3 pos)
        {
            BlockBehaviour bb;
            foreach (BlockBehaviour match in UnityEngine.Object.FindObjectsOfType<BlockBehaviour>().ToList<BlockBehaviour>())
            {
                try
                {
                    if(match.BuildingBlock.Guid.ToString() == block.ToString() && match.BuildingBlock.ParentMachine.PlayerID == (ushort)playid)
                    {
                        bb = match;
                        GameObject fire = (GameObject)UnityEngine.Object.Instantiate(fire1, pos, Quaternion.identity);
                        fire.transform.SetParent(bb.transform);
                        fire.transform.localScale = (100f * (float)Math.Pow(size, 0.333f) / 20f) * Vector3.one;

                        UnityEngine.Object.Destroy(fire, 7f);
                        break;
                    }
                }
                catch
                {

                }
            }
            //=UnityEngine.Object.FindObjectsOfType<BlockBehaviour>().ToList<BlockBehaviour>().Find((BlockBehaviour match) => match.BuildingBlock.Guid.ToString() == block.ToString() && match.BuildingBlock.ParentMachine.PlayerID == (ushort)playid);
            

        }
    }
}
