namespace BuildableObjects.BaseMachineClasses
{
    public abstract class UpgraderObject : MachineObject
    {
        protected UpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount) : base(name, description, cost, maxStorage, moveRate, moveAmount, BuildableObjectTypes.Upgrader)
        {
        }
    }
}