using System;
using MechanicScripts;
using UtilClasses;

namespace BuildableObjects.Tier2
{
    public class Turbine : MachineObject
    {
        private CountdownObject _countdownObject;
        private bool _powered;
        
        public Turbine() : base(
            "Turbine",
            "Generates electricity using water.",
            300,
            5,
            1,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        private void Start()
        {
            _countdownObject = new CountdownObject(3);
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnWaterBuildCondition();
        }

        public override void Tick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            
            if (GetWaterStorage().IsFull())
            {
                GetWaterStorage().RemoveAllWater();
                if (_powered)
                {
                    return;
                }
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerProduction(10);
                _powered = true;
            }
            else
            {
                if (!_powered)
                {
                    return;
                }
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).DecreasePowerProduction(10);
                _powered = false;
            }
        }
    }
}