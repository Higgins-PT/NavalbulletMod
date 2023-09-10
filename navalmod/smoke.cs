using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using System.Reflection;
namespace Navalmod
{
    class smoke : BlockBehaviour

    {
        public MSlider scale;
        public MKey buttom;
        public Navalhe navalhe;
        public TimedRocket TimedRocket;
        public float time;
        public int counta;
        public bool first = false;
        public bool open;
        public ParticleSystem.EmissionModule emission;
        public void Update()
        {
            if (openE.IsActive)
            {
                if (isSimulating)
                {
                    if (buttom.IsPressed)
                    {
                        open = !open;
                    }

                }
            }
        }
        public int firecount;
        public void FixedUpdate()
        {
            if (openE.IsActive)
            {
                if (isSimulating)
            {
                firecount--;
                if (open)
                {
                    boomobj.transform.position = base.transform.position;

                    if (firecount <= 0)
                    {
                        firecount = 10;
                        boompar.Emit(1);
                            
                    }
                }
            }
            if (first&& isSimulating)
            {
                boommat = new Material(Shader.Find("Particles/Alpha Blended"));
                boomtex = ModResource.GetTexture("boomtex");
                boommat.mainTexture = boomtex;

                boomobj = new GameObject();
                first = false;

                boompar = boomobj.AddComponent<ParticleSystem>();
                boompar.startLifetime = 50f;
                boompar.startSpeed = 2f * scale.Value;
                boompar.gravityModifier = 0.00001f;
                boompar.startSize = 200f * scale.Value;
                Color color;
                color.r = 10f;
                color.b = 10f;
                color.g = 10f;
                color.a = 10f;
                boompar.startColor = color;
                boompar.maxParticles = 500;
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
        }
        public Material boommat;
        public ParticleSystem boompar;
        public GameObject boomobj = new GameObject();
        public static ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        public ModTexture boomtex;
        public MToggle openE;
        public void Awake()
        {



            
            navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            TimedRocket = base.GetComponent<TimedRocket>();
            scale = TimedRocket.AddSlider("烟雾大小", "scaleE", 1f, 0.1f, 20f, "", "x");
            buttom = TimedRocket.AddKey("开关烟雾发生器","toggleE",UnityEngine.KeyCode.None);
            openE = TimedRocket.AddToggle("烟雾发生器模式","smoke",false);
            counta = 0;
            time = 0f;
            open = false;
            first = true;
            
            


        }

    }
}
