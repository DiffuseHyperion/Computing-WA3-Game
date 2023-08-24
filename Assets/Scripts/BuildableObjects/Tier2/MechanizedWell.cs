using BuildableObjects.BaseMachineClasses;
using MechanicScripts;
using UtilClasses;

namespace BuildableObjects.Tier2
{
    public class MechanizedWell : GeneratorObject
    {
        private ClickableObject _clickableObject;
        private readonly int _basecooldown = 5;
        private int _cooldown;
        
        public MechanizedWell() : base(
            "Mechanized Well", 
            "A faster version of the well.", 
            100, 
            MachineObjectConstants.ExporterDefaultWaterStorageSize * 2, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            5,
            5,
            3)
        {
            _cooldown = _basecooldown;
        }
        
        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnClick);
        }

        public override bool CanBuild()
        {
            return OnWater();
        }

        public int GetPowerConsumption()
        {
            return 5;
        }

        public override void Tick()
        {
            if (!GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered())
            {
                return;
            }
            MoveWaterTick();
            _cooldown -= 1;
        }

        private void OnClick()
        {
            if (_cooldown >= 0)
            {
                return;
            }
            GenerateWaterTick();
            _cooldown = _basecooldown;
        }
    }
}