using UnityEngine;

namespace Apollo11
{
    public class ToggleGameObjectActive : MonoBehaviour
    {
        [SerializeField] private GameObject gameObject;

        public void Toggle()
        {
            var status = gameObject.activeSelf;
            gameObject.SetActive(!status);
        }
    }
}
