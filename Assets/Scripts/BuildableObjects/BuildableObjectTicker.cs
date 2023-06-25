using System.Collections.Generic;
using UnityEngine;

namespace BuildableObjects
{
    public class BuildableObjectTicker : MonoBehaviour
    {
        private List<ITickableObject> _tickableObjects = new();
        void Start () {
            InvokeRepeating("Tick", 1f, 1f);  //1s delay, repeat every 1s
        }

        void Tick() {
            foreach (ITickableObject tickableObject in _tickableObjects)
            {
                tickableObject.Tick();
            }
        }

        public void AddTickableObject(ITickableObject tickableObject)
        {
            _tickableObjects.Add(tickableObject);
        }
    }
}