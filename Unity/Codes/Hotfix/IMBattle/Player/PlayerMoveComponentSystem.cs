using UnityEngine;

namespace ET
{
    public static class PlayerMoveComponentSystem
    {
        /// <summary>
        /// 更新物体坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pos"></param>
        public static void UpdatePos(this PlayerMoveComponent self,Vector3 pos)
        {
            self.LastFramePos = self.Position;
            self.Position = pos;
            Game.EventSystem.Publish(new EventType.PlayerMove(){TargetPos = pos,Unit = self.GetParent<Unit>()});
        }
        

        /// <summary>
        /// 设置配装状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isCollision"></param>
        public static void SetCollision(this PlayerMoveComponent self,bool isCollision)
        {
            self.IsCollision = isCollision;
        }
        

        /// <summary>
        /// 获取速度方向向量
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector3 GetSpeedVector(this PlayerMoveComponent self)
        {
            return self.Position - self.LastFramePos;
        }
        
        public static void SpeedUp_Left(this PlayerMoveComponent self)
        {
            int currentSpeed = self.CurrentXSpeed- self.Acceleration;
            if (currentSpeed<-self.MaxSpeed)
            {
                currentSpeed = -self.MaxSpeed;
            }
 
            self.CurrentXSpeed = currentSpeed;
        }
        public static void SpeedUp_Right(this PlayerMoveComponent self)
        {
            int  currentSpeed = self.CurrentXSpeed+ self.Acceleration;
            if (Mathf.Abs(currentSpeed)>self.MaxSpeed)
            {
                currentSpeed = self.MaxSpeed;
            }

            self.CurrentXSpeed = currentSpeed;
        }

        public static void SpeedDown(this PlayerMoveComponent self)
        {
            int currentSpeed = self.CurrentXSpeed;
            if (currentSpeed>0)
            {
                currentSpeed -= self.Acceleration;
            }
            else if (currentSpeed<0)
            {
                currentSpeed += self.Acceleration;
            }
            currentSpeed = Mathf.Clamp(Mathf.Abs(currentSpeed), 0, self.MaxSpeed);
            self.CurrentXSpeed = currentSpeed;
        }
        
        /// <summary>
        /// 输入向前移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Forward(this PlayerMoveComponent self)
        {
            Vector3 target = self.Position + Vector3.forward * self.CurrentZSpeed/10;
            self.UpdatePos(target);
        }
        /// <summary>
        /// 输入向后移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Back(this PlayerMoveComponent self)
        {
            Vector3 target = self.Position + Vector3.back * self.CurrentZSpeed/10;
            self.UpdatePos(target);
        }
        
        /// <summary>
        /// 输入向左移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Left(this PlayerMoveComponent self)
        {
            self.SpeedUp_Left();
            Vector3 target = self.Position + Vector3.right * self.CurrentXSpeed/10;
            self.UpdatePos(target);
        }
        
        /// <summary>
        /// 输入向右移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Right(this PlayerMoveComponent self)
        {
            self.SpeedUp_Right();
            Vector3 target = self.Position + Vector3.right * self.CurrentXSpeed/10;
            self.UpdatePos(target);
        }

        
        
        
    }
}