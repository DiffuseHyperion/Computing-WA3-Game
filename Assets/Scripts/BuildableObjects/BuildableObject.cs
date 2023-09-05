using PlayerScripts;
using UnityEngine;

namespace BuildableObjects
{
    public abstract class BuildableObject : MonoBehaviour
    {
        private readonly string _name;
        private readonly string _description;
        private readonly int _cost;
        private readonly BuildableObjectTypes _type;
        private Player _owner;
        private Camera _camera;

        protected BuildableObject(string name, string description, int cost, BuildableObjectTypes type)
        {
            _name = name;
            _description = description;
            _cost = cost;
            _type = type;
        }

        public abstract IBuildCondition GetBuildCondition();

        public virtual void OnBuild()
        {
        }
        
        public string GetName()
        {
            return _name;
        }

        public string GetDescription()
        {
            return _description;
        }

        public int GetCost()
        {
            return _cost;
        }

        public new BuildableObjectTypes GetType()
        {
            return _type;
        }
        public Player GetPlayer()
        {
            return _owner;
        }

        public void SetOwner(Player owner)
        {
            _owner = owner;
        }
    }
}
