using System.Drawing;
using PlayerScripts;
using PlayerScripts.PlayerUI;

namespace MechanicScripts
{
    public class ElectricityMechanic : Mechanic
    {
        private int _electricityProduction;
        private int _electricityConsumption;

        public ElectricityMechanic() : base(GlobalMechanicNames.ELECTRICITY, PlayerUIBarNames.ELECTRICITY)
        {
        }
        
        public void IncreasePowerProduction(int increase)
        {
            _electricityProduction += increase;
            UpdatePlayerBars();
            UpdateTextColour();
        }
        public void DecreasePowerProduction(int decrease)
        {
            _electricityProduction -= decrease;
            UpdatePlayerBars();
            UpdateTextColour();
        }
        
        public void IncreasePowerConsumption(int increase)
        {
            _electricityConsumption += increase;
            UpdatePlayerBars();
            UpdateTextColour();
        }
        public void DecreasePowerConsumption(int decrease)
        {
            _electricityConsumption -= decrease;
            UpdatePlayerBars();
            UpdateTextColour();
        }

        private void UpdatePlayerBars()
        {
            foreach (var player in Player.GetAllPlayers())
            {
                player.GetUIMenu().GetBar<PlayerUIBar>(PlayerUIBarNames.ELECTRICITY).GetText().text =
                    "Producing: " + _electricityProduction + "kW\nConsuming: " + _electricityConsumption + "kW";
            }
        }

        private void UpdateTextColour()
        {
            UnityEngine.Color colour;
            if (IsPowered())
            {
                colour = new UnityEngine.Color(1f, 1f, 1f);
            }
            else
            {
                colour = new UnityEngine.Color(1f, 0, 0);
            }
            foreach (var player in Player.GetAllPlayers())
            {
                player.GetUIMenu().GetBar<PlayerUIBar>(PlayerUIBarNames.ELECTRICITY).GetText().color = colour;
            }
        }

        public bool IsPowered()
        {
            return _electricityProduction >= _electricityConsumption;
        }
    }
}