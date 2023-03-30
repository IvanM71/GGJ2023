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
        [SerializeField] private List<Plate> wallSymbols;
        [SerializeField] private PuzzleProgressBar progressBar;

        private int s1, s2, s3; //correct symbols to solve the puzzle
        private List<int> _input = new (3);

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
                plate.Init(i, symbolsUp[i], symbolsDown[i], this);
                plate.OnPlatePressed += AtPlatePressed;
            }
        }

        private void AtPlatePressed(Plate plate)
        {
            if (IsSolved) return;
            print($"Pressed plate {plate.PlateID}");
            
            _input.Add(plate.PlateID);

            if(_input.Count == 1)
                progressBar.SetProgress(0.33f);
            
            if(_input.Count == 2)
                progressBar.SetProgress(0.66f);

            if (_input.Count == 3)
            {
                progressBar.SetProgress(0.999f);
                if (ValidateInput())
                    AtSolved();
                else
                    AtSolvedWrong();
            }

            
        }

        private void AtSolvedWrong()
        {
            _input.Clear();
            //indication
            print("BAD!");
        }

        private bool ValidateInput()
        {
            var combinations = new List<int>
            {
                s1 * 100 + s2 * 10 + s3,
                s1 * 100 + s3 * 10 + s2,
                s2 * 100 + s1 * 10 + s3,
                s2 * 100 + s3 * 10 + s1,
                s3 * 100 + s1 * 10 + s2,
                s3 * 100 + s2 * 10 + s1
            };

            var userInput = _input[0] * 100 + _input[1] * 10 + _input[2];

            return combinations.Contains(userInput);
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
            
            print($"Pass: {s1} {s2} {s3}");
        }
    }
}