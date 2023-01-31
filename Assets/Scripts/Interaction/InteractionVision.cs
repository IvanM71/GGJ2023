using System.Collections.Generic;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionVision : MonoBehaviour
    {
        public List<IInteractable> InteractablesInVision { get; } = new();

        public IInteractable FindClosestInteractable(List<IInteractable> list)
        {
            if (list.Count == 0)
                return null;

            var ClosestInteractable = list[0];
            var playerPos = transform.position;
            foreach (var interactable in list)
            {
                var itemPos = interactable.GetPosition();
                var thisItemDistance = Vector2.Distance(playerPos, itemPos);
                var closesItemDistance = Vector2.Distance(playerPos, ClosestInteractable.GetPosition());
                if (thisItemDistance < closesItemDistance)
                    ClosestInteractable = interactable;
            }

            return ClosestInteractable;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                InteractablesInVision.Add(interactable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                InteractablesInVision.Remove(interactable);
            }
        }
    }
}
