using System.Collections.Generic;
using MechanicScripts;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier2
{
    public class Turbine : MachineObject
    {
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer _renderer;
        private CountdownObject _countdownObject;
        private bool _powered;
        
        public Turbine() : base(
            "Turbine",
            "Generates electricity using water.",
            200,
            5,
            1,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _countdownObject = new CountdownObject(3);
        }

        public override bool CanBuild()
        {
            return OnWater();
        }

        public override void Tick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            
            if (GetWaterStorage().IsFull())
            {
                GetWaterStorage().RemoveAllWater();
                if (_powered)
                {
                    return;
                }
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerProduction(10);
                _powered = true;
                _renderer.sprite = sprites[0];
            }
            else
            {
                if (!_powered)
                {
                    return;
                }
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).DecreasePowerProduction(10);
                _powered = false;
                _renderer.sprite = sprites[1];
            }
        }
    }
}