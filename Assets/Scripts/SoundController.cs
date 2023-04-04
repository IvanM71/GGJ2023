using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Apollo11
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] EventReference axeHit;
        [SerializeField] EventReference bucketHit;
        [SerializeField] EventReference sawHit;
        [SerializeField] EventReference scissorsHit;
        [SerializeField] EventReference sprayerHit;
        [SerializeField] EventReference pickaxeHit;

        [SerializeField] EventReference getDamage;
        
        [SerializeField] EventReference steps;

        [SerializeField] EventReference rootDefeated;
        [SerializeField] EventReference rootsGrow;
        [SerializeField] EventReference rootsImpact;
        [SerializeField] EventReference rootsRoar;

        [SerializeField] EventReference itemOut;
        [SerializeField] EventReference itemIn;
        [SerializeField] EventReference pickItem;
        [SerializeField] EventReference throwItem;

        [SerializeField] EventReference toolCrafted;
        [SerializeField] EventReference toolWoosh;

        [SerializeField] EventReference winSound;
        [SerializeField] EventReference loseSound;
        [SerializeField] EventReference deathSound;

        [SerializeField] EventReference mainTheme;

        private float _musicVolume;
        private float _effectsVolume;
        
        private EventInstance _mainThemeEI;

        private void Awake()
        {
            _musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.9f);
            _effectsVolume = PlayerPrefs.GetFloat("effectsVolume", 0.5f);
        }
        private void Start()
        {
            _mainThemeEI = RuntimeManager.CreateInstance(mainTheme);
            PlayTheme(true);
        }
        public void PlayToolHit(Enums.HandWeapon weapon)
        {
            switch (weapon)
            {
                case Enums.HandWeapon.Axe:
                    
                    RuntimeManager.PlayOneShot(axeHit);
                    break;
                case Enums.HandWeapon.Saw:
                    RuntimeManager.PlayOneShot(sawHit);
                    break;
                case Enums.HandWeapon.Shears:
                    RuntimeManager.PlayOneShot(scissorsHit);
                    break;
                case Enums.HandWeapon.Pickaxe:
                    RuntimeManager.PlayOneShot(pickaxeHit);
                    break;
                case Enums.HandWeapon.Sprayer:
                    RuntimeManager.PlayOneShot(sprayerHit);
                    break;
                case Enums.HandWeapon.Bucket:
                    RuntimeManager.PlayOneShot(bucketHit);
                    break;
                case Enums.HandWeapon.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
            
        }
        public void PlayGetDamage()
        {
            RuntimeManager.PlayOneShot(getDamage);
        }
        public void PlayStep()
        {
            RuntimeManager.PlayOneShot(steps);
        }
        public void PlayRootDefeated()
        {
            RuntimeManager.PlayOneShot(rootDefeated);
        }
        public void PlayRootsGrow()
        {
            RuntimeManager.PlayOneShot(rootsGrow);
        }
        public void PlayRootsImpact()
        { 
            RuntimeManager.PlayOneShot(rootsImpact);
        }
        public void PlayRootsRoar()
        {
            RuntimeManager.PlayOneShot(rootsRoar);
        }
        public void PlayItemOut()
        {
            RuntimeManager.PlayOneShot(itemOut);
        }
        public void PlayItemIn()
        {
            RuntimeManager.PlayOneShot(itemIn);
        }
        public void PlayPickItem()
        { ;
            RuntimeManager.PlayOneShot(pickItem);
        }
        public void PlayThrowItem()
        {
            RuntimeManager.PlayOneShot(throwItem);
        }
        public void PlayToolCrafted()
        {
            RuntimeManager.PlayOneShot(toolCrafted);
        }
        public void PlayToolWoosh()
        {
            RuntimeManager.PlayOneShot(toolWoosh);
        }
        public void PlayWin()
        {
            RuntimeManager.PlayOneShot(winSound);
        }
        public void PlayLose()
        {
            RuntimeManager.PlayOneShot(loseSound);
        }
        public void PlayDeath()
        {
            RuntimeManager.PlayOneShot(deathSound);
        }
        public void PlayTheme(bool play)
        {
            //TODO ?
            _mainThemeEI.start();
        }

        private void OnDestroy()
        {
            _mainThemeEI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _mainThemeEI.release();
        }
    }
}
