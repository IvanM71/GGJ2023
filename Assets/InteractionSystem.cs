using System;
using UnityEngine;

namespace Apollo11
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private InteractionVision playersInteractionVision;
        [SerializeField] private GameObject interactionIcon;

        private void Update()
        {
            var obj = playersInteractionVision.ClosestInteractable;
            if (obj == null)
            {
                interactionIcon.SetActive(false);
                return;
            }
            
            UpdateIcon(obj);

            if (Input.GetKeyDown(KeyCode.E))
            {
                obj.OnButtonDown();
            }else if (Input.GetKeyUp(KeyCode.E))
            {
                obj.OnButtonUp();
            }
        }

        private void UpdateIcon(IInteractable obj)
        {
            var pos = obj.GetPosition() + obj.GetIconOffset();
            interactionIcon.transform.position = pos;
            interactionIcon.SetActive(true);
        }
    }
}
