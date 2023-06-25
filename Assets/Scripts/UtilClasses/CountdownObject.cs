namespace UtilClasses
{
    public class CountdownObject
    {
        private int _starting;
        private int _countdown;

        public CountdownObject(int starting)
        {
            _starting = starting;
        }

        public bool Countdown()
        {
            _countdown--;
            if (_countdown <= 0)
            {
                ResetCountdown();
                return true;
            }

            return false;
        }

        public void ResetCountdown()
        {
            _countdown = _starting;
        }

        public bool IsAvailable()
        {
            return _countdown <= 0;
        }

        public int GetCounter()
        {
            return _countdown;
        }
    }
}