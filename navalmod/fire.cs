using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using System.Reflection;
namespace Navalmod
{
    class fire : BlockBehaviour

    {
        public MSlider count;
        public MSlider reloding;
        public MSlider back;
        public MSlider scale;
        public Navalhe navalhe;
        public FlamethrowerController arrowTurret;
        public float time;
        public int counta;
        public bool first = false;
        public bool firee;
        public MSlider laught;
        
        public void Update()
        {

            if (StatMaster.InGlobalPlayMode || StatMaster.InLocalPlayMode || this.navalhe.open)
            {
                try
                {
                    
                    if (first)
                    {
                        
                        first = false;
  
                    }
                    if (arrowTurret.IgniteKey.IsHeld || arrowTurret.IgniteKey.EmulationHeld())
                    {
                        
                        firee = true;
                    }
                    if (arrowTurret.IgniteKey.IsHeld || arrowTurret.IgniteKey.EmulationHeld())
                    {
                        if (counta <= 0 && firee == true)
                        {
                            time -= Time.deltaTime;
                            if (time < 0f)
                            {
                                firee = false;
                                time = reloding.Value / navalhe.retime;
                                int a = (int)count.Value;


                                arrowTurret.OnReloadAmmo(ref a, AmmoType.All, true, false);
                            }
                        }
                    }
                    
                }
                catch
                {

                }

            }
        }
        List<Transform> spawnedProjectiles;
        public void Awake()
        {




            navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            arrowTurret = base.GetComponent<FlamethrowerController>();

            count = arrowTurret.AddSlider("燃烧时间", "count", 400f, 1f, 500f, "", "x");
            reloding = arrowTurret.AddSlider("装填时间", "rel", 1f, 0.1f, 5f, "", "x");

            counta = 0;
            time = 0f;
            firee = true;
            first = true;
            
        }

    }
}
