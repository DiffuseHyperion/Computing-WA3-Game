using PlayerScripts;
using PlayerScripts.PlayerActions;
using PlayerScripts.PlayerUI;

namespace MechanicScripts
{
    public class ElectricityMechanic : Mechanic
    {
        private int _electricityProduction;
        private int _electricityConsumption;
        private bool _lastPowerState;

        public ElectricityMechanic() : base(GlobalMechanicNames.ELECTRICITY, PlayerUIBarNames.ELECTRICITY)
        {
        }
        
        public void IncreasePowerProduction(int increase)
        {
            _electricityProduction += increase;
            UpdatePlayerBars();
            UpdateTextColour();
            UpdatePowered();
        }
        public void DecreasePowerProduction(int decrease)
        {
            _electricityProduction -= decrease;
            UpdatePlayerBars();
            UpdateTextColour();
            UpdatePowered();
        }
        
        public void IncreasePowerConsumption(int increase)
        {
            _electricityConsumption += increase;
            UpdatePlayerBars();
            UpdateTextColour();
            UpdatePowered();
        }
        public void DecreasePowerConsumption(int decrease)
        {
            _electricityConsumption -= decrease;
            UpdatePlayerBars();
            UpdateTextColour();
            UpdatePowered();
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

        private void UpdatePowered()
        {
            if (IsPowered() && !_lastPowerState)
            {
                _lastPowerState = true;
                foreach (var player in Player.GetAllPlayers())
                {
                    player.GetComponent<PlayerSoundManager>().PlayPowerUpSound();
                }
            } else if (!IsPowered() && _lastPowerState)
            {
                _lastPowerState = false;
                foreach (var player in Player.GetAllPlayers())
                {
                    player.GetComponent<PlayerSoundManager>().PlayPowerDownSound();
                }
            }
        }

        public bool IsPowered()
        {
            return _electricityProduction >= _electricityConsumption;
        }
    }
}