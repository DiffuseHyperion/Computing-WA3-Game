using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier1
{
    public class Disinfector : MultiplicativeUpgraderObject
    {
        public Disinfector() : base(
            "Disinfector", 
            "Removes common bacteria in water, making it worth 20% more!", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate, 
            1,
            1.2f
            )
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public override void Tick()
        {
            MoveUpgradedWaterTick();
        }
    }
}