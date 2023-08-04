using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public enum ButtonEnum  {
 
        Answer1 = 0,
        Answer2 = 1,
        Answer3 = 2,
        Answer4 = 3
    }
    public class ButtonID : MonoBehaviour {
 
        public ButtonEnum myType;
        [SerializeField] public Button answerButton;

        private void Start()
        {
            answerButton = answerButton.GetComponent<Button>();
            ColorBlock buttonColor = answerButton.colors;
            buttonColor.highlightedColor = Color.yellow;
            answerButton.onClick.AddListener(delegate{ButtonClick(answerButton,myType);});

            answerButton.colors = buttonColor;
        }
   
        public void ButtonClick(Button button, ButtonEnum buttonType)
        {
            GameManager.instance.AnswerCheck(button, buttonType);
        }
    }
}