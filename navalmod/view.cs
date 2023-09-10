using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Common;
using Modding.Blocks;
using UnityEngine;
using System.Reflection;
namespace Navalmod
{
    public class view : MonoBehaviour
    {
        public bool send;


        private IEnumerator SendToPlayer()
        {

            send = true;
            try
            {
                ModNetworking.SendTo(Player.From(bb.ParentMachine.PlayerID), Messages.flak.CreateMessage(new object[]
                {
                guid.ToString(),
                flaklock.transform.position,
                flaklock.Rigidbody.velocity
                }));
            }
            catch
            {

            }
            yield return new WaitForSecondsRealtime(0.2f);
            send = false;
            yield break;
        }
        public float angle=0f;
        Rigidbody rigidbodyE;
        public float selecttimeE=10f;
        public float sntime = 0f;
        private bool selecting;
        public bool fireing;
            private void FixedUpdate()
        {
            if (bb.isSimulating)
            {
                
                
                try
                {
                    if (flak.IsActive && initiativeselect.IsActive && !StatMaster.isClient)
                    {

                        selecttimeE -= Time.fixedDeltaTime;
                        if (selecttimeE <= 0f && bb.isSimulating)
                        {
                            selecttimeE = selecttime.Value;
                            
                                selecting = false;
                                foreach (Collider targets in Physics.OverlapSphere(base.transform.position, selectradius.Value))
                                {

                                    try
                                    {
                                        if (targets.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().BlockID != 114514)
                                        {
                                            MPTeam targetteam = targets.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().Team;
                                            if (selectteam.IsActive)
                                            {
                                                if ((targetteam == MPTeam.None || bb.Team != targetteam) && targets.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>().ParentMachine != bb.ParentMachine)
                                                {
                                                    if (targets.attachedRigidbody.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= selectvelocity.Value)
                                                    {

                                                        if (Physics.OverlapSphere(targets.attachedRigidbody.gameObject.transform.position, selecttargetsize.Value).Length >= selecttargetpackcount.Value)
                                                        {

                                                            selecting = true;
                                                            locktype = 2;
                                                            lockg = targets.attachedRigidbody.gameObject; 
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (targets.attachedRigidbody.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= selectvelocity.Value)
                                                {

                                                    if (Physics.OverlapSphere(targets.attachedRigidbody.gameObject.transform.position, selecttargetsize.Value).Length >= selecttargetpackcount.Value)
                                                    {

                                                        selecting = true;
                                                        locktype = 2;
                                                        lockg = targets.attachedRigidbody.gameObject;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    if (selecting)
                                    {
                                        break;
                                    }
                                }
                                if (selecting == false && outtarget.IsActive)
                                {
                                    locktype = 0;
                                }
                            
                        }//-------------
                    }
                    sntime += Time.fixedDeltaTime;
                    if (sonar.IsActive)
                    {

                        if (!bb.isSimulating && outputE.IsActive)
                        {
                            angle += 0.5f;
                            if (angle >= 60f)
                            {
                                angle = 0f;
                            }
                            if (outputgb == null)
                            {
                                outputgb = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                                outputgb.transform.localScale = new Vector3(2f, 500f, 2f);
                            }
                            outputgb.transform.position = base.transform.position;
                            outputgb.transform.up = Quaternion.AngleAxis(angle, base.transform.right) * base.transform.forward;
                            outputgb.SetActive(true);
                        }
                        else
                        {
                            try
                            {
                                outputgb.SetActive(false);
                            }
                            catch
                            {

                            }
                        }

                        if (bb.isSimulating && sntime >= 0.05f)
                        {
                            sntime = 0f;
                            if (StatMaster.isHosting)
                            {
                                base.StartCoroutine(SendToPlayer());
                            }
                            Vector3 start = new Vector3();
                            start = base.transform.position;
                            start.y -= 11f;
                            bool first = true;

                            angle += 1f;
                            if (angle >= 60f)
                            {
                                angle = 0f;
                            }
                            rigidbodyE = new Rigidbody();
                            foreach (Collider target in Physics.OverlapCapsule(start, start + 2000f * (Quaternion.AngleAxis(angle, base.transform.right) * base.transform.up.normalized), 10f))
                            {

                                try
                                {

                                    Rigidbody rigidbody = target.attachedRigidbody;
                                    if ((target.attachedRigidbody.transform.position.y <= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - 5f) && target.attachedRigidbody.gameObject.GetComponent<blockset>() != null && rigidbody.velocity.magnitude >= 3.6f)
                                    {
                                        if (first)
                                        {
                                            rigidbodyE = rigidbody;
                                            first = false;
                                        }
                                        if (rigidbodyE.velocity.magnitude < rigidbody.velocity.magnitude)
                                        {
                                            rigidbodyE = rigidbody;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }

                            lockp = rigidbodyE.transform.position;
                            locktype = 1;
                            
                            ModNetworking.SendToAll(Messages.sonarinfo.CreateMessage(new object[]
                {
                    guid.ToString(),
                    lockp
                }));
                        }
                    }
                    else
                    {
                        if (StatMaster.isHosting)
                        {

                            if (send == false && flak.IsActive && sntime >= 0.2f&&locktype!=0)
                            {
                                sntime = 0f;
                                base.StartCoroutine(SendToPlayer());
                            }
                        }
                        if (rader.IsActive)
                        {
                            if (looktype.Value == 7)
                            {
                                navalhe.isss = true;
                                navalhe.sspos = base.transform.position;

                            }
                        }
                        if (flak.IsActive || locktype == 2)
                        {
                            if (looktype.Value == 7)
                            {
                                if (base.transform.position.y >= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
                                {
                                    flaktime += Time.fixedDeltaTime;

                                }
                            }
                            else
                            {
                                flaktime += Time.fixedDeltaTime;
                            }

                            if (flaktime >= lookwait / navalhe.retime)
                            {
                                flaktime = 0f;

                                float magnitude = lockg.GetComponent<Rigidbody>().velocity.magnitude;
                                if (rideotype.Value == 1)
                                {
                                    float 炮弹速度 = this.power.Value * 0.5144f*3f;
                                    Vector3 posE = base.transform.position;
                                    posE.y += high.Value;
                                    flakpos = calculateNoneLinearTrajectory(炮弹速度, 0f, posE, magnitude, this.lockg.transform.position
                                        , this.lockg.GetComponent<Rigidbody>().velocity.normalized, calculateLinearTrajectory(炮弹速度, base.transform.position, magnitude
                                        , this.lockg.transform.position, this.lockg.GetComponent<Rigidbody>().velocity.normalized), 0f
                                        , errorE, float.PositiveInfinity);
                                }
                                else
                                {
                                    float 炮弹速度 = this.power.Value * 3f;
                                    Vector3 posE = base.transform.position;
                                    posE.y += high.Value;
                                    flakpos = calculateNoneLinearTrajectory(炮弹速度, drag.Value, posE, magnitude, this.lockg.transform.position
                                        , this.lockg.GetComponent<Rigidbody>().velocity.normalized, calculateLinearTrajectory(炮弹速度, base.transform.position, magnitude
                                        , this.lockg.transform.position, this.lockg.GetComponent<Rigidbody>().velocity.normalized), Physics.gravity.y
                                        , errorE, float.PositiveInfinity);
                                }
                                float n = (lockg.transform.position - base.transform.position).magnitude;
                                raterateOfVec = Mathf.Abs(rateOfVec - Mathf.Abs(lastRange - n));
                                rateOfVec = Mathf.Abs(lastRange-n);
                                errorMulti -= GetErrorMulti(rateOfVec,raterateOfVec);
                                if (errorMulti < 0)
                                {
                                    errorMulti = 0;
                                }
                                if (errorMulti > 3) { errorMulti = 3; }
                                lastRange = n;
                                flakdpos = lockg.transform.position - flakpos;
                                flakdpos += UnityEngine.Random.Range(-errorE, errorE) * flakdpos.normalized / 4f;
                                
                            }
                        }
                    }
                }
                catch
                {

                }
            }

        }
        private void Sendray()
        {
            
            ModNetworking.SendToAll(Messages.rayinfo.CreateMessage(new object[]
            {
                    guid.ToString(),
                    Camera.main.ScreenPointToRay(Input.mousePosition).origin,
                    Camera.main.ScreenPointToRay(Input.mousePosition).direction
            }));

        }
        public GameObject outputgb;
        public float lookerror = 0f;
        public float lookwait = 0f;
        public float coldtime = 0f;
        public float lastRange;
        public float raterateOfVec;
        public float errorMulti;
        public float GetErrorMulti(float rateOfVec,float raterateOfVec)
        {
            return (raterateOfVec + raterateOfVec * 100f)/10000f;
        }
        public void Update()
        {
            
            if (radio.IsActive && bb.isSimulating)
            {
                if (radiokey.IsPressed)
                {
                    radioopen = !radioopen;
                }
                if (firstE)
                {
                    firstE = false;
                    this.Ticon = ModResource.GetTexture("raderlock").Texture;
                    radioopen = openfirst.IsActive;
                    
                }
            }
            if ((firec.IsActive||sonar.IsActive)&&bb.isSimulating)
            {
                time += Time.deltaTime;
                coldtime-= Time.deltaTime;
                if (unluck.IsPressed&& coldtime<=0f)
                {
                    locktype = 0;
                }
                if (lockt.IsPressed)
                {
                    if (!StatMaster.isClient)
                    {
                        RaycastHit raycastHit;
                        bool flag3 = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Camera.main.farClipPlane);
                        if (flag3)
                        {
                            if (raycastHit.transform.gameObject.GetComponent<BlockBehaviour>() == null)
                            {
                                locktype = 1;
                                lockp = raycastHit.point;
                            }
                            else
                            {
                                locktype = 2;
                                lockg = raycastHit.transform.gameObject;
                                coldtime = 0.2f;
                            }

                        }
                    }
                    else
                    {
                        Sendray();
                    }
                }
                if (first)
                {
                    
                    switch (looktype.Value)
                    {
                        case 0:
                            lookwait = lookeyeT[lookeye.Value]* looktime[looktype.Value];
                            lookerror = lookeyeE[lookeye.Value];
                            break;
                        case 1:
                            lookwait = looktime[looktype.Value] * looksterLT[looklevelster.Value];
                            lookerror = lookster[looklengthster.Value] * looksterL[looklevelster.Value];
                            break;
                        case 2:
                            lookwait = looktime[looktype.Value] * lookcoiLT[looklevelcoi.Value];
                            lookerror = lookcoi[looklengthcoi.Value] * lookcoiL[looklevelcoi.Value];
                            break;
                        case 3:
                            lookwait = looktime[looktype.Value];
                            lookerror = lookriderS[lookrider.Value];
                            break;
                        case 4:
                            lookwait = looktime[looktype.Value];
                            lookerror = 0.1f;
                            break;

                    }
                    if (flak.IsActive)
                    {
                        AIflak.Instance.Addview(flaktext.Value, bb.ParentMachine.PlayerID, this);
                    }
                    views = new List<view>();
                    this.networkids = bb.BuildingBlock.ParentMachine.PlayerID;
                    first = false;

     
                    if (firec.IsActive)
                    {
                        foreach (BlockBehaviour blockBehaviour in ReferenceMaster.GetSimulationBlocks((uint)this.networkids))
                        {
                            if (blockBehaviour.BlockID == 35&&blockBehaviour.GetComponent<view>().rader.IsActive)
                            {
                                views.Add(blockBehaviour.GetComponent<view>());
              
                            }
                        }
                    }
                    this.Ticon = ModResource.GetTexture("raderlock").Texture;
                    if (flak.IsActive)
                    {

                        TiconE= ModResource.GetTexture("quan").Texture;
                    }
                }
                if (time >= 0.5f)
                {
                    time = 0f;
                    Vector3 pos = new Vector3();
                    if (locktype == 1)
                    {
                        pos = lockp;
                    }
                    if (locktype == 2)
                    {
                        pos = lockg.transform.position;
                    }
                    errorE = 1000000f;
                    float realError = lookerror / Math.Max(errorMulti+1f,1f);
                    foreach (view view in views)
                    {
                        
                        errorE = Math.Min(errorE, (view.transform.position - pos).magnitude * UnityEngine.Random.Range(0f, realError));
                    }
                    float ranger = (base.transform.position - pos).magnitude;
                    if (sonar.IsActive)
                    {
                        rangelast = range;
                    }
                    else
                    {
                        if (UnityEngine.Random.Range(-0.5f, 0.5f) > 0f)
                        {
                            rangelast = ranger + errorE;
                        }
                        else
                        {
                            rangelast = ranger - errorE;
                        }
                    }
                }
            }
        }
        public void output()
        {
            if (!hidesign.IsActive)
            {
                if (locktype != 0)
                {



                    Vector3 pos = new Vector3();

                    if (locktype == 1)
                    {
                        pos = Camera.main.WorldToScreenPoint(lockp);

                    }
                    if (locktype == 2)
                    {
                        pos = Camera.main.WorldToScreenPoint(lockg.transform.position);
                    }
                    GUI.color = iconcolor.Value;

                    if (pos.z >= 0)
                    {
                        GUI.DrawTexture(new Rect(pos.x - (float)(this.iconSize / 2), (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSize / 2), (float)this.iconSize, (float)this.iconSize), this.Ticon);
                        GUI.TextArea(new Rect(pos.x - (float)(this.iconSize / 2) + 50f, (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSize / 2), (float)this.iconSize, (float)this.iconSize), rangelast.ToString() + "m  rate:"+ (errorMulti*100).ToString()+"%");
                    }

                    if (locktype == 2 && flak.IsActive)
                    {

                        pos = Camera.main.WorldToScreenPoint(lockg.transform.position - flakdpos);
                        if (pos.z >= 0)
                        {
                            GUI.DrawTexture(new Rect(pos.x - (float)(this.iconSizeE / 2), (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSizeE / 2), (float)this.iconSizeE, (float)this.iconSizeE), this.TiconE);
                        }
                    }

                }
                if (radioopen)
                {
                    Vector3 pos = new Vector3();
                    pos = Camera.main.WorldToScreenPoint(base.transform.position);
                    GUI.color = iconcolor.Value;
                    if (pos.z >= 0)
                    {
                        GUI.DrawTexture(new Rect(pos.x - (float)(this.iconSize / 2), (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSize / 2), (float)this.iconSize, (float)this.iconSize), this.Ticon);
                        GUI.TextArea(new Rect(pos.x - (float)(this.iconSize / 2) + 50f, (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSize / 2), (float)this.iconSize, (float)this.iconSize), radioname.Value);
                    }
                }
            }
        }

        private void OnGUI()
        {
            try
            {
                if (PlayerData.localPlayer.networkId == bb.ParentMachine.PlayerID)
                {
                    if (navalhe.ss)
                    {
                        if (sonar.IsActive)
                        {
                            output();
                        }
                        else
                        {
                            if (looktype.Value==7)
                            {
                                if ((lockg.transform.position.y >= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - 1f)||base.gameObject.transform.position.y >= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position))
                                {
                                    output();
                                }
                            }
                            else{
                                if (lockg.transform.position.y >= SingleInstance<Sea>.Instance.Getseahigh(base.transform.position) - 1f)
                                {
                                    output();
                                }
                            }
                        }
                        

                    }
                    else
                    {
                        output();

                    }
                }
            }
            catch
            {

            }
        }


        public void Awake()
        {
            try
            {
                bb = base.GetComponent<BlockBehaviour>();
                radio = bb.AddToggle("设置为无线电台", "radio", false);
                rader = bb.AddToggle("设置为测距模块", "rader", false);
                sonar = bb.AddToggle("设置为声呐","sonar",false);
                firec = bb.AddToggle("设置为火控室", "firec", false);
                flak = bb.AddToggle("计算弹道", "flak", false);
                flaktext = bb.AddText("火控接口编号", "flaktext", "");
                power = bb.AddSlider("发射物初速度（m/s|节）", "power", 800f, 0f, 1000f, "", "x");
                drag = bb.AddSlider("炮弹速度衰减", "airdrag", 0.2f, 0f, 1f, "", "x");
                high = bb.AddSlider("火炮与火控室相对高度", "high", 0f, 0f, 10f, "", "x");
                selectradius = bb.AddSlider("主动锁定半径", "selectradius", 1000f, 0f, 10000f, "", "x");
                selecttargetpackcount = bb.AddSlider("主动锁定目标零件数下限", "selecttargetpackcount", 50f, 0f, 2000f, "", "x");
                selecttargetsize = bb.AddSlider("主动锁定目标大小", "selecttargetsize", 20f, 0f, 300f, "", "x");
                selectvelocity = bb.AddSlider("主动锁定目标速度下限(m/s)", "selectvelocity", 200f, 0f, 1000f, "", "x");
                selecttime = bb.AddSlider("主动锁定扫描时间间隔", "selecttime", 1f, 0f, 10f, "", "x");
                openfirst = bb.AddToggle("默认开启无线电台", "openfirst", false);
                radioname = bb.AddText("无线电台名称", "radioname", "");
                iconcolor = bb.AddColourSlider("hud颜色", "color", Color.green, false);
                lockt = bb.AddKey("锁定目标", "lockt", UnityEngine.KeyCode.None);
                unluck = bb.AddKey("目标解锁", "locktE", UnityEngine.KeyCode.None);
                radiokey = bb.AddKey("开启/关闭无线电台", "raidokey", UnityEngine.KeyCode.None);
                initiativeselect = bb.AddToggle("主动锁定目标", "initiativeselect", false);
                hidesign = bb.AddToggle("隐藏预瞄圈", "hidesign", false);
                outtarget = bb.AddToggle("自动解锁已锁定目标", "unlock", false);
                selectteam = bb.AddToggle("只锁定敌人", "selectteam", false);
                autofire = bb.AddToggle("自动开火", "autofire", false);
                looktype = bb.AddMenu("looktype", 0, new List<string>() { "目力观测", "体视式测距仪", "合成像测距仪", "雷达","潜望镜"}, false);
                looklengthcoi = bb.AddMenu("looklengthcoi", 0, new List<string>() { "合像测距3m", "合像测距5m", "合像测距7m", "合像测距9m", "合像测距11m", "合像测距13m", "合像测距15m" }, false);
                looklevelcoi = bb.AddMenu("looklevelcoi", 0, new List<string>() { "合像测距(1850)", "合像测距II(1900)", "合像测距III(1910)", "合像测距IV(1920)", "合像测距V(1930)" }, false);
                looklengthster = bb.AddMenu("looklengthster", 0, new List<string>() { "体视测距3m", "体视测距5m", "体视测距7m", "体视测距9m", "体视测距11m", "体视测距13m", "体视测距15m" }, false);
                looklevelster = bb.AddMenu("looklevelster", 0, new List<string>() { "体视测距(1906)", "体视测距II(1910)", "体视测距III(1915)", "体视测距IV(1920)", "体视测距V(1930)" }, false);
                lookrider = bb.AddMenu("lookrider", 0, new List<string>() { "雷达I(1930)", "雷达II(1935)", "雷达III(1940)", "战后雷达(1950)" }, false);
                lookeye = bb.AddMenu("lookeye", 0, new List<string>() { "快速观测", "精准观测", "飞机观测" }, false);
                rideotype = bb.AddMenu("rideotype", 0, new List<string>() { "炮弹弹道计算", "鱼雷弹道计算"}, false);
                selecttimeE = 0.3f;
                hidden(true);
                hiddenE(0);
                navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
                rader.Toggled += hidden;
                firec.Toggled += hidden;
                radio.Toggled += hidden;
                flak.Toggled += hidden;
                looktype.ValueChanged += hiddenE;
                initiativeselect.Toggled += hidden;
                autofire.Toggled += hidden;
                if (bb.isSimulating)
                {
                    first = true;
                    firstE = true;
                    time = 0f;
                }
                
                locktype = 0;
                radioopen = false;
                send = false;
                iterativeCount = 0;
                sender = false;
                guid = bb.BuildingBlock.Guid;
                if (flakgo == null)
                {
                    flakgo = new GameObject();
                    flakgo.AddComponent<Rigidbody>().useGravity=false;
                    
                }

    
            }
            catch
            {

            }

        }
        protected void hiddenE(int count)
        {
            looklengthster.DisplayInMapper = rader.IsActive && (looktype.Value == 1);
            looklevelster.DisplayInMapper = looklengthster.DisplayInMapper;
            looklengthcoi.DisplayInMapper = rader.IsActive && (looktype.Value == 2);
            looklevelcoi.DisplayInMapper = looklengthcoi.DisplayInMapper;
            lookrider.DisplayInMapper = rader.IsActive && (looktype.Value == 3);
            lookeye.DisplayInMapper = rader.IsActive && (looktype.Value == 0);
        }
        public static List<float> lookcoi = new List<float> { 0.06f, 0.04f, 0.025f, 0.02f, 0.015f, 0.01f, 0.005f};
        public static List<float> lookcoiL = new List<float> { 1f, 0.98f, 0.96f, 0.94f, 0.92f };
        public static List<float> lookcoiLT = new List<float> { 1f, 0.9f, 0.8f, 0.7f, 0.6f};
        public static List<float> lookster = new List<float> { 0.06f, 0.04f, 0.025f, 0.02f, 0.015f, 0.01f, 0.005f };
        public static List<float> looksterL = new List<float> { 0.9f, 0.7f, 0.6f, 0.5f, 0.3f };
        public static List<float> looksterLT = new List<float> { 1f, 0.95f, 0.85f, 0.83f, 0.81f };
        public static List<float> lookriderS = new List<float> { 0.01f, 0.005f, 0.003f, 0.0015f };
        public static List<float> lookE = new List<float> { 0.1f, 0.07f, 0.05f, 0.1f, 0.002f, 0.001f, 0.1f, 0.1f, 0.3f };
        public static List<float> lookeyeE = new List<float> {0.4f,0.3f,0.25f};
        public static List<float> lookeyeT = new List<float> { 0.05f, 1f, 0.25f };
        public static List<float> looktime = new List<float> { 10f, 2f, 1f, 0.2f, 30f};
        public bool sender;
        public Vector3 flakdpos;
        public BlockBehaviour flaklock;
        public GameObject flakgo;
        public MSlider power;
        public MSlider drag;
        public MSlider high;
        public MSlider selectradius;
        public MSlider selecttime;
        public MSlider selectvelocity;
        public MSlider selecttargetsize;
        public MSlider selecttargetpackcount;
        public float flaktime;
        public Vector3 flakpos;
        public MToggle firec;
        public Navalhe navalhe;
        public MToggle radio;
        public MToggle selectteam;
        public MToggle outputE;
        public MToggle outtarget;
        public MToggle autofire;

        public MToggle flak;
        public MKey lockt;
        public MKey radiokey;
        public MKey unluck;
        public MToggle openfirst;
        public BlockBehaviour bb;
        public bool first;
        public bool firstE;
        public bool radioopen;
        public MMenu looktype;
        public MMenu looklengthcoi;
        public MMenu looklengthster;
        public MMenu looklevelcoi;
        public MMenu looklevelster;
        public MMenu lookrider;
        public MMenu rideotype;
        public MMenu lookeye;
        public MToggle rader;
        public MToggle initiativeselect;
        public MToggle hidesign;
        public MText flaktext;
        public MToggle sonar;
        public MText radioname;
        public int networkids;
        public List<view> views;
        public GameObject lockg;
        public Vector3 lockp;
        public int locktype;
        private int iconSize = 64;
        private int iconSizeE = 40;
        private Texture Ticon;
        private Texture TiconE;
        public MColourSlider iconcolor;
        public float range;
        public float rangelast;
        private float time;
        public float errorE;
        public float timeEX;
        private float timeE;
        public float canfire;
        public float cantfire;
        public Guid guid;
        public float rateOfVec;

        protected void hidden(bool unused)
        {
            firec.DisplayInMapper = rader.IsActive;
            lockt.DisplayInMapper = (firec.IsActive&&rader.IsActive);
            looktype.DisplayInMapper = rader.IsActive;
            //outputE.DisplayInMapper = sonar.IsActive;
            iconcolor.DisplayInMapper = firec.IsActive || radio.IsActive;
            radioname.DisplayInMapper = radio.IsActive;
            radiokey.DisplayInMapper = radio.IsActive;
            initiativeselect.DisplayInMapper = flak.IsActive;
            hidesign.DisplayInMapper = flak.IsActive;
            openfirst.DisplayInMapper = radio.IsActive;
            flak.DisplayInMapper = firec.IsActive;
            drag.DisplayInMapper = flak.IsActive;
            power.DisplayInMapper = flak.IsActive;
            high.DisplayInMapper= flak.IsActive;
            selectradius.DisplayInMapper = initiativeselect.IsActive;
            selecttargetpackcount.DisplayInMapper= initiativeselect.IsActive;
            selecttargetsize.DisplayInMapper= initiativeselect.IsActive;
            selectvelocity.DisplayInMapper= initiativeselect.IsActive;
            selecttime.DisplayInMapper= initiativeselect.IsActive;
            selectteam.DisplayInMapper = initiativeselect.IsActive;
            autofire.DisplayInMapper = flak.IsActive;
            outtarget.DisplayInMapper = initiativeselect.IsActive&& autofire.IsActive;
        }
        

        public int iterativeCount;
        public Vector2 formulaProjectile(float X, float Y, float V, float G)
        {
            if (G == 0f)
            {
                float num = Mathf.Atan(Y / X);
                float y = Y / Mathf.Sin(num) / V;
                return new Vector2(num, y);
            }
            float num2 = Mathf.Pow(V, 4f) - G * (G * X * X - 2f * Y * V * V);
            if (num2 < 0f)
            {
                return Vector2.zero;
            }
            float num3 = Mathf.Atan((-(V * V) + Mathf.Sqrt(num2)) / (G * X));
            float num4 = Mathf.Atan((-(V * V) - Mathf.Sqrt(num2)) / (G * X));
            if (num3 > num4)
            {
                num3 = num4;
            }
            float y2 = X / (V * Mathf.Cos(num3));
            return new Vector2(num3, y2);
        }
        public Vector3 formulaTarget(float VT, Vector3 PT, Vector3 DT, float TT)
        {
            return PT + DT * (VT * TT);
        }
        public Vector3 calculateLinearTrajectory(float gunVelocity, Vector3 gunPosition, float TargetVelocity, Vector3 TargetPosition, Vector3 TargetDirection)
        {
            Vector3 result = Vector3.zero;
            if (TargetVelocity != 0f)
            {
                Vector3 from = gunPosition - TargetPosition;
                float f = Vector3.Angle(from, TargetDirection) * 0.017453292f;
                float magnitude = from.magnitude;
                float num = 1f - Mathf.Pow(gunVelocity / TargetVelocity, 2f);
                float num2 = -(2f * magnitude * Mathf.Cos(f));
                float num3 = magnitude * magnitude;
                if (num2 * num2 - 4f * num * num3 < 0f)
                {
                    return Vector3.zero;
                }
                float num4 = (-num2 + Mathf.Sqrt(num2 * num2 - 4f * num * num3)) / (2f * num);
                float num5 = (-num2 - Mathf.Sqrt(num2 * num2 - 4f * num * num3)) / (2f * num);
                if (num4 < num5)
                {
                    num4 = num5;
                }
                result = TargetPosition + TargetDirection * num4;
            }
            else
            {
                result = TargetPosition;
            }
            return result;
        }
        public Vector3 calculateNoneLinearTrajectory(float gunVelocity, float AirDrag, Vector3 gunPosition, float TargetVelocity, Vector3 TargetPosition, Vector3 TargetDirection, Vector3 hitPoint, float G, float accuracy, float diff)
        {
            this.iterativeCount++;
            if (this.iterativeCount > 512)
            {
                this.iterativeCount = 0;
                return hitPoint;
            }
            if (hitPoint == Vector3.zero || gunVelocity < 1f)
            {
                return this.lockg.transform.position;
            }
            Quaternion rotation = Quaternion.FromToRotation(new Vector3(hitPoint.x, gunPosition.y, hitPoint.z) - gunPosition, Vector3.forward);
            Vector3 vector = rotation * (hitPoint - gunPosition);
            float magnitude = (hitPoint - gunPosition).magnitude;
            float num = gunVelocity * gunVelocity - 4f * AirDrag * magnitude;
            if (num < 0f)
            {
                return this.lockg.transform.position;
            }
            float v = (float)Math.Sqrt((double)num);
            float z = vector.z;
            float y = vector.y;
            Vector2 vector2 = this.formulaProjectile(z, y, v, G);
            if (vector2 == Vector2.zero)
            {
                this.iterativeCount = 0;
                return this.lockg.transform.position;
            }
            float y2 = vector2.y;
            Vector3 vector3 = this.formulaTarget(TargetVelocity, TargetPosition, TargetDirection, y2);
            float magnitude2 = (vector3 - hitPoint).magnitude;
            if (magnitude2 > diff)
            {
                this.iterativeCount = 0;
                return this.lockg.transform.position;
            }
            if (magnitude2 < accuracy)
            {
                rotation = Quaternion.Inverse(rotation);
                y = Mathf.Tan(vector2.x) * z;
                vector3 = rotation * new Vector3(0f, y, z) + gunPosition;
                this.iterativeCount = 0;
                return vector3;
            }
            return this.calculateNoneLinearTrajectory(gunVelocity, AirDrag, gunPosition, TargetVelocity, TargetPosition, TargetDirection, vector3, G, accuracy, magnitude2);
        }
    }

}
