using BuildableObjects.BaseMachineClasses;

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
            2,
            5
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