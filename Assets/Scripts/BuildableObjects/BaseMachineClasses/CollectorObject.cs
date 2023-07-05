using System;
using PlayerScripts.PlayerActions;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class CollectorObject : MachineObject, ITickableObject
    {

        private readonly float _multiplier;
        private readonly CountdownObject _countdownObject;
        private readonly int _sellAmount;
        protected CollectorObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, int sellRate, int sellAmount, float multiplier) : base(name, description, cost, maxStorage, moveRate, moveAmount, BuildableObjectTypes.Collector)
        {
            // moveRate and moveAmount goes unused in collectorobjects because collectors should never move any water, but lazy to rewrite abstraction :shrug:
            _multiplier = multiplier;
            _sellAmount = sellAmount;
            _countdownObject = new CountdownObject(sellRate);
        }

        public abstract void Tick();

        protected void CollectWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }

            for (int i = 0; i < _sellAmount; i++)
            {
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
}