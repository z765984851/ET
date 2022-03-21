using System;
using UnityEngine;

namespace ET
{
    public static class PlayerMoveComponentSystem
    {
        /// <summary>
        /// 通过物体身上的TransformPosition组件更新坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pos"></param>
        public static void UpdatePos(this PlayerMoveComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            if (unit!=null)
            {
                var posComponent = unit.GetComponent<TransformPositionComponent>();
                if (posComponent!=null)
                {
                    Game.EventSystem.Publish(new EventType.PlayerMove(){TargetPos = posComponent.Position,Unit = self.GetParent<Unit>()});
                }
            }
        }
        

       
        
        /// <summary>
        /// 设置碰撞状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isCollision"></param>
        public static void SetCollision(this PlayerMoveComponent self,bool isCollision)
        {
            self.IsCollision = isCollision;
        }


        public static void SetJump(this PlayerMoveComponent self,bool isJump)
        {
            self.IsJump = isJump;
        }

        /// <summary>
        /// 获取速度方向向量
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector3 GetSpeedVector(this PlayerMoveComponent self)
        {
            Vector3 v=Vector3.zero;
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                if (posComp!=null)
                {
                    v = posComp.Position - self.LastFramePos;
                    
                }
            }
            return v;
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
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                if (posComp!=null)
                {
                    if (!self.ForwardLimit)
                    {
                        posComp.Z += self.CurrentZSpeed;
                    }
                   
                    self.UpdatePos();
                }
            }
           
        }
        /// <summary>
        /// 输入向后移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Back(this PlayerMoveComponent self)
        {
          
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                if (posComp!=null)
                {
                    // self.LastFramePos = posComp.Position;
                    if (!self.BackLimit)
                    {
                        posComp.Z -= self.CurrentZSpeed;
                    }
                  
                    self.UpdatePos();
                }
            }
        }
        
        /// <summary>
        /// 输入向左移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Left(this PlayerMoveComponent self)
        {
           
           
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                if (posComp!=null)
                {
                    self.SpeedUp_Left();
                    if (!self.LeftLimit)
                    {
                        posComp.X += self.CurrentXSpeed;
                    }
                    
                    self.UpdatePos();
                }
            }
           
            
        }
        
        /// <summary>
        /// 输入向右移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void InputMove_Right(this PlayerMoveComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                if (posComp!=null)
                {
                    self.SpeedUp_Right();
                    if (!self.RightLimit)
                    {
                        posComp.X += self.CurrentXSpeed;
                    }
                    self.UpdatePos();
                }
            }
        }
        
        

        /// <summary>
        /// 输入向右移动的按键
        /// </summary>
        /// <param name="self"></param>
        public static void Drop(this PlayerMoveComponent self)
        {
            
            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                var posComp = unit.GetComponent<TransformPositionComponent>();
                var dropComp = unit.GetComponent<DropComponent>();
               
                if (posComp!=null && dropComp!=null)
                {
                    
                    // self.LastFramePos = posComp.Position;
                    dropComp.Drop();
                    self.UpdatePos();
                }
            }
        }
        
        /// <summary>
        /// 获取跳跃的数据
        /// </summary>
        /// <param name="self"></param>
        public static void Jump(this PlayerMoveComponent self)
        {

            int distance = Math.Abs(self.CurrentXSpeed * self.JumpTime) ;
            Vector3 dir = self.GetSpeedVector().normalized;
            self.JumpData = dir * distance/15000;
            
        }
        
    }
}