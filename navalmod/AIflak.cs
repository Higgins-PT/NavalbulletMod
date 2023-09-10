using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
namespace Navalmod
{
    public class AIflak : SingleInstance<AIflak>
    {
        public override string Name { get; } = "114";
        public void Start()
        {


        }
        public List<flakN> flakNs=new List<flakN>();

        public AIflak()
        {
        }

        public void Addview(string name,uint play,view view)
        {
            try
            {
                foreach (flakN flakN in flakNs)
                {
                    if (flakN.name == name && flakN.play == play)
                    {
                        flakNs.Remove(flakN);
                    }
                }
            }
            catch
            {

            }
            flakN flakNS = new flakN();
            flakNS.name = name;
            flakNS.play = play;
            flakNS.view = view;
            flakNs.Add(flakNS);
        }
    }
}
