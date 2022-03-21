using UnityEngine;

namespace ET
{
    /// <summary>
    /// 赋值时 请将实际值*1000
    /// </summary>
    public class TransformEulerAngleComponent : Entity ,IAwake
    {
        
        public int X;
        public int Y;
        public int Z;
        
        public Vector3 EulerAngle
        {
            get
            {
                return new Vector3((float)X/1000,(float)this.Y/1000,(float)this.Z/1000);
            }
        }
        
    }
}