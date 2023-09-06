using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class Purifier : MachineObject, IPoweredObject
    {
        public Purifier() : base(
            "Purifier",
            "Removes all tags from water, as if they were never touched before.",
            300,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public override void Tick()
        {
            MoveWaterTick(water =>
                {
                    if (!water.ContainTag("Purified"))
                    {
                        water.ClearTags();
                        water.AddTag("Purified", true);
                    }
                });
        }

        public int GetPowerConsumption()
        {
            return 20;
        }
    }
}