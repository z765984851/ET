using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    public class LevelLifeComponentAwakeSystem : AwakeSystem<LevelLifeComponent>
    {
        public override void Awake(LevelLifeComponent self)
        {
            self.Players = new List<Unit>();
            self.LevelUnits = new List<Unit>();
            ColliderCheckComponent colliderSystem= self.AddComponent<ColliderCheckComponent>();
            colliderSystem.ColliderUnits = new List<Unit>();
            self.CreatePlayer1();
            self.CreatePlayer2();
            self.CollectSceneUnits();
        }
    }
    
    
    public class LevelLifeComponentFixedUpdateSystem : FixedUpdateSystem<LevelLifeComponent>
    {
        public override void FixedUpdate(LevelLifeComponent self)
        {
            //确认所有unit的实体已经生成完毕再开始进行逻辑
            for (int i = 0; i < self.LevelUnits.Count; i++)
            {
                Unit unit = self.LevelUnits[i];
                if (unit.GetComponent<GameObjectComponent>()==null)
                {
                    return;
                }
            }
            
            //检测用户按键
            for (int i = 0; i < self.Players.Count; i++)
            {
                Unit player = self.Players[i];
                var controllerComponent= player.GetComponent<PlayerControllerComponent>();
                if (controllerComponent!=null)
                {
                    // Log.Debug("用户控制检测");
                    controllerComponent.PlayerController();
                }
                
            }
            
            //检测碰撞
            ColliderCheckComponent colliderSystem= self.GetComponent<ColliderCheckComponent>();
            if (colliderSystem!=null)
            {
                colliderSystem.ColliderCheck();
            }
            
        }
    }
    
    
    public static class LevelLifeComponentSystem
    {
        public static void CreatePlayer1(this LevelLifeComponent self)
        {
            
            Unit player= UnitFactory.CreatePlayer(self.DomainScene(), UnitType.Player);
            self.Players.Add(player);
            self.LevelUnits.Add(player);
            ColliderCheckComponent colliderSystem= self.GetComponent<ColliderCheckComponent>();
            if (colliderSystem!=null)
            {
                colliderSystem.ColliderUnits.Add(player);
            }
 
        }
        
        public static void CreatePlayer2(this LevelLifeComponent self)
        {
            
            Unit player= UnitFactory.CreatePlayer(self.DomainScene(), UnitType.Player2);
            self.Players.Add(player);
            self.LevelUnits.Add(player);
            ColliderCheckComponent colliderSystem= self.GetComponent<ColliderCheckComponent>();
            if (colliderSystem!=null)
            {
                colliderSystem.ColliderUnits.Add(player);
            }
 
        }
        
        /// <summary>
        /// 收集本来就在场景内的unit
        /// </summary>
        /// <param name="self"></param>
        public static void CollectSceneUnits(this LevelLifeComponent self)
        {
            
           Unit ground=UnitFactory.CreateGround(self.DomainScene(),"Ground1");
           self.LevelUnits.Add(ground);
           ColliderCheckComponent colliderSystem= self.GetComponent<ColliderCheckComponent>();
           if (colliderSystem!=null)
           {
               colliderSystem.ColliderUnits.Add(ground);
           }
        }
        
    }
}