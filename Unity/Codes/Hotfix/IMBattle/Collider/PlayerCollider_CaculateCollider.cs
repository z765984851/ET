

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
            if (moveComponent1.IsCollision&& moveComponent2.IsCollision)
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
                int massDif = moveComponent1.Mass - moveComponent2.Mass;
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
                    distance = Mathf.Abs( massDif * 0.05f);
                
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
                            distance = Mathf.Abs( massDif * 0.05f);
                        }
                        else
                        {
                            Debug.Log("玩家1重且移动");
                            direction = speed1.normalized;
                            distance = Mathf.Abs( massDif * 0.05f);
                        }
                    }
                    else if(massDif<0)
                    {
                    
                        forcePlayer = player1;
                    
                        if (speed2==Vector3.zero)
                        {
                            Debug.Log("玩家2重且未移动");
                            direction = -speed1.normalized;
                            distance = Mathf.Abs( massDif * 0.05f);
                        }
                        else
                        {
                            Debug.Log("玩家2重且移动");
                            direction = speed2.normalized;
                            distance = Mathf.Abs( massDif * 0.05f);
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

                if (forcePlayer!=null)
                {
                    Debug.Log($"碰撞结果 {forcePlayer},{direction},{distance} ");
                    Game.EventSystem.Publish(new EventType.PlayerColliderDisplay()
                    {
                        Direction = direction,
                        Distance = distance,
                        ForcePlayer = forcePlayer
                    });
                    
                }
                
            }
            
            
            await ETTask.CompletedTask;
        }
    }
}