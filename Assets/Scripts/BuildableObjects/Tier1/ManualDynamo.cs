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

        private void OnMouseDown()
        {
            if (_powered)
            {
                return;
            }

            _powered = true;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerProduction(5);
            GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 0);
            Invoke(nameof(PowerDown), 5f);
        }

        private void PowerDown()
        {
            _powered = false;
            GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).DecreasePowerProduction(5);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.6f);
        }
    }
} 