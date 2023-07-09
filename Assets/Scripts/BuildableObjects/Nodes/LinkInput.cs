using PlayerScripts.PlayerActions;
using UnityEngine;

namespace BuildableObjects.Nodes
{
    public class LinkInput : LinkPort
    {
        public LinkInput() : base(new Color(0f, 1f, 0f, 1f))
        {
        }
        
        public void OnMouseDown()
        {
            Debug.Log("mouse down");
            PlayerLinking playerLinking = GetLinkableObject().GetPlayer().gameObject.GetComponent<PlayerLinking>();
            if (IsLinked())
            {
                if (playerLinking.IsLinking())
                {
                    return;
                }
                Debug.Log("unlinking pair");
                UnlinkPair();
            }
            else
            {
                if (!playerLinking.IsLinking())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = GetLinkingColour();
                    Debug.Log("linking pair");
                    playerLinking.StartLink(this);
                } else if (playerLinking.GetLinkInput() == this)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = GetDefaultColour();
                    Debug.Log("end linking pair");
                    playerLinking.EndLink();
                }
            }
        }

        private void UnlinkPair()
        {
            LinkOutput linkOutput = GetLinkableObject().GetLinkOutput(this);
            DisableLinked();
            linkOutput.DisableLinked();
                
            DestroyLine();
            GetLinkableObject().RemovePair(this);
            linkOutput.GetLinkableObject().RemovePair(this);
        }

        public void DestroyLine()
        {
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            Destroy(lineRenderer);
        }
    }
}