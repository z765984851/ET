namespace ET
{
    public class SceneChangeFinishEvent_CreateUIHelp : AEvent<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            await  UIHelper.Remove(args.ZoneScene, UIType.UILogin);;
            // await UIHelper.Create(args.CurrentScene, UIType.UIHelp, UILayer.Mid);
        }
    }
}
