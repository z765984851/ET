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
                        return new Vector3((float)comp.X/1000,(float)comp.Y/1000,(float)comp.Z/1000);
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
                        Vector3 angle = new Vector3((float)comp.X/1000,(float)comp.Y/1000,(float)comp.Z/1000);
                        quaternion=Quaternion.Euler(angle);
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
                        vector3 =0.5f * new Vector3((float)comp.X / 1000, (float)comp.Y / 1000, (float)comp.Z / 1000);
                    }
                }

                return vector3;
            }
        }
    }
    
    
}