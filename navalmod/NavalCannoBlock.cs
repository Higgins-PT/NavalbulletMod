using System;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace Navalmod
{
	// Token: 0x02000012 RID: 18
	public class NavalCannoBlock : MonoBehaviour
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002081 File Offset: 0x00000281
		private void OnCollisionEnter(Collision collision)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002081 File Offset: 0x00000281
		private void OnCollisionExit(Collision collision)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002081 File Offset: 0x00000281
		private void OnCollisionStay(Collision collision)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002225 File Offset: 0x00000425
		private void OnTriggerEnter(Collision collision)
		{
			ModConsole.Log("a3");
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002233 File Offset: 0x00000433
		private void Awake()
		{
			this.SafeAwake();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004B00 File Offset: 0x00002D00
		private void AddSliders(Block block)
		{
			BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
			bool flag = !Controller.HasEnhancement(internalObject);
			if (flag)
			{
				this.AddSliders(internalObject);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004B34 File Offset: 0x00002D34
		public static bool HasEnhancement(BlockBehaviour block)
		{
			return block.MapperTypes.Exists((MapperType match) => match.Key == "Stabilizer");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004B70 File Offset: 0x00002D70
		private void AddSliders(BlockBehaviour block)
		{

            if (block.GetComponent<blockset>() == null)
            {
				try
				{
					block.gameObject.AddComponent<blockset>();
					block.gameObject.AddComponent<H3NetworkBlock>().blockBehaviour = block;

					block.gameObject.AddComponent<UnderWater>();
				}
				catch
				{

				}
            }

            if (block.BlockID == 11 && block.GetComponent<NavalCannoBlockE>() == null)
			{
				block.gameObject.AddComponent<NavalCannoBlockE>();
                return;
			}
            if (block.BlockID == 61 && block.GetComponent<ArrowTurretBlock>() == null)
            {
                block.gameObject.AddComponent<ArrowTurretBlock>();
                return;
            }
            if (block.BlockID == 59 && block.GetComponent<smoke>() == null)
            {
                block.gameObject.AddComponent<smoke>();
                return;
            }
            if (block.BlockID == 21 && block.GetComponent<fire>() == null)
            {
                block.gameObject.AddComponent<fire>();
                return;
            }
            if (block.BlockID == 35 && block.GetComponent<view>() == null)
            {
                block.gameObject.AddComponent<view>();
                return;
            }
            if (block.BlockID == 74 && block.GetComponent<sqrballoonfloat>() == null)
            {
                block.gameObject.AddComponent<sqrballoonfloat>();
                return;
            }
            if (block.BlockID == 28 && block.GetComponent<AIbattery>() == null)
            {
                block.gameObject.AddComponent<AIbattery>();
                return;
            }
        }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000223D File Offset: 0x0000043D
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002245 File Offset: 0x00000445
		public BlockBehaviour BB { get; internal set; }
        /*public void Update()
        {
            ModConsole.Log("asdass");
            GameObject obj = GameObject.Find("CrossbowBolt(Clone)");
            if (obj)
            {

                ModConsole.Log("asda");
                TrailRenderer trailRenderer = obj.GetComponent<TrailRenderer>();
                if (!trailRenderer)
                {
                    trailRenderer = obj.AddComponent<TrailRenderer>();
                    trailRenderer.autodestruct = false;
                    trailRenderer.receiveShadows = false;


                    trailRenderer.startWidth = 0.3f;
                    trailRenderer.endWidth = 0.01f;

                    trailRenderer.material.SetColor("_TintColor", UnityEngine.Color.red);
                    trailRenderer.time = 0.04f;
                    trailRenderer.enabled = true;
                }

            }


        }*/
        // Token: 0x06000065 RID: 101 RVA: 0x00002081 File Offset: 0x00000281
        public virtual void ChangedProperties()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002081 File Offset: 0x00000281
		public virtual void OnSimulateStartClient()
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002081 File Offset: 0x00000281
		public virtual void DisplayInMapper(bool value)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000224E File Offset: 0x0000044E
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002256 File Offset: 0x00000456
		public bool EnhancementEnabled { get; set; }

		// Token: 0x0600006A RID: 106 RVA: 0x0000225F File Offset: 0x0000045F
		private void KeyLoad()
		{
			this.HEkey = this.CB.AddKey("HE", "HE", KeyCode.Alpha1);
			ModConsole.Log("按键装载完毕");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004BAC File Offset: 0x00002DAC
		private void AddSlidersE(BlockBehaviour CBB)
		{
			bool flag = CBB.BlockID == 11;
			if (flag)
			{
				this.HEkey = CBB.AddKey("HE", "HE", KeyCode.Alpha1);
				ModConsole.Log("按键装载完毕");
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002081 File Offset: 0x00000281
		public virtual void SimulateUpdateAlways()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000228A File Offset: 0x0000048A
		private void SafeAwake()
		{
			Events.OnBlockInit += this.AddSliders;
		}

		// Token: 0x04000050 RID: 80
		protected CanonBlock CB;

		// Token: 0x04000051 RID: 81
		private Controller controller;

		// Token: 0x04000052 RID: 82
		private List<CanonBlock> CBL;

		// Token: 0x04000053 RID: 83
		public Transform targetSavedInController;

		// Token: 0x04000054 RID: 84
		[Obsolete]
		internal PlayerMachineInfo PMI;

		// Token: 0x04000055 RID: 85
		public Dictionary<int, Type> dic_EnhancementBlock = new Dictionary<int, Type>();

		// Token: 0x04000056 RID: 86
		private List<GameObject> GO = new List<GameObject>();

		// Token: 0x04000057 RID: 87
		public List<CanonBlock> CBNA;

		// Token: 0x04000058 RID: 88
		public MKey HEkey;

		// Token: 0x04000059 RID: 89
		public MKey APkey;

		// Token: 0x0400005A RID: 90
		public MKey SAPkey;

		// Token: 0x0400005B RID: 91
		public MKey VTkey;

		// Token: 0x0400005C RID: 92
		public MKey timekey;

		// Token: 0x0400005D RID: 93
		public Action PropertiseChangedEvent;
	}
}
