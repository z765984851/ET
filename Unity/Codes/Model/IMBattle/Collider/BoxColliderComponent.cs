using UnityEngine;

namespace ET
{
    public class BoxColliderComponent:Entity,IAwake
    {
        public Vector3 Center;
        public Quaternion Rotation;
        public Vector3 Extents;
    }
    
    
}