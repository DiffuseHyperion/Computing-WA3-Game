using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier1
{
    public class Desalinator : AdditiveUpgraderObject
    {
        public Desalinator() : base(
            "Desalinator", 
            "Removes salt in water, giving it a $10 bonus!", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate,
            10
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