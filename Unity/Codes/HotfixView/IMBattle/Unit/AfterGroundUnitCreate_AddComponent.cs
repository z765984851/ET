

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
            var boxCollider= unit.GetComponent<BoxColliderComponent>();
            if (boxCollider!=null)
            {
                boxCollider.Center = go.transform.position;
                boxCollider.Rotation = go.transform.rotation;
                boxCollider.Extents = 0.5f * go.transform.localScale;
            }
            await ETTask.CompletedTask;
        }
    }
}