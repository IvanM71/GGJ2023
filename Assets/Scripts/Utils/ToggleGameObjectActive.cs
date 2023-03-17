using UnityEngine;

namespace Apollo11.Utils
{
    public class ToggleGameObjectActive : MonoBehaviour
    {
        [SerializeField] private GameObject @object;

        public void Toggle()
        {
            var status = @object.activeSelf;
            @object.SetActive(!status);
        }
    }
}
