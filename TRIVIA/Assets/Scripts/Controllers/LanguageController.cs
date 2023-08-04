using Helpers;
using Lean.Localization;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class LanguageController : MonoBehaviour
    {
        public void ChangeLanguage(int value)
        {
            switch (value)
            {
                case 0:
                    LeanLocalization.SetCurrentLanguageAll("English");
                    break;
            
                case 1:
                    LeanLocalization.SetCurrentLanguageAll("Italian");
                    break;
            
                case 2:
                    LeanLocalization.SetCurrentLanguageAll("German");
                    break;
            
                case 3:
                    LeanLocalization.SetCurrentLanguageAll("French");
                    break;
            
                case 4:
                    LeanLocalization.SetCurrentLanguageAll("Portuguese");
                    break;
            
                case 5:
                    LeanLocalization.SetCurrentLanguageAll("Spanish");
                    break;
            }
            PlayerPrefs.SetInt("language", value);
        }
    }
}
