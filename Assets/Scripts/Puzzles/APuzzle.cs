using UnityEngine;
using UnityEngine.Events;

namespace Apollo11.Puzzles
{
    public abstract class APuzzle : MonoBehaviour
    {
        public UnityEvent OnSolved;
        
        protected void AtSolved()
        {
            OnSolved?.Invoke();
        }
    }
}