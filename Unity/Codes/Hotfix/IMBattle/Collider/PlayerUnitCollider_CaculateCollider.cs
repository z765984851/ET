
namespace ET
{
    public class PlayerUnitCollider_CaculateCollider : AEvent<EventType.PlayerUnitCollider>
    {
        protected override async  ETTask Run(EventType.PlayerUnitCollider args)
        {
            var unit1 = args.Unit1;
            var unit2 = args.Unit2;
            if (unit1!=null && unit2 !=null)
            {
                //unit1是地面
                if (unit1.ConfigId.Equals((int)UnitType.Ground))
                {
                    var dropComponent = unit2.GetComponent<DropComponent>();
                    dropComponent.SetDrop(false);
                }
                //unit2是地面
                else if (unit2.ConfigId.Equals((int)UnitType.Ground))
                {
                    var dropComponent = unit1.GetComponent<DropComponent>();
                    dropComponent.SetDrop(false);
                }
               
            }
            
            await ETTask.CompletedTask;
        }
    }
}