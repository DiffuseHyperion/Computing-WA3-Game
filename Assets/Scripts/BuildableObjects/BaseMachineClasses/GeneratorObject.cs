using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class GeneratorObject : MachineObject, ITickableObject
    {
        private readonly int _genValue;
        private int _genAmount;
        private readonly CountdownObject _countdownObject;

        protected GeneratorObject(string name, string description, int cost, int max, int moveRate, int genRate, int genValue, int genAmount) : base(name, description, cost, max, moveRate, BuildableObjectTypes.Generator)
        {
            _countdownObject = new CountdownObject(genRate);
            _genValue = genValue;
        }

        public abstract void Tick();

        protected void GenerateWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            for (int i = 0; i < _genAmount; i++)
            {
                GetWaterStorage().AddWater(new WaterObject(_genValue));
            }
        }
    }
}