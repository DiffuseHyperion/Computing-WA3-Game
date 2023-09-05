using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class Freezer : AdditiveUpgraderObject, IPoweredObject
    {
        public Freezer() : base(
            "Freezer",
            "Freezes a batch of water for a $20 bonus.\nWater will only be frozen once the freezer is full, and water cannot be refrozen.",
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
                if (water.ContainTag("Frozen"))
                {
                    water.IncrementValue(GetBonus());
                }
                water.AddTag("Frozen", GetBonus());
                water.RemoveTag("Boiled");
            });
        }

        public int GetPowerConsumption()
        {
            return 10;
        } 
    }
}