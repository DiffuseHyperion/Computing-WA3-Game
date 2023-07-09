using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier1
{
    public class Disinfector : MultiplicativeUpgraderObject
    {
        public Disinfector() : base(
            "Disinfector", 
            "Removes common bacteria in water, making it worth 10% more!", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate, 
            1,
            1.1f
            )
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
    }
}