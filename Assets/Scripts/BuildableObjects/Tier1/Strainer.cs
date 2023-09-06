using System.Collections.Generic;
using BuildableObjects.BaseMachineClasses;
using PlayerScripts.PlayerActions;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Strainer : MultiplicativeUpgraderObject
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private int maxUses;
        private float _quarterMaxUses;
        private ClickableObject _clickableObject;
        private SpriteRenderer _renderer;
        private bool _clean;
        private int _usesLeft;
        private int _dirtStage;
        
        public Strainer() : base(
            "Strainer", 
            "Removes debris in the water, making it worth 10% more!\nNeeds to be emptied periodically.", 
            100, 
            MachineObjectConstants.UpgraderDefaultWaterStorageSize, 
            MachineObjectConstants.UpgraderDefaultWaterMoveRate, 
            1,
            1.1f
            )
        {
        }

        private void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _renderer = GetComponent<SpriteRenderer>();
            _clickableObject.AddCallback(OnClick);
            _quarterMaxUses = maxUses / 4f;
        }
        
        public override void OnBuild()
        {
            Clean();
        }

        private void OnClick()
        {
            GetPlayer().GetComponent<PlayerSoundManager>().PlayBrushSound();
            Clean();
        }

        private void Clean()
        {
            _usesLeft = maxUses;
            _dirtStage = 1;
            _clean = true;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _renderer.sprite = sprites[_dirtStage - 1];
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        private void ConsumeUse()
        {
            _usesLeft -= 1;
            if (_usesLeft <= _quarterMaxUses * 3 && _usesLeft > _quarterMaxUses * 2 && _dirtStage != 2)
            {
                _dirtStage = 2;
                UpdateSprite();
            } else if (_usesLeft <= _quarterMaxUses * 2 && _usesLeft > _quarterMaxUses && _dirtStage != 3)
            {
                _dirtStage = 3;
                UpdateSprite();
            } else if (_usesLeft <= _quarterMaxUses && _usesLeft > 0f && _dirtStage != 4)
            {
                _dirtStage = 4;
                UpdateSprite();
            }
            else if (_usesLeft <= 0f && _dirtStage != 5)
            {
                _dirtStage = 5;
                UpdateSprite();
                _clean = false;
            }
        }

        public override void Tick()
        {
            MoveWaterTick(water =>
            {
                if (!water.ContainTag("Strainer") && _clean)
                {
                    water.AddTag("Strainer", true);
                    water.MultiplyValue(GetMultiplier());
                }
                ConsumeUse();
            });
        }
    }
}