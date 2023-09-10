using System;
using Modding;
using skpCustomModule;
using UnityEngine;

namespace Navalmod
{
	// Token: 0x0200001B RID: 27
	public class Navalstart : ModEntryPoint
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00006780 File Offset: 0x00004980
		public override void OnLoad()
		{
            Navalstart.mod = new GameObject("Navalmod");
            Navalstart.mod.AddComponent<AdCustomModuleMod>();
            SingleInstance<waterE>.Instance.transform.SetParent(Navalstart.mod.transform);
            SingleInstance<waterE2>.Instance.transform.SetParent(Navalstart.mod.transform);
            SingleInstance<flakc>.Instance.transform.SetParent(Navalstart.mod.transform);
            SingleInstance<AIflak>.Instance.transform.SetParent(Navalstart.mod.transform);
            SingleInstance<Sea>.Instance.transform.SetParent(Navalstart.mod.transform);
			SingleInstance<H3NetworkManager>.Instance.transform.SetParent(Navalstart.mod.transform);
            GameObject gameObject = new GameObject("Navalhe");
			gameObject.AddComponent<Navalhe>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
            GameObject gameObject2 = new GameObject("Controller");
			gameObject2.AddComponent<Controller>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject2);
			GameObject gameObject3 = new GameObject("NavalCannoBlock");
			gameObject3.AddComponent<NavalCannoBlock>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject3);
            UnityEngine.Object.DontDestroyOnLoad(Navalstart.mod);
            //Navalstart.mod.AddComponent<Recording>();
            
            SingleInstance<MessageController>.Instance.transform.SetParent(Navalstart.mod.transform);
            
		}


		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000023D4 File Offset: 0x000005D4
		public static Configuration Configuration { get; private set; }

		// Token: 0x040000AF RID: 175
		public static GameObject mod;
	}
}
