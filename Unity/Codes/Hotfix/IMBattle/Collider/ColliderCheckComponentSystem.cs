namespace ET
{
    public static class ColliderCheckComponentSystem
    {
        
        /// <summary>
        /// 刷新碰撞盒数据
        /// </summary>
        /// <param name="self"></param>
        public static void UpdateBoxCollider(this ColliderCheckComponent self)
        {
            
            for (int i = 0; i < self.ColliderUnits.Count; i++)
            {
                Unit unit = self.ColliderUnits[i];
                var colliderComponent= unit.GetComponent<BoxColliderComponent>();
                var gameObject=unit.GetComponent<PlayerMoveComponent>();
                if (colliderComponent!=null&& gameObject !=null)
                {
                    colliderComponent.Center = gameObject.Position;
                    colliderComponent.Rotation =gameObject.Rotation;
                    colliderComponent.Extents = 0.5f * gameObject.LocalScale;
                }

            }
        }
        
        /// <summary>
        /// 检测碰撞系统内的用户是否碰撞
        /// </summary>
        /// <param name="self"></param>
        public static void PlayerColliderCheck(this ColliderCheckComponent self)
        {
            
            for (int i = 0; i < self.ColliderUnits.Count; i++)
            {
                var unit1 = self.ColliderUnits[i];
                for (int j = i+1; j < self.ColliderUnits.Count; j++)
                {
                    var unit2 = self.ColliderUnits[j];
                    var collider1 = unit1.GetComponent<BoxColliderComponent>();
                    var collider2 = unit2.GetComponent<BoxColliderComponent>();
                   
                    bool isCollider= BoxColliderHelper.CheckBoxCollider(collider1, collider2);
                    if (isCollider)
                    {
                        var moveComponent1 = unit1.GetComponent<PlayerMoveComponent>();
                        var moveComponent2 = unit2.GetComponent<PlayerMoveComponent>();
                        //如果是玩家和玩家碰撞
                        if (moveComponent1!=null && moveComponent2!=null)
                        {
                            
                            Game.EventSystem.Publish(new EventType.PlayerCollider(){Player1 = unit1,Player2 = unit2});
                        }
                       
                    }
                }
            }
        }
        
        
    }
}