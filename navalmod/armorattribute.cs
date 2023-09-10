using UnityEngine;

namespace Navalmod
{
    public class armorattribute
    {
        public float Tensile;//弹性限度 Tensile/2
        public float Yield;//抗张力 Yield
        public float EL;//延展性 % EL
        public float RA;//收缩性 % RA	
        public float Brinell;//布氏硬度
        //弹性限度决定装甲疲劳前能承受的最大力，抗张力决定装甲断裂前能承受的损伤，延展性与收缩性决定超过弹性限度时抗炮弹冲击造成受损的能力，布氏硬度决定装甲厚度等效
    }
}