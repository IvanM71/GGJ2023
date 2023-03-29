using UnityEngine;
using UnityEngine.Events;

namespace Apollo11.Puzzles
{
    public abstract class APuzzle : MonoBehaviour
    {
        public UnityEvent OnSolved;
        public bool IsSolved { get; private set; }

        protected void AtSolved()
        {
            IsSolved = true;
            OnSolved?.Invoke();
        }
    }
}