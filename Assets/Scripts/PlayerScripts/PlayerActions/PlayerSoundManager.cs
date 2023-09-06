using System;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerSoundManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip errorSoundClip;
        [SerializeField] private AudioClip meowSoundClip;
        [SerializeField] private AudioClip placedSoundClip;
        [SerializeField] private AudioClip pumpSoundClip;
        [SerializeField] private AudioClip mechPumpSoundClip;
        [SerializeField] private AudioClip refuelSoundClip;
        [SerializeField] private AudioClip BrushSoundClip;
        
        [SerializeField] private AudioClip powerDownClip;
        [SerializeField] private AudioClip powerUpClip;
        private float _powerCooldown;
        private float _maxPowerCooldown = 3.05f;

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

        public void PlayPlacedBuildingSound()
        {
            _audioSource.PlayOneShot(placedSoundClip);
        }

        public void PlayPumpSound()
        {
            _audioSource.PlayOneShot(pumpSoundClip);
        }
        
        public void PlayMechPumpSound()
        {
            _audioSource.PlayOneShot(mechPumpSoundClip);
        }
        
        public void PlayRefuelSound()
        {
            _audioSource.PlayOneShot(refuelSoundClip);
        }
        
        public void PlayBrushSound()
        {
            _audioSource.PlayOneShot(BrushSoundClip);
        }
        
        public void PlayPowerUpSound()
        {
            if (_powerCooldown >= 0)
            {
                return;
            }
            _audioSource.PlayOneShot(powerUpClip);
            _powerCooldown = _maxPowerCooldown;
        }
        
        public void PlayPowerDownSound()
        {
            if (_powerCooldown >= 0)
            {
                return;
            }
            _audioSource.PlayOneShot(powerDownClip);
            _powerCooldown = _maxPowerCooldown;
        }

        private void Update()
        {
            if (_powerCooldown >= 0)
            {
                _powerCooldown -= Time.deltaTime;
            }
        }
    }
}