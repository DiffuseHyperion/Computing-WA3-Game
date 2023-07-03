using PlayerScripts.PlayerActions;
using UnityEngine;

namespace BuildableObjects.LinkPorts
{
    public class LinkOutput : LinkPort
    {
        public LinkOutput() : base(new Color(1f, 0f, 0f, 1f))
        {
        }
        
        public void OnMouseDown()
        {
            PlayerLinking playerLinking = GetLinkableObject().GetPlayer().gameObject.GetComponent<PlayerLinking>();
            if (IsLinked())
            {
                if (playerLinking.GetLinkPort().GetLinkableObject() == GetLinkableObject())
                {
                    return;
                }
                UnlinkPair();
            }
            if (playerLinking.IsLinking() && playerLinking.GetLinkInput().GetLinkableObject() != GetLinkableObject())
            {
                playerLinking.StopLink(this);
            }
        }

        private void UnlinkPair()
        {
            LinkInput linkInput = GetLinkableObject().GetLinkInput(this);
            DisableLinked();
            linkInput.DisableLinked();
                
            linkInput.DestroyLine();
            GetLinkableObject().RemovePair(this);
            linkInput.GetLinkableObject().RemovePair(this);
        }
    }
}