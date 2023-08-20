using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilClasses
{
    public class ToggleableObject : MonoBehaviour
    {
        private readonly List<Action> _callbacks = new();
        private bool _state;

        public void AddCallback(Action action)
        {
            _callbacks.Add(action);
        }

        public void OnMouseDown()
        {
            _state = !_state;
            foreach (var callback in _callbacks)
            {
                callback();
            }
        }

        public bool GetState()
        {
            return _state;
        }
    }
}