using UtilClasses;

namespace BuildableObjects.BaseMachineClasses
{
    public abstract class GeneratorObject : MachineObject, ITickableObject
    {
        private int _genValue;
        private CountdownObject _countdownObject;

        protected GeneratorObject(string name, string description, int cost, int max, int moveRate, int genRate, int genValue) : base(name, description, cost, max, moveRate, BuildableObjectTypes.Generator)
        {
            _countdownObject = new CountdownObject(genRate);
            _genValue = genValue;
        }

        public abstract void Tick();

        public void GenerateWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            GetWaterStorage().AddWater(new WaterObject(_genValue));
        }
    }
}