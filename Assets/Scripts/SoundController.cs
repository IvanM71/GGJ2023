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
            audioSource.PlayOneShot(axeHits[Random.Range(0, axeHits.Length)]);
        }
        public void PlayBucketHit()
        {
            audioSource.PlayOneShot(axeHits[Random.Range(0, axeHits.Length)]);
        }
        public void PlaySawHit()
        {
            audioSource.PlayOneShot(sawHits[Random.Range(0, sawHits.Length)]);
        }
        public void PlayScissorsHit()
        {
            audioSource.PlayOneShot(scissorsHits[Random.Range(0, scissorsHits.Length)]);
        }
        public void PlaySprayerHit()
        {
            audioSource.PlayOneShot(sprayerHits[Random.Range(0, sprayerHits.Length)]);
        }
        public void PlayPickaxeHit()
        {
            audioSource.PlayOneShot(pickaxeHits[Random.Range(0, pickaxeHits.Length)]);
        }
        public void PlayGetDamage()
        {
            audioSource.PlayOneShot(getDamage[Random.Range(0, getDamage.Length)]);
        }
        public void PlayStep()
        {
            audioSource.PlayOneShot(steps[Random.Range(0, steps.Length)]);
        }
        public void PlayRootDefeated()
        {
            audioSource.PlayOneShot(rootDefeated);
        }
        public void PlayRootsGrow()
        {
            audioSource.PlayOneShot(rootsGrow[Random.Range(0, rootsGrow.Length)]);
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
            audioSource.PlayOneShot(itemOut);
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
            audioSource.PlayOneShot(toolWoosh[Random.Range(0, toolWoosh.Length)]);
        }
        public void PlayWin()
        {
            audioSource.PlayOneShot(winSound);
        }
        public void PlayLose()
        {
            audioSource.PlayOneShot(loseSound);
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
