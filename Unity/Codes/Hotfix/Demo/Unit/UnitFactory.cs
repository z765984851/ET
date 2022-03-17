using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
	        
	        unit.AddComponent<MoveComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.X.Count > 0)
		        {
			        using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
			        {
				        list.Add(unit.Position);
				        for (int i = 0; i < unitInfo.MoveInfo.X.Count; ++i)
				        {
					        list.Add(new Vector3(unitInfo.MoveInfo.X[i], unitInfo.MoveInfo.Y[i], unitInfo.MoveInfo.Z[i]));
				        }

				        unit.MoveToAsync(list).Coroutine();
			        }
		        }
	        }

	        unit.AddComponent<ObjectWait>();

	        unit.AddComponent<XunLuoPathComponent>();
	        
	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit});
            return unit;
        }
        

        /// <summary>
        /// 创建玩家
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static Unit CreatePlayer(Scene currentScene, UnitType unitType)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(Game.IdGenerater.GenerateId(), (int)unitType);
	        unitComponent.Add(unit);
	        unit.AddComponent<BoxColliderComponent>();
	        unit.AddComponent<PlayerMoveComponent>();
	        Game.EventSystem.Publish(new EventType.AfterPlayerUnitCreate() {Unit = unit});
	        return unit;
        }
        
        /// <summary>
        /// 创建地面
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static Unit CreateGround(Scene currentScene, string name)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(Game.IdGenerater.GenerateId(), (int)UnitType.Ground);
	        unitComponent.Add(unit);
	        unit.AddComponent<BoxColliderComponent>();
	        Game.EventSystem.Publish(new EventType.AfterGroundUnitCreate() {Unit = unit,Name = name});
	        return unit;
        }
        
        
        
    }
}
