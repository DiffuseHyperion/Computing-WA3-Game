using System;
using System.Collections.Generic;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Nodes
{
    public class Switch : MonoBehaviour
    {
        private List<Action> _callbacks = new();
        private ToggleableObject _toggleableObject;
        private Material _material;

        public void Start()
        {
            _toggleableObject = GetComponent<ToggleableObject>();
            _material = GetComponent<SpriteRenderer>().material;
            _toggleableObject.AddCallback(() =>
            {
                if (_toggleableObject.GetState())
                {
                    _material.SetColor(Shader.PropertyToID("_SolidOutline"), Color.red);
                }
                else
                {
                    _material.SetColor(Shader.PropertyToID("_SolidOutline"), Color.green);
                }

                foreach (Action action in _callbacks)
                {
                    action.Invoke();
                }
            });
        }

        public void AddOnClickCallback(Action action)
        {
            // this is not directly added to toggleableobject to ensure no nullreferenceerror
            _callbacks.Add(action);
        }

        public bool IsTurnedOn()
        {
            return _toggleableObject.GetState();
        }
    }
}