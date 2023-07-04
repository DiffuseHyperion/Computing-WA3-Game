using PlayerScripts;
using PlayerScripts.PlayerUI;

namespace MechanicScripts
{
    public abstract class Mechanic
    {
        private bool _isEnabled;
        private readonly GlobalMechanicNames _globalMechanicName;
        private readonly PlayerUIBarNames _uiBarName;

        protected Mechanic(GlobalMechanicNames globalMechanicNames, PlayerUIBarNames uiBarName)
        {
            _globalMechanicName = globalMechanicNames;
            _uiBarName = uiBarName;
        }
        
        public GlobalMechanicNames GetMechanicName()
        {
            return _globalMechanicName;
        }

        public void EnableMechanic()
        {
            _isEnabled = true;
            foreach (var player in Player.GetAllPlayers())
            {
                player.GetUIMenu().GetBar<PlayerUIBar>(_uiBarName).EnableBar();
            }
        }

        public void DisableMechanic()
        {
            _isEnabled = false;
            foreach (var player in Player.GetAllPlayers())
            {
                player.GetUIMenu().GetBar<PlayerUIBar>(_uiBarName).DisableBar();
            }
        }

        public bool IsEnabled()
        {
            return _isEnabled;
        }
    }
}