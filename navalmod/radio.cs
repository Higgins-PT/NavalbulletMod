using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
using Selectors;
using UnityEngine.SocialPlatforms;

namespace Navalmod
{
    public struct itinerary
    {
        private List<Vector3> eulars;
        private List<Vector3> poss;
        private Vector3 oriposs;
        private Vector3 orieulars;
        public itinerary(bool init)
        {
            eulars = new List<Vector3>();
            poss = new List<Vector3>();
            oriposs = new Vector3();
            orieulars = new Vector3();
        }
        public int getCount()
        {
            return eulars.Count;
        }
        public void repos(Vector3 addpos)
        {
            poss[0] = oriposs + addpos;
        }
        public void addvalue(Vector3 pos,Vector3 eular)
        {
            if (poss.Count == 0)
            {
                oriposs = pos;
                orieulars =eular;
            }
            eulars.Add(eular);
            poss.Add(pos);

        }
        public void putout(out Vector3 pos,out Vector3 eular)
        {
            pos = poss[0];
            eular = eulars[0];
        }
        public void putoutde(out Vector3 pos, out Vector3 eular)
        {
            pos = poss[0];
            eular = eulars[0];
            poss.Remove(poss[0]);
            eulars.Remove(eulars[0]);
        }
        public void removefirst()
        {
            poss.Remove(poss[0]);
            eulars.Remove(eulars[0]);
            if (poss.Count >= 1)
            {
                oriposs = poss[0];
                orieulars = eulars[0];
            }
        }
        public bool haveintinerary()
        {
            if (eulars.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
    public class radio : BlockScript
    {
        public PID PID_Pitch;
        public PID PID_Yaw;
        public PID PID_Roll;
        public MToggle radiotype;
        public MMenu clienttype;
        public MMenu atttypeplane;
        public MKey attkey;
        public MKey plane_Pitch_up;
        public MKey plane_Pitch_down;
        public MKey plane_Yaw_right;
        public MKey plane_Yaw_left;
        public MKey plane_Roll_clockwise;
        public MKey plane_Roll_anticlockwise;
        public MKey plane_fly;
        public MKey[] keys;
        public MSlider atthigh;
        public MSlider PID_P;
        public MSlider PID_I;
        public MSlider PID_D;
        public MSlider PID_S;
        public bool debug=true;
        public float time;
        private bool visualisingHeight;
        public GameObject dir;
        public GameObject dir2;
        public float timeflue;
        public itinerary itinerary;
        public float power_Pitch;
        public float power_Yaw;
        public float power_Roll;
        public float put_Pitch;
        public float put_Yaw;
        public float put_Roll;
        public int lastsimP;
        public int lastsimY;
        public int lastsimR;
        public MKey radioopen;
        public bool radiostate;
        public float directionOfPlane;
        public int numberOfPlane;
        public bool flying;
        public BlockBehaviour lockbb;
        public Vector3 originPos;
        public bool followMod;
        public int attNumber;
        public bool attacking;
        public MKey changekey;
        public MSlider attrange;
        public void Awake()
        {
            attrange = base.AddSlider("攻击距离修正", "attrange", 1f,0.1f, 2f);
            changekey = base.AddKey("切换装备按键", "changekey", KeyCode.Q);
            radioopen = base.AddKey("无线电呼叫","radioopen",KeyCode.B);
            radiotype = base.AddToggle("信号接受模式","client",false);
            clienttype = base.AddMenu("clienttype",0,new List<string> {"舰载机模式" ,"潜艇模式(无功能)"},false);
            atttypeplane = base.AddMenu("atttypeplane", 0, new List<string> { "俯冲轰炸", "地毯式轰炸", "神风特攻","鱼雷机轰炸","跳弹轰炸"}, false);
            atthigh = base.AddSlider("巡航高度","atthigh",1000f,500f,6000f);
            attkey = base.AddEmulatorKey("开火","attkey",KeyCode.None);
            plane_Pitch_up = base.AddEmulatorKey("俯仰（上）", "plane_Pitch_up", KeyCode.None);
            plane_Pitch_down = base.AddEmulatorKey("俯仰（下）", "plane_Pitch_down", KeyCode.None);
            plane_Yaw_left = base.AddEmulatorKey("偏航（左）", "plane_Yaw_left", KeyCode.None);
            plane_Yaw_right = base.AddEmulatorKey("偏航（右）", "plane_Yaw_right", KeyCode.None);
            plane_Roll_clockwise = base.AddEmulatorKey("滚转（顺）", "plane_Roll_clockwise", KeyCode.None);
            plane_Roll_anticlockwise = base.AddEmulatorKey("滚转（逆）", "plane_Roll_anticlockwise", KeyCode.None);
            plane_fly = base.AddEmulatorKey("引擎", "plane_fly", KeyCode.None);
            PID_P = base.AddSlider("PID(P)", "PID_P", 1f, 0f, 100f);
            PID_I = base.AddSlider("PID(I)", "PID_I", 0.001f, 0f, 100f);
            PID_D = base.AddSlider("PID(D)", "PID_D", 100f, 0f, 100f);
            PID_S = base.AddSlider("PID整体比例", "PID_S", 20f, 0f, 100f);
            //EmulateKeys(keys, attkey, true);
            radiotype.Toggled += triggle;
            clienttype.ValueChanged += triggleE;
            renewUI();
            keys = new MKey[] {};
            numberOfPlane = 1;
            itinerary = new itinerary(true);
            if (dir == null)
            {
                dir = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                dir.GetComponent<Collider>().enabled = false;
                dir.GetComponent<MeshFilter>().mesh = ModResource.GetMesh("direction").Mesh;
                dir.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                dir2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                dir2.GetComponent<Collider>().enabled = false;
                dir2.GetComponent<MeshFilter>().mesh = ModResource.GetMesh("direction").Mesh;
                dir2.transform.localScale = new Vector3(20f, 20f, 20f);
            }
            dir.SetActive(false);
            dir2.SetActive(false);
            navalhe = FindObjectOfType<Navalhe>();
        }
        public void renewUI()
        {
            radioopen.DisplayInMapper = !radiotype.IsActive;
            clienttype.DisplayInMapper = radiotype.IsActive;
            attkey.DisplayInMapper = radiotype.IsActive;
            atttypeplane.DisplayInMapper = clienttype.Value == 0 && radiotype.IsActive;
            plane_Pitch_up.DisplayInMapper = clienttype.Value == 0 && radiotype.IsActive;
            plane_Pitch_down.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            plane_Yaw_left.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            plane_Yaw_right.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            plane_Roll_clockwise.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            plane_Roll_anticlockwise.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            PID_P.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            PID_I.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            PID_D.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            PID_S.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            plane_fly.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
            changekey.DisplayInMapper = !radiotype.IsActive;
            attrange.DisplayInMapper = plane_Pitch_up.DisplayInMapper;
        }
        public override bool EmulatesAnyKeys
        {
            get
            {
                return true;
            }
        }
        public void getvaluefromitinerary()
        {
            Vector3 lockp = new Vector3();
            Vector3 lockq = new Vector3();
            itinerary.putout(out lockp, out lockq);
            float Yaw_oriang = searchProjectFromSelf(base.transform.forward, base.transform.right);
            float Pitch_oriang = searchProjectFromSelf(base.transform.forward, base.transform.up);
            float ass = base.transform.rotation.eulerAngles.z;
            float assE = lockq.z;
            if (base.transform.rotation.eulerAngles.z > 180f)
            {
                ass= base.transform.rotation.eulerAngles.z - 360f;
            }
            if (lockq.z > 180f)
            {
                assE = lockq.z;
            }
            float Roll_oriang = assE - ass;
            PID_Yaw.RenewCalculData(Yaw_oriang * PID_S.Value, 0f);
            PID_Pitch.RenewCalculData(Pitch_oriang * PID_S.Value, 0f); 
            PID_Roll.RenewCalculData(Roll_oriang* PID_S.Value, 0f);
            power_Yaw = PID_Yaw.Calcul();
            power_Pitch = PID_Pitch.Calcul();
            power_Roll = PID_Roll.Calcul();
        }
        public float searchProjectFromSelf(Vector3 dir1, Vector3 dir2)
        {
            Vector3 lockp = new Vector3();
            Vector3 lockq = new Vector3();
            
            itinerary.putout(out lockp, out lockq);
            Vector3 plane = Vector3.Cross(dir1, dir2);
            Vector3 lockv = Vector3.ProjectOnPlane(lockp - base.transform.position, plane);
            Vector3 n = Vector3.ProjectOnPlane(dir1, plane);
            float ang = Vector3.Angle(lockv, n);
            Vector3 ss = Vector3.Project(lockv, dir2).normalized;
            if (ss == -dir2.normalized)
            {
                return -ang;
            }
            return ang;
            
        }
        public bool Testitinerary()
        {
            Vector3 lockp = new Vector3();
            Vector3 lockq = new Vector3();
            itinerary.putout(out lockp, out lockq);
            if ((lockp - base.transform.position).magnitude<=600f) {
                Vector3 dir = (Quaternion.Euler(lockq) * Vector3.forward).normalized;
                Vector3 point = (lockp - dir * 100f) - base.transform.position;
                Vector3 ss = Vector3.Project(point, dir).normalized;
                if (ss == dir)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public void testToDelet()
        {
            if (Testitinerary())
            {
                itinerary.removefirst();
            }
        }
        public void renewkey()
        {
            try
            {
                switch (lastsimP)
                {
                    case 0:
                        break;
                    case 1:
                        EmulateKeys(keys, plane_Pitch_up, false);
                        break;
                    case 2:
                        EmulateKeys(keys, plane_Pitch_down, false);
                        break;

                }
                switch (lastsimY)
                {
                    case 0:
                        break;
                    case 3:
                        EmulateKeys(keys, plane_Yaw_right, false);
                        break;
                    case 4:
                        EmulateKeys(keys, plane_Yaw_left, false);
                        break;
                }
                switch (lastsimR)
                {
                    case 0:
                        break;
                    case 5:
                        EmulateKeys(keys, plane_Roll_clockwise, false);
                        break;
                    case 6:
                        EmulateKeys(keys, plane_Roll_anticlockwise, false);
                        break;
                }
            }
            catch
            {

            }
            lastsimP = 0;
            lastsimY = 0;
            lastsimR = 0;
        }
        public int em;
        public void FixedUpdate()
        {
            if (radiotype.IsActive)
            {
                try
                {
                    if (PID_Pitch == null)
                    {
                        PID_Pitch = new PID(PID_P.Value, PID_I.Value, PID_D.Value, -200, 200);
                        PID_Yaw = new PID(PID_P.Value, PID_I.Value, PID_D.Value, -200, 200);
                        PID_Roll = new PID(PID_P.Value, PID_I.Value, PID_D.Value, -200, 200);
                    }
                    if (itinerary.haveintinerary())
                    {
                        getvaluefromitinerary();
                        if (followMod)
                        {
                            itinerary.repos(lockbb.transform.position - originPos);
                        }
                        testToDelet();
                        if (!flying)
                        {
                            flying = true;
                            EmulateKeys(keys, plane_fly, true);
                        }
                        if (itinerary.getCount()==attNumber && attacking==false)
                        {
                            attacking=true;
                            EmulateKeys(keys, attkey, true);
                        }
                    }

                }
                catch
                {

                }

                put_Pitch += power_Pitch * Time.fixedDeltaTime * 100f;
                put_Roll += power_Roll * Time.fixedDeltaTime * 100f;
                put_Yaw += power_Yaw * Time.fixedDeltaTime * 100f;
                renewkey();
                if (Mathf.Abs(put_Pitch) >= 200f)
                {
                    try
                    {
                        if (put_Pitch > 0)
                        {
                            lastsimP = 1;
                            EmulateKeys(keys, plane_Pitch_up, true);

                        }
                        else
                        {
                            lastsimP = 2;
                            EmulateKeys(keys, plane_Pitch_down, true);

                        }
                    }
                    catch
                    {

                    }
                    put_Pitch = 0;
                }

                if (Mathf.Abs(put_Yaw) >= 200f)
                {
                    try
                    {
                        if (put_Yaw > 0)
                        {
                            lastsimY = 3;
                            EmulateKeys(keys, plane_Yaw_right, true);

                        }
                        else
                        {

                            lastsimY = 4;
                            EmulateKeys(keys, plane_Yaw_left, true);

                        }
                    }
                    catch
                    {

                    }
                    put_Yaw = 0;
                }

                if (Mathf.Abs(put_Roll) >= 200f)
                {
                    try
                    {
                        if (put_Roll < 0)
                        {
                            lastsimR = 5;
                            EmulateKeys(keys, plane_Roll_clockwise, true);

                        }
                        else
                        {
                            lastsimR = 6;
                            EmulateKeys(keys, plane_Roll_anticlockwise, true);

                        }
                    }
                    catch
                    {

                    }
                    put_Roll = 0;
                }
            }//舰载机移动

        }   


        public void triggle(bool a)
        {
            renewUI();
        }
        public void triggleE(int a)
        {
            renewUI();
        }
        public void OnGUI()
        {
            if (PlayerData.localPlayer.networkId == base.BlockBehaviour.ParentMachine.PlayerID)
            {
                
                if (!radiotype.IsActive)
                {
                    if (radiostate)
                    {
                        
                        try
                        {
                            if (navalhe.planeinfos.Count > 0)
                            {
                                Vector2 pos = Input.mousePosition;
                                GUI.color = Color.green;
                                GUI.TextArea(new Rect(pos.x - (float)(this.iconSize / 2) + 50f, (float)Camera.main.pixelHeight - pos.y - (float)(this.iconSize / 2), (float)this.iconSize, (float)this.iconSize), navalhe.planeinfos[numberOfPlane - 1].planename);
                            }
                        }
                        catch
                        {

                        }
                    }

                }
            }
        }
        public void Update()
        {
            if (PlayerData.localPlayer.networkId == base.BlockBehaviour.ParentMachine.PlayerID) {
                try
                {
                    if (BlockMapper.IsOpen)
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
                if (!radiotype.IsActive)
                {
                    if (radiostate)
                    {
                        if (changekey.IsPressed)
                        {
                            numberOfPlane += 1;
                            if (navalhe.planeinfos.Count< numberOfPlane)
                            {
                                numberOfPlane = 1;
                            }
                        }
                       
                        RaycastHit raycastHit;
                        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Camera.main.farClipPlane);

                        dir2.transform.forward = new Vector3(Camera.main.transform.forward.x, 0.4f, Camera.main.transform.forward.z);
                        dir2.transform.position = raycastHit.point+ dir2.transform.forward*150f;
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (StatMaster.isHosting)
                            {

                                navalhe.loadplane(raycastHit.point,dir2.transform.forward, numberOfPlane - 1);
                            }
                        }

                    }
                    if (radioopen.IsPressed)
                    {
                        if (radiostate)
                        {
                            radiostate = false;
                            dir2.SetActive(false);

                        }   
                        else
                        {
                            radiostate = true;
                            dir2.SetActive(true);
                        }
                    }
                    
                }
            }
        }
        public Navalhe navalhe;
        public void sendState()
        {
            Message sendState = Messages.stateOfRadio.CreateMessage(new object[]
    {
                    base.BlockBehaviour.BuildingBlock.Guid,
                    base.BlockBehaviour.ParentMachine.PlayerID,
                    radiostate
    });
            ModNetworking.SendToAll(sendState);
        }
        public float iconSize = 64f;
        protected void ShowVisualisation()
        {
            if (!this.visualisingHeight)
            {
                this.dir.gameObject.SetActive(true);
                this.visualisingHeight = true;
            }

            this.UpdateVisualisation();
        }
        protected void UpdateVisualisation()
        {
            dir.transform.forward = -base.transform.forward;
            dir.transform.position = base.transform.position + -dir.transform.forward*(Mathf.Sin(timeflue*2f)*0.25f-1f);
            
            
            timeflue += Time.deltaTime;
        }
        protected void HideVisualisation()
        {

            if (!this.visualisingHeight)
            {
                return;
            }
            this.dir.gameObject.SetActive(false);
            this.visualisingHeight = false;
        }
    }
}