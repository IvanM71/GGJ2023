using System;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class PlayOnSolvedSound : MonoBehaviour
    {
        [SerializeField] private SolvedSound sound = SolvedSound.BlocksGoDown;

        public void Play()
        {
            switch (sound)
            {
                case SolvedSound.None:
                    break;
                case SolvedSound.BlocksGoDown:
                    SystemsLocator.Inst.SoundController.PlayObstacleDown();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum SolvedSound
        {
            None,
            BlocksGoDown
        }
    }
}