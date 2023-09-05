using System;
using System.Collections.Generic;
using BuildableObjects.BaseMachineClasses;
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
        private float _fueledTime;
        private int _fuelStage;
        
        public Campfire() : base(
            "Campfire", 
            "Boils impurities within the water, giving a $10 bonus./nNeeds to be refuelled periodically.", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate,
            1,
            10
            )
        {
        }

        public override void OnBuild()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _renderer = GetComponent<SpriteRenderer>();
            _clickableObject.AddCallback(OnClick);
            _quarterMaxFueledTime = maxFueledTime / 4f;
            Refuel();
        }

        private void Update()
        {
            _fueledTime -= Time.deltaTime;
            if (_fueledTime <= _quarterMaxFueledTime * 3 && _fueledTime > _quarterMaxFueledTime * 2 && _fuelStage != 2)
            {
                Debug.Log("regressing to 2");
                _fuelStage = 2;
                UpdateSprite();
            } else if (_fueledTime <= _quarterMaxFueledTime * 2 && _fueledTime > _quarterMaxFueledTime && _fuelStage != 3)
            {
                Debug.Log("regressing to 3");
                _fuelStage = 3;
                UpdateSprite();
            } else if (_fueledTime <= _quarterMaxFueledTime && _fueledTime > 0f && _fuelStage != 4)
            {
                Debug.Log("regressing to 4");
                _fuelStage = 4;
                UpdateSprite();
            }
            else if (_fueledTime <= 0f && _fuelStage != 5)
            {
                Debug.Log("regressing to 5");
                _fuelStage = 5;
                UpdateSprite();
                _fueled = false;
            }
        }

        private void OnClick()
        {
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

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        public override void Tick()
        {
            if (!_fueled)
            {
                return;
            }
            MoveWaterTick(water => water.IncrementValue(GetBonus()));
        }
    }
}