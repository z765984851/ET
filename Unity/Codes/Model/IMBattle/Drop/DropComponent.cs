namespace ET
{
    public class DropComponent : Entity ,IAwake
    {

        /// <summary>
        /// 掉落的速度 实际计算时要除以1000
        /// </summary>
        public int DropSpeed=150;
        
        /// <summary>
        /// 是否执行掉落
        /// </summary>
        public bool IsDrop = false;

       
        


    }
}