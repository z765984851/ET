
using UnityEngine;

namespace ET
{
    public class AfterPlayerUnitCreate_CreateUnitView : AEvent<EventType.AfterPlayerUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterPlayerUnitCreate args)
        {
            Unit unit = args.Unit;
          
            PlayerControllerComponent playerControllerComponent= unit.AddComponent<PlayerControllerComponent>();
           
            MassComponent massComponent = unit.GetComponent<MassComponent>();
            GameObject prefab=null;

            //设置玩家操作按钮
            if (unit.ConfigId==1)
            {
                playerControllerComponent.ForwardKey = KeyCode.W;
                playerControllerComponent.BackKey = KeyCode.S;
                playerControllerComponent.LeftKey = KeyCode.A;
                playerControllerComponent.RightKey = KeyCode.D;
                playerControllerComponent.JumpKey = KeyCode.Space;

                if (massComponent!=null)
                {
                    massComponent.Mass = 5;
                   
                }
                
                
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player1");
            }
            if (unit.ConfigId==4)
            {
                playerControllerComponent.ForwardKey = KeyCode.UpArrow;
                playerControllerComponent.BackKey = KeyCode.DownArrow;
                playerControllerComponent.LeftKey = KeyCode.LeftArrow;
                playerControllerComponent.RightKey = KeyCode.RightArrow;
                playerControllerComponent.JumpKey = KeyCode.RightControl;
                if (massComponent!=null)
                {
                    massComponent.Mass = 10;
                   
                }
                
                prefab = await AddressableComponent.Instance.LoadAsset<GameObject>("Player2");
            }
            //生成实体
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            unit.AddComponent<GameObjectComponent>().GameObject=go;
            
            //设置位置
            TransformPositionComponent transformPositionComponent = unit.GetComponent<TransformPositionComponent>();
            if (transformPositionComponent!=null)
            {
                Vector3 pos = go.transform.position;
                transformPositionComponent.X = IMMathHelper.FloatToInt(pos.x * 1000);
                transformPositionComponent.Y = IMMathHelper.FloatToInt(pos.y * 1000);
                transformPositionComponent.Z = IMMathHelper.FloatToInt(pos.z * 1000);
            }
            //设置大小
            TransformLocalScaleComponent transformLocalScaleComponent = unit.GetComponent<TransformLocalScaleComponent>();
            if (transformLocalScaleComponent!=null)
            {
                Vector3 scale = go.transform.localScale;
                transformLocalScaleComponent.X = IMMathHelper.FloatToInt(scale.x * 1000);
                transformLocalScaleComponent.Y = IMMathHelper.FloatToInt(scale.y * 1000);
                transformLocalScaleComponent.Z = IMMathHelper.FloatToInt(scale.z * 1000);
            }
            //设置旋转
            TransformEulerAngleComponent transformEulerAngleComponent = unit.GetComponent<TransformEulerAngleComponent>();
            if (transformEulerAngleComponent!=null)
            {
                Vector3 angle = go.transform.eulerAngles;
                transformEulerAngleComponent.X = IMMathHelper.FloatToInt(angle.x * 1000);
                transformEulerAngleComponent.Y = IMMathHelper.FloatToInt(angle.y * 1000);
                transformEulerAngleComponent.Z = IMMathHelper.FloatToInt(angle.z * 1000);
            }
            
            
            
            PlayerMoveComponent moveComponent = unit.GetComponent<PlayerMoveComponent>();
            if (moveComponent != null)
            {
                
                moveComponent.LastFramePos = transformPositionComponent.Position;
                
            }
            await ETTask.CompletedTask;
        }
    }
}