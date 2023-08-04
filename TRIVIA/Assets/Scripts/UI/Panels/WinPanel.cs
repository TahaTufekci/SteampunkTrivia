using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject moneyTreePanel;

    public void markTheMoney()
    {
        Image[] moneyTree = moneyTreePanel.gameObject.GetComponentsInChildren<Image>();
        int index = GameManager.instance.GetQuestionIndex();
        moneyTree[--index].color = Color.green;
    }
    
    public void OnEnable()
    {
        EnableAnimation();
    }

    public void EnableAnimation()
    {
        markTheMoney();
        Sequence sequence = DOTween.Sequence();
        sequence.PrependInterval(1f).OnComplete( () => {
            DisableAnimation();
        } );
        GameManager.instance.ChangeGameState(GameState.Playing);
    }
    public void DisableAnimation()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.5f).From(Vector3.one).OnComplete( () => {
            gameObject.SetActive(false);
        } );
    }

    public GameObject GetMoneyTree()
    {
        return moneyTreePanel;
    }
}
