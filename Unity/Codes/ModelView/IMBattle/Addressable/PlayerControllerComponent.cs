using UnityEngine;

namespace ET
{

    public class PlayerControllerComponent : Entity,IAwake
    {
        public Unit Unit;
        public KeyCode ForwardKey;
        public KeyCode BackKey;
        public KeyCode LeftKey;
        public KeyCode RightKey;
       
        public bool CanCtrl;
        

    }
    
}