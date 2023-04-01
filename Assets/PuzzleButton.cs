using Apollo11.Interaction;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Apollo11
{
    public class PuzzleButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer buttonSR;
        [SerializeField] private Sprite buttonUpSprite;
        [SerializeField] private Sprite buttonDownSprite;
        [Space]
        [SerializeField] private Vector2 interactionIconOffset = new(0f, 0.65f);
        [SerializeField] private UnityEvent OnPressed;

        private Enums.InteractableObjectType _type = Enums.InteractableObjectType.Action;

        private Tween _pressTween;

        public void Activate(bool on)
        {
            _type = on ? Enums.InteractableObjectType.Action : Enums.InteractableObjectType.Inactive;
        }

        public Vector2 GetPosition() => (Vector2)transform.position;

        public Vector2 GetIconPosition() => (Vector2)transform.position + interactionIconOffset;

        public Enums.InteractableObjectType GetInteractableType() => _type;

        public void OnInteractionStart()
        {
            OnPressed?.Invoke();
            Animate();
            //TODO animation
        }

        private void Animate()
        {
            _pressTween?.Kill();

            buttonSR.sprite = buttonUpSprite;
            _pressTween = DOTween.Sequence()
                .AppendInterval(0.075f)
                .AppendCallback(() => buttonSR.sprite = buttonDownSprite)
                .AppendInterval(0.15f)
                .AppendCallback(() => buttonSR.sprite = buttonUpSprite)
                ;
        }
    }
}
