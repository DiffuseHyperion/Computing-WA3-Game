using System.Collections;
using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerUI
{
    public class PlayerUIBar : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve curve;

        [SerializeField] 
        private bool leftSide;
        
        private TextMeshProUGUI _text;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private readonly float _desiredTime = 1f;
        private float _elapsedTime;

        void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        void Start()
        {
            _startPosition = gameObject.transform.position;
            _endPosition = _startPosition;
            if (leftSide)
            {
                _endPosition.x += 300;
            }
            else
            {
                _endPosition.x -= 300;
            }
        }

        public void PreEnableBar() // skips animation
        {
            gameObject.transform.position = _endPosition;
            gameObject.SetActive(true);
        }

        public void EnableBar()
        {
            gameObject.transform.position = _startPosition;
            gameObject.SetActive(true);
            StartCoroutine(StartAnimation());
        }
        
        public void DisableBar()
        {
            gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetText()
        {
            return _text;
        }

        IEnumerator StartAnimation()
        {
            while (gameObject.transform.position != _endPosition)
            {
                _elapsedTime += Time.deltaTime;
                float percentageComplete = _elapsedTime / _desiredTime;
            
                gameObject.transform.position = Vector3.Lerp(_startPosition, _endPosition, curve.Evaluate(percentageComplete));
                yield return null;
            }
        }
    }
}