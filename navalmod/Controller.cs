using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace Navalmod
{
	// Token: 0x02000003 RID: 3
	internal class Controller : SingleInstance<Controller>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		public override string Name { get; } = "Controller";

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		private void Awake()
		{
			this.CBNA = new List<CanonBlock>();
			Events.OnBlockInit += this.AddSliders;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002081 File Offset: 0x00000281
		private void Update()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023E8 File Offset: 0x000005E8
		public static bool HasEnhancement(BlockBehaviour block)
		{
			return block.MapperTypes.Exists((MapperType match) => match.Key == "Stabilizer");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002424 File Offset: 0x00000624
		public void AddAllSliders()
		{
			foreach (BlockBehaviour blockBehaviour in Machine.Active().BuildingBlocks.FindAll((BlockBehaviour block) => !Controller.HasEnhancement(block)))
			{
				ConsoleController.ShowMessage(string.Format("Cannon", new object[0]));
				this.AddSliders(blockBehaviour);
				int blockID = blockBehaviour.BlockID;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000024C4 File Offset: 0x000006C4
		private void AddSliders(Block block)
		{
			BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
			bool flag = !Controller.HasEnhancement(internalObject);
			if (flag)
			{
				this.AddSliders(internalObject);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024F8 File Offset: 0x000006F8
		private void AddSliders(Transform block)
		{
			BlockBehaviour component = block.GetComponent<BlockBehaviour>();
			bool flag = !Controller.HasEnhancement(component);
			if (flag)
			{
				this.AddSliders(component);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002524 File Offset: 0x00000724
		private void AddSliders(BlockBehaviour block)
		{
			bool flag = block.BlockID == 11;
			if (flag)
			{
				this.CBNA.Add(this.CB);
			}
			bool flag2 = this.dic_EnhancementBlock.ContainsKey(block.BlockID);
			if (flag2)
			{
				Type type = this.dic_EnhancementBlock[block.BlockID];
				bool flag3 = block.GetComponent(type) == null;
				if (flag3)
				{
					block.gameObject.AddComponent(type);
				}
				this.GO.Add(block.gameObject);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002084 File Offset: 0x00000284
		[Obsolete]
		public IEnumerator RefreshSliders()
		{
			int i = 0;
			for (;;)
			{
				int num = i;
				i = num + 1;
				bool flag = num >= 3;
				if (flag)
				{
					break;
				}
				yield return new WaitForEndOfFrame();
			}
			this.CBNA = new List<CanonBlock>();
			foreach (BlockBehaviour block in Machine.Active().BuildingBlocks)
			{
				this.AddSliders(block);
				
			}
			List<BlockBehaviour>.Enumerator enumerator = default(List<BlockBehaviour>.Enumerator);
			ConsoleController.ShowMessage("Refresh");
			yield break;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000025B0 File Offset: 0x000007B0
		private void HE()
		{
			Vector3 localScale = default(Vector3);
			localScale.x = 10f;
			localScale.y = 10f;
			localScale.z = 10f;
			this.CB.boltObject.transform.localScale = localScale;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002604 File Offset: 0x00000804
		public List<CanonBlock> GetCanonBlocks()
		{
			return this.CBNA;
		}

		// Token: 0x04000005 RID: 5
		public Transform targetSavedInController;

		// Token: 0x04000006 RID: 6
		[Obsolete]
		internal PlayerMachineInfo PMI;

		// Token: 0x04000007 RID: 7
		public Dictionary<int, Type> dic_EnhancementBlock = new Dictionary<int, Type>();

		// Token: 0x04000008 RID: 8
		private List<GameObject> GO = new List<GameObject>();

		// Token: 0x04000009 RID: 9
		protected CanonBlock CB;

		// Token: 0x0400000A RID: 10
		public List<CanonBlock> CBNA;
	}
}
