namespace ET
{
    public static class TransformLocalScaleComponentSystem
    {
        public static void SetX(this TransformLocalScaleComponent self,int value)
        {
            
            self.X = value;
        }
        
        public static void SetY(this TransformLocalScaleComponent self,int value)
        {
            self.Y = value;
        }
        
        public static void SetZ(this TransformLocalScaleComponent self,int value)
        {
            self.Z = value;
        }
        
        
        
        
        public static void SetX(this TransformLocalScaleComponent self,float value)
        {
            self.SetX((int)(value*1000));
        }
        
        public static void SetY(this TransformLocalScaleComponent self,float value)
        {
            self.SetY((int)(value*1000));
        }
        
        public static void SetZ(this TransformLocalScaleComponent self,float value)
        {
            self.SetZ((int)(value*1000));
        }
    }
}