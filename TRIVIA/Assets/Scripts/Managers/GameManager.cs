using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using JSONReader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using GameObjects;
using UI.Buttons;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region UI
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI questionCounter;
        [SerializeField] private WinPanel winPanel;
        [SerializeField] public GameObject buttonPanel;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private JokerID jokerID;
        #endregion
        
        #region Booleans
        [SerializeField] public bool isAnswerTrue = false;
        [SerializeField] public bool is2XJokerOn = false;
        #endregion
        
        #region Integers
        private static int numOfAnswers = 0;
        private static int score = 0;
        private static int totalScore = 0;
        private static int questionIndex;
        #endregion
        
        #region States
        public GameState currentGameState = GameState.Default;
        public static Language currentLanguage;
        public static Action<GameState> OnGameStateChanged;
        #endregion

        #region Lists
        [SerializeField] private static List<Question> questionList;
        #endregion

        [SerializeField] public QuestionReader questionFromJson;
        private Question currentQuestion;
        [SerializeField]private ADController adController;

        void Start()
        {
            questionIndex = 0; // Started from 0 cause the array in win panel
            LoadNewQuestion();
        }
        public void ScoreTracer()
        {
            int index = questionIndex; // at this point the index is 0 because of increment in CheckMarkedAnswer function
            index = (questionIndex == 0) ? ++index : index; // checking for the last question index to stop the increment

            Image[] moneyTree = winPanel.GetMoneyTree().gameObject.GetComponentsInChildren<Image>(); // the whole money tree array
            TextMeshProUGUI[] moneyTextValue = moneyTree[--index].gameObject.GetComponentsInChildren<TextMeshProUGUI>(); // taking the text for its value
            if (questionIndex == 0)
            {
                score = 0;
            }
            else
            {
                int.TryParse(moneyTextValue[0].text,out int value);
                score = value;
            }
            ShowTheCurrentQuestionInfo();
        }
        public void ShowTheCurrentQuestionInfo()
        {
            int index = questionIndex;
            index = (questionIndex == 12) ? --index : index; // checking for the last question index to stop the increment

            Image[] moneyTree = winPanel.GetMoneyTree().gameObject.GetComponentsInChildren<Image>(); // the whole money tree array
            TextMeshProUGUI[] moneyText = moneyTree[index].gameObject.GetComponentsInChildren<TextMeshProUGUI>(); // taking the text for showing the user
      
            scoreText.text = "" + moneyText[1].text;
            questionCounter.text = (questionIndex == 12) ? questionIndex + "/12" : questionIndex + 1 + "/12";
        }
        public void SaveScore()
        {
            if (PlayerPrefs.HasKey("totalScore"))
            {
                PlayerPrefs.SetInt("totalScore",PlayerPrefs.GetInt("totalScore") + score);
            }
            else
            {
                PlayerPrefs.SetInt("totalScore",score);
            }
        }
        public bool AnswerCheck(Button button, ButtonEnum buttonType)
        {
            switch (buttonType)
            {
                case ButtonEnum.Answer1:
                   CheckMarkedAnswer(button,ButtonEnum.Answer1);
                   break;
                
                case ButtonEnum.Answer2:
                    CheckMarkedAnswer(button,ButtonEnum.Answer2);
                    break;
                
                case ButtonEnum.Answer3:
                    CheckMarkedAnswer(button,ButtonEnum.Answer3);
                    break;
                
                case ButtonEnum.Answer4:
                    CheckMarkedAnswer(button,ButtonEnum.Answer4);
                    break;
                
                default:
                    isAnswerTrue = false;
                    break;
            }
            return isAnswerTrue;
        }
        public void CheckMarkedAnswer(Button button,ButtonEnum buttonType)
        {
            if (currentQuestion.correctAnswerID == (int)buttonType)
            {
                button.image.color = Color.green;
                isAnswerTrue = true;
                is2XJokerOn = false;
                jokerID.CheckChangeJoker(is2XJokerOn);
                numOfAnswers = 0;
                AudioManager.Instance.PlaySound("CorrectSFX");
                if (!is2XJokerOn || numOfAnswers == 2)
                {
                    questionIndex++;
                    if (questionIndex == 12)
                    {
                        ChangeGameState(GameState.Finish);
                    }
                    else
                    {
                        ChangeGameState(GameState.Win);
                        StartCoroutine(WaitForTheNextQuestion());
                    }
                    ScoreTracer();
                }
                
            }
            else
            {
                numOfAnswers++; // incrementing the number of answers in order to keep tracking the usage of 2X joker 
                button.image.color = Color.red;
                AudioManager.Instance.PlaySound("IncorrectSFX");
                if (!is2XJokerOn || numOfAnswers == 2)
                {
                    ShowTheCorrectAnswer();
                    isAnswerTrue = false;
                    ScoreTracer(); // before every question, I show the question score
                    ChangeGameState(GameState.Lose);
                    SaveScore();
                    numOfAnswers = 0;
                    adController.ShowInterstitial();
                }
                else
                {
                    DisableTheWrongAnswer(button);
                }
            }
        }
        public void LoadNewQuestion()
        {
            questionText.text = SetTheQuestion().question;
            LoadNewAnswers();
        }
        public void LoadNewAnswers()
        {
            ResetButtonColors();
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            for (int i = 0; i < Constants.ANSWER_NUMBER; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];

            }
        }
        public void DisableTheWrongAnswer(Button wrongAnswerButton)
        {
            wrongAnswerButton = wrongAnswerButton.GetComponent<Button>();
            wrongAnswerButton.enabled = false;
        }
        public void DisableButtons()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            foreach (Button buttons in answerButtons)
            {
                buttons.enabled = false;
            }
        }
        public void EnableButtons()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            foreach (Button buttons in answerButtons)
            {
                buttons.enabled = true;
            }
        }
        public IEnumerator WaitForTheNextQuestion()
        {
            DisableButtons();
            yield return new WaitForSeconds(Constants.DELAY_TIME_BETWEEN_QUESTIONS);
            LoadNewQuestion();
            EnableButtons();
            OpenButtons();
        }
        public void OpenButtons()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            foreach (var button in answerButtons)
            {
                button.interactable = true;
            }
        }
        public void ShowTheCorrectAnswer()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            switch (currentQuestion.correctAnswerID)
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
        }
        public Question SetTheQuestion()
        {
            Random rnd = new Random();
            int randomNumber;
            if (questionIndex < 5)
            {            
                randomNumber = rnd.Next(0, questionFromJson.GetEasyQuestionList().Count);
                currentQuestion = questionFromJson.GetEasyQuestionList()[randomNumber];
                Debug.Log("SOORU " + questionFromJson.GetEasyQuestionList()[randomNumber].question);
                questionFromJson.GetEasyQuestionList().RemoveAt(randomNumber);
            }
            else if(questionIndex > 4 && questionIndex < 9)
            {
                randomNumber  = rnd.Next(0, questionFromJson.GetMediumQuestionList().Count);
                currentQuestion = questionFromJson.GetMediumQuestionList()[randomNumber];
                questionFromJson.GetMediumQuestionList().RemoveAt(randomNumber);
            }
            else
            {
                randomNumber  = rnd.Next(0, questionFromJson.GetHardQuestionList().Count);
                currentQuestion = questionFromJson.GetHardQuestionList()[randomNumber];
                questionFromJson.GetHardQuestionList().RemoveAt(randomNumber);
            }

            return currentQuestion;
        }
        public void ResetButtonColors()
        {
            Button[] answerButtons = buttonPanel.gameObject.GetComponentsInChildren<Button>();
            foreach (Button buttons in answerButtons)
            {
                buttons.image.color = Color.white;
            }

        }
        public void ChangeGameState(GameState state)
        {
            if (currentGameState != state)
            {
                currentGameState = state;
                OnGameStateChanged?.Invoke(state);
            }
        }

        public UIManager GetUIManager()
        {
            return uIManager;
        }
        public void SetUIManager(UIManager manager)
        {
            uIManager = manager;
        }
        public int GetScore()
        {
            return score;
        }
        public int GetTotalScore()
        {
            return totalScore;
        }
        public TextMeshProUGUI GetScoreText()
        {
            return scoreText;
        }
        public int GetQuestionIndex()
        {
            return questionIndex;
        }
        public void SetQuestionIndex(int index)
        {
            questionIndex = index;
        }
        public Question GetCurrentQuestion()
        {
            return currentQuestion;
        }
        
    }
}

