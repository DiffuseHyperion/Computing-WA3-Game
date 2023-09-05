namespace BuildableObjects.Tier1
{
    public class Tank : MachineObject
    {
        public Tank() : base(
            "Tank",
            "Store large amounts of water for later use.",
            50,
            10,
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