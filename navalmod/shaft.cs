using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
using Selectors;
using UnityEngine.SocialPlatforms;
using System;

namespace Navalmod
{
    public class shaft : BlockScript
    {
        public float shaftHP;
        public float MAXHP;
        public float speedscale;
        public MText engineInterface;
        public MToggle dir;
        public MMenu type;
        public List<boiler> boilers = new List<boiler>();
        public float renewtime;
        
        // Use this for initialization
        public void Start()
        {
            
        }
        public void Awake()
        {

            dir = base.AddToggle("反转","dir",true);
            engineInterface = base.AddText("传动轴编号", "engineInterface", "engine");
            type = base.AddMenu("shaft_type", 0, new List<string>() { "三桨叶", "四桨叶", "五桨叶"}, false);
            
            type.ValueChanged += changemenu;
            changemenu(1);
            boilers = new List<boiler>();
            renewtime = 0f;
            shaftHP = 0f;
            MAXHP = 0f;
            first = false;
        }
        // Update is called once per frame
        public void changemenu(int val)
        {
            try
            {
                switch (type.Value)
                {
                    case 0:
                        base.BlockBehaviour.transform.FindChild("Vis").GetComponent<MeshFilter>().mesh = ModResource.GetMesh("shaft_3").Mesh;
                        break;
                    case 1:
                        base.BlockBehaviour.transform.FindChild("Vis").GetComponent<MeshFilter>().mesh = ModResource.GetMesh("shaft_4").Mesh;
                        break;
                    case 2:
                        base.BlockBehaviour.transform.FindChild("Vis").GetComponent<MeshFilter>().mesh = ModResource.GetMesh("shaft_5").Mesh;
                        break;
                }
            }
            catch
            {

            }
        }
        void Update()
        {

        }
        public bool first;
        public void FixedUpdate()
        {
            if (IsSimulating)
            {
                if (!first)
                {
                    if (IsSimulating)
                    {

                        changemenu(1);
                        foreach (Joint joint in base.BlockBehaviour.jointsToMe)
                        {
                            joint.breakForce = 0;
                        }
                        
                        first = true;
                        ConfigurableJoint configurableJoints = base.GetComponent<ConfigurableJoint>();
                        configurableJoints.angularYMotion = ConfigurableJointMotion.Free;
                        configurableJoints.projectionMode = JointProjectionMode.PositionAndRotation;
                        configurableJoints.projectionDistance = 0f;
                        configurableJoints.projectionAngle = 5f;

                        foreach (boiler boiler in FindObjectsOfType<boiler>())
                        {
                            if (boiler.BlockBehaviour.ParentMachine.PlayerID == base.BlockBehaviour.ParentMachine.PlayerID && boiler.engineInterface.Value == engineInterface.Value)
                            {
                                boilers.Add(boiler);
                            }
                        }
                    }
                }

                renewtime += Time.fixedDeltaTime;
                try
                {
                    if (renewtime >= 0.1f)
                    {
                        renewtime = 0;
                        RenewHP();
                    }
                    
                    if (dir.IsActive)
                    {
                        base.BlockBehaviour.Rigidbody.AddForce(base.transform.forward * (-shaftHP * 15f));
                    }
                    else
                    {
                        base.BlockBehaviour.Rigidbody.AddForce(base.transform.forward * (shaftHP * 15f));
                    }
                    if (MAXHP != 0f)
                    {

                        base.transform.RotateAround(base.transform.position, base.transform.forward, Mathf.Clamp(34f * (speedscale), -34f, 34f));
                    }
                }
                catch
                {

                }
            }
            else
            {
                if (!first)
                {
                    changemenu(1);
                    first = true;
                }
                }
        }
        public void RenewHP()
        {
            float HP = 0f;
            float maxHP = 0f;
            float speedscaleE = 0f;
            int n = 0;
            foreach (boiler boiler in boilers)
            {
                n++;
                maxHP += boiler.engine_hp.Value;
                HP += boiler.engine_provideHp;
                speedscaleE += boiler.speedscale;
            }
            if (n > 0)
            {
                speedscale = speedscaleE / n;
                MAXHP = maxHP / n;
                shaftHP = HP;
            }
        }
    }
}