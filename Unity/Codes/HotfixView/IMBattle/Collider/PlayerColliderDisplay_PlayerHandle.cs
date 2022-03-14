using ET.EventType;

namespace ET
{
    public class PlayerColliderDisplay_PlayerHandle : AEvent<EventType.PlayerColliderDisplay>
    {
        protected override async ETTask Run(PlayerColliderDisplay args)
        {
            var forcePlayer = args.ForcePlayer;
            var playerController = forcePlayer.GetComponent<PlayerControllerComponent>();
            if (playerController!=null)
            {
                playerController.ColliderDisplay(args.Distance,args.Direction);
            }
            await ETTask.CompletedTask;
        }
    }
}