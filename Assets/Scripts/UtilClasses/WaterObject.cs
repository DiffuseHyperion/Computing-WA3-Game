using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilClasses
{
    public class WaterObject
    {
        private int _value;
        private readonly Dictionary<string, object> _tags = new();

        public WaterObject(int value)
        {
            _value = value;
        }

        public int GetValue()
        {
            return _value;
        }

        public int SetValue(int newValue)
        {
            _value = newValue;
            return _value;
        }

        public int IncrementValue(int increment)
        {
            _value += increment;
            return _value;
        }
        
        public int MultiplyValue(float multiply)
        {
            _value = (int) Math.Floor(_value * multiply);
            return _value;
        }

        public void AddTag(string tag, object value)
        {
            _tags.Add(tag, value);
            Debug.Log(_tags);
        }

        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
        }

        public bool UseTag(string tag)
        {
            return _tags.Remove(tag);
        }

        public object GetTagValue(string tag)
        {
            return _tags[tag];
        }

        public bool ContainTag(String tag)
        {
            return _tags.ContainsKey(tag);
        }
    }
}