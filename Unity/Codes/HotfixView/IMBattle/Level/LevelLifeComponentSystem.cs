using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    public class LevelLifeComponentAwakeSystem : AwakeSystem<LevelLifeComponent>
    {
        public override void Awake(LevelLifeComponent self)
        {
            self.Players = new List<Unit>();
            ColliderCheckComponent colliderSystem= self.AddComponent<ColliderCheckComponent>();
            Log.Debug("Level Life  Awake !!!!!");
            Unit player1= UnitFactory.Create(self.DomainScene(), UnitType.Player);
            Unit player2= UnitFactory.Create(self.DomainScene(), UnitType.Player2);
            self.Players.Add(player1);
            self.Players.Add(player2);

            colliderSystem.ColliderUnits = new List<Unit>();
            colliderSystem.ColliderUnits.Add(player1);
            colliderSystem.ColliderUnits.Add(player2);


        }
    }
    
    
    public class LevelLifeComponentFixedUpdateSystem : FixedUpdateSystem<LevelLifeComponent>
    {
        public override void FixedUpdate(LevelLifeComponent self)
        {
            // Log.Debug($"Level Life  FixedUpdate !!!!!,{Time.timeSinceLevelLoad}");
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
            for (int i = 0; i < self.Players.Count; i++)
            {
                Unit player = self.Players[i];
                var colliderComponent= player.GetComponent<BoxColliderComponent>();
                var gameObject=player.GetComponent<GameObjectComponent>();
                if (colliderComponent!=null&& gameObject !=null)
                {
                    // Log.Debug("用户控制检测");
                    colliderComponent.Center = gameObject.GameObject.transform.position;
                    colliderComponent.Rotation =gameObject.GameObject.transform.rotation;
                    colliderComponent.Extents = 0.5f * gameObject.GameObject.transform.localScale;
                }
                
            }
            
            
            ColliderCheckComponent colliderSystem= self.GetComponent<ColliderCheckComponent>();
            if (colliderSystem!=null)
            {
                colliderSystem.PlayerColliderCheck();
            }
            
        }
    }
    
    
    public class LevelLifeComponentSystem
    {
        
    }
}