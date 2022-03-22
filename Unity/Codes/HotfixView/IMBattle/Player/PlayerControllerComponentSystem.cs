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
            var posComp = self.Unit.GetComponent<TransformPositionComponent>();
            if (posComp==null)
            {
                return;
            }

          
             
            //碰撞演出
            if (moveComponent.IsCollision)
            {
                //取消jump的计时器
                if (moveComponent.IsJump)
                {
                    
                }
                
                moveComponent.LastFramePos = posComp.Position;
                float delta = Time.fixedDeltaTime / (moveComponent.CollisionTime/(float)1000);
                Vector3 addData = moveComponent.CollisionData * delta;
                //碰撞演出
                Vector3 targetPos = posComp.Position + addData;
                moveComponent.MoveX(IMMathHelper.FloatToInt(targetPos.x*1000-posComp.X));
                moveComponent.MoveY(IMMathHelper.FloatToInt(targetPos.y*1000-posComp.Y));
                moveComponent.MoveZ(IMMathHelper.FloatToInt(targetPos.z*1000-posComp.Z));
               
                moveComponent.UpdatePos();
                // Debug.Log($"碰撞播放,{moveComponent.LastFramePos},  {targetPos}, {addData}");
            }
            //跳跃演出
            else if (moveComponent.IsJump)
            {
                // Log.Debug("Enter Jump");
                moveComponent.LastFramePos = posComp.Position;
                var dropComp = self.Unit.GetComponent<DropComponent>();
                if (dropComp!=null)
                {
                    //水平移动
                   
                    float delta = Time.fixedDeltaTime / (moveComponent.CollisionTime/(float)1000);
                    Vector3 addData = moveComponent.JumpData * delta;
                    Vector3 targetPos = posComp.Position + addData;
                   
                    moveComponent.MoveX(IMMathHelper.FloatToInt(targetPos.x*1000-posComp.X));
                    moveComponent.MoveZ(IMMathHelper.FloatToInt(targetPos.z*1000-posComp.Z));
                    //执行跳跃
                    if (self.IsJumpUp)
                    {
                        
                        dropComp.Jump();
                        dropComp.SetDrop(false);
                    }
                    moveComponent.UpdatePos();
                    
                }
                
            }
            //玩家操作
            else
            {
                
                
                if (Input.GetKey(self.JumpKey))
                {
                    if (self.CanCtrl)
                    {
                        DropComponent dropComponent = self.Unit.GetComponent<DropComponent>();
                        if (dropComponent!=null)
                        {
                           
                            moveComponent.Jump();
                            self.JumpDisplay();
                            moveComponent.LastFramePos = posComp.Position;
                        }
                        
                    }
                }
                else
                {
                    moveComponent.LastFramePos = posComp.Position;
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

            
            moveComponent.Drop();
           

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
        
        public static void JumpDisplay(this PlayerControllerComponent self)
        {
            
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent!=null)
            {
                self.JumpTimer().Coroutine();
            }
        }
        
        public static async ETTask JumpTimer(this PlayerControllerComponent self)
        {
            
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent==null)
            {
                return;
            }
            var dropComponent = self.Unit.GetComponent<DropComponent>();
            if (dropComponent==null)
            {
                return;
            }

            self.CanCtrl = false;
            moveComponent.SetJump(true);
            self.IsJumpUp = true;
            await TimerComponent.Instance.WaitAsync(moveComponent.JumpTime/2);
            self.IsJumpUp = false;
            await TimerComponent.Instance.WaitAsync(moveComponent.JumpTime/2);
            self.JumpFinish();
        }

        public static void JumpFinish(this PlayerControllerComponent self)
        {
            var moveComponent= self.Unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent==null)
            {
                return;
            }
            self.IsJumpUp = false;
            self.CanCtrl = true;
            moveComponent.SetJump(false);
        }
        
    }
}