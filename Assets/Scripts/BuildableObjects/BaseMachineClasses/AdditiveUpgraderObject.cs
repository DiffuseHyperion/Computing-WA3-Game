using System.Collections.Generic;
using BuildableObjects.Nodes;
using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class AdditiveUpgraderObject : UpgraderObject
    {
        private readonly int _bonus;
        protected AdditiveUpgraderObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, int bonus) : base(name, description, cost, maxStorage, moveRate, moveAmount)
        {
            _bonus = bonus;
        }

        public int GetBonus()
        {
            return _bonus;
        }
    }
}