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
    public class AIbattery: MonoBehaviour
    {
        public SteeringWheel CCMH;
        public MKey modeChange;
        public bool fineMode;
        public MSlider fastmodsli;
        public void Awake()
        {
            
            CCMH = base.gameObject.GetComponent<SteeringWheel>();
            modeChange = CCMH.AddKey("精细模式", "modeChange", KeyCode.None);
            AI = CCMH.AddToggle("开启ai炮台", "ai", false);
            XX = CCMH.AddSlider("指示零件相对X轴", "XX", 0f, -20f, 20f, "", "x");
            YY = CCMH.AddSlider("指示零件相对Y轴", "YY", 0f, -20f, 20f, "", "x");
            ZZ = CCMH.AddSlider("指示零件相对Z轴", "ZZ", 0f, -20f, 20f, "", "x");
            Xr = CCMH.AddSlider("指示方向X轴", "Xr", 0f, -360f, 360f, "", "x");
            Yr = CCMH.AddSlider("指示方向Y轴", "Yr", 0f, -360f, 360f, "", "x");
            Zr = CCMH.AddSlider("指示方向Z轴", "Zr", 0f, -360f, 360f, "", "x");
            r = CCMH.AddSlider("选取指示零件半径", "r", 0.05f, 0.05f, 1f, "", "x");
            flaktext=CCMH.AddText("火控接口编号", "flaktext", "");
            fastattack = CCMH.AddToggle("快速瞄准", "fastattack", true);
            fastmodsli = CCMH.AddSlider("精确瞄准启动角度", "fastmodsli", 5f, 0f, 10f, "", "x");
            XX.ValueChanged += poschange;
            YY.ValueChanged += poschange;
            ZZ.ValueChanged += poschange;
            Xr.ValueChanged += poschange;
            Yr.ValueChanged += poschange;
            Zr.ValueChanged += poschange;
            r.ValueChanged += poschange;
            poschange(1);
            boolchange(false);
            AI.Toggled += boolchange;
            first = 0;
            renum = 0;
            if (selectgb == null)
            {
                selectgb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                selectgb.GetComponent<Collider>().enabled = false;
            }
            if (selectgbE == null)
            {
                selectgbE = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                selectgbE.GetComponent<Collider>().enabled = false;

            }
            if (see == null)
            {
                see = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                see.GetComponent<Collider>().enabled = false;
                seeE = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                seeE.GetComponent<Collider>().enabled = false;
                see.transform.SetParent(seeE.transform);
                see.transform.position = new Vector3(1000f,1000f,1000f);
                seeE.transform.position = new Vector3(1000f, 1000f, 1000f);
            }
            perparefire = false;
            ccmro = CCMH.ParentMachine.Rotation;
            this.visualisingHeight = true;
            fineMode = false;

        }
        public void boolchange(bool value)
        {
            XX.DisplayInMapper = AI.IsActive;
            YY.DisplayInMapper = AI.IsActive;
            ZZ.DisplayInMapper = AI.IsActive;
            Xr.DisplayInMapper = AI.IsActive;
            Yr.DisplayInMapper = AI.IsActive;
            Zr.DisplayInMapper = AI.IsActive;
            r.DisplayInMapper = AI.IsActive;
            flaktext.DisplayInMapper = AI.IsActive;
            CCMH.canBeAutomatic = AI.IsActive;
            fastattack.DisplayInMapper = AI.IsActive;

        }
            public void poschange(float value)
        {
            
                selectpos = new Vector3(XX.Value, YY.Value, ZZ.Value) + base.transform.position;
                selectr = new Vector3(Xr.Value, Yr.Value, Zr.Value);

            
        }
        public bool emulating;
        public float angle;
        public float time;
        protected MKey emulateKey;
        public GameObject see;
        public GameObject seeE;
        public Quaternion ccmro;
        private bool perparefire;
        public void FixedUpdate()
        {
            try
            {
                if (CCMH.isSimulating && AI.IsActive)
                {
                    renum += 1;
                    if (renum==1)
                    {
                        poschange(1);
                    }
                    if (first == 0 && renum <= 10 && renum >= 2)
                    {
                        selectbb = null;
                        foreach (Collider collider in Physics.OverlapSphere(selectpos, r.Value))
                        {


                            try
                            {
                                selectbb = collider.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>();
                                selectrsub = selectr - selectbb.transform.localRotation.eulerAngles;
                                seeE.transform.rotation = selectbb.transform.rotation;
                                see.transform.rotation = Quaternion.Euler(selectr);
                                break;

                            }
                            catch
                            {

                            }
                        }

                        if (selectbb != null)
                        {
                            first = 1;
                            try
                            {
                                foreach (flakN flakN in AIflak.Instance.flakNs)
                                {
                                    if (flakN.name == flaktext.Value && flakN.play == CCMH.BuildingBlock.ParentMachine.PlayerID)
                                    {
                                        view = flakN.view;

                                        view.cantfire += 1;
                                        perparefire = false;
                                        renum = 11;

                                    }
                                }
                            }
                            catch
                            {

                            }
                        }


                    }
                }
            }
            catch
            {

            }
            try
            {

                if (CCMH.isSimulating && AI.IsActive && view.locktype != 0)
                {
                    Vector3 i = view.lockg.transform.position - view.flakdpos;
                    seeE.transform.rotation = selectbb.transform.rotation;
                    Vector3 plane = Vector3.Cross(base.transform.right, base.transform.forward);
                    Vector3 lockv = Vector3.ProjectOnPlane(i - selectbb.transform.position, plane);
                    Vector3 n = Vector3.ProjectOnPlane((see.transform.rotation) * new Vector3(0, 0, 1), plane);
                    Vector3 nn = Quaternion.FromToRotation(base.transform.forward, n) * base.transform.right;
                    if (time >= 0.2f)
                    {
                        CCMH.AutomaticToggle.IsActive = true;
                        time = 0f;
                    }
                    else
                    {
                        time += Time.fixedDeltaTime;
                        CCMH.AutomaticToggle.IsActive = false;
                    }
                    float ang = Vector3.Angle(lockv - Vector3.Project(lockv,n), nn - Vector3.Project(nn, n));
                    float angleattack = 0;
                    /*
                    if (fastattack.IsActive)
                    {
                        angleattack = 10f;
                    }
                    else
                    {
                        angleattack = 1f;
                    }*/

                    if (fastattack.IsActive)
                    {
                        angleattack = 3f;
                    }
                    else
                    {
                        angleattack = 0.5f;
                    }
                    if ( !fastattack.IsActive)
                    {
                        if (Vector3.Angle(lockv, n) < fastmodsli.Value)
                        {
                            if (!fineMode)
                            {
                                fineMode = !fineMode;
                                if (fineMode)
                                {
                                    CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value / 5f;
                                }
                                else
                                {
                                    CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value * 5f;
                                }
                            }
                        }
                        else
                        {
                            if (fineMode)
                            {
                                fineMode = !fineMode;
                                if (fineMode)
                                {
                                    CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value / 5f;
                                }
                                else
                                {
                                    CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value * 5f;
                                }
                            }
                        }

                    }
                    if (ang >= 90f)
                    {
                        if (Vector3.Angle(lockv, n) > 0.5f)
                        {



                            CCMH.AngleToBe += Math.Max(-CCMH.SpeedSlider.Value * Vector3.Angle(lockv, n) / angleattack,   -CCMH.SpeedSlider.Value);
                            angle = CCMH.AngleToBe;
                            
                        }
                        else
                        {
                            CCMH.AngleToBe = angle;
                            

                        }


                    }
                    else
                    {
                        
                        if (Vector3.Angle(lockv, n) > 0.5f)
                        {
                            CCMH.AngleToBe += -Math.Max(-CCMH.SpeedSlider.Value * Vector3.Angle(lockv, n) / angleattack, -CCMH.SpeedSlider.Value);
                            angle = CCMH.AngleToBe;

                        }
                        else
                        {
                            CCMH.AngleToBe = angle;

                        }
                        
                    }
                    if (this.CCMH.allowLimits && CCMH.LimitsSlider.IsActive)
                    {
                        float num4;
                        float num5;
                        num4 = -this.CCMH.LimitsSlider.Max;
                        num5 = this.CCMH.LimitsSlider.Min;
                        if (CCMH.Flipped)
                        {
                            num4 = -this.CCMH.LimitsSlider.Min;
                            num5 = this.CCMH.LimitsSlider.Max;
                        }
                        else
                        {
                            num4 = -this.CCMH.LimitsSlider.Max;
                            num5 = this.CCMH.LimitsSlider.Min;
                        }
                        CCMH.AngleToBe = ((CCMH.AngleToBe >= num4) ? ((CCMH.AngleToBe <= num5) ? CCMH.AngleToBe : num5) : num4);
                    }
                }
            }
            catch
            {

            }
             
        }
        public void Update()
        {
            try
            {
                if (BlockMapper.IsOpen &&BlockMapper.CurrentInstance.Current == CCMH &&AI.IsActive==true && !CCMH.isSimulating)
                {
                    this.ShowVisualisation();
                }
                else
                {
                    HideVisualisation();
                }
            }
            catch
            {

            }
            try
            {
                if (CCMH.isSimulating)
                {
                    if (modeChange.IsPressed)
                    {
                        fineMode = !fineMode;
                        if (fineMode)
                        {
                            CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value / 5f;
                        }
                        else
                        {
                            CCMH.SpeedSlider.Value = CCMH.SpeedSlider.Value * 5f;
                        }
                    }
                }

            }
            catch
            {

            }
            
        }
        protected void ShowVisualisation()
        {
            if (!this.visualisingHeight)
            {
                this.selectgb.gameObject.SetActive(true);
                this.selectgbE.gameObject.SetActive(true);
                this.visualisingHeight = true;
            }
            
            this.UpdateVisualisation();
        }
        protected void UpdateVisualisation()
        {
            selectgb.transform.localScale = Vector3.one*0.5f;
            this.selectgb.transform.position = selectpos;
            selectgb.transform.rotation = Quaternion.Euler(selectr);
            selectgbE.transform.localScale = Vector3.one * 0.5f;
            selectgbE.transform.rotation = Quaternion.Euler(selectr);

            this.selectgbE.transform.position = selectpos + 1f * (Quaternion.Euler(selectr)*CCMH.ParentMachine.Rotation* new Vector3(0, 0, 1));
        }
        protected void HideVisualisation()
        {
            if (!this.visualisingHeight)
            {
                return;
            }
            this.selectgb.gameObject.SetActive(false);
            this.selectgbE.gameObject.SetActive(false);
            this.visualisingHeight = false;
        }
        private int renum;
        public GameObject selectgb;
        public GameObject selectgbE;
        private bool visualisingHeight;
        private int first;
        public MSlider XX;
        public MSlider YY;
        public MSlider ZZ;
        public MSlider Xr;
        public MSlider Yr;
        public MSlider Zr;
        public MSlider r;
        public MToggle AI;
        protected MKey[] activationKeys;
        public MToggle AIE;
        public MToggle fastattack;
        public MText flaktext;
        private Vector3 selectpos;
        private Vector3 selectr;
        private Vector3 selectsize;
        private Vector3 selectrsub;
        private Vector3 selectrrsub;
        public BlockBehaviour selectbb;
        private view view;
        private MKey activateKey;
    }
}
