using BuildableObjects.BaseMachineClasses;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class Well : GeneratorObject
    {

        private ClickableObject _clickableObject;
        private readonly float _baseCooldown = 5f;
        private float _cooldown;
        
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
        }

        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnClick);
            _cooldown = 0f;
        }

        public void Update()
        {
            // normally i would have reduced cooldown in Tick() method, but faced some "quantum entanglement" issues
            // _cooldown variable here would be completely detached from the _cooldown variable in OnClick()
            // the variable persisted through play sessions and is shared with all objects
            _cooldown -= Time.deltaTime;
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnWaterBuildCondition();
        }

        public override void Tick()
        {
            MoveWaterTick();
        }

        private void OnClick()
        {
            if (_cooldown >= 0f)
            {
                return;
            }
            GenerateWaterTick();
            _cooldown = _baseCooldown;
        }
    }
}