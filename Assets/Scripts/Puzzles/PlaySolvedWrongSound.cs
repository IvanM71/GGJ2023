using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class PlaySolvedWrongSound : MonoBehaviour
    {
        public void Play()
        {
            SystemsLocator.Inst.SoundController.PlayPuzzleSolvedWrong();
        }
    }
}