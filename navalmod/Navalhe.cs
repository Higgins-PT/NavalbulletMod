using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Modding;
using Modding.Common;
using UnityEngine;
using UnityEngine.Networking.Types;
using static ServerMachine;

namespace Navalmod
{
	// Token: 0x02000019 RID: 25
    public struct planeinfo
    {
        public string planename;
        public int planenumber;
        public planeinfo(string name, int number)
        {
            planename = name;
            planenumber = number;
        }
    }
	public class Navalhe : BlockBehaviour
	{
        public bool isss;
		// Token: 0x060000A0 RID: 160
		public Navalhe()
		{
            SingleInstance<SpiderFucker>.Instance.ExExpandScale = 20000;

            this.window = new Rect(0f, 300f, 500f, 100f);
            this.window2 = new Rect(0f, 400f, 300f, 300f);
            this.type = 1;
			this.volume = 1f;
			this.seahigh = 20f;
            number = "";
            str = "";
            stage = "";
            retime = 3f;
            windowsid1 = ModUtility.GetWindowId();
            windowsid2 = ModUtility.GetWindowId();

            windowsid3 = ModUtility.GetWindowId();
        }

		// Token: 0x060000A1 RID: 161
		public virtual void SimilerUpdate()
		{
		}

		// Token: 0x060000A2 RID: 162
		public virtual void test()
		{
		}
        //--------------------
        private bool ExpandFloor;
        public float BoundarySize;
        public int windowsid1;
        public int windowsid2;
        private Rect windowRect = new Rect(15f, 100f, 280f, 50f);
        public int windowsid3;
        //--------------------
		// Token: 0x060000A3 RID: 163
		public void OnGUI()
		{
            if (openbuild)
            {
                window2 = GUI.Window(windowsid1, this.window2, new GUI.WindowFunction(this.buildwindow), "Naval bullet mod扩展");
            }
            if (!this.hide && !this.hide2)
			{
                window = GUI.Window(windowsid2, this.window, new GUI.WindowFunction(this.Windowfunction), "Naval bullet mod(leftalt+1隐藏) 装填功能不正常请关闭无限弹药");
                windowRect = GUILayout.Window(windowsid3, windowRect, new GUI.WindowFunction(this.Windows2),"环境操作界面", new GUILayoutOption[0]);
			}
            
		}
        public string number;
        public string stage;
        public string str;
        public string abc;

        public void buildwindow(int windowid)
        {
            try
            {
                GUI.Label(new Rect(20f, 40f, 150f, 40f), "替换为(材料序号/默认输入框)");
                this.number = GUI.TextArea(new Rect(20f, 70f, 50f, 20f), this.number);
                GUI.Label(new Rect(20f, 100f, 150f, 30f), "位数(材料序号2)");
                this.stage = GUI.TextArea(new Rect(20f, 130f, 50f, 20f), this.stage);
                GUI.Label(new Rect(20f, 160f, 70f, 25f), "分段符号");
                this.str = GUI.TextArea(new Rect(20f, 190f, 50f, 20f), this.str);


                if (GUI.Button(new Rect(150f, 100f, 70f, 20f), "确定(材料)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            blockBehaviour.gameObject.GetComponent<blockset>().menu.Value = Convert.ToInt32(number);
                            if (Convert.ToInt32(number)==0)
                            {
                                blockBehaviour.gameObject.GetComponent<blockset>().ZJG.Value = Convert.ToInt32(stage);
                            }
                            if (Convert.ToInt32(number) == 1)
                            {
                                blockBehaviour.gameObject.GetComponent<blockset>().JGG.Value = Convert.ToInt32(stage);
                            }
                            if (Convert.ToInt32(number) == 2)
                            {
                                blockBehaviour.gameObject.GetComponent<blockset>().ZX.Value = Convert.ToInt32(stage);
                            }
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }
                if (GUI.Button(new Rect(150f, 150f, 70f, 20f), "确定(厚度)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            blockBehaviour.gameObject.GetComponent<blockset>().thickness.Value = Convert.ToInt32(number);
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }
                if (GUI.Button(new Rect(220f, 150f, 70f, 20f), "确定(上限)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            blockBehaviour.gameObject.GetComponent<blockset>().jointmax.Value = Convert.ToInt32(number);
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }
                if (GUI.Button(new Rect(220f, 200f, 70f, 20f), "确定(连接端口名)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            blockBehaviour.gameObject.GetComponent<blockset>().jointname.Value = number;
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }
                if (GUI.Button(new Rect(220f, 100f, 70f, 20f), "确定(刚体)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            blockBehaviour.gameObject.GetComponent<blockset>().rigidityScale.IsActive = number=="1";
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }
                if (GUI.Button(new Rect(150f, 200f, 70f, 20f), "确定(弹速)"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            try
                            {
                                blockBehaviour.gameObject.GetComponent<NavalCannoBlockE>().shootspeed.Value = Convert.ToInt32(number);
                            }
                            catch
                            {

                            }
                            blockBehaviour.OnSave(new XDataHolder());


                        }
                    }
                    catch
                    {

                    }
                }

                if (GUI.Button(new Rect(20f, 210f, 50f, 20f), "确定"))
                {
                    try
                    {
                        foreach (ISelectable selectable in AdvancedBlockEditor.Instance.selectionController.Selection)
                        {
                            BlockBehaviour blockBehaviour = (BlockBehaviour)selectable;
                            List<MKey> keys = blockBehaviour.KeyList;
                            foreach (MKey key in keys)
                            {
                                if (key.useMessage)
                                {
                                    for (int i = 0; i < key.message.Length; i++)
                                    {

                                        string[] mess = key.message[i].Split(new char[] { str.ToCharArray()[0] });
                                        mess[Convert.ToInt32(stage) - 1] = number;
                                        string k = "";
                                        for (int n = 0; n < mess.Length; n++)
                                        {

                                            k += mess[n];
                                            if (n < mess.Length - 1)
                                            {
                                                k += str;
                                            }


                                        }

                                        key.message[i] = k;


                                    }
                                    key.ApplyValue();



                                }
                            }
                            blockBehaviour.OnSave(new XDataHolder());
                        }
                    }
                    catch
                    {

                    }

                }

                GUI.DragWindow();
            }
            catch
            {

            }
        }
        
        // Token: 0x060000A4 RID: 164
        public float retime;
        public bool ss;
        public bool watershow=true;
        public float seadrag=0.2f;
        public bool highspeed;
        public void Windowfunction(int windowid)
		{
            try
            {
                this.type = GUI.Toolbar(new Rect(20f, 20f, 300f, 20f), this.type, this.typeinfo);
                if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting && this.type == 0)
                {

                    this.window.height = 200f;
                    GUI.Label(new Rect(100, 40f, 50f, 25f), "海刷新速度");
                    this.seadrag = Convert.ToSingle(GUI.TextArea(new Rect(100f, 60f, 50f, 20f), this.seadrag.ToString()));
                    this.open = GUI.Toggle(new Rect(20f, 80f, 250f, 20f), this.open, "开启非模拟状态时的mod功能（联机模式）");
                    this.wood = GUI.Toggle(new Rect(20f, 100f, 250f, 20f), this.wood, "木船模式（不是木船海战别点）");
                    GUI.Label(new Rect(20f, 120f, 100f, 25f), "装填速度加成");
                    this.retime = Convert.ToSingle(GUI.TextArea(new Rect(20f, 140f, 50f, 20f), this.retime.ToString()));
                    ss = GUI.Toggle(new Rect(50f, 180f, 100f, 20f), this.ss, "潜艇战模式");
                    
                    highspeed = GUI.Toggle(new Rect(200f, 120f, 100f, 20f), this.highspeed, "弹射起步");
                    //ExpandFloor = GUI.Toggle(new Rect(20f, 120f, 250f, 20f), ExpandFloor, "设置空气墙大小");
                    //BoundarySizeE= Convert.ToSingle(GUI.TextArea(new Rect(20f, 140f, 100f, 20f), this.BoundarySizeE.ToString()));

                }

                if (this.type == 1)
                {
                    GUI.Label(new Rect(20f, 40f, 300f, 25f), "联机编号（如果显示错误请按f3查看联机编号并填写）");
                    this.networkids = (int)Convert.ToSingle(GUI.TextArea(new Rect(20f, 60f, 50f, 20f), this.networkids.ToString()));
                    this.window.height = 100f;
                    float num = 60f;
                    foreach (cannonlist cannonlist in this.cannonlist)
                    {

                        num += 20f;
                        if (cannonlist.time < 0f)
                        {
                            if (cannonlist.type == 1)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮已装备高爆弹");
                            }
                            if (cannonlist.type == 2)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮已装备穿甲弹");
                            }
                            if (cannonlist.type == 3)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮已装备半穿弹");
                            }
                            if (cannonlist.type == 4)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮已装备近炸引信弹");
                            }
                            if (cannonlist.type == 5)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮已装备延迟引信弹");
                            }
                        }
                        else
                        {
                            if (cannonlist.type == 1)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮正在装填高爆弹，距离装填完毕还有" + cannonlist.time.ToString() + "秒");
                            }
                            if (cannonlist.type == 2)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮正在装填穿甲弹，距离装填完毕还有" + cannonlist.time.ToString() + "秒");
                            }
                            if (cannonlist.type == 3)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮正在装填半穿弹，距离装填完毕还有" + cannonlist.time.ToString() + "秒");
                            }
                            if (cannonlist.type == 4)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮正在装填近炸引信弹，距离装填完毕还有" + cannonlist.time.ToString() + "秒");
                            }
                            if (cannonlist.type == 5)
                            {
                                GUI.Label(new Rect(20f, num, 500f, 25f), cannonlist.count.ToString() + "门" + cannonlist.scale.ToString() + "mm火炮正在装填延迟引信弹，距离装填完毕还有" + cannonlist.time.ToString() + "秒");
                            }
                        }
                        this.window.height = this.window.height + 20f;
                    }
                }
                if (this.type == 3)
                {
                    this.window.height = 80f;
                    float num2 = 20f;
                    foreach (blotlist blotlist in this.blotlists)
                    {


                        num2 += 20f;
                        if (blotlist.state == 1)
                        {
                            GUI.Label(new Rect(20f, num2, 500f, 25f), string.Concat(new string[]
                            {
                                blotlist.scale.ToString(),
                                "mm口径",
                                this.blottype[blotlist.type],
                                "爆炸，造成了",
                                blotlist.damage.ToString(),
                                "点崩落伤害"
                            }));
                        }
                        if (blotlist.state == 2)
                        {
                            GUI.Label(new Rect(20f, num2, 500f, 25f), string.Concat(new string[]
                            {
                                blotlist.scale.ToString(),
                                "mm口径",
                                this.blottype[blotlist.type],
                                "碎弹，炮弹穿深",
                                blotlist.penetrating.ToString(),
                                "mm,被击中的装甲等效厚度",
                                blotlist.penetratingE.ToString(),
                                "mm"
                            }));
                        }
                        if (blotlist.state == 3)
                        {
                            GUI.Label(new Rect(20f, num2, 500f, 25f), string.Concat(new string[]
                            {
                                blotlist.scale.ToString(),
                                "mm口径",
                                this.blottype[blotlist.type],
                                "击穿，炮弹穿深",
                                blotlist.penetrating.ToString(),
                                "mm,被击中的装甲等效厚度",
                                blotlist.penetratingE.ToString(),
                                "mm"
                            }));
                        }
                        if (blotlist.state == 4)
                        {
                            GUI.Label(new Rect(20f, num2, 500f, 25f), string.Concat(new string[]
                            {
                                blotlist.scale.ToString(),
                                "mm口径",
                                this.blottype[blotlist.type],
                                "跳弹，炮弹穿深",
                                blotlist.penetrating.ToString(),
                                "mm,被击中的装甲等效厚度",
                                blotlist.penetratingE.ToString(),
                                "mm"
                            }));
                        }
                        this.window.height = this.window.height + 20f;

                    }
                }
                if (this.type == 2)
                {
                    this.window.height = 200f;
                    GUI.Label(new Rect(20f, 40f, 50f, 25f), "音量");
                    this.volume = Convert.ToSingle(GUI.TextArea(new Rect(20f, 60f, 50f, 20f), this.volume.ToString()));
                    GUI.Label(new Rect(80f, 40f, 100f, 25f), "开火音效开关");
                    this.boomvoice = GUI.Toggle(new Rect(80f, 60f, 50f, 20f), this.boomvoice, "");
                    GUI.Label(new Rect(40f, 80f, 150f, 25f), "开启建造辅助面板");
                    this.openbuild = GUI.Toggle(new Rect(20f, 80f, 50f, 20f), this.openbuild, "");
                    GUI.Label(new Rect(80f, 110f, 150f, 25f), "鱼雷深度(m)");
                    watershow = GUI.Toggle(new Rect(100f, 160f, 100f, 20f), this.watershow, "海面显示");
                    if(GUI.Button(new Rect(200f, 160f, 100f, 20f), "刷新"))
                    {
                        OrderedRPCQueue orderedRPCQueue = FindObjectOfType<OrderedRPCQueue>();
                        orderedRPCQueue.ToggleLock(false);
                    }
                    this.high = Convert.ToSingle(GUI.TextArea(new Rect(20f, 110f, 50f, 20f), this.high.ToString()));
                    if (StatMaster.isHosting)
                    {
                        if (GUI.Button(new Rect(150f, 80f, 70f, 20f), "保存飞机"))
                        {
                            saveplane();

                        }
                    }
                    
                }
                GUI.DragWindow();
            }
            catch
            {

            }
		}
        public bool offphy;
        public bool openbuild;
        public float time2;
        public float high;
        public float time3;
        public GameObject water;
        public GameObject waterfog;
        private Material fogMaterial;
        public Shader fogShader;
        public Vector3 sspos;
        public List<planeinfo> planeinfos= new List<planeinfo>();
        public bool useSkyBox = false;
        // Token: 0x060000A5 RID: 165
        private void ToggleIndent(string text, float w, ref bool flag, Action func)
        {
            flag = GUILayout.Toggle(flag, text, new GUILayoutOption[0]);
            bool flag2 = flag;
            if (flag2)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("", new GUILayoutOption[]
                {
                    GUILayout.Width(w)
                });
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                func();
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
        int skyboxSelector;
        bool skychanged;
        bool isFirstFrame = true;
        public GameObject skybox;
        public Material[] matArray = new Material[2];
        public void RunTime()
        {
            if (!isFirstFrame)
            {
                if (StatMaster.isMainMenu)
                {
                    isFirstFrame = true;
                    Destroy(skybox);
                }
            }
            else
            {
                if (!StatMaster.isMainMenu)
                {
                    isFirstFrame = false;
                    //orgskybox = (GameObject.Find("MULTIPLAYER LEVEL").transform.FindChild("Environments").FindChild("Barren").FindChild("AviamisAtmosphere").FindChild("STAR SPHERE").gameObject);

                    skybox = new GameObject("WWII Sky Box");

                    matArray[0] = new Material(Shader.Find("Instanced/Block Shader (GPUI off)"));
                    matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("sun_1").Texture);
                    matArray[0].SetTexture("_MainTex", Texture2D.blackTexture);
                    matArray[0].SetColor("_Color", Color.black);
                    matArray[0].SetColor("_EmissCol", Color.white);
                    matArray[1] = new Material(Shader.Find("Particles/Additive"));
                    matArray[1].mainTexture = ModResource.GetTexture("sun_1").Texture;
                    matArray[1].SetColor("_TintColor", new Color(0, 0, 0, 1f));
                    skybox.AddComponent<MeshFilter>().mesh = ModResource.GetMesh("SkyBall").Mesh;
                    skybox.AddComponent<MeshRenderer>().materials = matArray;
                    skybox.GetComponent<MeshRenderer>().sortingOrder = -32768;
                    skybox.transform.localScale = new Vector3(100000, 100000, 100000);
                    
                    mySmoothFollow MSF = skybox.AddComponent<mySmoothFollow>();
                    MSF.target = Camera.main.transform;


                    //skybox.GetComponent<MeshRenderer>().material.SetColor("_Emission", Color.white);
                    //skybox.GetComponent<MeshRenderer>().material.renderQueue = 4000;

                    skybox.SetActive(false);
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;



                }

            }
            if (!StatMaster.isMainMenu && !isFirstFrame)
            {
                if (useSkyBox)
                {

                    skybox.SetActive(true);

                }
                else
                {
                    skybox.SetActive(false);
                }
                if (skychanged)
                {
                    switch (skyboxSelector)
                    {
                        case 0:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("sun_1").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("sun_1").Texture;
                            break;
                        case 1:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("sun_2").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("sun_2").Texture;
                            break;
                        case 2:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("sun_3").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("sun_3").Texture;
                            break;
                        case 3:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("sun_4").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("sun_4").Texture;
                            break;
                        case 4:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("night_1").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("night_1").Texture;
                            break;
                        case 5:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("night_2").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("night_2").Texture;
                            break;
                        case 6:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("night_3").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("night_3").Texture;
                            break;
                        case 7:
                            matArray[0].SetTexture("_EmissMap", ModResource.GetTexture("night_4").Texture);
                            matArray[1].mainTexture = ModResource.GetTexture("night_4").Texture;
                            break;
                        default:
                            break;
                    }
                    skybox.GetComponent<MeshRenderer>().materials = matArray;
                    skychanged = false;
                }
            }
        }
        public void Windows2(int windowid)
        {

            GUILayout.BeginVertical();
            ToggleIndent("Use SkyBox", 20, ref useSkyBox, delegate
            {
                GUILayout.BeginVertical("box", new GUILayoutOption[0]);
                if (GUILayout.Button("sun_1", new GUILayoutOption[0]))
                {
                    skyboxSelector = 0;
                    skychanged = true;
                }
                if (GUILayout.Button("sun_2", new GUILayoutOption[0]))
                {
                    skyboxSelector = 1;
                    skychanged = true;
                }
                if (GUILayout.Button("sun_3", new GUILayoutOption[0]))
                {
                    skyboxSelector = 2;
                    skychanged = true;
                }
                if (GUILayout.Button("sun_4", new GUILayoutOption[0]))
                {
                    skyboxSelector = 3;
                    skychanged = true;
                }
                if (GUILayout.Button("night_1", new GUILayoutOption[0]))
                {
                    skyboxSelector = 4;
                    skychanged = true;
                }
                if (GUILayout.Button("night_2", new GUILayoutOption[0]))
                {
                    skyboxSelector = 5;
                    skychanged = true;
                }
                if (GUILayout.Button("night_3", new GUILayoutOption[0]))
                {
                    skyboxSelector = 6;
                    skychanged = true;
                }
                if (GUILayout.Button("night_4", new GUILayoutOption[0]))
                {
                    skyboxSelector = 7;
                    skychanged = true;
                }
                GUILayout.EndVertical();
            });

            if (GUILayout.Button("Apply", new GUILayoutOption[0]))

            {
                
                Camera.main.farClipPlane = 300000;
                SingleInstance<SpiderFucker>.Instance.Apply();
            }
            SingleInstance<SpiderFucker>.Instance.FloorDeactiveSwitch = GUILayout.Toggle(SingleInstance<SpiderFucker>.Instance.FloorDeactiveSwitch, "FloorDeactive", new GUILayoutOption[0]);
            //SingleInstance<SpiderFucker>.Instance.ExpandFloorSwitch = GUILayout.Toggle(SingleInstance<SpiderFucker>.Instance.ExpandFloorSwitch, "空气墙扩大10倍", new GUILayoutOption[0]);
            if (SingleInstance<SpiderFucker>.Instance.ExpandFloorSwitch && SingleInstance<SpiderFucker>.Instance.ExExpandFloorSwitch)
            {
                SingleInstance<SpiderFucker>.Instance.ExExpandFloorSwitch = false;
            }
            SingleInstance<SpiderFucker>.Instance.ExExpandFloorSwitch = GUILayout.Toggle(SingleInstance<SpiderFucker>.Instance.ExExpandFloorSwitch, "空气墙自定义长度(m)", new GUILayoutOption[0]);
            if (SingleInstance<SpiderFucker>.Instance.ExpandFloorSwitch && SingleInstance<SpiderFucker>.Instance.ExExpandFloorSwitch)
            {
                SingleInstance<SpiderFucker>.Instance.ExpandFloorSwitch = false;
            }
            SingleInstance<SpiderFucker>.Instance.ExExpandScale = Convert.ToSingle(GUILayout.TextArea(SingleInstance<SpiderFucker>.Instance.ExExpandScale.ToString(), new GUILayoutOption[0]));
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUI.DragWindow();
        }
        public void savesend()
        {
            Message saveplane = Messages.saveplane.CreateMessage(new object[]
    {
                    preplane.Count,
                    preplane.Count.ToString()
    });
            ModNetworking.SendToAll(saveplane);
        }
        public void Update()
		{
            try
            {
                RunTime();
                try
                {
                    skybox.transform.position = Camera.main.transform.position;
                }
                catch
                {

                }
                time2 += Time.deltaTime;
                time3 += Time.deltaTime;

                if (time3 > 5f && (StatMaster.InGlobalPlayMode || StatMaster.InLocalPlayMode))
                {
                    time3 = 0f;
                    SendExplosionPositionToAll2(retime);
                }
                if (StatMaster.InGlobalPlayMode || StatMaster.InLocalPlayMode)
                {
                    if (ss)//潜艇视角
                    {

                        if (water == null)
                        {
                            water = GameObject.CreatePrimitive(PrimitiveType.Plane);
                            water.transform.localScale = Vector3.one * 6000f;
                            UnityEngine.Object.Destroy(water.GetComponent<Rigidbody>());
                            UnityEngine.Object.Destroy(water.GetComponent<Collider>());
                            Material material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
                            Color colors = new Color(1f, 1f, 1f, 255f);
                            material.SetColor("_Color", colors);
                            water.GetComponent<MeshRenderer>().sharedMaterial = material;

                        }
                        if (waterfog == null)
                        {
                            waterfog = GameObject.CreatePrimitive(PrimitiveType.Plane);
                            waterfog.transform.localScale = Vector3.one * 1000f;
                            UnityEngine.Object.Destroy(waterfog.GetComponent<Rigidbody>());
                            UnityEngine.Object.Destroy(waterfog.GetComponent<Collider>());
                            Material material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
                            Color colors = new Color(1f, 1f, 1f, 255f);
                            material.SetColor("_Color", colors);
                            waterfog.GetComponent<MeshRenderer>().sharedMaterial = material;
                        }
                        if (isss)
                        {
                            if (sspos.y < seahigh - 0.2f)
                            {
                                if ((Camera.main.transform.position.y <= seahigh) && ((sspos - Camera.main.transform.position).magnitude) <= 51f)
                                {
                                    water.SetActive(true);
                                    waterfog.SetActive(true);
                                    if ((Camera.main.transform.forward.normalized - (sspos - Camera.main.transform.position).normalized).magnitude > 1.41f)
                                    {
                                        waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * Mathf.Max((50f - (sspos - Camera.main.transform.position).magnitude), 2f);
                                    }
                                    else
                                    {
                                        waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * Mathf.Max((50f + (sspos - Camera.main.transform.position).magnitude), 2f);
                                    }
                                    waterfog.transform.up = -Camera.main.transform.forward;
                                    water.transform.up = new Vector3(0, -1, 0);
                                }
                                else
                                {
                                    water.SetActive(true);
                                    waterfog.SetActive(true);
                                    waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * 2f;
                                    waterfog.transform.up = -Camera.main.transform.forward;
                                    water.transform.up = new Vector3(0, -1, 0);
                                }
                            }
                            else
                            {
                                if ((Camera.main.transform.position.y <= seahigh) && ((sspos - Camera.main.transform.position).magnitude) <= 51f)
                                {
                                    water.SetActive(true);
                                    waterfog.SetActive(true);
                                    if ((Camera.main.transform.forward.normalized - (sspos - Camera.main.transform.position).normalized).magnitude > 1.41f)
                                    {
                                        waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * Mathf.Max((50f - (sspos - Camera.main.transform.position).magnitude), 2f);
                                    }
                                    else
                                    {
                                        waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * Mathf.Max((50f + (sspos - Camera.main.transform.position).magnitude), 2f);
                                    }
                                    waterfog.transform.up = -Camera.main.transform.forward;
                                    water.transform.up = new Vector3(0, -1, 0);
                                }
                                else
                                {
                                    water.SetActive(true);
                                    water.transform.up = new Vector3(0, 1, 0);
                                    waterfog.SetActive(false);
                                }


                            }


                            water.transform.position = new Vector3(Camera.main.transform.position.x, seahigh - 1f, Camera.main.transform.position.z);
                        }
                        else
                        {
                            if (Camera.main.transform.position.y <= seahigh - 0.5f)
                            {
                                water.SetActive(true);
                                waterfog.SetActive(true);
                                waterfog.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * 2f;
                                waterfog.transform.up = -Camera.main.transform.forward;
                                water.transform.up = new Vector3(0, -1, 0);
                            }
                            else
                            {
                                water.SetActive(true);
                                water.transform.up = new Vector3(0, 1, 0);
                                waterfog.SetActive(false);
                            }
                            water.transform.position = new Vector3(Camera.main.transform.position.x, seahigh - 1f, Camera.main.transform.position.z);
                        }
                    }
                    else
                    {
                        try
                        {
                            water.SetActive(false);
                            waterfog.SetActive(false);
                        }
                        catch
                        {

                        }
                    }
                }


                else
                {
                    try
                    {
                        water.SetActive(false);
                        waterfog.SetActive(false);
                    }
                    catch
                    {

                    }
                    isss = false;
                }
                if (this.time >= 0f)
                {
                    this.time += Time.deltaTime;
                    if (this.time > 3f)
                    {

                        this.time = -1f;
                        this.blotlists = new List<blotlist>();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    this.hide = !this.hide;
                }
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    this.hide2 = !this.hide2;
                }

                if (!StatMaster.InGlobalPlayMode && !StatMaster.InLocalPlayMode)
                {
                    this.add = true;
                }
                if (Input.anyKeyDown)
                {
                    this.add = true;
                }

                if ((StatMaster.InGlobalPlayMode || StatMaster.InLocalPlayMode) && time2 > 0.5f)
                {

                    StatMaster.GodTools.InfiniteAmmoMode = true;

                    time2 = 0f;
                    base.StartCoroutine(this.Delay());
                    this.add = false;
                    this.cannonlist = new List<cannonlist>();

                    foreach (BlockBehaviour blockBehaviour in ReferenceMaster.GetSimulationBlocks((uint)this.networkids))
                    {

                        if (blockBehaviour.BlockID == 11)
                        {
                            this.addcannonlist = false;
                            NavalCannoBlockE component = blockBehaviour.GetComponent<NavalCannoBlockE>();
                            component.networkid = this.networkids;
                            foreach (cannonlist cannonlist in this.cannonlist)
                            {
                                if (cannonlist.type == component.type && cannonlist.scale == component.scale.Value && (float)Math.Round(component.relodingtime * retime, 0) == cannonlist.time)
                                {
                                    cannonlist.count++;
                                    this.addcannonlist = true;
                                }
                            }
                            if (!this.addcannonlist)
                            {
                                cannonlist cannonlist2 = new cannonlist();
                                cannonlist2.type = component.type;
                                cannonlist2.time = (float)Math.Round(component.relodingtime * retime, 0);

                                cannonlist2.scale = component.scale.Value;
                                cannonlist2.count = 1;
                                this.cannonlist.Add(cannonlist2);
                            }
                        }
                    }
                }
                if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.S))
                {
                    //saveplane();
                    


                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
                {
                    /*
                    RaycastHit raycastHit;
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Camera.main.farClipPlane);
                    putplane(preplane.Count-1, raycastHit.point);*/
                }
            }
            catch
            {

            }
		}
        
        public List<ServerMachine> preplane= new List<ServerMachine>();
        public IEnumerator addItinerary(Vector3 attPos, Vector3 attDir, ServerMachine serverMachine)
        {
            attCD = true;
            yield return new WaitForSeconds(0.5f);
            attCD = false;


            foreach (BlockBehaviour blockBehaviour in serverMachine.SimulationBlocks)
            {
                if (blockBehaviour.gameObject.GetComponent<radio>() != null)
                {
                    int atttype;
                    radio radio;
                    radio = blockBehaviour.gameObject.GetComponent<radio>();
                    atttype = radio.atttypeplane.Value;
                    try
                    {
                        foreach (Collider collider in Physics.OverlapSphere(attPos, 30f))
                        {
                            try
                            {
                                if (collider.attachedRigidbody.gameObject.GetComponent<blockset>() != null)
                                {
                                    radio.followMod = true;
                                    radio.originPos = collider.attachedRigidbody.gameObject.transform.position;
                                    radio.lockbb = collider.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>();
                                    break;
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
                    switch (radio.atttypeplane.Value)
                    {
                        case 0:
                            Vector3 pos = attPos + attDir.normalized * 2000f;
                            pos.y = radio.atthigh.Value;
                            radio.itinerary.addvalue(pos, -attDir);
                            radio.itinerary.addvalue(attPos+ attDir.normalized * 300f + new Vector3(0f,50f,0f) * radio.attrange.Value, -attDir);
                            radio.itinerary.addvalue(attPos - attDir.normalized * 2000f + new Vector3(0f, 3000f, 0f), -attDir);
                            radio.attNumber = 1;
                            break;
                        case 1:
                            Vector3 pos1;

                            pos1 = attPos + attDir.normalized * radio.atthigh.Value/ 2f * radio.attrange.Value;
                            pos1.y = radio.atthigh.Value;
                            radio.itinerary.addvalue(pos1, -attDir);
                            radio.itinerary.addvalue(attPos - attDir.normalized * 3000f + new Vector3(0f, 3000f, 0f), -attDir);
                            radio.attNumber = 1;
                            break;
                        case 2:

                            radio.itinerary.addvalue(attPos, -attDir);
                            break;
                            
                        case 3:
                            Vector3 pos2 = attPos + attDir.normalized * 3000f;
                            pos.y = radio.atthigh.Value;
                            radio.itinerary.addvalue(pos2, -attDir);
                            pos2 = attPos + attDir.normalized * 1500f * radio.attrange.Value;
                            pos2.y = seahigh+200f;

                            radio.itinerary.addvalue(pos2, -attDir);
                            pos2 = attPos + attDir.normalized * 1000f * radio.attrange.Value;
                            pos2.y = seahigh + 50f;
                            radio.itinerary.addvalue(pos2, -attDir);
                            radio.itinerary.addvalue(attPos - attDir.normalized * 2000f + new Vector3(0f, 3000f, 0f), -attDir);
                            radio.attNumber = 1;
                            break;

                        case 4:
                            Vector3 pos3 = attPos + attDir.normalized * 3000f;
                            pos.y = radio.atthigh.Value;
                            radio.itinerary.addvalue(pos3, -attDir);
                            pos3 = attPos + attDir.normalized * 1500f * radio.attrange.Value;
                            pos3.y = seahigh + 200f;

                            radio.itinerary.addvalue(pos3, -attDir);
                            pos3 = attPos + attDir.normalized * 300f * radio.attrange.Value;
                            pos3.y = seahigh + 50f;
                            radio.itinerary.addvalue(pos3, -attDir);
                            radio.itinerary.addvalue(attPos - attDir.normalized * 2000f + new Vector3(0f, 3000f, 0f), -attDir);
                            radio.attNumber = 1;
                            break;

                    }
                    //radio.itinerary.addvalue(attPos + new Vector3(0f, 200f, 0f), -attDir);
                    
                    
                    break;
                }
            }
        }
        public bool attCD;
        public Networkillusory networkillusory = new Networkillusory();
        public void AddNetworkBlock(ServerMachine ServerMachine)
        {
            try
            {
                foreach (BlockBehaviour blockBehaviour in ServerMachine.BuildingBlocks)
                {
                    PrefabMaster.AddNetworkBlock(blockBehaviour.gameObject);
                }
            }
            catch
            {

            }
        }
        public void loadplane(Vector3 attPos, Vector3 attDir, int planeNumber)
        {
            if (!attCD)
            {
                Vector3 r = Quaternion.LookRotation(-attDir).eulerAngles;
                r.y -= 90f;
                r.z = 0f;
                r.x = 0f;
                ServerMachine serverMachine = putplane(planeNumber, attPos + attDir.normalized * 4000f, Quaternion.Euler(r));
                StartCoroutine(addItinerary(attPos, attDir, serverMachine));

                UnityEngine.GameObject.Destroy(serverMachine.gameObject, 60f);
            }
        }

        public ServerMachine putplane(int value,Vector3 pos,Quaternion rotation)
        {
            ushort num = (ushort)UnityEngine.Random.Range(10, 20000);
            ServerMachine ServerMachineE = cloneplane(preplane[value],pos, num);
            ServerMachineE.SetRotation(rotation, false);
            ServerMachineE.gameObject.SetActive(true);
            AddNetworkBlock(ServerMachineE);
            StartCoroutine(FindObjectOfType<NetworkAddPiece>().StartMachines(new List<Machine> {ServerMachineE as Machine}));

            
            return ServerMachineE;


            }
        public ServerMachine cloneplane(ServerMachine ServerMachine,Vector3 pos,int num)
        {
            PlayerData playerData = new PlayerData((ushort)(num));
            playerData.name = ScoreboardTester.GetRandomName();
            playerData.isLocalPlayer = false;
            playerData.isSpectator = false;
            NetworkAuxAddPiece networkAuxAddPiece=FindObjectOfType<NetworkAuxAddPiece>();
            byte[] configData = networkAuxAddPiece.GetPlayerConfig();
            configData[0] = 0;
            //networkAuxAddPiece.InitServerPlayer(playerData, configData);

            for (int i = 0; i < Playerlist.Players.Count; i++)
            {
                PlayerData playerData2 = Playerlist.Players[i];
                if (!playerData2.isSpectator)
                {
                    ServerMachine machine = playerData2.machine;
                    machine.fullUpdate.Add(playerData.networkId);
                }
            }
            
            Playerlist.AddPlayer(playerData);
            FindObjectOfType<LevelEditor>().PlayerJoin((ushort)num);
            ServerMachine ServerMachineE = FindObjectOfType<NetworkAuxAddPiece>().CreateClient(playerData, pos, new Quaternion(), false);
            try
            {
                playerData.isLocalPlayer = true;
                //ServerMachineE.GetComponent<PlayerBuildZone>().ResetBounds();
                FindObjectOfType<NetworkAddPiece>().SetupZone(ServerMachineE.PlayerID);
                FindObjectOfType<NetworkAuxAddPiece>().UpdateBuildZoneTransform(pos,new Quaternion());
                ServerMachineE.gameObject.GetComponent<PlayerBuildZone>().Init(playerData);
                playerData.isLocalPlayer = false;

            }
            catch
            {

            }

            try
            {

                ServerMachineE.SetPlayer(playerData);
                ServerMachineE.enabled = true;
                List<ServerMachine.BlockData> list = new List<ServerMachine.BlockData>();
                List<BlockBehaviour> buildingBlocks = ServerMachine.BuildingBlocks;
                
                /*for (int n = 0; n < buildingBlocks.Count; n++)
                {
                    list.Add(new ServerMachine.BlockData(BlockInfo.FromBlockBehaviour(buildingBlocks[n])));
                }*/
                ushort i = 0;
                try
                {
                    ServerMachineE.Clone(ServerMachine);
                    //AddNetworkBlock(ServerMachineE);
                   // networkAuxAddPiece.LoadMachineInfo(ServerMachineE.CreateMachineInfo());

                }
                catch
                {

                }
                try
                {
                    //ModConsole.Log("---------------------");
                    //networkillusory.RefreshPlay();
                }
                catch
                {

                }
                /*
                while ((int)i < list.Count)
                {
                    try
                    {

                        
                        ServerMachine.BlockData block = list[(int)i];
                        BlockBehaviour newBlock;
                        bool aaa = ServerMachineE.AddBlock(block.info, out newBlock);
                        if (aaa)
                        {
                            try
                            {
                                newBlock.OnAddRemote();
                                if (newBlock.Prefab.hasBVC)
                                {
                                    newBlock.VisualController.PlaceFromBlockInfo(block.info);
                                }
                                //newBlock.transform.FindChild("DirectionArrow").gameObject.SetActive(false);

                            }
                            catch
                            {

                            }


                        }
                        


                    }
                    catch
                    {

                    }
                    i += 1;
                }
                */
                //ServerMachineE.nodeController.RefreshBlocks(true, true);
                //ServerMachineE.SetPosition(pos, false);
                //playerData.isLocalPlayer = false;
                //ServerMachineE.isLoadingInfo = false;
                //ServerMachineE.PostLoad(false);
                
            }
            catch
            {
            }
            return ServerMachineE;
        }
        public void saveplane()
        {
            RaycastHit raycastHit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Camera.main.farClipPlane);
            
            foreach (ServerMachine ServerMachine in FindObjectsOfType<ServerMachine>())
            {
                if (ServerMachine.PlayerID == 0)
                {


                    ServerMachine ServerMachineE = cloneplane(ServerMachine, new Vector3(0f,1000f,0f), preplane.Count + 10);

                    preplane.Add(ServerMachineE);
                    planeinfos.Add(new planeinfo(preplane.Count.ToString(), preplane.Count));
                    
                    StartCoroutine(FindObjectOfType<NetworkAddPiece>().StartMachines(new List<Machine> { ServerMachineE as Machine }));
                    StartCoroutine(closegb(ServerMachineE));
                    savesend();
                    break;
                }

            }

                




        }
        public IEnumerator closegb(ServerMachine ServerMachineE)
        {
            yield return new WaitForSeconds(1f);
            ServerMachineE.gameObject.SetActive(false);
            
        }

        public GameObject light;
        // Token: 0x060000A6 RID: 166
        private new void Start()
		{

			this.typeinfo = new string[]
			{
				"房主设置",
				"炮弹",//---------------------
				"玩家设置",
				"战斗信息"
			};
			this.add = true;
			this.open = false;
			this.wood = false;
			this.networkids = (int)PlayerData.localPlayer.networkId;
			this.boomvoice = true;
            waterE.Instance.wood = base.gameObject.GetComponent<Navalhe>();
            waterE2.Instance.wood = base.gameObject.GetComponent<Navalhe>();
            networkillusory = base.gameObject.AddComponent<Networkillusory>();
            /*
            light = new GameObject();
            DontDestroyOnLoad(light);
            light.name = "light";
            light.transform.rotation = Quaternion.Euler(83f, 0f, 0f);
            Light li = light.AddComponent<Light>();
            li.type = LightType.Directional;
            li.color = new Color(210f,210f,210f);
            li.intensity =0.001f;
            li.shadows = LightShadows.Soft;
            li.shadowResolution = LightShadowResolution.Medium;
            */

        }

		// Token: 0x060000A7 RID: 167

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A8 RID: 168
		public ushort NetworkId { get; }

		// Token: 0x060000A9 RID: 169
		private IEnumerator Delay()
		{
			yield return new WaitForFixedUpdate();
			yield break;
		}

        // Token: 0x060001E4 RID: 484
        private void SendExplosionPositionToAll(blotlist item)
        {
            if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting)
            {
                ModNetworking.SendToAll(Messages.bultinfo.CreateMessage(new object[]
                {
                    (float)item.type,
                    (float)item.state,
                    (float)item.scale,
                    (float)item.penetratingE,
                    (float)item.penetrating,
                    (float)item.networkids,
                    (float)item.damage
                }));
            }
        }
        private void SendExplosionPositionToAll2(float time)
        {
            if (StatMaster.isMP && !StatMaster.isLocalSim && StatMaster.isHosting)
            {
                ModNetworking.SendToAll(Messages.fireinfo.CreateMessage(new object[]
                {
                    (float)time,
                    ss
                }));
            }
        }

        

        public void addblot(blotlist item)
		{
          
           
            this.time = 0f;
            
			
			this.blotlists.Add(item);
            if (blotlists.Count > 20)
            {
                blotlists.Remove(blotlists[1]);
            }
            if (!StatMaster.isClient)
            {
                SendExplosionPositionToAll(item);
            }
       
        }

		// Token: 0x0400009C RID: 156
		private Rect window;
        private Rect window2;
        public float BoundarySizeE;
        // Token: 0x0400009D RID: 157
        public float seahigh;

		// Token: 0x0400009E RID: 158
		private bool hide;

		// Token: 0x0400009F RID: 159
		public bool wood;

		// Token: 0x040000A0 RID: 160
		private int type;

		// Token: 0x040000A1 RID: 161
		private string[] typeinfo;

		// Token: 0x040000A2 RID: 162
		private bool hide2;

		// Token: 0x040000A3 RID: 163
		private bool add;

		// Token: 0x040000A4 RID: 164
		private List<cannonlist> cannonlist;

		// Token: 0x040000A5 RID: 165
		private bool addcannonlist;

		// Token: 0x040000A6 RID: 166
		public float volume;

		// Token: 0x040000A7 RID: 167
		public Player player;

		// Token: 0x040000A8 RID: 168
		private int networkids;

		// Token: 0x040000A9 RID: 169
		public bool boomvoice;

		// Token: 0x040000AA RID: 170
		public bool open;

		// Token: 0x04000114 RID: 276
		public List<blotlist> blotlists;

		// Token: 0x04000234 RID: 564
		private float time;

		// Token: 0x0400027F RID: 639
		private Dictionary<int, string> blottype = new Dictionary<int, string>
		{
			{
				1,
				"高爆弹"
			},
			{
				2,
				"穿甲弹"
			},
			{
				3,
				"半穿弹"
			},
			{
				4,
                "近炸引信弹"
            },
			{
				5,
				"延迟引信弹"
			}
		};
	}
}
