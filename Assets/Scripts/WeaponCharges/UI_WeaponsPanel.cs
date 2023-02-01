using TMPro;
using UnityEngine;

namespace Apollo11.WeaponCharges
{
    public class UI_WeaponsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text weapon1TMP;
        [SerializeField] private TMP_Text weapon2TMP;
        [SerializeField] private TMP_Text weapon3TMP;

        public void SetValues(int w1, int w2, int w3)
        {
            weapon1TMP.text = w1.ToString();
            weapon2TMP.text = w2.ToString();
            weapon3TMP.text = w3.ToString();
        }
    }
}
