using UnityEngine;

namespace ET
{
    public class AfterUnitCreate_CreateUnitView: AEvent<EventType.AfterUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            Log.Debug("添加控制");
            PlayerControllerComponent playerControllerCompone= unit.AddComponent<PlayerControllerComponent>();
            GameObject prefab=null;
            if (unit.ConfigId==1)
            {
                playerControllerCompone.ForwardKey = KeyCode.W;
                playerControllerCompone.BackKey = KeyCode.S;
                playerControllerCompone.LeftKey = KeyCode.A;
                playerControllerCompone.RightKey = KeyCode.D;
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player1");
            }
            if (unit.ConfigId==4)
            {
                playerControllerCompone.ForwardKey = KeyCode.UpArrow;
                playerControllerCompone.BackKey = KeyCode.DownArrow;
                playerControllerCompone.LeftKey = KeyCode.LeftArrow;
                playerControllerCompone.RightKey = KeyCode.RightArrow;
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player2");
            }
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            unit.AddComponent<GameObjectComponent>().GameObject=go;
            
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            // GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");
	           //
            // GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            // go.transform.position = args.Unit.Position;
            // args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            // args.Unit.AddComponent<AnimatorComponent>();
            
            
            await ETTask.CompletedTask;
        }
    }
}