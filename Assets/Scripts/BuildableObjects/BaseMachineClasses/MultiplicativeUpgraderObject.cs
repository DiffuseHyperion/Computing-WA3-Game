using System.Collections.Generic;
using BuildableObjects.LinkPorts;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class MultiplicativeUpgraderObject : UpgraderObject, ITickableObject
    {
        private readonly float _multiplier;
        private readonly int _moveAmount;
        private readonly CountdownObject _countdownObject;
        protected MultiplicativeUpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, float multiplier) : base(name, description, cost, maxStorage, moveRate, moveAmount)
        {
            _multiplier = multiplier;
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
                    transferredWater.MultiplyValue(_multiplier);
                    machineObject.GetWaterStorage().AddWater(transferredWater);
                }
            }
        }
    }
}