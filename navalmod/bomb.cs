using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
using static NonConvexMeshCollider;
using UnityEngine.Rendering;

namespace Navalmod
{
    public class bomb : BlockScript
    {
        public MKey fire;
        public MSlider tntmass;
        public MSlider mass;
        public MSlider salftime;
        public MToggle AP;
        public Navalhe navalhe;
        public NAbolt nAbolt;
        public bool fireing;
        public void Awake()
        {
            fire = base.AddKey("脱离","throwbomb",KeyCode.None);
            tntmass = base.AddSlider("装药","tntmass",300f,10f,1000f);
            mass = base.AddSlider("弹重(t)", "mass", 0.5f, 0.1f, 1f);
            salftime = base.AddSlider("安全引信时间", "salftime", 0.5f, 0.1f, 2f);
            AP = base.AddToggle("穿甲弹头","APtype",false);
            navalhe = FindObjectOfType<Navalhe>();
            AP.DisplayInMapper = false;
        }

        public void Update()
        {
            if (IsSimulating)
            {
                if (nAbolt == null)
                {
                    
                }
                if ((fire.IsPressed || fire.EmulationPressed()) && fireing == false)
                {
                    fireing = true;
                    try
                    {
                        base.BlockBehaviour.blockJoint.breakForce = -100;
                        foreach (Joint joint in base.BlockBehaviour.jointsToMe)
                        {
                            joint.breakForce = -100;
                        }
                        
                    }
                    catch
                    {

                    }
                    StartCoroutine(salftimeopen());

                }
            }
        }
        public IEnumerator salftimeopen()
        {
            yield return new WaitForSeconds(salftime.Value);

            InitBullet();
        }
        public void InitBullet()
        {
            if (nAbolt == null)
            {
                NAbolt nabolt = base.gameObject.AddComponent<NAbolt>();
                try
                {
                    nabolt.blot = base.gameObject;
                    nabolt.canexplo = true;
                    nabolt.NavalCannoBlockE = new NavalCannoBlockE();
                    nabolt.cjxs = 0f;
                    nabolt.seahigh = SingleInstance<Sea>.Instance.Getseahigh(base.transform.position);
                    nabolt.asd = false;
                    nabolt.tntmass = this.tntmass.Value;
                    nabolt.mass = this.mass.Value * 1000f;
                    if (AP.IsActive)
                    {
                        nabolt.type = 0;
                    }
                    else
                    {
                        nabolt.type = 1;
                    }
                    nabolt.timer = 0f;
                    nabolt.navalhe = navalhe;
                    nabolt.ycyx = false;
                    nabolt.hefuse = 0;
                    nabolt.saptype = 1;
                    nabolt.aptype = 1;
                    nabolt.apfuzetime = 1;
                    nabolt.wood = this.navalhe.wood;
                    nabolt.waterin = true;
                    nabolt.time2 = -1f;
                    nabolt.MyGUID = base.BlockBehaviour.BuildingBlock.Guid.ToString();
                    nabolt.simplehe = 2;
                    nabolt.isboom= true;
                    Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
                    nabolt.rigidbody2 = component;
                    component.freezeRotation = true;
                    component.drag = 0.05f;
                    component.mass = this.mass.Value;
                    component.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    component.freezeRotation = true;

                    nabolt.hit3 = true;

                    nabolt.networkid = base.BlockBehaviour.ParentMachine.PlayerID;
                    nabolt.scale = 200f;


                    nabolt.hit2 = false;
                    try
                    {
                        nabolt.player = (int)base.BlockBehaviour.ParentMachine.PlayerID;
                    }
                    catch
                    {

                    }
                }
                catch
                {

                }
                nAbolt = nabolt;
                
            }

            


        }
    }
}