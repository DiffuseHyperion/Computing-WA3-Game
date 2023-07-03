using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class ElectricPump : GeneratorObject, ITickableObject
    {
        public ElectricPump() : base(
            "Electric Pump", 
            "Pumps large amounts of water slowly.", 
            100, 
            10, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
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
    }
}