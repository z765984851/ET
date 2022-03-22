namespace ET
{
    public static class TransformEulerAngleComponentSystem
    {
        public static void SetX(this TransformEulerAngleComponent self,int value)
        {
            
            self.X = value;
        }
        
        public static void SetY(this TransformEulerAngleComponent self,int value)
        {
            self.Y = value;
        }
        
        public static void SetZ(this TransformEulerAngleComponent self,int value)
        {
            self.Z = value;
        }
        
        
        
        
        public static void SetX(this TransformEulerAngleComponent self,float value)
        {
            self.SetX((int)(value*1000));
        }
        
        public static void SetY(this TransformEulerAngleComponent self,float value)
        {
            self.SetY((int)(value*1000));
        }
        
        public static void SetZ(this TransformEulerAngleComponent self,float value)
        {
            self.SetZ((int)(value*1000));
        }
    }
}