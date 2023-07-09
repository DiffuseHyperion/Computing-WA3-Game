using System.Collections.Generic;
using BuildableObjects.Nodes;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class MultiplicativeUpgraderObject : UpgraderObject
    {
        private readonly float _multiplier;
        protected MultiplicativeUpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, float multiplier) : base(name, description, cost, maxStorage, moveRate, moveAmount)
        {
            _multiplier = multiplier;
        }

        public float GetMultiplier()
        {
            return _multiplier;
        }
    }
}