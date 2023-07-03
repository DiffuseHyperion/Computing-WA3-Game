using System;
using PlayerScripts.PlayerActions;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class CollectorObject : MachineObject, ITickableObject
    {

        private readonly float _multiplier;
        private readonly CountdownObject _countdownObject;
        protected CollectorObject(string name, string description, int cost, int maxStorage, int moveRate, int sellRate, float multiplier) : base(name, description, cost, maxStorage, moveRate, BuildableObjectTypes.Collector)
        {
            // moveRate goes unused in collectorobjects because collectors should never move any water, but still :shrug:
            _multiplier = multiplier;
            _countdownObject = new CountdownObject(sellRate);
        }

        public abstract void Tick();

        protected void CollectWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            if (GetWaterStorage().IsEmpty())
            {
                return;
            }
            WaterObject sellingWater = GetWaterStorage().RemoveWater();
            int finalValue = (int) Math.Floor(sellingWater.GetValue() * _multiplier);
            GetPlayer().GetComponent<PlayerMoney>().IncrementMoney(finalValue);
        }
    }
}