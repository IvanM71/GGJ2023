using UnityEngine;

namespace Apollo11.Puzzles
{
    public class ObstacleBlock : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D collider2D;
        [SerializeField] private SpriteRenderer sr;
        
        private static readonly int HideSTH = Animator.StringToHash("Hide");

        public void Hide()
        {
            animator.SetTrigger(HideSTH);
            collider2D.enabled = false;
            sr.sortingOrder = -10;
        }
    }
}