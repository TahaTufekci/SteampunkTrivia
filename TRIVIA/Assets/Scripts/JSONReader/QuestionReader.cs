using System;
using System.Collections;
using System.Collections.Generic;
using GameObjects;
using Helpers;
using Managers;
using UnityEngine;

namespace JSONReader
{
    public class QuestionReader : MonoBehaviour
    {
        public List<TextAsset> questionText;
        private static int languageDecider;
        private  List<Question> easyQuestionList = new List<Question>();
        private  List<Question> mediumQuestionList = new List<Question>();
        private  List<Question> hardQuestionList = new List<Question>();
        
        [System.Serializable]
        public class QuestionList
        {
            public List<Question> questions;
        }
        public QuestionList questionList = new QuestionList();

        private void Awake()
        {
            if (PlayerPrefs.HasKey("language"))
            {
                questionList = JsonUtility.FromJson<QuestionList>(questionText[PlayerPrefs.GetInt("language")].text); 
            }
            else
            {
                questionList = JsonUtility.FromJson<QuestionList>(questionText[0].text); 
            }
            FillTheQuestionLists();
        }
        public void FillTheQuestionLists()
        {
            foreach (var question in questionList.questions)
            {
                switch (question.questionDifficulty)
                {
                    case QuestionDifficulty.Easy:
                        easyQuestionList.Add(question);
                        break;
                    case QuestionDifficulty.Medium:
                        mediumQuestionList.Add(question);
                        break;
                    case QuestionDifficulty.Hard:
                        hardQuestionList.Add(question);
                        break;
                }
            }
        }
        public QuestionList GetQuestionList()
        {
            return questionList;
        }
        public List<Question> GetEasyQuestionList()
        {
            return easyQuestionList;
        }
        public List<Question> GetMediumQuestionList()
        {
            return mediumQuestionList;
        }
        public List<Question> GetHardQuestionList()
        {
            return hardQuestionList;
        }
        
    }
}

