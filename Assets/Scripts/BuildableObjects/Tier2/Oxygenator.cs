using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier2
{
    public class Oxygenator : MultiplicativeUpgraderObject, ITickableObject
    {
        public Oxygenator() : base(
            "Oxygenator", 
            "Absorb oxygen within the atmosphere and add it into your water.\nThis may bring unforeseen consequences...", 
            500, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate, 
            1, 
            1.5f)
        {
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        public override void Tick()
        {
            MoveWaterTick(water => water.MultiplyValue(GetMultiplier()));
        }
    }
}