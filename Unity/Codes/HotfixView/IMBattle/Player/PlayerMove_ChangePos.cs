
namespace ET
{
    public class PlayerMove_ChangePos: AEvent<EventType.PlayerMove>
    {
        protected override async ETTask Run(EventType.PlayerMove a)
        {
            Unit unit = a.Unit;
            GameObjectComponent goComponent = unit.GetComponent<GameObjectComponent>();
            if (goComponent!=null)
            {
                goComponent.GameObject.transform.position = a.TargetPos;
            }
            
            await ETTask.CompletedTask;
        }
    }
}