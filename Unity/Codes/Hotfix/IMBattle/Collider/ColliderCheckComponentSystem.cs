namespace ET
{
    public static class ColliderCheckComponentSystem
    {
        
        /// <summary>
        /// 检测碰撞系统内的用户是否碰撞
        /// </summary>
        /// <param name="self"></param>
        public static void ColliderCheck(this ColliderCheckComponent self)
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
                        //玩家和场景物体碰撞
                        else if(moveComponent1!=null || moveComponent2!=null)
                        {
                            Game.EventSystem.Publish(new EventType.PlayerUnitCollider(){Unit1 = unit1,Unit2 = unit2});
                        }
                        //场景物体和场景物体碰撞
                        else
                        {
                            Game.EventSystem.Publish(new EventType.UnitCollider(){Unit1 = unit1,Unit2 = unit2});
                        }
                    }
                }
            }
        }
        
        
    }
}