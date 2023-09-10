using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using System.Reflection;
namespace Navalmod
{
    class flakc:SingleInstance<flakc>
    {

        public override string Name { get; } = "flak";
        public List<BlockBehaviour> blockBehaviours;
        
    }
}
