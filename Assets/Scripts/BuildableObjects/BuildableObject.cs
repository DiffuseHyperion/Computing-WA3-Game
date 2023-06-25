using PlayerScripts;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

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

        private void Start()
        {
            _camera = Camera.main;
        }

        public abstract bool CanBuild();

        public bool OnWater()
        {
            int layerMask = LayerMask.GetMask("Background");
            if (_camera != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), transform.forward * 30, float.PositiveInfinity, layerMask);
        
                if (hit.collider != null)
                {
                    if (hit.collider.transform.CompareTag("Water"))
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        public bool OnLand()
        {
            int layerMask = LayerMask.GetMask("Background");
            if (_camera != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), transform.forward * 30, float.PositiveInfinity, layerMask);
        
                if (hit.collider != null)
                {
                    if (hit.collider.transform.CompareTag("Land"))
                    {
                        return true;
                    }

                    return false;
                }
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
