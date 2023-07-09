using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class Purifier : MultiplicativeUpgraderObject, IPoweredObject
    {
        public Purifier() : base(
            "Purifier",
            "Slowly removes bacteria using heavy UV rays for a 20% boost!",
            300,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate * 3,
            1,
            1.2f)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public override void Tick()
        {
            MoveWaterTick(water => water.MultiplyValue(GetMultiplier()));
        }

        public int GetPowerConsumption()
        {
            return 20;
        }
    }
}