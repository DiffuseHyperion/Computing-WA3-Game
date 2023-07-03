using System.Collections.Generic;
using BuildableObjects.LinkPorts;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class AdditiveUpgraderObject : UpgraderObject, ITickableObject
    {
        private readonly int _bonus;
        private readonly CountdownObject _countdownObject;
        protected AdditiveUpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int bonus) : base(name, description, cost, maxStorage, moveRate)
        {
            _bonus = bonus;
            _countdownObject = new CountdownObject(moveRate);
        }

        public abstract void Tick();

        protected void MoveUpgradedWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            List<LinkInput> inputs = GetInputs();
            foreach (var input in inputs)
            { 
                if (GetWaterStorage().IsEmpty())
                {
                    return;
                }
                if (input.GetLinkableObject() != this)
                {
                    continue;
                }

                LinkOutput output = GetLinkOutput(input);
                MachineObject machineObject = output.gameObject.transform.parent.gameObject.GetComponent<MachineObject>();
                if (machineObject == null)
                {
                    return;
                }
                if (machineObject.GetWaterStorage().IsFull())
                {
                    return;
                }
                WaterObject transferredWater = GetWaterStorage().RemoveWater();
                transferredWater.IncrementValue(_bonus);
                machineObject.GetWaterStorage().AddWater(transferredWater);
            }
        }
    }
}