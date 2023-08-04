using DG.Tweening;
using Lean.Localization;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] public LeanToken SCORE_INT;

        public void PlayAgain()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(1);
        }

        public void MainMenuButtonFunction()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        }

        public void ShowTheScore()
        {
            SCORE_INT.SetValue(GameManager.instance.GetScore());
        }
        public void OnEnable()
        {
            EnableAnimation();
        }

        public void EnableAnimation()
        {
            gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
            ShowTheScore();
        }
    }
}
