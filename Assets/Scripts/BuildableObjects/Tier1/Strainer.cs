using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier1
{
    public class Strainer : MultiplicativeUpgraderObject
    {
        public Strainer() : base(
            "Strainer", 
            "Removes debris in the water, making it worth 10% more!\nNeeds to be emptied periodically.", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate, 
            1,
            1.1f
            )
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