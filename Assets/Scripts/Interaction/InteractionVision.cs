using System.Collections.Generic;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionVision : MonoBehaviour
    {
        public List<IInteractable> InteractablesInVision { get; } = new();
        public List<IDamagable> DamagablesInVision { get; } = new();
        
        
        public T FindClosestFromList<T>(List<T> list) where T : IGetPosition
        {
            if (list.Count == 0)
                return default;

            var closestElement = list[0];
            var playerPos = transform.position;
            foreach (var element in list)
            {
                var elementPos = element.GetPosition();
                var thisItemDistance = Vector2.Distance(playerPos, elementPos);
                var closesItemDistance = Vector2.Distance(playerPos, closestElement.GetPosition());
                if (thisItemDistance < closesItemDistance)
                    closestElement = element;
            }

            return closestElement;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                InteractablesInVision.Add(interactable);
                if (interactable is IInPlayerVision ipv)
                    ipv.AtPlayerVisionEnter();
            }
            if (col.gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                DamagablesInVision.Add(damagable);
                if (damagable is IInPlayerVision ipv)
                    ipv.AtPlayerVisionEnter();
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                InteractablesInVision.Remove(interactable);
                if (interactable is IInPlayerVision ipv)
                    ipv.AtPlayerVisionExit();
            }
            if (col.gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                DamagablesInVision.Remove(damagable);
                if (damagable is IInPlayerVision ipv)
                    ipv.AtPlayerVisionExit();
            }
        }
    }
}
