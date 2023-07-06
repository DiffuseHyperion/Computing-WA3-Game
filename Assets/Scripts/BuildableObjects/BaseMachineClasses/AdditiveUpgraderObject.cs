using System.Collections.Generic;
using BuildableObjects.Nodes;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class AdditiveUpgraderObject : UpgraderObject, ITickableObject
    {
        private readonly int _bonus;
        private readonly int _moveAmount;
        private readonly CountdownObject _countdownObject;
        protected AdditiveUpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, int bonus) : base(name, description, cost, maxStorage, moveRate, moveAmount)
        {
            _bonus = bonus;
            _moveAmount = moveAmount;
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

                for (int i = 0; i < _moveAmount; i++)
                {
                    if (GetWaterStorage().IsEmpty())
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
}