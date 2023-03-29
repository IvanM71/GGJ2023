using System;
using System.Collections;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class Bulb: APuzzle
    {
        [SerializeField] Sprite bulbOn;
        [SerializeField] Sprite bulbOff;
        [SerializeField] SpriteRenderer bulbSpriteRenderer;
        [SerializeField] AudioSource audioS;
        [Space]
        [SerializeField] float turnedOnTime = 0.25f;
        [SerializeField] float beforeBlinkTolerance = 0.1f;
        [SerializeField] float afterBlinkTolerance = 0.1f;
        [Space]
        [SerializeField] float shortPauseTime = 0.2f;
        [SerializeField] float longPauseTime = 0.5f;
        [SerializeField] float loopDelay = 2f;
        [Space]
        [SerializeField] Pause[] sequence;
        
        private int rightPressesCount = 0;
        private DateTime currentPressDT;
        private DateTime nextPressDT;
        private bool currentPressDone;
        
        private TimeSpan beforeToleranceTS;
        private TimeSpan afterToleranceTS;
        private TimeSpan loopDelayTS;
        private TimeSpan blinkTS;
        private WaitForSeconds afterBlinkToleranceWFS;

        private enum Pause
        {
            Short,
            Long
        }

        void Start ()
        {
            beforeToleranceTS = TimeSpan.FromSeconds(beforeBlinkTolerance);
            afterToleranceTS = TimeSpan.FromSeconds(afterBlinkTolerance);
            loopDelayTS = TimeSpan.FromSeconds(loopDelay);
            blinkTS = TimeSpan.FromSeconds(turnedOnTime);
            afterBlinkToleranceWFS = new WaitForSeconds(afterBlinkTolerance);
            
            StartCoroutine(Blinking());
        }
        

        private void Update()
        {
            if (IsSolved)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AtPlayerInteracts();
            }

        }

        private void AtPlayerInteracts()
        {
            print("Pressed");
            var minTime = currentPressDT - beforeToleranceTS;
            var maxTime = currentPressDT + blinkTS + afterToleranceTS;
            var currentTime = DateTime.Now;

            if (currentTime > minTime && currentTime < maxTime && !currentPressDone)
                AtCorrectPress();
            else
                AtWrongPress();
        }

        private void AtCorrectPress()
        {
            print("+");
            currentPressDone = true;
            rightPressesCount++;
            CheckIfSolved();
        }

        private void AtWrongPress()
        {
            print("Wrong!");
            currentPressDone = false;
            rightPressesCount = 0;
        }

        void CheckIfSolved()
        {
            if (rightPressesCount == sequence.Length - 1)
            {
                print("SOLVED!");
                StopAllCoroutines();
                AtSolved();
            }
        }

        IEnumerator Blinking()
        {
            var shortPauseWFS = new WaitForSeconds(shortPauseTime);
            var longPauseWFS = new WaitForSeconds(longPauseTime);
            var loopDelayWFS = new WaitForSeconds(loopDelay);
            var blinkWFS = new WaitForSeconds(turnedOnTime);
            
            while (true)
            {
                currentPressDT = DateTime.Now + loopDelayTS;
                yield return loopDelayWFS;
                
                for (var i = 0; i < sequence.Length; i++)
                {
                    Blink(true);
                    yield return blinkWFS;
                    Blink(false);
                    StartCoroutine(SwapPressDT());

                    if (sequence[i] == Pause.Short)
                    {
                        nextPressDT = DateTime.Now + TimeSpan.FromSeconds(shortPauseTime); 
                        yield return shortPauseWFS;
                    }
                    else
                    {
                        nextPressDT = DateTime.Now + TimeSpan.FromSeconds(longPauseTime); 
                        yield return longPauseWFS;
                    }
                }
                
            }
        }

        IEnumerator SwapPressDT()
        {
            yield return afterBlinkToleranceWFS;
            currentPressDT = nextPressDT;
            if (currentPressDone == false)
            {
                print("miss");
                rightPressesCount = 0;
            }
        }

        private void Blink(bool on)
        {
            bulbSpriteRenderer.sprite = on ? bulbOn : bulbOff;
            if (on)
            {
                audioS.PlayOneShot(audioS.clip);
            }
        }
    }
}
