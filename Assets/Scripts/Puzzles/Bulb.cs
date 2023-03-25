using Apollo11.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class Bulb: APuzzle
    {
        [SerializeField] Sprite bulbOn;
        [SerializeField] Sprite bulbOff;
        [SerializeField] SpriteRenderer bulbSpriteRenderer;

        [SerializeField] float[] sequence;

        private bool isSolved = false;
        private bool isCoroutineRunning = false;
        private int rightPressesCount = 0;
        private bool isPressNeeded = false;

        void Start ()
        {
            StartCoroutine(Bulbing());
        }

        private void Update()
        {
            if (isSolved)
            {
                StopAllCoroutines();
                AtSolved();
                return;
            }

            if (!isCoroutineRunning && !isSolved)
            {
                StartCoroutine(Bulbing());
            }

            if (Input.GetKeyDown(KeyCode.Space) && isPressNeeded)
            {
                Debug.Log("Pressed right!");
                isPressNeeded = false;
                rightPressesCount++;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isPressNeeded)
            {
                Debug.Log("Pressed wrong!");
                rightPressesCount = 0;
            }

            if(rightPressesCount == 8)
            {
                Debug.Log("SOLVED!");
                isSolved = true;
            }
        }

        IEnumerator Bulbing()
        {
            isCoroutineRunning = true;
            rightPressesCount = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                Debug.Log("Turn off for " + sequence[i] + " seconds");
                bulbSpriteRenderer.sprite = bulbOff;
                isPressNeeded = false;

                yield return new WaitForSeconds(sequence[i]);

                Debug.Log("Turn on");
                bulbSpriteRenderer.sprite = bulbOn;
                isPressNeeded = true;

                yield return new WaitForSeconds(1);
            }
            isCoroutineRunning = false;
        }
    }
}
