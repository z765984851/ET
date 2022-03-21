namespace ET
{
    public static class IMMathHelper
    {
        /// <summary>
        /// 为了防止在将计算后的结果转为int时 忘记添加括号而添加的方法
        /// 错误写法 int v=(int)a * b 此时 如果a是小于1的小数 则结果会永远为0
        /// 正确写法 int v= (int)(a * b) 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FloatToInt(float value)
        {
            return (int)value;
        } 
    }
}