using System.Collections.Generic;
using BuildableObjects.Nodes;
using UnityEngine;

namespace BuildableObjects.Tier2
{
    public class Valve : MachineObject
    {
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer _renderer;
        private bool _powered;
        
        private Switch _switch;
        public Valve() : base(
            "Valve", 
            "Allows you to shut off the flow of water.", 
            50, 
            1,
            1,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        void Start()
        {
            _switch = GetComponentInChildren<Switch>();
            _renderer = GetComponent<SpriteRenderer>();
            _switch.AddOnClickCallback(OnClick);
        }
        
        private void UpdateSprite(bool currentlyPowered)
        {
            if (currentlyPowered && !_powered)
            {
                _powered = true;
                _renderer.sprite = sprites[0];
            } else if (!currentlyPowered && _powered)
            {
                _powered = false;
                _renderer.sprite = sprites[1];
            }
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        private void OnClick()
        {
            UpdateSprite(_switch.IsTurnedOn());
        }

        public override void Tick()
        {
            if (_switch.IsTurnedOn())
            {
                MoveWaterTick();
            }
        }
    }
}