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
        [Space]
        [SerializeField] float[] sequence;

        private bool isSolved = false;
        private bool isCoroutineRunning = false;
        private int rightPressesCount = 0;
        private bool isPressNeeded = false;
        private List<float[]> sequences = new List<float[]>();

        void Start ()
        {
            StartCoroutine(Bulbing());
            StartCoroutine(SolvingCheck());
            sequences.Add(new float[] { 3f, 0.3f, 0.3f, 1.5f, 0.3f, 0.3f, 1.5f, 0.5f });
            sequences.Add(new float[] { 3f, 0.3f, 0.3f, 0.3f, 1.5f, 1.5f, 0.3f, 0.3f, 0.3f, 1.5f, 1.5f });
            sequences.Add(new float[] { 3f, 1.5f, 0.3f, 0.3f, 0.5f, 1.5f, 0.5f });
            sequences.Add(new float[] { 3f, 0.8f, 0.8f, 0.8f, 1.2f, 1.5f, 0.5f, 0.5f });
            sequences.Add(new float[] { 3f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0.5f });
        }

        private void Update()
        {
            if (isSolved)
                return;

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
        }

        IEnumerator SolvingCheck()
        {
            while (!isSolved)
            {
                if (rightPressesCount == sequence.Length - 1)
                {
                    Debug.Log("SOLVED!");
                    isSolved = true;
                    StopAllCoroutines();
                    AtSolved();
                }
                yield return null;
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
