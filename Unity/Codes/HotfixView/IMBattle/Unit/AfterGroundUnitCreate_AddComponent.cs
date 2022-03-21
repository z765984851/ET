

using UnityEngine;

namespace ET
{
    public class AfterGroundUnitCreate_AddComponent : AEvent<EventType.AfterGroundUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterGroundUnitCreate args)
        {
            Unit unit = args.Unit;
            string name = args.Name;

            GameObject go= GameObject.Find(name);
            unit.AddComponent<GameObjectComponent>().GameObject=go;
            
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
            
            await ETTask.CompletedTask;
        }
    }
}