using BuildableObjects.BaseMachineClasses;
using UnityEngine;

namespace BuildableObjects.Tier1
{
    public class Pump : GeneratorObject
    {
        public Pump() : base(
            "Pump", 
            "Pumps up water from the sea.", 
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

        public override bool CanBuild()
        {
            return OnWater();
        }

        public override void Tick()
        {
            GenerateWaterTick();
            MoveWaterTick();
        }
    }
}