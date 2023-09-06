using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerSoundManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip errorSoundClip;
        [SerializeField] 
        private AudioClip meowSoundClip;

        private void Start()
        {
            _audioSource = GetComponentInChildren<PlayerCamera>().GetComponent<AudioSource>();
        }

        public void PlayErrorSound()
        {
            _audioSource.PlayOneShot(errorSoundClip);
        }
        
        public void PlayMeowSound()
        {
            _audioSource.PlayOneShot(meowSoundClip);
        }
    }
}