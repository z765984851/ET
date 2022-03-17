using System.Collections.Generic;

namespace ET
{
    public class LevelLifeComponent : Entity,IAwake,IFixedUpdate
    {
        public List<Unit> Players;
        public List<Unit> LevelUnits;
    }
}