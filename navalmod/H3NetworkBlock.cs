using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navalmod
{
    public class H3NetworkBlock : MonoBehaviour
    {
        public BlockBehaviour blockBehaviour;
        public Vector3 lastpos;
        public Quaternion lastqua;
        public Vector3 nowpos;
        public Quaternion nowqua;
        public Vector3 deltavec;
        public Rigidbody rb;
        public float time;
        public float maxtime;
        public bool haschange = false;
        public H3NetworkBlock()
        {
            lastpos = Vector3.zero;
            lastqua = Quaternion.identity;

        }
        public void PushObject(ref int offset, byte[] buffer)// byte[19]
        {
            H3NetCompression.CompressPosition(blockBehaviour.transform.position, buffer, offset);//12
            NetworkCompression.CompressRotation(blockBehaviour.transform.rotation, buffer,offset + 12);//7
            offset += 19;
        }
        public void PullObject(ref int offset, byte[] buffer)// byte[19]
        {
            Vector3 vector3;
            Quaternion quat;
            H3NetCompression.DecompressPosition(buffer, offset,out vector3);//12
            NetworkCompression.DecompressRotation(buffer, offset+12, out quat);//7
            lastpos = blockBehaviour.transform.position;
            lastqua = blockBehaviour.transform.rotation;
            nowpos = vector3;
            nowqua = quat;
            haschange = true;
            SmoothToPoint(SingleInstance<H3NetworkManager>.Instance.rateSend, nowpos, blockBehaviour.transform.position);


            offset += 19;
        }
        public void Update()
        {
            if (StatMaster.isClient)
            {
                time -= Time.deltaTime;

                if (blockBehaviour.isSimulating) {
                    if (haschange)
                    {
                        blockBehaviour.transform.position = (nowpos - lastpos).normalized * (maxtime - time) / maxtime + lastpos;
                        blockBehaviour.transform.rotation = Quaternion.Lerp(lastqua, nowqua, (maxtime - Math.Max(time, 0)) / maxtime);
                    }
                }
            }
        }
        public void SmoothToPoint(float time,Vector3 nowpos,Vector3 lastpos)
        {
            this.time = time;
            maxtime = time;
            deltavec = (nowpos - lastpos).normalized*time;
        }
    }
}
