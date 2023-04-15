using UnityEngine;

namespace Apollo11.Puzzles
{
    public class ObstacleBlock : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D col2D;
        [SerializeField] private SpriteRenderer sr;
        
        private static readonly int HideSTH = Animator.StringToHash("Hide");

        public void Hide()
        {
            animator.SetTrigger(HideSTH);
            col2D.enabled = false;
            sr.sortingOrder = -10;
        }
    }
}