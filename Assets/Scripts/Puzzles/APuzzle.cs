using UnityEngine;
using UnityEngine.Events;

namespace Apollo11.Puzzles
{
    public abstract class APuzzle : MonoBehaviour
    {
        public UnityEvent OnSolved;
        public UnityEvent OnSolvedWrong;
        public bool IsSolved { get; private set; }

        protected void AtSolved()
        {
            IsSolved = true;
            OnSolved?.Invoke();
        }
        
        protected void AtSolvedWrong()
        {
            OnSolvedWrong?.Invoke();
        }
    }
}