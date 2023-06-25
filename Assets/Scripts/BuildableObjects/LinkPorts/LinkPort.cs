using System;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace BuildableObjects.LinkPorts
{
    public abstract class LinkPort : MonoBehaviour
    {
        private LinkableObject _linkableObject;
        private bool _linked;
        private Color _defaultColour;
        public readonly Color HighlightColour;
        public readonly Color LinkingColour;
        public readonly Color LinkedColour;
        public readonly Color CancelLinkColour;

        public LinkPort(Color defaultColour)
        {
            _defaultColour = defaultColour;
            HighlightColour = new Color(1f, 1f, 0f, 1f);
            LinkingColour = new Color(0f, 0f, 1f, 1f);
            LinkedColour = new Color(0f, 0f, 0f, 1f);
            CancelLinkColour = new Color(1f, 0f, 0f, 1f);
        }
        
        private void Start()
        {
            _linked = false;
            _linkableObject = gameObject.GetComponentInParent<LinkableObject>();
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
                        gameObject.GetComponent<SpriteRenderer>().color = HighlightColour;
                    }
                    return;
                }
                if (playerLinking.GetLinkPort() == this)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = HighlightColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = HighlightColour;
                }
            }
            else 
            {
                if (!playerLinking.IsLinking())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = CancelLinkColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = HighlightColour;
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
                    gameObject.GetComponent<SpriteRenderer>().color = LinkingColour;
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
                    gameObject.GetComponent<SpriteRenderer>().color = LinkedColour;
                    return;
                }
                if (this is LinkOutput && playerLinking.GetLinkPort().GetLinkableObject() != _linkableObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = LinkedColour;
                }
            }
        }

        public void EnableLinked()
        {
            gameObject.GetComponent<SpriteRenderer>().color = LinkedColour;
            _linked = true;
        }

        public void DisableLinked()
        {
            gameObject.GetComponent<SpriteRenderer>().color = _defaultColour;
            _linked = false;
        }
    }
}