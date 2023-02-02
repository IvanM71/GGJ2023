using Apollo11.Core;
using Apollo11.Interaction;
using UnityEngine;

namespace Apollo11.Items
{
    public class Item : MonoBehaviour, IInteractable, IInteractionTarget
    {
        [SerializeField] private Enums.Items itemType;
        [SerializeField] private Sprite iconSprite;
        public Enums.Items ItemType => itemType;
        public Sprite IconSprite => iconSprite;


        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.Item;

        public Vector2 GetPosition() => transform.position;
        public Vector2 GetIconPosition() => (Vector2)transform.position + new Vector2(0, 0.2f);

        public void OnInteractionStart()
        {
            SystemsLocator.Inst.PlayerItemCarry.TakeItem(this);
        }

        public void AtInteractionTargetSelected()
        {
            //outline? scale up?
        }

        
    }
}