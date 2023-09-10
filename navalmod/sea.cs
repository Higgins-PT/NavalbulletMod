using Modding;
using Navalmod;
using System;
using UnityEngine;

namespace Navalmod
{
    // Token: 0x0200002E RID: 46
    public class Sea : SingleInstance<Sea>
    {
        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000141 RID: 321 RVA: 0x000135D5 File Offset: 0x000117D5
        public override string Name { get; } = "Sea";
        public float seahigh;
        // Token: 0x06000142 RID: 322 RVA: 0x00003090 File Offset: 0x00001290
        public void Awake()
        {
            navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            seaeffect = new GameObject();
            //seaeffect.AddComponent<SeaSurfacer>();
            //DontDestroyOnLoad(seaeffect);
        }
        public Navalhe navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
        public float Getseahigh(Vector3 pos)
        {
            try
            {
                return navalhe.seahigh;
            }
            catch
            {
                return 0;
            }
        }
        // Token: 0x06000143 RID: 323 RVA: 0x000135E0 File Offset: 0x000117E0
        public void FixedUpdate()
        {

        }
        public void Update()
        {
            if (navalhe == null)
            {
                navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            }
            bool flag = this.preShowSea && !navalhe.watershow;
            if (flag)
            {
                this.preShowSea = false;
                try
                {
                    UnityEngine.Object.Destroy(this.SeaPlane);
                }
                catch
                {
                }
            }
            else
            {
                bool flag2 = !this.preShowSea && navalhe.watershow;
                if (flag2)
                {
                    this.preShowSea = true;
                    this.SeaPlane = UnityEngine.Object.Instantiate<GameObject>(SingleInstance<waterE>.Instance.water);
                    this.SeaPlane.transform.position = new Vector3(0f, navalhe.seahigh, 0f);
                    this.SeaPlane.AddComponent<Water>().waterMode = Water.WaterMode.Reflective;
                    this.SeaPlane.SetActive(true);
                    SeaPlane.transform.localScale *= 10f;
                }
            }
        }

        // Token: 0x0400015D RID: 349
        public GameObject SeaPlane;
        public GameObject seaeffect;
        // Token: 0x0400015E RID: 350
        public bool preShowSea = false;
    }
}
