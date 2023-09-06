using System.Collections.Generic;
using MechanicScripts;
using UnityEngine;

namespace BuildableObjects.Tier2
{
    public class Purifier : MachineObject, IPoweredObject
    {
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer _renderer;
        private bool _powered = true;
        public Purifier() : base(
            "Purifier",
            "Removes all tags from water, as if they were never touched before.",
            200,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate,
            1,
            BuildableObjectTypes.Misc)
        {
        }

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            UpdateSprite(GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered());
        }

        public override bool CanBuild()
        {
            return OnLand();
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

        public override void Tick()
        {
            if (!GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered())
            {
                UpdateSprite(false);
                return;
            }
            UpdateSprite(true);
            MoveWaterTick(water =>
                {
                    if (!water.ContainTag("Purified"))
                    {
                        water.ClearTags();
                        water.AddTag("Purified", true);
                    }
                });
        }

        public int GetPowerConsumption()
        {
            return 20;
        }
    }
}