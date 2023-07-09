using PlayerScripts.PlayerActions;
using UnityEngine;

namespace BuildableObjects.Nodes
{
    public abstract class LinkPort : MonoBehaviour
    {
        private LinkableObject _linkableObject;
        private bool _linked;
        private readonly Color _defaultColour;
        private readonly Color _highlightColour;
        private readonly Color _linkingColour;
        private readonly Color _linkedColour;
        private readonly Color _cancelLinkColour;

        public LinkPort(Color defaultColour)
        {
            _defaultColour = defaultColour;
            _highlightColour = new Color(1f, 1f, 0f, 1f);
            _linkingColour = new Color(0f, 0f, 1f, 1f);
            _linkedColour = new Color(0f, 0f, 0f, 1f);
            _cancelLinkColour = new Color(1f, 0f, 0f, 1f);
        }
        
        private void Start()
        {
            _linked = false;
            _linkableObject = gameObject.GetComponentInParent<LinkableObject>();
        }

        public Color GetHighlightColour()
        {
            return _highlightColour;
        }
        public Color GetLinkingColour()
        {
            return _linkingColour;
        }
        public Color GetLinkedColour()
        {
            return _linkedColour;
        }
        public Color GetCancelLinkColour()
        {
            return _cancelLinkColour;
        }

        public LinkableObject GetLinkableObject()
        {
            return _linkableObject;
        }

        public bool IsLinked()
        {
            return _linked;
        }

        public Color GetDefaultColour()
        {
            return _defaultColour;
        }
        
        public void OnMouseEnter()
        {
            if (!_linkableObject.IsBuilt())
            {
                return;
            }
            PlayerLinking playerLinking = GetLinkableObject().GetPlayer().gameObject.GetComponent<PlayerLinking>();
            if (!_linked)
            {
                if (!playerLinking.IsLinking())
                {
                    if (this is LinkInput)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = _highlightColour;
                    }
                    return;
                }
                if (playerLinking.GetLinkPort() == this)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _highlightColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _highlightColour;
                }
            }
            else 
            {
                if (!playerLinking.IsLinking())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _cancelLinkColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _highlightColour;
                }
            }
        }

        public void OnMouseExit()
        {
            if (!_linkableObject.IsBuilt())
            {
                return;
            }
            PlayerLinking playerLinking = GetLinkableObject().GetPlayer().gameObject.GetComponent<PlayerLinking>();
            if (!_linked)
            {
                if (!playerLinking.IsLinking())
                {
                    if (this is LinkInput)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = _defaultColour;
                    }
                    return;
                }
                if (playerLinking.GetLinkPort() == this)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _linkingColour;
                    return;
                }
            
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _defaultColour;
                }
            }
            else 
            {
                if (!playerLinking.IsLinking())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _linkedColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = _linkedColour;
                }
            }
        }

        public void EnableLinked()
        {
            gameObject.GetComponent<SpriteRenderer>().color = _linkedColour;
            _linked = true;
        }

        public void DisableLinked()
        {
            gameObject.GetComponent<SpriteRenderer>().color = _defaultColour;
            _linked = false;
        }
    }
}