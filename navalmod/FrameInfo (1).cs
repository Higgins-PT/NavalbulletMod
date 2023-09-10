using System;
using UnityEngine;

namespace Navalmod
{
    internal class FrameInfo
    {
        public string Time { get; set; }
        public int NetWorkId { get; set; }
        //public string SteamId { get; set; }
        public int CannonCount { get; set; }
        public int BlockCount { get; set; }
        public int Ping { get; set; }
        public Vector3 Size { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Mass { get; set; }
        public float Health { get; set; }
    }
}
