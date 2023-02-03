using UnityEngine.Audio;
using UnityEngine;

namespace Apollo11
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource themeSource;

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

        [SerializeField] AudioClip itemOut;
        [SerializeField] AudioClip[] itemIn;
        [SerializeField] AudioClip[] pickItem;
        [SerializeField] AudioClip[] throwItem;

        [SerializeField] AudioClip[] toolCrafted;
        [SerializeField] AudioClip[] toolWoosh;

        [SerializeField] AudioClip winSound;
        [SerializeField] AudioClip loseSound;

        [SerializeField] AudioClip theme;

        public void PlayAxeHit()
        {
            audioSource.clip = axeHits[Random.Range(0, axeHits.Length)];
            audioSource.Play();
        }
        public void PlayBucketHit()
        {
            audioSource.clip = axeHits[Random.Range(0, axeHits.Length)];
            audioSource.Play();
        }
        public void PlaySawHit()
        {
            audioSource.clip = sawHits[Random.Range(0, sawHits.Length)];
            audioSource.Play();
        }
        public void PlayScissorsHit()
        {
            audioSource.clip = scissorsHits[Random.Range(0, scissorsHits.Length)];
            audioSource.Play();
        }
        public void PlaySprayerHit()
        {
            audioSource.clip = sprayerHits[Random.Range(0, sprayerHits.Length)];
            audioSource.Play();
        }
        public void PlayPickaxeHit()
        {
            audioSource.clip = pickaxeHits[Random.Range(0, pickaxeHits.Length)];
            audioSource.Play();
        }
        public void PlayGetDamage()
        {
            audioSource.clip = getDamage[Random.Range(0, getDamage.Length)];
            audioSource.Play();
        }
        public void PlayStep()
        {
            audioSource.clip = steps[Random.Range(0, steps.Length)];
            audioSource.Play();
        }
        public void PlayRootDefeated()
        {
            audioSource.clip = rootDefeated;
            audioSource.Play();
        }
        public void PlayRootsGrow()
        {
            audioSource.clip = rootsGrow[Random.Range(0, rootsGrow.Length)];
            audioSource.Play();
        }
        public void PlayRootsImpact()
        {
            audioSource.clip = rootsImpact[Random.Range(0, rootsImpact.Length)];
            audioSource.Play();
        }
        public void PlayRootsRoar()
        {
            audioSource.clip = rootsRoar[Random.Range(0, rootsRoar.Length)];
            audioSource.Play();
        }
        public void PlayItemOut()
        {
            audioSource.clip = itemOut;
            audioSource.Play();
        }
        public void PlayItemIn()
        {
            audioSource.clip = itemIn[Random.Range(0, itemIn.Length)];
            audioSource.Play();
        }
        public void PlayPickItem()
        {
            audioSource.clip = pickItem[Random.Range(0, pickItem.Length)];
            audioSource.Play();
        }
        public void PlayThrowItem()
        {
            audioSource.clip = throwItem[Random.Range(0, throwItem.Length)];
            audioSource.Play();
        }
        public void PlayToolCrafted()
        {
            audioSource.clip = toolCrafted[Random.Range(0, toolCrafted.Length)];
            audioSource.Play();
        }
        public void PlayToolWoosh()
        {
            audioSource.clip = toolWoosh[Random.Range(0, toolWoosh.Length)];
            audioSource.Play();
        }
        public void PlayWin()
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }
        public void PlayLose()
        {
            audioSource.clip = loseSound;
            audioSource.Play();
        }
        public void PlayTheme()
        {
            themeSource.clip = theme;
            themeSource.loop = true;
            themeSource.Play();
        }

        private void Start()
        {
            PlayTheme();
        }
    }
}
