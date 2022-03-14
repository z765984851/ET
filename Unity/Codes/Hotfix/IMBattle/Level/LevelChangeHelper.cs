namespace ET
{
    public static class LevelChangeHelper
    {
        public static async ETTask LevelChange(Scene zoneScene,string levelName)
        {
            
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(Game.IdGenerater.GenerateId(), zoneScene.Zone, levelName, currentScenesComponent);
            
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();
            
            // 可以订阅这个事件中创建Loading界面
            Game.EventSystem.Publish(new EventType.SceneChangeStart() {ZoneScene = zoneScene});
            
            //根据levelname取配置获取要添加哪些组件 TODO
            
            Game.EventSystem.Publish(new EventType.SceneChangeFinish() {ZoneScene = zoneScene, CurrentScene = currentScene});
            
            await ETTask.CompletedTask;
        }
    }
}