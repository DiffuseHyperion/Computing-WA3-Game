namespace BuildableObjects.Tier1
{
    public class ManualDynamo : BuildableObject
    {
        public ManualDynamo() : base(
            "Manual Dynamo", 
            "Manually generate electricity.", 
            500, 
            BuildableObjectTypes.Misc)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }
    }
} 