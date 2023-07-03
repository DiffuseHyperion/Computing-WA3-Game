using System.Collections.Generic;

namespace UtilClasses
{
    public class WaterStorageObject
    {
        private List<WaterObject> _storage = new();
        private int _size;

        public WaterStorageObject(int size)
        {
            _size = size;
        }

        public void AddWater(WaterObject water)
        {
            if (IsFull())
            {
                return;
            }
            _storage.Add(water);
        }

        public WaterObject RemoveWater()
        {
            WaterObject oldestWater = _storage[_storage.Count - 1];
            _storage.Remove(oldestWater);
            return oldestWater;
        }

        public List<WaterObject> GetStorage()
        {
            return _storage;
        }

        public int GetCount()
        {
            return _storage.Count;
        }

        public int GetTotalValue()
        {
            int totalValue = 0;
            foreach (var water in _storage)
            {
                totalValue += water.GetValue();
            }
            return totalValue;
        }

        public bool IsFull()
        {
            return _storage.Count >= _size;
        }

        public bool IsEmpty()
        {
            return _storage.Count <= 0;
        }

        public int GetMax()
        {
            return _size;
        }
        
    }
}