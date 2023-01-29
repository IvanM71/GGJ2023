using System.Collections.Generic;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11
{
    public class InteractionVision : MonoBehaviour
    {
        private readonly List<IInteractable> _interactablesInVision = new();

        public IInteractable ClosestInteractable { get; private set; }

        private void Update()
        {
            FindClosestInteractable();
        }

        private void FindClosestInteractable()
        {
            if (_interactablesInVision.Count == 0)
            {
                ClosestInteractable = null;
                return;
            }

            ClosestInteractable = _interactablesInVision[0];
            var playerPos = transform.position;
            foreach (var interactable in _interactablesInVision)
            {
                var itemPos = interactable.GetPosition();
                var thisItemDistance = Vector2.Distance(playerPos, itemPos);
                var closesItemDistance = Vector2.Distance(playerPos, ClosestInteractable.GetPosition());
                if (thisItemDistance < closesItemDistance)
                    ClosestInteractable = interactable;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                _interactablesInVision.Add(interactable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                _interactablesInVision.Remove(interactable);
            }
        }
    }
}
