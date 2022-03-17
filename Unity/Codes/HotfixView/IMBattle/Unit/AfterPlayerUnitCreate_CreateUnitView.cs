
using UnityEngine;

namespace ET
{
    public class AfterPlayerUnitCreate_CreateUnitView : AEvent<EventType.AfterPlayerUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterPlayerUnitCreate args)
        {
            Unit unit = args.Unit;
          
            PlayerControllerComponent playerControllerComponent= unit.AddComponent<PlayerControllerComponent>();
            GameObject prefab=null;
            PlayerMoveComponent moveComponent = unit.GetComponent<PlayerMoveComponent>();
            if (unit.ConfigId==1)
            {
                playerControllerComponent.ForwardKey = KeyCode.W;
                playerControllerComponent.BackKey = KeyCode.S;
                playerControllerComponent.LeftKey = KeyCode.A;
                playerControllerComponent.RightKey = KeyCode.D;

                if (moveComponent!=null)
                {
                    
                    moveComponent.Mass = 5;
                }
                
                
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player1");
            }
            if (unit.ConfigId==4)
            {
                playerControllerComponent.ForwardKey = KeyCode.UpArrow;
                playerControllerComponent.BackKey = KeyCode.DownArrow;
                playerControllerComponent.LeftKey = KeyCode.LeftArrow;
                playerControllerComponent.RightKey = KeyCode.RightArrow;
                
                if (moveComponent!=null)
                {
                    moveComponent.Mass = 10;
                }
                
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player2");
            }
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            unit.AddComponent<GameObjectComponent>().GameObject=go;
            if (moveComponent != null)
            {
                moveComponent.Position = go.transform.position;
                moveComponent.LastFramePos = moveComponent.Position;
                moveComponent.Rotation = go.transform.rotation;
                moveComponent.LocalScale = go.transform.localScale;
            }


            await ETTask.CompletedTask;
        }
    }
}