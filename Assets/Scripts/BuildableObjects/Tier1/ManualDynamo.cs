using System;
using MechanicScripts;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Tier1
{
    public class ManualDynamo : BuildableObject
    {

        private bool _powered;
        private ClickableObject _clickableObject;
        private GameObject _ringOn;
        private GameObject _ringOff;
        
        public ManualDynamo() : base(
            "Manual Dynamo", 
            "Manually generate electricity.", 
            500, 
            BuildableObjectTypes.Misc)
        {
        }

        public void Start()
        {
            _clickableObject = GetComponent<ClickableObject>();
            _clickableObject.AddCallback(OnPress);
            
            foreach (Transform child in transform) {
                if (child.name == "RingOn")
                {
                    _ringOn = child.gameObject;
                } else if (child.name == "RingOff")
                {
                    _ringOff = child.gameObject;
                }
            }
            
            _ringOn.SetActive(false);
            _ringOff.SetActive(true);
        }

        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        private void OnPress()
        {
            if (_powered)
            {
                return;
            }

            _powered = true;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerProduction(5);
            _ringOn.SetActive(true);
            _ringOff.SetActive(false);
            Invoke(nameof(PowerDown), 5f);
        }

        private void PowerDown()
        {
            _powered = false;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).DecreasePowerProduction(5);
            _ringOn.SetActive(false);
            _ringOff.SetActive(true);
        }
    }
} 