using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static NonConvexMeshCollider;
using static PhysNodeBase;

namespace Navalmod
{
    public class blockset : MonoBehaviour
    {
        public BlockBehaviour blockBehaviour;
        public MMenu menu;
        public MMenu ZJG;   
        public MMenu JGG;
        public MMenu ZX;
        public MSlider thickness;
        public MSlider jointmax;
        public MText jointname;
        private bool first;
        public float br;
        public float time;
        public float runningtime;
        public bool complete;
        public MToggle rigidityScale;
        public void Awake()
        {
            first = false;
            try
            {
                blockBehaviour = base.gameObject.GetComponent<BlockBehaviour>();
            }
            catch
            {

            }
            menu = blockBehaviour.AddMenu("type", 0, new List<string> { "0.装甲钢", "1.结构钢", "2.杂项"}, false);
            ZJG = blockBehaviour.AddMenu("typeZJ", 0, new List<string> { "0.高镍装甲钢", "1.生铁", "2.克虏伯低碳装甲钢", "3.KNC", "4.Wh", "5.Ww", "6.Wsh", "7.KC", "8.VC", "9.NVNC", "10.VH", "11.MNC", "12.CNC", "13.ClassA", "14.ClassB", "15.NCA", "16.AOD", "17.CA", "18.BTC" }, false);
            JGG = blockBehaviour.AddMenu("typeJG", 0, new List<string> { "0.HT", "1.HTT", "2.DS", "3.STS", "4.Schiffbaustahl", "5.Schiffbaustahl_1", "6.Schiffbaustahl_2", "7.Schiffbaustahl_3", ".8.Schiffbaustahl_42", "9.Schiffbaustahl_52", "10.克虏伯低镍渗碳钢", "11.中镍结构钢" }, false);
            ZX = blockBehaviour.AddMenu("typeZX", 0, new List<string> { "0.木头", "1.飞机蒙皮", "2.钢筋混凝土" }, false);
            thickness = this.blockBehaviour.AddSlider("装甲厚度(mm)", "thickness", 5f, 0f, 500f, "", "x");
            jointmax = this.blockBehaviour.AddSlider("连接点限制", "jointmax", 100f, 1f, 100f, "", "x");
            rigidityScale = this.blockBehaviour.AddToggle("刚体模式", "rigidityScale",false );
            jointname = blockBehaviour.AddText("连接点端口","jointname","");
            menu.ValueChanged += change;
            time = 0f;
            runningtime = 0f;
            complete = false;
        }
        private float bf = 10000f;
        private float bt = 2000f;
        public void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (time >= 3f)
            {
                time = 0f + UnityEngine.Random.Range(0f, 0.3f);
                try
                {   
                    foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (meshRenderer.gameObject.name == "Vis" || meshRenderer.gameObject.name == "A" || meshRenderer.gameObject.name == "B" || meshRenderer.gameObject.name == "Cylinder")
                        {
                            if (meshRenderer.material.shader != SingleInstance<waterE>.Instance.matelshader)
                            {
                                meshRenderer.material.shader = SingleInstance<waterE>.Instance.matelshader;
                                meshRenderer.material.SetColor("_EmissionColor", new Color(220f, 220f, 220f));
                                meshRenderer.material.SetFloat("_DiffuseScatteringContraction", 10f);
                                meshRenderer.material.SetFloat("_DiffuseScatteringBias", 0.5f);
                                meshRenderer.material.SetColor("_Color", new Color(1.6f, 1.6f, 1.6f));
                            }
                        }

                    }
                }
                catch { 
                }
            }
            if (!first)
            {
                first = true;
                runningtime = 2f;
                try
                {
                    foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (meshRenderer.gameObject.name == "Vis" || meshRenderer.gameObject.name == "A" || meshRenderer.gameObject.name == "B" || meshRenderer.gameObject.name == "Cylinder")
                        {
                            if (meshRenderer.material.shader != SingleInstance<waterE>.Instance.matelshader)
                            {
                                meshRenderer.material.shader = SingleInstance<waterE>.Instance.matelshader;
                                meshRenderer.material.SetColor("_EmissionColor", new Color(220f, 220f, 220f));
                                meshRenderer.material.SetFloat("_DiffuseScatteringContraction", 10f);
                                meshRenderer.material.SetFloat("_DiffuseScatteringBias", 0.5f);
                                meshRenderer.material.SetColor("_Color", new Color(1.6f, 1.6f, 1.6f));
                            }
                        }

                    }
                }
                catch
                {

                }
                try
                {
                    if (blockBehaviour.BlockID != 56 && blockBehaviour.BlockID != 74)
                    {
                        float realz = Mathf.Min(Mathf.Min(blockBehaviour.transform.localScale.x, blockBehaviour.transform.localScale.y), blockBehaviour.transform.localScale.z);
                        float realy = 0;
                        float realx = 0;
                        if (realz == blockBehaviour.transform.localScale.x)
                        {
                            realy = blockBehaviour.transform.localScale.y;
                            realx = blockBehaviour.transform.localScale.z;
                        }
                        else if (realz == blockBehaviour.transform.localScale.y)
                        {
                            realx = blockBehaviour.transform.localScale.x;
                            realy = blockBehaviour.transform.localScale.z;
                        }
                        else
                        {
                            realx = blockBehaviour.transform.localScale.x;
                            realy = blockBehaviour.transform.localScale.y;
                        }
                        List<armorattribute> armorattributes = new List<armorattribute>();

                        int x = ZJG.Value;
                        if (menu.Value == 0)
                        {

                            armorattributes = SingleInstance<Armorlistat>.Instance.armorattributesZJ;
                            x = ZJG.Value;

                        }
                        if (menu.Value == 1)
                        {
                            armorattributes = SingleInstance<Armorlistat>.Instance.armorattributesJG;
                            x = JGG.Value;

                        }
                        if (menu.Value == 2)
                        {
                            armorattributes = SingleInstance<Armorlistat>.Instance.armorattributesZX;
                            x = ZX.Value;

                        }

                        SoftJointLimitSpring so = new SoftJointLimitSpring();
                        so.spring = armorattributes[x].EL / 20f;


                        br = armorattributes[x].Yield * realx * realy * thickness.Value * bf;
                        blockBehaviour.blockJoint.breakForce = br;
                        blockBehaviour.blockJoint.breakTorque = armorattributes[x].Yield * realx * realy * blockBehaviour.transform.localScale.z * thickness.Value * bt;




                    }

                }
                catch
                {

                }
            }
            else
            {
                if (blockBehaviour.isSimulating)
                {
                    runningtime -= 1f;
                }

            }

            if (runningtime <= 0f)
            {
                try
                {
                    ConfigurableJoint[] configurableJoints = blockBehaviour.GetComponentsInChildren<ConfigurableJoint>();
                    JointDrive jointDrive = new JointDrive();
                    jointDrive.maximumForce = 100000000000000f;
                    jointDrive.positionDamper = 0f;
                    for (int i = 0; i < configurableJoints.Length; i++)
                    {
                        if (rigidityScale.IsActive)
                        {

                            configurableJoints[i].projectionMode = JointProjectionMode.PositionAndRotation;
                            configurableJoints[i].projectionDistance /= 10f;
                            configurableJoints[i].projectionAngle /= 10f;
                            configurableJoints[i].rotationDriveMode = RotationDriveMode.Slerp;
                            configurableJoints[i].slerpDrive = jointDrive;

                        }
                        if ((i + 1 > (int)jointmax.Value && jointmax.Value < 100) || Checkjointname(configurableJoints[i].connectedBody.gameObject.GetComponent<blockset>()))
                        {
                            try
                            {
                                UnityEngine.Object.Destroy(configurableJoints[i]);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch
                {

                }
            }

        }
        public bool Checkjointname(blockset targetBlockset)
        {
            if (targetBlockset.jointname.Value == this.jointname.Value)
            {
                return false;
            }
            if (targetBlockset.jointname.Value == ""||this.jointname.Value == "")
            {
                return false;
            }
            return true;
        }
        public void change(int a)
        {
            ZJG.DisplayInMapper = menu.Value == 0;
            JGG.DisplayInMapper = menu.Value == 1;
            ZX.DisplayInMapper = menu.Value == 2;
        }
		
	}
}
