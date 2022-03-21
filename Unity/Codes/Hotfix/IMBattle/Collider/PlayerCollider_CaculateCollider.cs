


using UnityEngine;

namespace ET
{
    public class PlayerCollider_CaculateCollider : AEvent<EventType.PlayerCollider>
    {
        protected override async ETTask Run(EventType.PlayerCollider args)
        {
            Unit player1 = args.Player1;
            Unit player2 = args.Player2;
            PlayerMoveComponent moveComponent1 = player1.GetComponent<PlayerMoveComponent>();
            PlayerMoveComponent moveComponent2 = player2.GetComponent<PlayerMoveComponent>();
            if (moveComponent1==null || moveComponent2==null)
            {
                return;
            }
            if (!moveComponent1.IsCollision&& !moveComponent2.IsCollision)
            {
                Vector3 speed1 = moveComponent1.GetSpeedVector();
                Vector3 speed2 = moveComponent2.GetSpeedVector();

                //受力对象
                Unit forcePlayer;
                //受力方向
                Vector3 direction;
                //移动距离
                float distance;
                
                //两个玩家的重力差
                int massDif = 0;
                MassComponent player1Mass = player1.GetComponent<MassComponent>();
                MassComponent player2Mass = player2.GetComponent<MassComponent>();
                if (player1Mass!=null && player2Mass!=null)
                {
                    massDif = player1Mass.Mass - player2Mass.Mass;
                }
                
                //双方都在移动
                if (speed1!=Vector3.zero&& speed2!=Vector3.zero)
                {
                    Debug.Log("双方都在移动");
                    //玩家1重 受力对象为玩家2 
                    if (massDif>0)
                    {
                        forcePlayer = player2;
                        direction = speed1.normalized;
                   
                    }
                    else if(massDif<0)
                    {
                        forcePlayer = player1;
                        direction = speed2.normalized;
                    }
                    //相同如何处理？ TODO
                    else
                    {
                        forcePlayer = player1;
                        direction = speed2.normalized;
                    }
                    distance = Mathf.Abs( massDif) ;
                
                }
                //双方有一方不移动
                else
                {
                    Debug.Log("一方未移动");
                    //玩家1重 受力对象为玩家2 
                    if (massDif>0)
                    {
                    
                        forcePlayer = player2;
                    
                        if (speed1==Vector3.zero)
                        {
                            Debug.Log("玩家1重且未移动");
                            direction = -speed2.normalized;
                            distance = Mathf.Abs( massDif);
                        }
                        else
                        {
                            Debug.Log("玩家1重且移动");
                            direction = speed1.normalized;
                            distance = Mathf.Abs( massDif);
                        }
                    }
                    else if(massDif<0)
                    {
                    
                        forcePlayer = player1;
                    
                        if (speed2==Vector3.zero)
                        {
                            Debug.Log("玩家2重且未移动");
                            direction = -speed1.normalized;
                            distance = Mathf.Abs( massDif);
                        }
                        else
                        {
                            Debug.Log("玩家2重且移动");
                            direction = speed2.normalized;
                            distance = Mathf.Abs( massDif);
                        }
                    }
                    //相同 停下
                    else
                    {
                        Debug.Log("一样重");
                        if (speed1==Vector3.zero)
                        {
                            forcePlayer = player1;
                            direction = speed2.normalized;
                            distance = 0;
                        }
                        else
                        {
                            forcePlayer = player1;
                            direction = speed2.normalized;
                            distance = 0;
                        }
                    }
                
                }

                var moveComponent = forcePlayer.GetComponent<PlayerMoveComponent>();
                
                var speedCal = Mathf.Clamp((float)moveComponent.CurrentXSpeed / moveComponent.MaxSpeed,0.5f,1f) ;
                //如果其中一方前一帧在垂直落地
                if (direction.x==0 && direction.z==0 && direction.y !=0)
                {
                    direction = Vector3.left;
                }
                else
                {
                    //限制在高度上不做碰撞处理
                    direction = new Vector3(direction.x,0,direction.z);
                }
                
                
                
                Debug.Log($"碰撞结果 {forcePlayer.ConfigId},{speedCal},{direction},{distance} ");
                Game.EventSystem.Publish(new EventType.PlayerColliderDisplay()
                {
                    Direction = direction ,
                    Distance = distance * speedCal*0.5f,//乘0.5是为了缩短距离
                    ForcePlayer = forcePlayer
                });
                
                
            }
            
            
            await ETTask.CompletedTask;
        }
    }
}