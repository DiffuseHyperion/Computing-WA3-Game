using PlayerScripts;
using UnityEngine;

namespace BuildableObjects
{
    public class OnWaterBuildCondition : IBuildCondition
    {
        public bool IsBuildable(Player player)
        {
            Camera _camera = player.gameObject.GetComponentInChildren<Camera>();
            if (_camera == null)
            {
                return false;
            }
            int layerMask = LayerMask.GetMask("Background");
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), player.transform.forward, float.PositiveInfinity, layerMask);
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
    }
}