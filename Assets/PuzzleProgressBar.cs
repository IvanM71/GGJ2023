using DG.Tweening;
using UnityEngine;

namespace Apollo11
{
    public class PuzzleProgressBar : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer positiveRenderer;
        [SerializeField] private SpriteRenderer negativeRenderer;
        public void SetProgress(float percentage01)
        {
            Debug.Log($"set progress to {percentage01}");
            //animator.speed = 1;
            animator.Play("ProgressBar", -1, percentage01);
            //animator.CrossFade("ProgressBar", 1, -1, percentage01);
        }

        public void IndicatePositive()
        {
            
        }
        
        public void IndicateNegative()
        {
            
        }
        
    }
}