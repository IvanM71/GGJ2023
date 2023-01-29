using UnityEngine;

namespace Apollo11.Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private Enums.Items itemType;
        public Enums.Items ItemType => itemType;
        
        
        public void OnSelect()
        {
            //outline? scale up?
        }

        public Vector2 GetIconOffset()
        {
            return new Vector2(0, 0.2f);
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }
    }

    public interface IInteractable
    {
        public void OnSelect();
        public Vector2 GetIconOffset();
        public Vector2 GetPosition();
    }
}