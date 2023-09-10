using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
namespace Navalmod
{
    public class Bolt : MonoBehaviour
    {
       
        private void Update()
        {
            foreach(NavalCannoBlockE navalCannoBlockE in NavalCannoBlockEs) 
            {
                bool can=false;
                NAbolt NAboltE = new NAbolt();
                float jl = 100f;
                foreach (NAbolt nAbolt in Nablots)
                {
                    if ((nAbolt.transform.position - navalCannoBlockE.transform.position).magnitude<jl)
                    {
                        jl = (nAbolt.transform.position - navalCannoBlockE.transform.position).magnitude;
                        NAboltE = nAbolt;
                        can = true;
                    }
                    
                }
                if (can) {
                    NavalCannoBlockEs.Remove(navalCannoBlockE);
                    Nablots.Remove(NAboltE);
                    Rigidbody component = NAboltE.GetComponent<Rigidbody>();
                    component.mass = navalCannoBlockE.mass.Value;
                    NAboltE.tntmass = navalCannoBlockE.tntmass.Value;
                    component.drag = navalCannoBlockE.velocityattenuation.Value;
                    if (navalCannoBlockE.navalhe.wood)
                    {
                        NAboltE.transform.localScale = Vector3.one * navalCannoBlockE.scale.Value / 2500f;
                    }
                    else
                    {
                        NAboltE.transform.localScale = Vector3.one * navalCannoBlockE.scale.Value / 500f;
                    }
                    NAboltE.transform.localScale = Vector3.one * navalCannoBlockE.scale.Value / 500f;
                    NAboltE.blot = base.gameObject;
                    NAboltE.canexplo = true;
                    NAboltE.cjxs = 0f;

                    NAboltE.seahigh = navalCannoBlockE.seahigh;
                    NAboltE.asd = false;
                    NAboltE.boom = navalCannoBlockE.boom.IsActive;
                    NAboltE.type = navalCannoBlockE.type;
                    NAboltE.timer = 0f;
                    NAboltE.ycyx = false;
                }
            }
        }
        public bool addNavalCannoBlockE(NavalCannoBlockE navalCannoBlock)
        {
            bool isthere = false;
            foreach (NavalCannoBlockE navalCannoBlockEE in NavalCannoBlockEs)
            {
                if (navalCannoBlockEE == navalCannoBlock)
                {
                    isthere = true;
                }
            }
            if (!isthere)
            {
                NavalCannoBlockEs.Add(navalCannoBlock);
            }
            return isthere;
        }
        public bool addNablot(NAbolt Nabolt)
        {
            bool isthere = false;
            foreach (NAbolt nAbolt in Nablots)
            {
                if (Nabolt == nAbolt)
                {
                    isthere = true;
                }
            }
            if (!isthere)
            {
                Nablots.Add(Nabolt);
            }
            return isthere;
        }
        public List<NAbolt> Nablots = new List<NAbolt>();
        public List<NavalCannoBlockE> NavalCannoBlockEs=new List<NavalCannoBlockE>();

    }
    
}
