using BuildableObjects.BaseMachineClasses;
using PlayerScripts;
using UtilClasses;

namespace BuildableObjects.Tier3
{
    public class Pump : GeneratorObject
    {
        private ClickableObject _clickableObject;
        private readonly int _basecooldown = 5;
        
        public Pump() : base(
            "Pump", 
            "Collects water from the ground.", 
            50, 
            MachineObjectConstants.GeneratorDefaultWaterStorageSize, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            2,
            5,
            1
            )
        {
        }

        public override void Tick()
        {
            GenerateWaterTick();
            MoveWaterTick();
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnWaterBuildCondition();
        }
    }
}