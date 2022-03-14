using UnityEngine;

namespace ET
{
    public static class BoxColliderHelper
    {
        public static bool CheckBoxCollider(BoxColliderComponent box0, BoxColliderComponent box1)
        {
            Vector3 v = box1.Center - box0.Center;

            //Compute A's basis
            Vector3 VAx = box0.Rotation * new Vector3(1, 0, 0);
            Vector3 VAy = box0.Rotation * new Vector3(0, 1, 0);
            Vector3 VAz = box0.Rotation * new Vector3(0, 0, 1);

            Vector3[] VA = new Vector3[3];
            VA[0] = VAx;
            VA[1] = VAy;
            VA[2] = VAz;

            //Compute B's basis
            Vector3 VBx = box1.Rotation * new Vector3(1, 0, 0);
            Vector3 VBy = box1.Rotation * new Vector3(0, 1, 0);
            Vector3 VBz = box1.Rotation * new Vector3(0, 0, 1);

            Vector3[] VB = new Vector3[3];
            VB[0] = VBx;
            VB[1] = VBy;
            VB[2] = VBz;

            Vector3 T = new Vector3(Vector3.Dot(v, VAx), Vector3.Dot(v, VAy), Vector3.Dot(v, VAz));

            float[,] R = new float[3, 3];
            float[,] FR = new float[3, 3];
            float ra, rb, t;

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    R[i, k] = Vector3.Dot(VA[i], VB[k]);
                    FR[i, k] = 1e-6f + Mathf.Abs(R[i, k]);
                }
            }

            // A's basis vectors
            for (int i = 0; i < 3; i++)
            {
                ra = box0.Extents[i];
                rb = box1.Extents[0] * FR[i, 0] + box1.Extents[1] * FR[i, 1] + box1.Extents[2] * FR[i, 2];
                t = Mathf.Abs(T[i]);
                if (t > ra + rb) return false;
            }

            // B's basis vectors
            for (int k = 0; k < 3; k++)
            {
                ra = box0.Extents[0] * FR[0, k] + box0.Extents[1] * FR[1, k] + box0.Extents[2] * FR[2, k];
                rb = box1.Extents[k];
                t = Mathf.Abs(T[0] * R[0, k] + T[1] * R[1, k] + T[2] * R[2, k]);
                if (t > ra + rb) return false;
            }

            //9 cross products

            //L = A0 x B0
            ra = box0.Extents[1] * FR[2, 0] + box0.Extents[2] * FR[1, 0];
            rb = box1.Extents[1] * FR[0, 2] + box1.Extents[2] * FR[0, 1];
            t = Mathf.Abs(T[2] * R[1, 0] - T[1] * R[2, 0]);
            if (t > ra + rb) return false;

            //L = A0 x B1
            ra = box0.Extents[1] * FR[2, 1] + box0.Extents[2] * FR[1, 1];
            rb = box1.Extents[0] * FR[0, 2] + box1.Extents[2] * FR[0, 0];
            t = Mathf.Abs(T[2] * R[1, 1] - T[1] * R[2, 1]);
            if (t > ra + rb) return false;

            //L = A0 x B2
            ra = box0.Extents[1] * FR[2, 2] + box0.Extents[2] * FR[1, 2];
            rb = box1.Extents[0] * FR[0, 1] + box1.Extents[1] * FR[0, 0];
            t = Mathf.Abs(T[2] * R[1, 2] - T[1] * R[2, 2]);
            if (t > ra + rb) return false;

            //L = A1 x B0
            ra = box0.Extents[0] * FR[2, 0] + box0.Extents[2] * FR[0, 0];
            rb = box1.Extents[1] * FR[1, 2] + box1.Extents[2] * FR[1, 1];
            t = Mathf.Abs(T[0] * R[2, 0] - T[2] * R[0, 0]);
            if (t > ra + rb) return false;

            //L = A1 x B1
            ra = box0.Extents[0] * FR[2, 1] + box0.Extents[2] * FR[0, 1];
            rb = box1.Extents[0] * FR[1, 2] + box1.Extents[2] * FR[1, 0];
            t = Mathf.Abs(T[0] * R[2, 1] - T[2] * R[0, 1]);
            if (t > ra + rb) return false;

            //L = A1 x B2
            ra = box0.Extents[0] * FR[2, 2] + box0.Extents[2] * FR[0, 2];
            rb = box1.Extents[0] * FR[1, 1] + box1.Extents[1] * FR[1, 0];
            t = Mathf.Abs(T[0] * R[2, 2] - T[2] * R[0, 2]);
            if (t > ra + rb) return false;

            //L = A2 x B0
            ra = box0.Extents[0] * FR[1, 0] + box0.Extents[1] * FR[0, 0];
            rb = box1.Extents[1] * FR[2, 2] + box1.Extents[2] * FR[2, 1];
            t = Mathf.Abs(T[1] * R[0, 0] - T[0] * R[1, 0]);
            if (t > ra + rb) return false;

            //L = A2 x B1
            ra = box0.Extents[0] * FR[1, 1] + box0.Extents[1] * FR[0, 1];
            rb = box1.Extents[0] * FR[2, 2] + box1.Extents[2] * FR[2, 0];
            t = Mathf.Abs(T[1] * R[0, 1] - T[0] * R[1, 1]);
            if (t > ra + rb) return false;

            //L = A2 x B2
            ra = box0.Extents[0] * FR[1, 2] + box0.Extents[1] * FR[0, 2];
            rb = box1.Extents[0] * FR[2, 1] + box1.Extents[1] * FR[2, 0];
            t = Mathf.Abs(T[1] * R[0, 2] - T[0] * R[1, 2]);
            if (t > ra + rb) return false;

            return true;
        }
    }
    
    
}