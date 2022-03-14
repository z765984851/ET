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
            if (moveComponent.IsCollision)
            {
                // Log.Debug("正常碰撞");
                return;
            }

           
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

        public static void ColliderDisplay(this PlayerControllerComponent self,float distance,Vector3 direction)
        {
            
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent!=null)
            {
                GameObject gameObject = self.Unit.GetComponent<GameObjectComponent>().GameObject;
                if (gameObject!=null)
                {
                    var tranform = gameObject.transform;
                    var target = direction * distance + moveComponent.Position;
                    self.CanCtrl = false;
                    moveComponent.IsCollision = true;
                    tranform.DOMove(target,0.5f).OnComplete(() =>
                    {
                        self.CanCtrl = true;
                        moveComponent.IsCollision = false;
                    });
                }
                
            }

            
            
        }
    }
}