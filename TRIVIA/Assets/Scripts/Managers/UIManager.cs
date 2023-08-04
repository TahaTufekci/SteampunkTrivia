using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Canvas Groups
    [SerializeField] private CanvasGroup losePanel;
    [SerializeField] private CanvasGroup backButtonPanel;
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private CanvasGroup finishPanel;
    #endregion 
    
    [SerializeField] private Image mainMask;
    private bool isSettingsPanelOpen = false;
    [SerializeField] private Button moneyTreeButton;
    
    public void FadeInLosePanel()
    {
        losePanel.gameObject.SetActive(true);
        losePanel.gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
    }
    public void FadeInBackButtonPanel()
    {
        backButtonPanel.gameObject.SetActive(true);
        backButtonPanel.gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
    }
    public void FadeInWinPanel()
    {
        if (GameManager.instance.GetQuestionIndex() != 0) // in order to prevent the index errors, win panel doesn't show up in the 1 question
        {
            moneyTreeButton.interactable = true;
            winPanel.gameObject.SetActive(true);
            winPanel.gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
        }
    }
    public void FadeInFinishPanel()
    {
        finishPanel.gameObject.SetActive(true);
        finishPanel.gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
    }
   
    public void SetMaskState(Image mask, bool isActive, Action onClickAction = null)
    {
        if (isActive)
        {
            SetMaskClickAction(mask, onClickAction);
            mask.gameObject.SetActive(true);
        }
        else
        {
            mask.gameObject.SetActive(false);
        }
    }
    private void SetMaskClickAction(Image mask, Action action)
    {
        EventTrigger trigger = mask.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        trigger.triggers.Clear();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { action?.Invoke(); });
        trigger.triggers.Add(entry);
    }
    
    public void ControlPanels(GameState gameState)
    {
        if (gameState.HasFlag(GameState.Lose))
        {
            mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
            SetMaskState(mainMask, true);
            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(0.8f).OnStepComplete(() => FadeInLosePanel());
        }
        else if (gameState.HasFlag(GameState.Win))
        {
            mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
            SetMaskState(mainMask, true);
            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(0.8f).OnStepComplete(() => FadeInWinPanel());

        }
        else if(gameState.HasFlag(GameState.Pause))
        {
            mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
            SetMaskState(mainMask, true);
            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(0.2f).OnStepComplete(() => FadeInBackButtonPanel());
        }
        else if(gameState.HasFlag(GameState.Playing))
        {
            SetMaskState(mainMask, false);
        }
        else if(gameState.HasFlag(GameState.Finish))
        {
            mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
            SetMaskState(mainMask, true);
            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(0.8f).OnStepComplete(() => FadeInFinishPanel());
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (GameManager.instance.GetUIManager() == null) GameManager.instance.SetUIManager(this);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.OnGameStateChanged += ControlPanels;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameManager.OnGameStateChanged -= ControlPanels;
    }
}
