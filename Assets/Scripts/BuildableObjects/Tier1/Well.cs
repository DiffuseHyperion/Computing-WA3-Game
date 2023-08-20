using BuildableObjects.BaseMachineClasses;
using UnityEngine;

namespace BuildableObjects.Tier1
{
    public class Well : GeneratorObject
    {
        public Well() : base(
            "Well", 
            "Manually collects water from the ground.", 
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