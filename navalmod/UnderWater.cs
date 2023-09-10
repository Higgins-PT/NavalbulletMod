using System;
using UnityEngine;

namespace Navalmod
{
    // Token: 0x02000035 RID: 53
    public class UnderWater : MonoBehaviour
    {
        // Token: 0x06000178 RID: 376 RVA: 0x00016224 File Offset: 0x00014424
        public void addDrag()
        {
            try
            {
                if (renewtime >= SingleInstance<Sea>.Instance.navalhe.seadrag * UnityEngine.Random.Range(0.8f, 1.2f))
                {
                    renewtime = 0f;
                    bool flag = !this.rigid;
                    if (flag)
                    {
                        this.rigid = base.transform.GetComponent<Rigidbody>();
                        this.InitialDrag = this.rigid.drag;
                        this.InitialAngularDrag = this.rigid.angularDrag;
                    }
                    bool showSea = SingleInstance<Sea>.Instance.navalhe.watershow;
                    if (showSea)
                    {
                        bool flag2 = base.transform.position.y < SingleInstance<Sea>.Instance.Getseahigh(base.transform.position);
                        if (flag2)
                        {
                            this.rigid.drag = this.UnderWaterDrag;
                            this.rigid.angularDrag = this.UnderWaterAngularDrag;
                        }
                        else
                        {
                            this.rigid.drag = this.InitialDrag;
                            this.rigid.angularDrag = this.InitialAngularDrag;
                        }
                    }
                    else
                    {
                        this.rigid.drag = this.InitialDrag;
                        this.rigid.angularDrag = this.InitialAngularDrag;
                    }
                }
            }
            catch
            {

            }
            
        }

        // Token: 0x06000179 RID: 377 RVA: 0x0001631A File Offset: 0x0001451A
        public void Awake()
        {
            this.BB = base.GetComponent<BlockBehaviour>();
        }
        // Token: 0x0600017A RID: 378 RVA: 0x0001632C File Offset: 0x0001452C
        public void FixedUpdate()
        {
            renewtime += Time.fixedDeltaTime;
            
            bool flag = !this.BB;
            if (flag)
            {
                this.BB = base.GetComponent<BlockBehaviour>();
            }
            bool isSimulating = this.BB.isSimulating;
            if (isSimulating)
            {
                bool isMP = StatMaster.isMP;
                if (isMP)
                {
                    bool flag2 = !StatMaster.isClient;
                    if (flag2)
                    {
                        this.addDrag();
                    }
                }
                else
                {
                    this.addDrag();
                }
            }
        }

        // Token: 0x04000192 RID: 402
        public float InitialDrag;

        // Token: 0x04000193 RID: 403
        public float InitialAngularDrag;

        // Token: 0x04000194 RID: 404
        public float UnderWaterDrag = 0.98f;
        public float renewtime;
        // Token: 0x04000195 RID: 405
        public float UnderWaterAngularDrag = 3f;

        // Token: 0x04000196 RID: 406
        public Rigidbody rigid;

        // Token: 0x04000197 RID: 407
        public BlockBehaviour BB;
    }
}
