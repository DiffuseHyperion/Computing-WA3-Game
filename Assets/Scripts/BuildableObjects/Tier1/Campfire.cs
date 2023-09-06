using System.Collections.Generic;
using BuildableObjects.BaseMachineClasses;
using PlayerScripts.PlayerActions;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Campfire : AdditiveUpgraderObject
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private int maxFueledTime;
        private float _quarterMaxFueledTime;
        private ClickableObject _clickableObject;
        private SpriteRenderer _renderer;
        private bool _fueled;
        private int _fueledTime;
        private int _fuelStage;
        
        public Campfire() : base(
            "Campfire", 
            "Boils impurities within the water, giving a $10 bonus.\nNeeds to be refuelled periodically.", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate,
            1,
            10
            )
        {
        }

        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _renderer = GetComponent<SpriteRenderer>();
            _clickableObject.AddCallback(OnClick);
            _quarterMaxFueledTime = maxFueledTime / 4f;
        }
        
        public override void OnBuild()
        {
            Refuel();
        }
        
        private void OnClick()
        {
            GetPlayer().GetComponent<PlayerSoundManager>().PlayRefuelSound();
            Refuel();
        }

        private void Refuel()
        {
            _fueledTime = maxFueledTime;
            _fuelStage = 1;
            _fueled = true;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _renderer.sprite = sprites[_fuelStage - 1];
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        private void TickFuel()
        {
            _fueledTime -= 1;
            if (_fueledTime <= _quarterMaxFueledTime * 3 && _fueledTime > _quarterMaxFueledTime * 2 && _fuelStage != 2)
            {
                _fuelStage = 2;
                UpdateSprite();
            } else if (_fueledTime <= _quarterMaxFueledTime * 2 && _fueledTime > _quarterMaxFueledTime && _fuelStage != 3)
            {
                _fuelStage = 3;
                UpdateSprite();
            } else if (_fueledTime <= _quarterMaxFueledTime && _fueledTime > 0f && _fuelStage != 4)
            {
                _fuelStage = 4;
                UpdateSprite();
            }
            else if (_fueledTime <= 0f && _fuelStage != 5)
            {
                _fuelStage = 5;
                UpdateSprite();
                _fueled = false;
            }
        }

        public override void Tick()
        {
            TickFuel();
            if (!_fueled)
            {
                return;
            }
            MoveWaterTick(water =>
            {
                int campfiredCount;
                if (water.ContainTag("Campfire"))
                {
                    campfiredCount = (int) water.GetTagValue("Campfire");
                }
                else
                {
                    campfiredCount = 0;
                }

                if (campfiredCount < 3)
                {
                    water.AddTag("Campfire", campfiredCount + 1);
                    water.IncrementValue(GetBonus());
                }
            });
        }
    }
}