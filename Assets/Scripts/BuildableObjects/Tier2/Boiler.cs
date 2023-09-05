using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class Boiler : AdditiveUpgraderObject, ITickableObject, IPoweredObject
    {
        public Boiler() : base(
            "Boiler",
            "Boils a batch of water for a $20 bonus.\nWater will only be boiled once the boiler is full, and water cannot be reboiled.",
            150,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize * 5,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate * 5,
            5,
            20)
        {
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
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

            MoveWaterTick(water =>
            {
                if (water.ContainTag("Boiled"))
                {
                    water.IncrementValue(GetBonus());
                }
                water.AddTag("Boiled", GetBonus());
                water.RemoveTag("Frozen");
            });
        }

        public int GetPowerConsumption()
        {
            return 10;
        }
    }
}