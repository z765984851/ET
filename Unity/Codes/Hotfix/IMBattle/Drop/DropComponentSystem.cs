using UnityEngine;

namespace ET
{
    public static class DropComponentSystem 
    {
        public static void SetDrop(this DropComponent self,bool isDrop)
        {
            self.IsDrop = isDrop;
        }

        public static void Drop(this DropComponent self)
        {
            
            if (self.IsDrop)
            {
                Unit unit = self.GetParent<Unit>();
                if (unit!=null)
                {
                    
                    var transform= unit.GetComponent<TransformPositionComponent>();
                    if (transform!=null)
                    {
                        transform.Y -= self.DropSpeed;
                        
                    }
                }
            }
            else
            {
                self.IsDrop = true;
            }
           
            
        }
        
        public static void Jump(this DropComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            if (unit!=null)
            {
                var transform= unit.GetComponent<TransformPositionComponent>();
                if (transform!=null)
                {
                    transform.Y += self.DropSpeed;
                }
            }
            
        }
    }
}