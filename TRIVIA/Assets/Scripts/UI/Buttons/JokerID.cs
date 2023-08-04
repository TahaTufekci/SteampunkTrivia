using System.Collections;
using System.Collections.Generic;
using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace UI.Buttons
{
    public enum JokerEnum{
    
        Joker5050 = 0,
        X2Joker = 1,
        AnswerJoker = 2,
        ChangeJoker = 3
    }
    public class JokerID : MonoBehaviour
    {
        public JokerEnum jokerType; 
        [SerializeField] public Button jokerButton ;
        
        [SerializeField] public Button changeJoker ;
        [SerializeField] public GameObject buttonPanel;


        void Start()
        {
            jokerButton = jokerButton.GetComponent<Button>();
            jokerButton.onClick.AddListener(delegate{JokerClick(jokerButton,jokerType);});
        }

        public void JokerClick(Button jokerButton, JokerEnum jokerType)
        {
            switch (jokerType)
            {
                case JokerEnum.Joker5050:
                    int wrongChoiceNumber1 = WrongChoiceSelector();
                    int wrongChoiceNumber2 = WrongChoiceSelector();
                    while (wrongChoiceNumber1 == GameManager.instance.GetCurrentQuestion().correctAnswerID || wrongChoiceNumber2 == wrongChoiceNumber1)
                    {
                        wrongChoiceNumber1 = WrongChoiceSelector();
                    }
                    while (wrongChoiceNumber2 == GameManager.instance.GetCurrentQuestion().correctAnswerID || wrongChoiceNumber2 == wrongChoiceNumber1)
                    {
                        wrongChoiceNumber2 = WrongChoiceSelector();
                    }
                
                    DeleteButtons(wrongChoiceNumber1);
                    DeleteButtons(wrongChoiceNumber2);
                    CloseJokers(jokerButton);
                    break;

                case JokerEnum.X2Joker:
                    GameManager.instance.is2XJokerOn = true;
                    CheckChangeJoker(GameManager.instance.is2XJokerOn);
                    CloseJokers(jokerButton);
                    break;

                case JokerEnum.AnswerJoker:
                    GameManager.instance.isAnswerTrue = true;
                    ShowTheAnswer();
                    StartCoroutine(GameManager.instance.WaitForTheNextQuestion());
                    CloseJokers(jokerButton);
                    break;

                case JokerEnum.ChangeJoker:
                    StartCoroutine(GameManager.instance.WaitForTheNextQuestion());
                    CloseJokers(jokerButton);  
                    break;
            }
        }
        
        public void CloseJokers(Button jokerButton)
        {
            jokerButton = jokerButton.GetComponent<Button>();
            jokerButton.interactable = false;
            
        }

        public void CheckChangeJoker(bool is2XJokerOn)
        {
            changeJoker = changeJoker.gameObject.GetComponent<Button>();
            if (changeJoker.interactable)
            {
                if (is2XJokerOn)
                {
                    changeJoker.enabled = false;
                }
                else
                {
                    changeJoker.enabled = true;
                }
            }

        }
        public void DeleteButtons(int wrongChoiceNumber)
        {
            Button[] answerButtons = GameManager.instance.buttonPanel.gameObject.GetComponentsInChildren<Button>();
            switch (wrongChoiceNumber)
            {
                case 0:
                    answerButtons[0].interactable = false;
                    break;
                case 1:
                    answerButtons[1].interactable = false;
                    break;
                case 2:
                    answerButtons[2].interactable = false;
                    break;
                case 3:
                    answerButtons[3].interactable = false;
                    break;
            }
        
        }
        public static int WrongChoiceSelector()
        {
            Random rnd = new Random();
            int number  = rnd.Next(0,4);
            return number;
        }
        public void ShowTheAnswer()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            switch (GameManager.instance.GetCurrentQuestion().correctAnswerID)
            {
                case 0:
                    answerButtons[0].image.color = Color.green;
                    break;
                case 1:
                    answerButtons[1].image.color = Color.green;
                    break;
                case 2:
                    answerButtons[2].image.color = Color.green;
                    break;
                case 3:
                    answerButtons[3].image.color = Color.green;
                    break;
            }

            int questionIndex = GameManager.instance.GetQuestionIndex();
            GameManager.instance.SetQuestionIndex(++questionIndex);
            GameManager.instance.ChangeGameState(GameState.Win);
            GameManager.instance.is2XJokerOn = false;
        }


    }
}