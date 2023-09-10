using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using System.Reflection;
namespace Navalmod
{
    public class sqrballoonfloat : MonoBehaviour
    {
        
        public Navalhe navalhe;
        
        public MMenu menuE;
        public MSlider drag;
        public SqrBalloonController sqrBalloonController;

        private bool first;
        public float[] floatv = new float[7] { 0.1f, 0.08f, 0.05f, 0.03f, 0.01f,0.03f,0.03f };
        public float[] maxfloat = new float[7] { 1f, 0.8f, 0.5f, 0.3f, 0.1f,0.1f,0.5f };
        public float[] downfloat = new float[7] { 0f, 127f, 203f, 305f, 406f, 356f, 230f };
        public float drawingwmax;
        public float drawingv;
        public float floatmax;
        public float time;
        public MSlider floatmaxE;
        public MSlider massE;
        public void Awake()
        {
            sqrBalloonController = gameObject.GetComponent<SqrBalloonController>();
            navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            sqrBalloonController.popImpactThreshold = 10000000f;
            menuE= sqrBalloonController.AddMenu("typeE", 0, new List<string> { "无隔仓" , "小艇隔仓", "驱逐/护卫隔仓", "巡洋隔仓", "战列隔仓","战列巡洋舰隔仓","侦查巡洋舰隔仓"}, false);

            floatmaxE = sqrBalloonController.AddSlider("浮力储备(t)", "floatmax", 1f, 1f, 50000f, "", "x");
            massE = sqrBalloonController.AddSlider("质量", "massE", 5f, 1f, 100f, "", "x");
            first = false;
            
        }
        /*public static void SetPrivateField(object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(instance, value);
        }*/

        public float floating(float size,float tnt)
        {
            
            drawingwmax += Mathf.Pow(size,2f) * Mathf.Pow(tnt,0.3f)/15f* maxfloat[menuE.Value];
            if (size < downfloat[menuE.Value])
            {
                drawingwmax = drawingwmax * (size/downfloat[menuE.Value]);
                return (drawingwmax - floatmaxE.Value) /(maxfloat[menuE.Value]*(size / downfloat[menuE.Value]));
            }
            return (drawingwmax - floatmaxE.Value) / maxfloat[menuE.Value];
         
        }
        public float floatingE(float size, float tnt)
        {

            drawingwmax += Mathf.Pow(size, 0.3f) * Mathf.Pow(tnt, 0.5f)*10* maxfloat[menuE.Value];

            return (drawingwmax - floatmaxE.Value) / maxfloat[menuE.Value];
        }
        public float floatingT(float tnt)
        {

            drawingwmax += Mathf.Pow(tnt,1.5f) * 10f * maxfloat[menuE.Value];

            return (drawingwmax - floatmaxE.Value) / maxfloat[menuE.Value];
        }
        public float floatingEE(float size)
        {

            drawingwmax += size * maxfloat[menuE.Value];
            if (size < downfloat[menuE.Value])
            {
                drawingwmax = drawingwmax * (size / downfloat[menuE.Value]);
                return (drawingwmax - floatmaxE.Value) / (maxfloat[menuE.Value] * (size / downfloat[menuE.Value]));
            }
            return (drawingwmax - floatmaxE.Value) / maxfloat[menuE.Value];
        }
        public void FixedUpdate()
        {
            if (!first)
            {
                first = true;
                try {
                    Destroy(sqrBalloonController.fireTag);
                }
                catch
                {

                }
            }
            try
            {
                if (sqrBalloonController.isSimulating)
                {
                    time += Time.fixedDeltaTime;
                    if (time >= 1f)
                    {
                        time = 0f;
                        floatmax += drawingwmax * floatv[menuE.Value];
                        if (floatmax >= drawingwmax)
                        {
                            floatmax = drawingwmax;
                        }
                        if (floatmax >= floatmaxE.Value)
                        {
                            sqrBalloonController.Pop();
                        }
                    }
                }
            }
            catch
            {

            }

        }
        public void Start()
        {
            sqrBalloonController.blockJoint.breakForce = 1000000000000000000f;
            sqrBalloonController.blockJoint.breakTorque = 1000000000000000f;
            sqrBalloonController.Rigidbody.mass = massE.Value;
            time = 0f;
            drawingwmax = 0f;
            floatmax = 0f;
        }
    }
}
