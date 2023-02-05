using System;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Apollo11
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource themeSource;
        
        [SerializeField] bool playThemeOnStart = true;
        
        [SerializeField] AudioClip[] axeHits;
        [SerializeField] AudioClip[] bucketHits;
        [SerializeField] AudioClip[] sawHits;
        [SerializeField] AudioClip[] scissorsHits;
        [SerializeField] AudioClip[] sprayerHits;
        [SerializeField] AudioClip[] pickaxeHits;

        [SerializeField] AudioClip[] getDamage;
        [SerializeField] AudioClip[] steps;

        [SerializeField] AudioClip rootDefeated;
        [SerializeField] AudioClip[] rootsGrow;
        [SerializeField] AudioClip[] rootsImpact;
        [SerializeField] AudioClip[] rootsRoar;

        [SerializeField] AudioClip[] itemOut;
        [SerializeField] AudioClip[] itemIn;
        [SerializeField] AudioClip[] pickItem;
        [SerializeField] AudioClip[] throwItem;

        [SerializeField] AudioClip[] toolCrafted;
        [SerializeField] AudioClip[] toolWoosh;

        [SerializeField] AudioClip winSound;
        [SerializeField] AudioClip loseSound;
        [SerializeField] AudioClip deathSound;

        [SerializeField] AudioClip theme;
        
        private void Start()
                {
                    if (playThemeOnStart)
                    {
                        PlayTheme(true);
                    }

                }

        
        public void PlayToolHit(Enums.HandWeapon weapon)
        {
            switch (weapon)
            {
                case Enums.HandWeapon.Axe:
                    audioSource.PlayOneShot(axeHits[Random.Range(0, axeHits.Length)]);
                    break;
                case Enums.HandWeapon.Saw:
                    audioSource.PlayOneShot(sawHits[Random.Range(0, sawHits.Length)]);
                    break;
                case Enums.HandWeapon.Shears:
                    audioSource.PlayOneShot(scissorsHits[Random.Range(0, scissorsHits.Length)]);
                    break;
                case Enums.HandWeapon.Pickaxe:
                    audioSource.PlayOneShot(pickaxeHits[Random.Range(0, pickaxeHits.Length)]);
                    break;
                case Enums.HandWeapon.Sprayer:
                    audioSource.PlayOneShot(sprayerHits[Random.Range(0, sprayerHits.Length)]);
                    break;
                case Enums.HandWeapon.Bucket:
                    audioSource.PlayOneShot(bucketHits[Random.Range(0, bucketHits.Length)]);
                    break;
                case Enums.HandWeapon.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
            
        }
        
        
        public void PlayGetDamage()
        {
            audioSource.PlayOneShot(getDamage[Random.Range(0, getDamage.Length)]);
        }
        public void PlayStep()
        {
            audioSource.PlayOneShot(steps[Random.Range(0, steps.Length)], 0.4f);
        }
        public void PlayRootDefeated()
        {
            audioSource.PlayOneShot(rootDefeated);
        }
        public void PlayRootsGrow()
        {
            audioSource.PlayOneShot(rootsGrow[Random.Range(0, rootsGrow.Length)], 0.2f);
        }
        public void PlayRootsImpact()
        { 
            audioSource.PlayOneShot(rootsImpact[Random.Range(0, rootsImpact.Length)]);
        }
        public void PlayRootsRoar()
        {
            audioSource.PlayOneShot(rootsRoar[Random.Range(0, rootsRoar.Length)]);
        }
        public void PlayItemOut()
        {
            audioSource.PlayOneShot(itemOut[Random.Range(0, itemOut.Length)]);
        }
        public void PlayItemIn()
        {
            audioSource.PlayOneShot(itemIn[Random.Range(0, itemIn.Length)]);
        }
        public void PlayPickItem()
        { ;
            audioSource.PlayOneShot(pickItem[Random.Range(0, pickItem.Length)]);
        }
        public void PlayThrowItem()
        {
            audioSource.PlayOneShot(throwItem[Random.Range(0, throwItem.Length)]);
        }
        public void PlayToolCrafted()
        {
            audioSource.PlayOneShot(toolCrafted[Random.Range(0, toolCrafted.Length)]);
        }
        public void PlayToolWoosh()
        {
            audioSource.PlayOneShot(toolWoosh[Random.Range(0, toolWoosh.Length)], 0.3f);
        }
        public void PlayWin()
        {
            audioSource.PlayOneShot(winSound);
        }
        public void PlayLose()
        {
            audioSource.PlayOneShot(loseSound);
        }
        public void PlayDeath()
        {
            audioSource.PlayOneShot(deathSound); 
        }
        public void PlayTheme(bool play)
        {
            if (play)
            {
                themeSource.clip = theme;
                themeSource.loop = true;
                themeSource.Play();
            }
            else
            {
                DOTween.To(value => themeSource.volume = value, 1f, 0f, 1f)
                    .OnComplete(() => themeSource.Stop());
                
            }
            
        }

        
    }
}
