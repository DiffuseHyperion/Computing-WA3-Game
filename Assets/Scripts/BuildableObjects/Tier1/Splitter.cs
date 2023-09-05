using System.Collections.Generic;
using BuildableObjects.Nodes;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Splitter : MachineObject
    {
        private readonly CountdownObject _countdownObject;

        public Splitter() : base(
            "Splitter", 
            "Evenly splits a pipe apart.",
            10,
            3,
            1,
            1,
            BuildableObjectTypes.Misc)
        {
            _countdownObject = new CountdownObject(1); // should be same as moverate, lazy to make constant field lol
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        public override void Tick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            List<LinkInput> inputs = GetInputs();
            List<MachineObject> machineOutputs = new();
            foreach (var input in inputs)
            {
                if (input.GetLinkableObject() != this)
                {
                    continue;
                }

                LinkOutput output = GetLinkOutput(input);
                MachineObject machineObject = output.gameObject.transform.parent.gameObject.GetComponent<MachineObject>();
                if (machineObject == null)
                {
                    break;
                }
                if (machineObject.GetWaterStorage().IsFull())
                {
                    continue;
                }
                machineOutputs.Add(machineObject);
            }
            
            if (GetWaterStorage().GetCount() < machineOutputs.Count)
            {
                return;
            }
            
            foreach (var machineObject in machineOutputs)
            {
                WaterObject transferredWater = GetWaterStorage().RemoveWater();
                machineObject.GetWaterStorage().AddWater(transferredWater);
            }
        }
    }
}