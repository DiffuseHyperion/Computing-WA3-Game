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
        private bool _built;
        private Player _owner;
        private Camera _camera;

        protected BuildableObject(string name, string description, int cost, BuildableObjectTypes type)
        {
            SetBuilt(false);
            _name = name;
            _description = description;
            _cost = cost;
            _type = type;
        }

        public abstract bool CanBuild();

        public bool OnWater()
        {
            _camera = _owner.gameObject.GetComponentInChildren<Camera>();
            if (_camera == null)
            {
                return false;
            }
            int layerMask = LayerMask.GetMask("Background");
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.PositiveInfinity, layerMask);
            if (hit.collider == null)
            {
                return false;
            }
            if (hit.collider.transform.CompareTag("Water"))
            {
                return true;
            }

            return false;
        }

        public bool OnLand()
        {
            _camera = _owner.gameObject.GetComponentInChildren<Camera>();
            if (_camera == null)
            {
                return false;
            }
            int layerMask = LayerMask.GetMask("Background");
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.PositiveInfinity, layerMask);
            if (hit.collider == null)
            {
                return false;
            }
            if (hit.collider.transform.CompareTag("Land"))
            {
                return true;
            }
            return false;
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

        public bool IsBuilt()
        {
            return _built;
        }

        public new BuildableObjectTypes GetType()
        {
            return _type;
        }

        public void SetBuilt(bool built)
        {
            _built = built;
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
