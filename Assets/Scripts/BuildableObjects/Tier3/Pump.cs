using BuildableObjects.BaseMachineClasses;
using PlayerScripts;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier3
{
    public class Pump : GeneratorObject
    {
        private ClickableObject _clickableObject;
        
         public Pump() : base(
            "Pump", 
            "Collects water from the ground.", 
            50, 
            MachineObjectConstants.GeneratorDefaultWaterStorageSize, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            2,
            5,
            1
            )
        {
        }

        public override void Tick()
        {
            GenerateWaterTick();
            MoveWaterTick();
            Debug.Log("stored water: " + GetWaterStorage().GetCount());
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnWaterBuildCondition();
        }
    }
}