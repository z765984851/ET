namespace ET
{
    public static class TransformPositionComponentSystem
    {

        public static void SetX(this TransformPositionComponent self,int value)
        {
            
            self.X = value;
        }
        
        public static void SetY(this TransformPositionComponent self,int value)
        {
            self.Y = value;
        }
        
        public static void SetZ(this TransformPositionComponent self,int value)
        {
            self.Z = value;
        }
        
        
        
        
        public static void SetX(this TransformPositionComponent self,float value)
        {
            self.SetX((int)(value*1000));
        }
        
        public static void SetY(this TransformPositionComponent self,float value)
        {
            self.SetY((int)(value*1000));
        }
        
        public static void SetZ(this TransformPositionComponent self,float value)
        {
            self.SetZ((int)(value*1000));
        }
        
    }
}