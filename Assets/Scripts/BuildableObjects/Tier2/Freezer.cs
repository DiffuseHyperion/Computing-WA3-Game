using System.Collections.Generic;
using BuildableObjects.BaseMachineClasses;
using MechanicScripts;
using UnityEngine;

namespace BuildableObjects.Tier2
{
    public class Freezer : AdditiveUpgraderObject, IPoweredObject
    {
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer _renderer;
        private bool _powered = true;
        public Freezer() : base(
            "Freezer",
            "Freezes a batch of water for a $20 bonus.\nWater will only be frozen once the freezer is full, and water cannot be refrozen.",
            150,
            MachineObjectConstants.UpgraderDefaultWaterStorageSize * 5,
            MachineObjectConstants.UpgraderDefaultWaterMoveRate * 5,
            5,
            20)
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
            
            if (!GetWaterStorage().IsFull())
            {
                return;
            }

            MoveWaterTick(water =>
            {
                if (water.ContainTag("Frozen"))
                {
                    water.IncrementValue(GetBonus());
                }
                water.AddTag("Frozen", GetBonus());
                water.RemoveTag("Boiled");
            });
        }

        public int GetPowerConsumption()
        {
            return 10;
        } 
    }
}