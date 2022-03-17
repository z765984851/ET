using System.Timers;
using DG.Tweening;
using UnityEngine;

namespace ET
{
     public class PlayerControllerComponentAwakeSystem : AwakeSystem<PlayerControllerComponent>
     {
         public override void Awake(PlayerControllerComponent self)
         {
             self.Unit = self.GetParent<Unit>();
             self.CanCtrl = true;
             
         }
     }

  
    
     
    public static class PlayerControllerComponentSystem
    {
        public static void PlayerController(this PlayerControllerComponent self)
        {
            PlayerMoveComponent moveComponent = self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent==null)
            {
                // Log.Debug("没有获取到移动组件");
                return;
            }
            if (!moveComponent.IsCollision)
            {
                if (Input.GetKey(self.ForwardKey))
                {
               
                    if (self.CanCtrl)
                    {
                        moveComponent.InputMove_Forward();
                    }
                                               
                }
                if (Input.GetKey(self.BackKey))
                {
                    if (self.CanCtrl)
                    {
                        moveComponent.InputMove_Back();
                    }
                }
                                               
                if (Input.GetKey(self.LeftKey))
                {
                                       
                    if (self.CanCtrl)
                    {
                        moveComponent.InputMove_Left();
                    }
                }
                else if (Input.GetKey(self.RightKey))
                {
                                       
                    if (self.CanCtrl)
                    {
                        moveComponent.InputMove_Right();
                    }
                }
                else
                {
                    moveComponent.SpeedDown();
                             
                }
            }
            else
            {
                //碰撞演出
                Vector3 targetPos = moveComponent.Position + moveComponent.CollisionData * (Time.fixedDeltaTime / 0.3f);
                moveComponent.SetPos(targetPos);
                Debug.Log($"碰撞播放{self.Unit.ConfigId},{targetPos}");
            }

           
           
        }

        public static void ColliderDisplay(this PlayerControllerComponent self,float distance,Vector3 direction)
        {
            
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent!=null)
            {

                moveComponent.CollisionData = direction * distance;
                self.ColliderTimer().Coroutine();
            }
        }

        public static async ETTask ColliderTimer(this PlayerControllerComponent self)
        {
            
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            moveComponent?.SetCollision(true);
            self.CanCtrl = false;
            await TimerComponent.Instance.WaitAsync(moveComponent.CollisionTime);
            self.CanCtrl = true;
            moveComponent?.SetCollision(false);
        }
    }
}