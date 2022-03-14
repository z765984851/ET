using UnityEngine;

namespace ET
{
    public class PlayerMoveComponent: Entity,IAwake
    {
        /// <summary>
        /// 重力
        /// </summary>
        public int Mass;
   
        /// <summary>
        /// 当前x轴速度,发生变化
        /// </summary>
        public int CurrentXSpeed=0;
  
        /// <summary>
        /// 当前z轴速度,不发生变化
        /// </summary>
        public int CurrentZSpeed=5;
        
        /// <summary>
        /// 加速度
        /// </summary>
        public int Acceleration=1;

        /// <summary>
        /// 最大速度
        /// </summary>
        public int MaxSpeed=5;
        
        /// <summary>
        /// 上一帧所在位置
        /// </summary>
        public Vector3 LastFramePos;

   
        /// <summary>
        /// 是否正在碰撞
        /// </summary>
        public bool IsCollision;


        /// <summary>
        /// 当前所在位置
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// 当前旋转信息
        /// </summary>
        public Quaternion Rotation;

        /// <summary>
        /// 当前大小信息
        /// </summary>
        public Vector3 LocalScale;
        
        
        
    }
}