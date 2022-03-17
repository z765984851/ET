namespace ET
{
    public static class DropComponentSystem 
    {
        public static void SetDrop(this DropComponent self,bool isDrop)
        {
            self.IsDrop = isDrop;
        }

        public static async ETTask Drop(this DropComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            if (unit!=null)
            {
                Game.EventSystem.Publish(new EventType.UnitDrop(){Unit = unit});
            }
            await ETTask.CompletedTask;
        }
    }
}