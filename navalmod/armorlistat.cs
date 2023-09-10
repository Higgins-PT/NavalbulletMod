using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Navalmod
{
    public class Armorlistat : SingleInstance<Armorlistat>
    {
        public override string Name => throw new NotImplementedException();
        public List<armorattribute> armorattributesZJ = new List<armorattribute>();
        public List<armorattribute> armorattributesJG = new List<armorattribute>();
        public List<armorattribute> armorattributesZX = new List<armorattribute>();
        //装甲钢
        armorattribute 高镍装甲钢 = new armorattribute(); 
        armorattribute 生铁 = new armorattribute();
        armorattribute 克虏伯低镍装甲钢 = new armorattribute();
        armorattribute KNC = new armorattribute();
        armorattribute Wh = new armorattribute();
        armorattribute Ww = new armorattribute();
        armorattribute Wsh = new armorattribute();
        armorattribute KC = new armorattribute();
        armorattribute VC = new armorattribute();
        armorattribute NVNC = new armorattribute();
        armorattribute VH = new armorattribute();
        armorattribute MNC = new armorattribute();
        armorattribute CNC = new armorattribute();
        armorattribute ClassA = new armorattribute();
        armorattribute ClassB = new armorattribute();
        armorattribute NCA = new armorattribute();
        armorattribute AOD = new armorattribute();
        armorattribute CA = new armorattribute();
        armorattribute BTC = new armorattribute();
        
        //结构钢
        armorattribute HT = new armorattribute();
        armorattribute HTT = new armorattribute();
        armorattribute DS = new armorattribute();
        armorattribute STS = new armorattribute();
        armorattribute Schiffbaustahl = new armorattribute();
        armorattribute Schiffbaustahl_1 = new armorattribute();
        armorattribute Schiffbaustahl_2 = new armorattribute();
        armorattribute Schiffbaustahl_3 = new armorattribute();
        armorattribute Schiffbaustahl_42 = new armorattribute();
        armorattribute Schiffbaustahl_52 = new armorattribute();
        armorattribute 克虏伯低镍结构钢 = new armorattribute();
        armorattribute 中镍结构钢 = new armorattribute();
        //杂项装甲
        armorattribute 木头 = new armorattribute();
        armorattribute 飞机蒙皮 = new armorattribute();
        armorattribute 钢筋混凝土 = new armorattribute();
        public void Start()
        {
            //装甲钢
            armorattributesZJ.Add(高镍装甲钢);
            armorattributesZJ.Add(生铁);
            armorattributesZJ.Add(克虏伯低镍装甲钢);
            armorattributesZJ.Add(KNC);
            armorattributesZJ.Add(Wh);
            armorattributesZJ.Add(Ww);
            armorattributesZJ.Add(Wsh);
            armorattributesZJ.Add(KC);
            armorattributesZJ.Add(VC);
            armorattributesZJ.Add(NVNC);
            armorattributesZJ.Add(VH);
            armorattributesZJ.Add(MNC);
            armorattributesZJ.Add(CNC);
            armorattributesZJ.Add(ClassA);
            armorattributesZJ.Add(ClassB);
            armorattributesZJ.Add(NCA);
            armorattributesZJ.Add(AOD);
            armorattributesZJ.Add(CA);
            armorattributesZJ.Add(BTC);
            高镍装甲钢.Tensile = 113f;
            高镍装甲钢.Yield = 78f;
            高镍装甲钢.EL = 20f;
            高镍装甲钢.RA = 60f;
            高镍装甲钢.Brinell = 220f;

            生铁.Tensile = 42f;
            生铁.Yield = 26f;
            生铁.EL = 42f;
            生铁.RA = 72f;
            生铁.Brinell = 80f;

            克虏伯低镍装甲钢.Tensile = 73f;
            克虏伯低镍装甲钢.Yield = 45f;
            克虏伯低镍装甲钢.EL = 21f;
            克虏伯低镍装甲钢.RA = 65f;
            克虏伯低镍装甲钢.Brinell = 150f;

            KNC.Tensile = 113f;
            KNC.Yield = 78f;
            KNC.EL = 20f;
            KNC.RA = 60f;
            KNC.Brinell = 220f;

            Wh.Tensile = 127f;
            Wh.Yield = 79f;
            Wh.EL = 18f;
            Wh.RA = 60f;
            Wh.Brinell = 250f;

            Ww.Tensile = 117f;
            Ww.Yield = 68f;
            Ww.EL = 22f;
            Ww.RA = 65f;
            Ww.Brinell = 180f;

            Wsh.Tensile = 142f;
            Wsh.Yield = 92f;
            Wsh.EL = 16f;
            Wsh.RA = 53f;
            Wsh.Brinell = 280f;

            KC.Tensile = 105f;
            KC.Yield = 71f;
            KC.EL = 22f;
            KC.RA = 59f;
            KC.Brinell = 225f;

            VC.Tensile = 113f;
            VC.Yield = 64f;
            VC.EL = 20f;
            VC.RA = 58f;
            VC.Brinell = 210f;

            NVNC.Tensile = 110f;
            NVNC.Yield = 85f;
            NVNC.EL = 20f;
            NVNC.RA = 60f;
            NVNC.Brinell = 220f;

            VH.Tensile = 106f;
            VH.Yield = 82f;
            VH.EL = 21f;
            VH.RA = 57f;
            VH.Brinell = 235f;

            MNC.Tensile = 100f;
            MNC.Yield = 60f;
            MNC.EL = 26f;
            MNC.RA = 66f;
            MNC.Brinell = 200f;

            CNC.Tensile = 122f;
            CNC.Yield = 85f;
            CNC.EL = 22f;
            CNC.RA = 58f;
            CNC.Brinell = 225f;

            ClassA.Tensile = 114f;
            ClassA.Yield = 92f;
            ClassA.EL = 29f;
            ClassA.RA = 72f;
            ClassA.Brinell = 220f;

            ClassB.Tensile = 120f;
            ClassB.Yield = 98f;
            ClassB.EL = 25f;
            ClassB.RA = 66f;
            ClassB.Brinell = 240f;

            NCA.Tensile = 120f;
            NCA.Yield = 85f;
            NCA.EL = 25f;
            NCA.RA = 60f;
            NCA.Brinell = 225f;

            AOD.Tensile = 116.6f;
            AOD.Yield = 110.7f;
            AOD.EL = 15f;
            AOD.RA = 38f;
            AOD.Brinell = 281f;

            CA.Tensile = 120f;
            CA.Yield = 85f;
            CA.EL = 25f;
            CA.RA = 60f;
            CA.Brinell = 225f;

            BTC.Tensile = 100f;
            BTC.Yield = 75f;
            BTC.EL = 26f;
            BTC.RA = 60f;
            BTC.Brinell = 200f;
            //结构钢
            armorattributesJG.Add(HT);
            armorattributesJG.Add(HTT);
            armorattributesJG.Add(DS);
            armorattributesJG.Add(STS);
            armorattributesJG.Add(Schiffbaustahl);
            armorattributesJG.Add(Schiffbaustahl_1);
            armorattributesJG.Add(Schiffbaustahl_2);
            armorattributesJG.Add(Schiffbaustahl_3);
            armorattributesJG.Add(Schiffbaustahl_42);
            armorattributesJG.Add(Schiffbaustahl_52);
            armorattributesJG.Add(克虏伯低镍结构钢);
            armorattributesJG.Add(中镍结构钢);
            HT.Tensile = 78f;
            HT.Yield = 47f;
            HT.EL = 22f;
            HT.RA = 68f;
            HT.Brinell = 160f;

            HTT.Tensile = 78f;
            HTT.Yield = 47f;
            HTT.EL = 22f;
            HTT.RA = 68f;
            HTT.Brinell = 160f;

            DS.Tensile = 89f;
            DS.Yield = 55f;
            DS.EL = 22f;
            DS.RA = 64f;
            DS.Brinell = 170f;

            STS.Tensile = 125f;
            STS.Yield = 85f;
            STS.EL = 25f;
            STS.RA = 68f;
            STS.Brinell = 240f;

            Schiffbaustahl.Tensile = 53f;
            Schiffbaustahl.Yield = 28f;
            Schiffbaustahl.EL = 16f;
            Schiffbaustahl.RA = 58f;
            Schiffbaustahl.Brinell = 120f;

            Schiffbaustahl_1.Tensile = 58f;
            Schiffbaustahl_1.Yield = 28f;
            Schiffbaustahl_1.EL = 16f;
            Schiffbaustahl_1.RA = 58f;
            Schiffbaustahl_1.Brinell = 120f;

            Schiffbaustahl_2.Tensile = 68f;
            Schiffbaustahl_2.Yield = 34f;
            Schiffbaustahl_2.EL = 22f;
            Schiffbaustahl_2.RA = 65f;
            Schiffbaustahl_2.Brinell = 140f;

            Schiffbaustahl_3.Tensile = 80f;
            Schiffbaustahl_3.Yield = 51f;
            Schiffbaustahl_3.EL = 18f;
            Schiffbaustahl_3.RA = 60f;
            Schiffbaustahl_3.Brinell = 160f;

            Schiffbaustahl_42.Tensile = 50f;
            Schiffbaustahl_42.Yield = 34f;
            Schiffbaustahl_42.EL = 18f;
            Schiffbaustahl_42.RA = 63f;
            Schiffbaustahl_42.Brinell = 140f;

            Schiffbaustahl_52.Tensile = 74f;
            Schiffbaustahl_52.Yield = 51f;
            Schiffbaustahl_52.EL = 18f;
            Schiffbaustahl_52.RA = 58f;
            Schiffbaustahl_52.Brinell = 150f;

            克虏伯低镍结构钢.Tensile = 73f;
            克虏伯低镍结构钢.Yield = 45f;
            克虏伯低镍结构钢.EL = 21f;
            克虏伯低镍结构钢.RA = 65f;
            克虏伯低镍结构钢.Brinell = 150f;

            中镍结构钢.Tensile = 90f;
            中镍结构钢.Yield = 60f;
            中镍结构钢.EL = 19f;
            中镍结构钢.RA = 45f;
            中镍结构钢.Brinell = 180f;
            //杂项
            armorattributesZX.Add(木头);
            armorattributesZX.Add(飞机蒙皮);
            armorattributesZX.Add(钢筋混凝土);
            木头.Tensile = 90f;
            木头.Yield = 30f;
            木头.EL = 10f;
            木头.RA = 20f;
            木头.Brinell = 10f;

            飞机蒙皮.Tensile = 90f;
            飞机蒙皮.Yield = 60f;
            飞机蒙皮.EL = 100f;
            飞机蒙皮.RA = 100f;
            飞机蒙皮.Brinell = 10f;

            钢筋混凝土.Tensile = 90f;
            钢筋混凝土.Yield = 60f;
            钢筋混凝土.EL = 5f;
            钢筋混凝土.RA = 10f;
            钢筋混凝土.Brinell = 40f;
        }
    }
}