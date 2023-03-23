using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Apollo11.Puzzles
{
    public class PlatePuzzle : APuzzle
    {
        [SerializeField] private List<Sprite> symbolsUp;
        [SerializeField] private List<Sprite> symbolsDown;
        [SerializeField] private List<Plate> plates;

        private int s1, s2, s3; //correct symbols to solve the puzzle
        
        private void Start()
        {
            SetupPlates();
            SelectRandomSymbols();
        }

        private void SetupPlates()
        {
            if (plates.Count != symbolsUp.Count)
                throw new Exception("Different symbols and plates count!");

            for (var i = 0; i < plates.Count; i++)
            {
                var plate = plates[i];
                plate.Init(i, symbolsUp[i], symbolsDown[i]);
            }
        }


        private void SelectRandomSymbols()
        {
            int GetRandomSymbol() => Random.Range(0, symbolsUp.Count);
            
            if (symbolsUp.Count < 2)
                throw new Exception("Not enough symbols specified in PlatePuzzle!");

            s1 = GetRandomSymbol();
            s2 = GetRandomSymbol();
            s3 = GetRandomSymbol();

            if (s1 == s2 && s2 == s3)
                SelectRandomSymbols();
        }
    }
}