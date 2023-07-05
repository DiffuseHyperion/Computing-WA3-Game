using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class ElectricPump : GeneratorObject, ITickableObject, IPoweredObject
    {
        public ElectricPump() : base(
            "Electric Pump", 
            "Pumps large amounts of water slowly.", 
            100, 
            MachineObjectConstants.ExporterDefaultWaterStorageSize * 2, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            5,
            5,
            3)
        {
        }

        public override bool CanBuild()
        {
            return OnWater();
        }

        public override void Tick()
        {
            if (!GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered())
            {
                return;
            }
            GenerateWaterTick();
            MoveWaterTick();
        }

        public int GetPowerConsumption()
        {
            return 5;
        }
    }
}