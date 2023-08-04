using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using Lean.Localization;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField] public LeanToken SCORE_INT;

        public void ChangeTheGameState()
        {
            GameManager.instance.ChangeGameState(GameState.Pause);
        }
        
        public void Continue()
        {
            DOTween.KillAll();
            gameObject.SetActive(false);
            GameManager.instance.ChangeGameState(GameState.Playing);
        }

        public void TakePoints()
        {
            DOTween.KillAll();
            if (GameManager.instance.GetQuestionIndex() != 0) // not to add any point because the first question hasn't answered yet
            {
                GameManager.instance.SaveScore();
            }
            SceneManager.LoadScene(0);
        }

        public void ShowTheText()
        {
            if (GameManager.instance.GetQuestionIndex() != 0)
            {
                SCORE_INT.SetValue(GameManager.instance.GetScore());
            }
            else
            {
                SCORE_INT.SetValue(0);
            }
        }
        public void OnEnable()
        {
            EnableAnimation();
        }

        public void EnableAnimation()
        {
            gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
            ShowTheText();
        }

    }
}

