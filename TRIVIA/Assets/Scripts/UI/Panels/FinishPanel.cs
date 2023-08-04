using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Localization;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
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
       scoreText.text = "CONGRATS! YOU FINISHED AND WON {SCORE_INT} POINTS";
       GameManager.instance.SaveScore();
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
