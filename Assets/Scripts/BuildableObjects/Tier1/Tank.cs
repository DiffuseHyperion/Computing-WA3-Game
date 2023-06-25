namespace BuildableObjects.Tier1
{
    public class Tank : MachineObject, ITickableObject
    {
        public Tank() : base(
            "Tank",
            "Store large amounts of water for later use.",
            50,
            10,
            1, 
            BuildableObjectTypes.Misc)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public void Tick()
        {
            MoveWaterTick();
        }
    }
}