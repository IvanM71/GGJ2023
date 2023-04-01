using Apollo11.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace Apollo11
{
    public class PuzzleButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private Vector2 interactionIconOffset = new(0f, 0.65f);
        [SerializeField] private UnityEvent OnPressed;

        private Enums.InteractableObjectType _type = Enums.InteractableObjectType.Action;

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
            //TODO animation
        }
    }
}
