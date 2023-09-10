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
            blockBehaviour.transform.position = vector3;
            blockBehaviour.transform.rotation = quat;
            offset += 19;
        }
    }
}
