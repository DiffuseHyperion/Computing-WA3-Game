using BuildableObjects.Nodes;
using UnityEngine;

namespace BuildableObjects.Tier2
{
    public class Valve : MachineObject
    {
        private Switch _switch;
        public Valve() : base(
            "Valve", 
            "Allows you to shut off the flow of water.", 
            100, 
            1,
            1,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        void Start()
        {
            _switch = GetComponentInChildren<Switch>();
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public override void Tick()
        {
            if (_switch.IsTurnedOn())
            {
                MoveWaterTick();
            }
        }
    }
}