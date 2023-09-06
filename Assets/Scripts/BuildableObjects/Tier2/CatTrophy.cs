using PlayerScripts.PlayerActions;
using UtilClasses;

namespace BuildableObjects.Tier2
{
    public class CatTrophy : BuildableObject
    {
        private ClickableObject _clickableObject;
        
        public CatTrophy() : base(
            "Cat Trophy", 
            "Meows when clicked...", 
            300,
            BuildableObjectTypes.Misc)
        {
        }

        private void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnClick);
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        private void OnClick()
        {
            GetPlayer().GetComponent<PlayerSoundManager>().PlayMeowSound();
        }
    }
}