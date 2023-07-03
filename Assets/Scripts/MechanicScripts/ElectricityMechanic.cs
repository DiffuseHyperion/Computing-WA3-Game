using PlayerScripts.PlayerUI;

namespace MechanicScripts
{
    public class ElectricityMechanic : Mechanic
    {

        private int _electricityConsumption;
        private int _electricityProduction;

        public ElectricityMechanic() : base(GlobalMechanicNames.ELECTRICITY, PlayerUIBarNames.ELECTRICITY)
        {
        }
        
        public void IncreasePowerProduction(int increase)
        {
            _electricityProduction += increase;
        }
        public void DecreasePowerProduction(int decrease)
        {
            _electricityProduction -= decrease;
        }
        
        public void IncreasePowerConsumption(int increase)
        {
            _electricityConsumption += increase;
        }
        public void DecreasePowerConsumption(int decrease)
        {
            _electricityConsumption -= decrease;
        }

        public bool IsPowered()
        {
            return _electricityProduction >= _electricityConsumption;
        }
    }
}