using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] Pause[] pausesSequence;
        

        private enum Pause
        {
            Short,
            Long
        }
        
        private struct PressSpan
        {
            public DateTime start;
            public DateTime end;
            public bool pressed;
        }

        private PressSpan[] _pressSpans;
        private List<int> _suitableSpansIndexesForNow = new (3);

        void Start ()
        {
            _pressSpans = new PressSpan[pausesSequence.Length+1];
            for (var i = 0; i < pausesSequence.Length+1; i++)
                _pressSpans[i] = new PressSpan();
            
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
            //print("Pressed");

            _suitableSpansIndexesForNow.Clear();
            for (var i = 0; i < _pressSpans.Length; i++)
            {
                var span = _pressSpans[i];
                if (span.start < DateTime.Now && span.end > DateTime.Now && span.pressed == false)
                {
                    _suitableSpansIndexesForNow.Add(i);
                }
            }
            
            if (_suitableSpansIndexesForNow.Count == 0)
            {
                AtWrongPress();
            }
            else
            {
                _pressSpans[_suitableSpansIndexesForNow[0]].pressed = true;
                AtCorrectPress();
            }
            
        }

        private void ResetBlinkCycle()
        {
            var nextBlinkDT = DateTime.Now + TimeSpan.FromSeconds(loopDelay);
            for (var i = 0; i < pausesSequence.Length + 1; i++)
            {
                _pressSpans[i].pressed = false;
                _pressSpans[i].start = nextBlinkDT - TimeSpan.FromSeconds(beforeBlinkTolerance);
                _pressSpans[i].end = nextBlinkDT + TimeSpan.FromSeconds(turnedOnTime+afterBlinkTolerance);
                
                if (i == pausesSequence.Length) continue;
                var pause = pausesSequence[i];
                nextBlinkDT += TimeSpan.FromSeconds(turnedOnTime+PauseEnumToSeconds(pause));
            }
        }
        
        private float PauseEnumToSeconds(Pause pauseEnum)
        {
            switch (pauseEnum)
            {
                case Pause.Short:
                    return shortPauseTime;
                case Pause.Long:
                    return longPauseTime;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pauseEnum), pauseEnum, null);
            }
        }
        
        private void AtCorrectPress()
        {
            print("+");
            CheckIfSolved();
        }

        private void AtWrongPress()
        {
            print("Wrong!");
            //TODO flag
        }

        void CheckIfSolved()
        {
            var hasNotPressedSpan = false;
            foreach (var span in _pressSpans)
            {
                if (span.pressed == false)
                {
                    hasNotPressedSpan = true;
                }
            }

            if (!hasNotPressedSpan)
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
                ResetBlinkCycle();
                yield return loopDelayWFS;
                
                for (var i = 0; i < pausesSequence.Length + 1; i++)
                {
                    Blink(true);
                    yield return blinkWFS;
                    Blink(false);

                    if (i == pausesSequence.Length) continue;
                    if (pausesSequence[i] == Pause.Short)
                        yield return shortPauseWFS;
                    else
                        yield return longPauseWFS;
                }
                
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
