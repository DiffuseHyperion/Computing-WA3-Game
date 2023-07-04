using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerSoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip errorSoundClip;

        public void PlayErrorSound()
        {
            audioSource.PlayOneShot(errorSoundClip);
        }
    }
}