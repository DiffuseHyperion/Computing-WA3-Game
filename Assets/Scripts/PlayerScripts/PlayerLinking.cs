using System.Collections.Generic;
using BuildableObjects.LinkPorts;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerLinking : MonoBehaviour
    {
        private bool _linking;
        private LinkInput _linkInput;
        private int _cooldown;

        public Material lineRendererMaterial;

        private void Start()
        {
            _cooldown = 0;
        }

        private void Update()
        {
            if (_cooldown > 0)
            {
                _cooldown--;
            }
        }

        public void ResetCooldown()
        {
            _cooldown = 200;
        }
        
        public void StartLink(LinkInput linkInput)
        {
            if (_cooldown > 0)
            {
                return;
            }
            _linking = true;
            _linkInput = linkInput;
            ResetCooldown();
        }

        public void StopLink(LinkOutput linkOutput)
        {
            if (_cooldown > 0)
            {
                return;
            }
            _linking = false;
            DrawLine(_linkInput, linkOutput);
            _linkInput.GetLinkableObject().AddPair(_linkInput, linkOutput);
            linkOutput.GetLinkableObject().AddPair(_linkInput, linkOutput);
            _linkInput.EnableLinked();
            linkOutput.EnableLinked();
            ResetCooldown();
        }

        // end linking process prematurely (e.g. cancelling linking process)
        public void EndLink()
        {
            if (_cooldown > 0)
            {
                return;
            }
            _linking = false;
            _linkInput = null;
            ResetCooldown();
        }

        public bool IsLinking()
        {
            return _linking;
        }

        public LinkInput GetLinkInput()
        {
            return _linkInput;
        }

        public LinkPort GetLinkPort()
        {
            return _linkInput;
        }

        private void DrawLine(LinkInput input, LinkOutput output)
        {
            LineRenderer lindRenderer = input.gameObject.AddComponent<LineRenderer>();
            lindRenderer.positionCount = 2;
            List<Material> materials = new List<Material>();
            materials.Add(lineRendererMaterial);
            lindRenderer.SetMaterials(materials);
            lindRenderer.startWidth = 0.5f;
            lindRenderer.endWidth = 0.5f;
            lindRenderer.startColor = new Color(0f, 1f, 0f, 1f);
            lindRenderer.endColor = new Color(1f, 0f, 0f, 1f);
            lindRenderer.SetPosition(0, input.gameObject.transform.position);
            lindRenderer.SetPosition(1, output.gameObject.transform.position);
        }
    }
}