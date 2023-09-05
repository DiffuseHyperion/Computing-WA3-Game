using BuildableObjects;
using PlayerScripts;
using UnityEngine;

namespace UtilClasses
{
    public class TemporaryBuildableObject : MonoBehaviour
    {
        private BuildableObject _buildableObject;
        private Player _player;

        public void Init(BuildableObject buildableObject, Player player)
        {
            _buildableObject = buildableObject;
            _player = player;
            gameObject.name = buildableObject.name;
            GetComponent<SpriteRenderer>().sprite = buildableObject.GetComponent<SpriteRenderer>().sprite;
            transform.rotation = buildableObject.transform.rotation;
            foreach (Transform childernTransform in buildableObject.transform)
            {
                GameObject newChildrenObj = new GameObject(childernTransform.name);
                newChildrenObj.transform.SetParent(transform);
                SpriteRenderer newRenderer = newChildrenObj.AddComponent<SpriteRenderer>();
                SpriteRenderer oldRenderer = childernTransform.GetComponent<SpriteRenderer>();
                newRenderer.sprite = oldRenderer.sprite;
                newRenderer.color = oldRenderer.color;
                newChildrenObj.transform.position = childernTransform.position;
                newChildrenObj.transform.localScale = childernTransform.localScale;
            }
        }
        
        
        public BuildableObject ConfirmBuild()
        {
            BuildableObject finalBuildableObject = Instantiate(_buildableObject).GetComponent<BuildableObject>();
            finalBuildableObject.transform.position = transform.position;
            finalBuildableObject.SetOwner(_player);
            return finalBuildableObject;
        }
    }
}