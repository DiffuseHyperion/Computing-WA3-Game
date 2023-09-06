﻿using System.Collections.Generic;
using BuildableObjects.BaseMachineClasses;
using PlayerScripts.PlayerActions;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Well : GeneratorObject
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private int maxCooldown;
        private float _quarterMaxCooldown;
        private ClickableObject _clickableObject;
        private int _cooldown;
        private int _wellStage;
        private SpriteRenderer _renderer;
        
        public Well() : base(
            "Well", 
            "Manually collects water from the ground.", 
            50, 
            MachineObjectConstants.GeneratorDefaultWaterStorageSize, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            1,
            5,
            1
            )
        {
        }

        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnClick);
            _renderer = GetComponent<SpriteRenderer>();
            _quarterMaxCooldown = maxCooldown / 4f;
        }

        public override bool CanBuild()
        {
            return OnWater();
        }

        public override void Tick()
        {
            MoveWaterTick();
            _cooldown -= 1;
            if (_cooldown <= _quarterMaxCooldown * 3 && _cooldown > _quarterMaxCooldown * 2 && _wellStage != 2)
            {
                _wellStage = 2;
                UpdateSprite();
            } else if (_cooldown <= _quarterMaxCooldown * 2 && _cooldown > _quarterMaxCooldown && _wellStage != 3)
            {
                _wellStage = 3;
                UpdateSprite();
            } else if (_cooldown <= _quarterMaxCooldown && _cooldown > 0f && _wellStage != 4)
            {
                _wellStage = 4;
                UpdateSprite();
            }
            else if (_cooldown <= 0f && _wellStage != 5)
            {
                _wellStage = 5;
                UpdateSprite();
            }
        }
        
        private void UpdateSprite()
        {
            _renderer.sprite = sprites[_wellStage - 1];
        }

        private void OnClick()
        {
            if (_cooldown > 0)
            {
                return;
            }
            GenerateWaterTick();
            _cooldown = maxCooldown;
            _wellStage = 1;
            UpdateSprite();
            GetPlayer().GetComponent<PlayerSoundManager>().PlayPumpSound();
        }
    }
}