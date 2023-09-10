using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using Modding;
using Modding.Blocks;
using UnityEngine;
using Selectors;

namespace Navalmod
{
    public class PID
    {
        private float setPoint;  //设置点
        private float measuredPoint; //测量点​
       private float integral;//积分器​
        private float previous_error; //上一次的误差
        private float now_error; //本次误差​
        private float Kp;
        private float Ki;
        private float Kd;
        private float minLimit;  //输出量的下限
        private float maxLimit;  //输出量的上限​
                                 //构造方法，用来初始化对象
        public PID(float Kp, float Ki, float Kd, float minLimit, float maxLimit)
        {
            //将初始化的数据公开到对象上，以便各个方法使用
            this.Kp = Kp;
            this.Ki = Ki;
            this.Kd = Kd;
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
        public void RenewCalculData(float setPoint, float measuredPoint)
        {
            this.setPoint = setPoint;
            this.measuredPoint = measuredPoint;
        }
        //PID算法核心计算
        public float Calcul()
        {
            /***********位置式PID算法实现，确切说是PD算法***********/
            now_error = setPoint - measuredPoint; //计算得到偏差
            integral += now_error;
            float Output = Kp * now_error + Ki * integral + Kd * (now_error - previous_error);//位置式PID控制器
            Output = Mathf.Clamp(Output, this.minLimit, this.maxLimit);
            previous_error = now_error;
        return Output;
        }
}
}