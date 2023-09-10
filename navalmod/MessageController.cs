using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Modding;
using UnityEngine;
namespace Navalmod
{
	// Token: 0x02000007 RID: 7
	public class MessageController : SingleInstance<MessageController>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002107 File Offset: 0x00000307
		public override string Name { get; } = "Message Controller";

		// Token: 0x0600001A RID: 26 RVA: 0x0000273C File Offset: 0x0000093C
		public MessageController()
		{
			Messages.bultboomtype = ModNetworking.CreateMessageType(new DataType[]
			{
				DataType.Vector3,
				DataType.Single
			});
            Messages.bultboom2type = ModNetworking.CreateMessageType(new DataType[]
            {
                DataType.Vector3,
                DataType.Single
            });
            Messages.bultinfo = ModNetworking.CreateMessageType(new DataType[]
            {
                DataType.Single,
                DataType.Single,
                DataType.Single,
                DataType.Single,
                DataType.Single,
                DataType.Single,
                DataType.Single
            });
            Messages.fireinfo = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.Single,
                DataType.Boolean
           });
            Messages.radioinfo = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.Single,
                DataType.Single,
                DataType.Vector3,
  
           });
            Messages.torpedo = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,
                DataType.Integer
           });
            Messages.rayinfo = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,
                DataType.Vector3,//pos
                DataType.Vector3//dec
           });
            Messages.sonarinfo = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,
                DataType.Vector3,//pos
           });
            Messages.rayinfoE = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,
                DataType.String,
                DataType.Vector3,
                DataType.Single,
                DataType.Single
           });
            Messages.flak = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,
                DataType.Vector3,//pos
                DataType.Vector3//v

           });
            Messages.ballshoot = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,//guid
                DataType.Integer,//count
                DataType.Vector3,//pos
                DataType.Vector3,//v
                DataType.Integer//player

           });
            Messages.ballshootE = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,//guid
                DataType.Integer,//count
                DataType.Vector3,//pos
                DataType.Vector3,//v
                DataType.Integer//player

           });
            Messages.balldestroy = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,//guid
                DataType.Integer,//count
                DataType.Integer,//player
                DataType.Integer//type 1=can 2=arr 3=boom
           });
            Messages.fire = ModNetworking.CreateMessageType(new DataType[]
           {
                DataType.String,//guid
                DataType.Integer,//player
                DataType.Single,//size
                DataType.Vector3

           });
            Messages.stateOfRadio = ModNetworking.CreateMessageType(new DataType[]
{
                DataType.Integer,//planeNumber
                DataType.Vector3,//attpos
                DataType.Single//dir

});
            Messages.saveplane = ModNetworking.CreateMessageType(new DataType[]
{
                DataType.Integer,//planeNumber
                DataType.String //planeName

});


            List<cannonball> cannonballs = new List<cannonball>();
            this.navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
			ModNetworking.CallbacksWrapper callbacks = ModNetworking.Callbacks;
			MessageType bultboomtype = Messages.bultboomtype;
			ModNetworking.CallbacksWrapper callbacksWrapper = callbacks;
			ModNetworking.CallbacksWrapper callbacksWrapper2 = callbacksWrapper;
			ModNetworking.CallbacksWrapper callbacksWrapper3 = callbacksWrapper2;
			MessageType messageType = bultboomtype;
			ModNetworking.CallbacksWrapper callbacksWrapper4 = callbacksWrapper3;
			MessageType type = messageType;
			callbacksWrapper4[Messages.bultboomtype] += delegate(Message msg)
			{
				bool isClient = StatMaster.isClient;
				if (isClient)
				{
					ModConsole.Log("生成爆炸");
					Vector3 position = (Vector3)msg.GetData(0);
					float num = (float)msg.GetData(1);



                    waterE2.Instance.EmitSparks(position,num);
                    /*
                    bool boomvoice = this.navalhe.boomvoice;
					if (boomvoice)
					{
						GameObject gameObject2 = new GameObject();
						gameObject2.transform.position = position;
						this.explosionvoice = gameObject2.gameObject.AddComponent<AudioSource>();
						this.explosionvoice.spatialBlend = 1f;
						this.explosionvoice.rolloffMode = AudioRolloffMode.Linear;
						this.explosionvoice.playOnAwake = false;
						this.explosionvoice.loop = false;
						this.explosionvoice.volume = UnityEngine.Object.FindObjectOfType<Navalhe>().volume * num / 60f;
						bool flag = num >= 30f;
						if (flag)
						{
							this.explosionvoice.maxDistance = 1000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionbig[UnityEngine.Random.Range(1, 4)]);
							this.explosionvoice.maxDistance = 3000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionbigfar2[UnityEngine.Random.Range(1, 4)]);
							this.explosionvoice.maxDistance = 6000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionbigfar[UnityEngine.Random.Range(1, 4)]);
						}
						bool flag2 = num >= 1f;
						if (flag2)
						{
							this.explosionvoice.maxDistance = 1000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionmid[UnityEngine.Random.Range(1, 4)]);
							this.explosionvoice.maxDistance = 3000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionmidfar[UnityEngine.Random.Range(1, 4)]);
						}
						bool flag3 = num < 1f;
						if (flag3)
						{
							this.explosionvoice.maxDistance = 1000f;
							this.explosionvoice.PlayOneShot(Soundfiles.explosionsmall[UnityEngine.Random.Range(1, 5)]);
						}
						UnityEngine.Object.Destroy(gameObject2, 10f);
					}
					*/
				}
			};

            ModNetworking.CallbacksWrapper callbacks3 = ModNetworking.Callbacks;
            MessageType bultinfo = Messages.bultinfo;
            callbacksWrapper = callbacks3;
            callbacksWrapper2 = callbacksWrapper;
            callbacksWrapper3 = callbacksWrapper2;
            messageType = bultinfo;
            callbacksWrapper4 = callbacksWrapper3;
            type = messageType;
            callbacks3[type] += delegate (Message msg)
            {
      
                    blotlist blotlist = new blotlist();

                int num1 = Convert.ToInt32((float)msg.GetData(0));
                int num2= Convert.ToInt32((float)msg.GetData(1));
                float num3= (float)msg.GetData(2);
                float num4 = (float)msg.GetData(3);
                float num5= (float)msg.GetData(4);
                int num6= Convert.ToInt32((float)msg.GetData(5));
                float num7= (float)msg.GetData(6);
                blotlist.type = num1;

                blotlist.state = num2;

                blotlist.scale = num3;
   
                blotlist.penetratingE = num4;

                blotlist.penetrating = num5;
  
                blotlist.networkids = num6;

                blotlist.damage = num7;

               
               
                navalhe.addblot(blotlist);
            };
            ModNetworking.CallbacksWrapper callbacks4 = ModNetworking.Callbacks;
            MessageType fireinfo = Messages.fireinfo;
   
            messageType = fireinfo;
  
            type = messageType;
            callbacks4[type] += delegate (Message msg)
            {
  

                navalhe.retime = (float)msg.GetData(0);
                navalhe.ss = (bool)msg.GetData(1);

            };



            ModNetworking.CallbacksWrapper callbacksE = ModNetworking.Callbacks;
            MessageType rayinfo = Messages.rayinfo;

            messageType = rayinfo;

            callbacksE[messageType] += delegate (Message msg)
            {
                if (StatMaster.isHosting)
                {
                  
                    Guid guid = new Guid((string)msg.GetData(0));

                    try
                    {
                        RaycastHit raycastHit = new RaycastHit();
                  
                        view View = new view();
                        foreach (view match in UnityEngine.Object.FindObjectsOfType<view>().ToList<view>())
                        {
                            if (match.guid == guid && match.bb.ParentMachine.PlayerID == msg.Sender.NetworkId)
                            {
                                View = match;
                                break;
                            }
                        }

                        if (Physics.Raycast(new Ray((Vector3)msg.GetData(1), (Vector3)msg.GetData(2)), out raycastHit, Camera.main.farClipPlane))
                        {
                            
                            if (raycastHit.transform.gameObject.GetComponent<BlockBehaviour>() == null)
                            {
                                
                                View.locktype = 1;
                                View.lockp = raycastHit.point;
                                View.sender = false;
                                
                                ModNetworking.SendToAll(Messages.rayinfoE.CreateMessage(new object[]
                            {
                                guid.ToString(),
                                "",
                                raycastHit.point,
                                1f,
                                0f
                            }));

                            }
                            else
                            {
                                View.sender = true;
                                View.locktype = 2;
                                View.coldtime = 0.2f;
                                View.lockg = raycastHit.transform.gameObject;
                                if (View.flak.IsActive)
                                {
                                    View.flaklock = raycastHit.transform.gameObject.GetComponent<BlockBehaviour>();
                                }
                                ModNetworking.SendToAll(Messages.rayinfoE.CreateMessage(new object[]
                            {

                                guid.ToString(),
                                raycastHit.transform.gameObject.GetComponent<BlockBehaviour>().BuildingBlock.Guid.ToString(),
                                raycastHit.point,
                                2f,
                                (float)raycastHit.transform.gameObject.GetComponent<BlockBehaviour>().ParentMachine.PlayerID
                            }));

                            }
                            

                        }
                        
                    }
                    catch
                    {
                    }
                }
                

            };
           
            ModNetworking.CallbacksWrapper callbacksR = ModNetworking.Callbacks;
            MessageType rayinfoE = Messages.rayinfoE;

            messageType = rayinfoE;

            callbacksR[messageType] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {
                    


                    try
                    {
                        Guid guid = new Guid((string)msg.GetData(0));
                        
                        

                        //view View = UnityEngine.Object.FindObjectsOfType<view>().ToList<view>().Find((view match) => match.guid == guid && match.bb.ParentMachine.PlayerID == PlayerData.localPlayer.networkId);

                        foreach (view View in UnityEngine.Object.FindObjectsOfType<view>().ToList<view>())
                            {
                               
                                if(View.guid == guid && View.bb.ParentMachine.PlayerID == PlayerData.localPlayer.networkId)
                                {
                                View.locktype = 1;

                                if ((float)msg.GetData(3) == 1f)
                                {

                                    View.locktype = 1;
                                    View.lockp = (Vector3)msg.GetData(2);
                                }
                                else
                                {

                                    float id = (float)msg.GetData(4);
   
                                    Guid guide = new Guid((string)msg.GetData(1));

                                    View.locktype = 2;

                             

                                    BlockBehaviour[] bb = UnityEngine.Object.FindObjectsOfType<BlockBehaviour>();

                                    for (int j = 0; j < bb.Length; j = j+1)
                                    {
                                        try
                                        {
                                            ModConsole.Log(((float)bb[j].ParentMachine.PlayerID).ToString());
                                            if (bb[j].BuildingBlock.Guid == guide && (float)bb[j].ParentMachine.PlayerID == id)
                                            {
                
                                                ModConsole.Log(bb[j].transform.position.ToString());
         

                                                View.lockg = bb[j].gameObject;
     
                                                if (View.flak.IsActive)
                                                {
                                                    View.flaklock = bb[j];
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

                        
                        
                       
                    }
                    catch
                    {
                    }
                    
                }


            };


            callbacksR[Messages.sonarinfo] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {



                    try
                    {
                        Guid guid = new Guid((string)msg.GetData(0));



                        //view View = UnityEngine.Object.FindObjectsOfType<view>().ToList<view>().Find((view match) => match.guid == guid && match.bb.ParentMachine.PlayerID == PlayerData.localPlayer.networkId);

                        foreach (view View in UnityEngine.Object.FindObjectsOfType<view>().ToList<view>())
                        {

                            if (View.guid == guid && View.bb.ParentMachine.PlayerID == PlayerData.localPlayer.networkId)
                            {
                                View.locktype = 1;

                                View.lockp = (Vector3)msg.GetData(1);
                            }
                        }




                    }
                    catch
                    {
                    }

                }


            };






            callbacksR[Messages.ballshoot] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {
                    String guid = (string)msg.GetData(0);
                    foreach (NavalCannoBlockE match in UnityEngine.Object.FindObjectsOfType<NavalCannoBlockE>().ToList<NavalCannoBlockE>())
                    {
                        try
                        {
                            if (match.MyGUID == guid && match.CB.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(4))
                            {
                                NavalCannoBlockE navalCannoBlockE = match;
                                GameObject canball = (GameObject)UnityEngine.Object.Instantiate(navalCannoBlockE.cannonball, (Vector3)msg.GetData(2), navalCannoBlockE.CB.transform.rotation);
                                Rigidbody rigidbody = canball.GetComponent<Rigidbody>();
                                rigidbody.velocity = (Vector3)msg.GetData(3);
                                canball.SetActive(true);
                                canball.isStatic = false;
                                navalCannoBlockE.shootsound();
                                break;
                            }
                        }
                        catch
                        {

                        }
                    }
                    
                }
            };
            callbacksR[Messages.ballshootE] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {


                    String guid = (string)msg.GetData(0);

                    
                    foreach (ArrowTurretBlock match in UnityEngine.Object.FindObjectsOfType<ArrowTurretBlock>().ToList<ArrowTurretBlock>())
                    {
                        if (match.MyGUID == guid && match.CB.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(4))
                        {
                            ArrowTurretBlock navalCannoBlockE = match;
                            GameObject canball = (GameObject)UnityEngine.Object.Instantiate(navalCannoBlockE.cannonball, (Vector3)msg.GetData(2), navalCannoBlockE.CB.transform.rotation);
                            Rigidbody rigidbody = canball.GetComponent<Rigidbody>();
                            //rigidbody.transform.position = navalCannoBlockE.CB.projectileSpawnPos.position;
                            //navalCannoBlockE.CB.gameObject.GetComponent<Rigidbody>().AddForce(navalCannoBlockE.shootspeed.Value * navalCannoBlockE.transform.up * navalCannoBlockE.backlash.Value);
                            rigidbody.velocity = (Vector3)msg.GetData(3);
                            canball.SetActive(true);
                            canball.isStatic = false;
                            canball.GetComponent<NAbolt>().simplehe = 1;
                            navalCannoBlockE.shootsound();
                            break;

                        }
                    }
                    
                    
                }
            };
            callbacksR[Messages.balldestroy] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {
                    /*
                    String guid = (string)msg.GetData(0);
                    int typeE = (int)msg.GetData(3);
                    if (typeE == 0)//炮弹
                    {
                        foreach (NavalCannoBlockE match in UnityEngine.Object.FindObjectsOfType<NavalCannoBlockE>().ToList<NavalCannoBlockE>())
                        {
                            if (match.MyGUID == guid && match.CB.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(2))
                            {
                                NavalCannoBlockE navalCannoBlockE = match;
                                NAbolt nAbolt = navalCannoBlockE.nAbolts[(int)msg.GetData(1)];
                                nAbolt.desrotyballclient();
                                break;
                            }
                        }
                    }
                    if (typeE == 1)//弩箭
                    {
                        foreach (ArrowTurretBlock match in UnityEngine.Object.FindObjectsOfType<ArrowTurretBlock>().ToList<ArrowTurretBlock>())
                        {
                            if (match.MyGUID == guid && match.CB.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(2))
                            {
                                ArrowTurretBlock navalCannoBlockE = match;
                                NAbolt nAbolt = navalCannoBlockE.nAbolts[(int)msg.GetData(1)];
                                nAbolt.desrotyballclient();
                                break;
                            }
                        }
                    }
                    if (typeE == 2)//航弹
                    {
                        foreach (bomb match in UnityEngine.Object.FindObjectsOfType<bomb>().ToList<bomb>())
                        {
                            if (match.BlockBehaviour.BuildingBlock.Guid.ToString() == guid && match.BlockBehaviour.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(2))
                            {
                                UnityEngine.GameObject.Destroy(match.gameObject);
                                break;
                            }
                        }
                    }
                    */


                }
            };
            MessageType rayinfoR = Messages.flak;



            callbacksR[rayinfoR] += delegate (Message msg)
            {
                if (StatMaster.isClient)
                {
                   
                    Guid guid = new Guid((string)msg.GetData(0));

                    foreach (view match in UnityEngine.Object.FindObjectsOfType<view>().ToList<view>())
                    {
                        if (match.guid == guid && match.bb.ParentMachine.PlayerID == PlayerData.localPlayer.networkId)
                        {
                            match.flakgo.transform.position = (Vector3)msg.GetData(1);
                            match.flakgo.GetComponent<Rigidbody>().velocity = (Vector3)msg.GetData(2);
                            match.lockp = (Vector3)msg.GetData(1);
                            match.lockg = match.flakgo;
                            break;
                        }
                    }

                    
                    

                }
            };


			MessageType bultboom2typeE = Messages.bultboom2type;
            callbacksR[bultboom2typeE] += delegate(Message msg)
			{
				bool isClient = StatMaster.isClient;
				if (isClient)
				{
					Vector3 position = (Vector3)msg.GetData(0);
					float num = (float)msg.GetData(1);
                    waterE.Instance.EmitSparks(position,num,false);
					bool boomvoice = this.navalhe.boomvoice;
					if (boomvoice)
					{
                        soundplay.Instance.explosionsound(num, position);

                    }
					
					
				}
			};
            MessageType tropeto = Messages.torpedo;
            callbacksR[tropeto] += delegate (Message msg)
            {
                bool isClient = StatMaster.isClient;
                if (isClient)
                {
                    Guid guid = new Guid((string)msg.GetData(0));
                    torpedo torpedo = UnityEngine.Object.FindObjectsOfType<torpedo>().ToList<torpedo>().Find((torpedo match) => match.BlockBehaviour.BuildingBlock.Guid == guid && match.BlockBehaviour.BuildingBlock.ParentMachine.PlayerID == (int)msg.GetData(1));
                    torpedo.special();
                }
            };
            callbacksR[Messages.fire] += delegate (Message msg)
            {
                bool isClient = StatMaster.isClient;
                if (isClient)
                {
                    Guid guid = new Guid((string)msg.GetData(0));
                    SingleInstance<waterE>.Instance.firein(guid, (int)msg.GetData(1), (float)msg.GetData(2), (Vector3)msg.GetData(3));
                }
            };

            callbacksR[Messages.stateOfRadio] += delegate (Message msg)
            {

            };
            callbacksR[Messages.saveplane] += delegate (Message msg)
            {

                Navalhe navalhe = FindObjectOfType<Navalhe>();
                navalhe.planeinfos.Add(new planeinfo((string)msg.GetData(1), (int)msg.GetData(0)));
            };

        }
       

        // Token: 0x0600001B RID: 27 RVA: 0x00002081 File Offset: 0x00000281
        private void explosionsound()
		{
		}

		// Token: 0x04000016 RID: 22
		private AudioSource explosionvoice;

		// Token: 0x04000017 RID: 23
		private Navalhe navalhe;
	}
}
