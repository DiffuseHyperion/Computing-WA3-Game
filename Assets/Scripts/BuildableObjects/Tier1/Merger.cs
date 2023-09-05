namespace BuildableObjects.Tier1
{
    public class Merger : MachineObject
    {
        public Merger() : base(
            "Merger", 
            "Merges two pipes together.", 
            10,
            3,
            1, 
            1,
            BuildableObjectTypes.Misc)
        {
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        public override void Tick()
        {
            MoveWaterTick();
        }
    }
}