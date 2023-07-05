using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class Boiler : AdditiveUpgraderObject, ITickableObject, IPoweredObject
    {
        public Boiler() : base(
            "Boiler",
            "Boils a batch of water for a $10 bonus.\nIt must be filled to the brim before boiling!",
            150,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize * 5,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate * 5,
            5,
            10)
        {
        }

        public override bool CanBuild()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick()
        {
            if (!GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered())
            {
                return;
            }
            if (!GetWaterStorage().IsFull())
            {
                return;
            }
            MoveUpgradedWaterTick();
        }

        public int GetPowerConsumption()
        {
            return 10;
        }
    }
}