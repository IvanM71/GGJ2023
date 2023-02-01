﻿using Apollo11.Core;
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

        public Vector2 GetIconOffset()
        {
            return new Vector2(0, 0.2f);
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

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