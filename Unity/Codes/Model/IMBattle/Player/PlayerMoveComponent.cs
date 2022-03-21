using UnityEngine;

namespace ET
{
    public class PlayerMoveComponent: Entity,IAwake
    {
       
   
        /// <summary>
        /// 当前x轴速度,发生变化,需要除以1000
        /// </summary>
        public int CurrentXSpeed=0;
        

        /// <summary>
        /// 当前z轴速度,不发生变化,需要除以1000
        /// </summary>
        public int CurrentZSpeed = 100;
        
        /// <summary>
        /// 加速度
        /// </summary>
        public int Acceleration = 10;

        /// <summary>
        /// 最大速度
        /// </summary>
        public int MaxSpeed = 100;
        
        /// <summary>
        /// 上一帧所在位置
        /// </summary>
        public Vector3 LastFramePos;

   
        /// <summary>
        /// 是否正在碰撞
        /// </summary>
        public bool IsCollision;

        

        /// <summary>
        /// 碰撞持续时间 单位ms
        /// </summary>
        public int CollisionTime=500;


        /// <summary>
        /// 碰撞后需要位移的数据 方向x长度
        /// </summary>
        public Vector3 CollisionData;
        
        /// <summary>
        /// 是否正在跳跃
        /// </summary>
        public bool IsJump;
        
        /// <summary>
        /// 跳跃持续时间 单位ms
        /// </summary>
        public int JumpTime=500;


        /// <summary>
        /// 碰撞后需要位移的数据 方向x长度
        /// </summary>
        public Vector3 JumpData;


        /// <summary>
        /// 是否不可再向左边移动
        /// </summary>
        public bool LeftLimit=false;
        /// <summary>
        /// 是否不可再向右边移动
        /// </summary>
        public bool RightLimit=false;
        /// <summary>
        /// 是否不可再向前边移动
        /// </summary>
        public bool ForwardLimit=false;
        /// <summary>
        /// 是否不可再向后边移动
        /// </summary>
        public bool BackLimit=false;

    }
}