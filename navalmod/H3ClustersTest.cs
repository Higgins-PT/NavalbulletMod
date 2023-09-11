using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static SurfaceInfo;

namespace Navalmod
{
    public class H3ClustersTest :MonoBehaviour
    {
        public BlockBehaviour ClusterBaseBlock;
        Quaternion rotLast;
        public bool send;
        public float time;
        public GameObject targetobj;

        public void FixedUpdate()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = SingleInstance<H3NetworkManager>.Instance.rateSend;
                Quaternion n = GetLocalRotation();
                if (Quaternion.Angle(n, rotLast) > 1f)
                {
                    send = true;
                    rotLast = n;
                }
            }
            
        }
        public Quaternion GetLocalRotation()
        {
            if (targetobj==null) {
                targetobj = new GameObject("targetobj");
            }
            targetobj.transform.SetParent(null);
            targetobj.transform.rotation = ClusterBaseBlock.transform.rotation;
            targetobj.transform.parent = base.transform;
            Quaternion qua = targetobj.transform.localRotation;
            //Destroy(targetobj);
            return qua;
        }
    }
}
