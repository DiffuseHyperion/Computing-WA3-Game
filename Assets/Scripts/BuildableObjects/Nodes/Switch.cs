using System;
using UnityEngine;
using UtilClasses;

namespace BuildableObjects.Nodes
{
    public class Switch : MonoBehaviour
    {
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
                
            });
        }

        public bool IsTurnedOn()
        {
            return _toggleableObject.GetState();
        }
    }
}