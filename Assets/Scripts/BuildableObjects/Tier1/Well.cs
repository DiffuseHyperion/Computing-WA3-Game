using System;
using BuildableObjects.BaseMachineClasses;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Well : GeneratorObject
    {

        private ClickableObject _clickableObject;
        private readonly int _basecooldown = 5;
        private int _cooldown;
        
        public Well() : base(
            "Well", 
            "Manually collects water from the ground.", 
            50, 
            MachineObjectConstants.GeneratorDefaultWaterStorageSize, 
            MachineObjectConstants.GeneratorDefaultWaterMoveRate, 
            1,
            2,
            5,
            1
            )
        {
            _cooldown = _basecooldown;
        }

        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnClick);
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnWaterBuildCondition();
        }

        public override void Tick()
        {
            MoveWaterTick();
            _cooldown -= 1;
        }

        private void OnClick()
        {
            if (_cooldown >= 0)
            {
                return;
            }
            GenerateWaterTick();
            _cooldown = _basecooldown;
        }
    }
}