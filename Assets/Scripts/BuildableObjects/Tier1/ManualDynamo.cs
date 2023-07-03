using MechanicScripts;
using UnityEngine;

namespace BuildableObjects.Tier1
{
    public class ManualDynamo : BuildableObject
    {

        private bool _powered;
        public ManualDynamo() : base(
            "Manual Dynamo", 
            "Manually generate electricity.", 
            500, 
            BuildableObjectTypes.Misc)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public void OnMouseDown()
        {
            if (_powered)
            {
                return;
            }

            _powered = true;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerProduction(5);
            GetComponent<SpriteRenderer>().material.color = new Color(0, 0.5f, 0, 1f);
            Invoke(nameof(PowerDown), 5f);
        }

        public void PowerDown()
        {
            _powered = false;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).DecreasePowerConsumption(5);
            GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0f, 0, 1f);
        }
    }
} 