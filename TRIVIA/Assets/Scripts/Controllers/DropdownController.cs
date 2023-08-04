using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class DropdownController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropDown;
        private void Start()
        {
            dropDown.value = PlayerPrefs.GetInt("Dropdown");

            dropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDown); });
        }
        public void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            switch(dropdown.value)
            {
                case 0: PlayerPrefs.SetInt("Dropdown", 0);
                    break;
                case 1: PlayerPrefs.SetInt("Dropdown", 1);
                    break;
                case 2:PlayerPrefs.SetInt("Dropdown", 2);
                    break;
                case 3:PlayerPrefs.SetInt("Dropdown", 3);
                    break;
                case 4:PlayerPrefs.SetInt("Dropdown", 4);
                    break;
                case 5: PlayerPrefs.SetInt("Dropdown", 5);
                    break;
            }
        }
    }
}