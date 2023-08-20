
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilClasses
{
    public class ClickableObject : MonoBehaviour
    {
        private readonly List<Action> _callbacks = new();

        public void AddCallback(Action action)
        {
            _callbacks.Add(action);
        }

        public void OnMouseDown()
        {
            foreach (var callback in _callbacks)
            {
                callback();
            }
        }
    }
}