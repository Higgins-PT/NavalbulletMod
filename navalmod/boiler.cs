using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
using Selectors;
using UnityEngine.SocialPlatforms;
namespace Navalmod
{
    public class boiler : BlockScript
    {
        public MKey engine_power_up;
        public MMenu engine_type;
        public MKey engine_power_down;
        public MSlider engine_hp;
        public MSlider ship_speed;
        public int engine_SetSpeed;
        public float engine_WillProvideHp;
        public float engine_provideHp;
        public float renewtime;
        public Navalhe Navalhe;
        public bool first;
        public float maxengine_hp=0f;
        public List<float> engine_type_acceleration = new List<float>() {1f,1f,1f,1f,1.5f,1.6f,3f,1.65f,0.75f,1.45f};
        public MText engineInterface;
        public bool destoryed;
        public float speedscale;
        
        // Use this for initialization
        void Start()
        {

        }
        public void Awake()
        {
            engine_type = base.AddMenu("engine_type", 0, new List<string>(){"往复式蒸汽发动机", "三胀式蒸汽轮机", "多胀式蒸汽轮机", "多胀式蒸汽轮机", "涡轮蒸汽轮机", "齿轮传动蒸汽轮机", "涡轮电传动蒸汽轮机", "双齿轮传动蒸汽轮机","柴油轮机","燃气轮机" }, false) ;
            engine_power_up = base.AddKey("引擎档位提高","engine_power_up", KeyCode.UpArrow);
            engine_power_down = base.AddKey("引擎档位下降", "engine_power_down", KeyCode.DownArrow);
            engine_hp = base.AddSlider("马力","engine_hp",1000f,1f,300000f);
            ship_speed = base.AddSlider("最高速度(kt)", "ship_speed", 20f, 1f, 60f);
            engineInterface = base.AddText("传动轴编号", "engineInterface", "engine");
            engine_SetSpeed = 0;
            renewtime = 0f;
            first = true;
            destoryed = false;
            Navalhe = FindObjectOfType<Navalhe>();
        }
        public void HPdown(float hp)
        {
            maxengine_hp-=hp;
            if (maxengine_hp <= 0)
            {
                maxengine_hp = 0;
                if (!destoryed)
                {
                    destoryed = true;
                    SingleInstance<waterE>.Instance.EmitSparks(base.transform.position, engine_hp.Value/100f, false);



                }
            }

        }
        public void FixedUpdate()
        {
            if (IsSimulating)
            {
                renewtime += Time.fixedDeltaTime;
                if (first)
                {
                    maxengine_hp = engine_hp.Value;
                    first = false;
                    destoryed = false;
                }
                if (renewtime > 0.1f)
                {
                    try
                    {
                        RenewProvideSpeed();
                        
                        float updatespeed = 10f * engine_type_acceleration[engine_type.Value];
                        if (Navalhe.highspeed)
                        {
                            updatespeed*=30f;
                        }
                        renewtime = 0f;
                        Vector3 vector3 = new Vector3(base.Rigidbody.velocity.x, 0f, base.Rigidbody.velocity.z);

                        float speed = vector3.magnitude;
                        speedscale = speed * 1.944f / ship_speed.Value * 3f;
                        if (engine_SetSpeed>=0)
                        {
                            if (engine_provideHp < 0)
                            {
                                engine_provideHp += updatespeed;
                            }
                            else
                            {

                            
                            if (speed * 1.944f > ship_speed.Value * engine_SetSpeed)
                            {

                                engine_provideHp -= updatespeed;

                            }
                            else
                            {
                                if (engine_provideHp < engine_WillProvideHp)
                                {
                                    engine_provideHp += updatespeed;
                                }
                            }
                            if (engine_provideHp > engine_WillProvideHp)
                            {
                                engine_provideHp = engine_WillProvideHp;
                            }
                            }
                        }
                        else
                        {
                            if (engine_provideHp > 0)
                            {
                                engine_provideHp -= updatespeed;
                            }
                            else
                            {
                                if (speed * 1.944f > -ship_speed.Value * engine_SetSpeed)
                                {

                                    engine_provideHp += updatespeed;

                                }
                                else
                                {
                                    if (engine_provideHp < engine_WillProvideHp)
                                    {
                                        engine_provideHp = engine_WillProvideHp;
                                    }
                                }
                                if (engine_provideHp > engine_WillProvideHp)
                                {
                                    engine_provideHp -= updatespeed;
                                }
                            }
                        }
                        
                        
                    }
                    catch
                    {

                    }
                }
            }
        }
        // Update is called once per frame
        public void RenewProvideSpeed()
        {
            engine_WillProvideHp = (maxengine_hp / 3f) * engine_SetSpeed;
        }
        void Update()
        {
            if (IsSimulating)
            {
                if (engine_power_up.IsPressed|| engine_power_up.EmulationPressed())
                {
                    if (engine_SetSpeed<3)
                    {
                        engine_SetSpeed++;
                    }
                    RenewProvideSpeed();
                }
                if (engine_power_down.IsPressed || engine_power_down.EmulationPressed())
                {
                    if (engine_SetSpeed > -1)
                    {
                        engine_SetSpeed--;
                    }
                    RenewProvideSpeed();
                }

            }
        }
    }
}