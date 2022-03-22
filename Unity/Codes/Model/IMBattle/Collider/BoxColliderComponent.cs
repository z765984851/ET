using UnityEngine;

namespace ET
{
    public class BoxColliderComponent:Entity,IAwake
    {
        public Vector3 Center {
            get
            {
                Unit unit = this.GetParent<Unit>();
                if (unit!=null)
                {
                    var comp = unit.GetComponent<TransformPositionComponent>();
                    if (comp!=null)
                    {
                        return comp.Position;
                    }
                }

                return Vector3.zero;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                Unit unit = this.GetParent<Unit>();
                Quaternion quaternion=Quaternion.Euler(Vector3.zero);
                if (unit!=null)
                {
                    var comp = unit.GetComponent<TransformEulerAngleComponent>();
                    if (comp!=null)
                    {
                       
                        quaternion=Quaternion.Euler(comp.EulerAngle);
                    }
                }

                return quaternion;
            }
        }


        public Vector3 Extents
        {
            get
            {
                Unit unit = this.GetParent<Unit>();
                Vector3 vector3=Vector3.zero;
                if (unit!=null)
                {
                    var comp = unit.GetComponent<TransformLocalScaleComponent>();
                    if (comp!=null)
                    {
                        vector3 =0.5f * comp.LocalScale;
                    }
                }

                return vector3;
            }
        }
    }
    
    
}