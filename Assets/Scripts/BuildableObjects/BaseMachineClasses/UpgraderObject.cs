namespace BuildableObjects.BaseMachineClasses
{
    public abstract class UpgraderObject : MachineObject
    {
        protected UpgraderObject(string name, string description, int cost, int maxStorage, int moveRate) : base(name, description, cost, maxStorage, moveRate, BuildableObjectTypes.Upgrader)
        {
        }
    }
}