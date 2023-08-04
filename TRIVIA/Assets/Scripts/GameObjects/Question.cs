using System;
using System.Collections.Generic;
using Helpers;

namespace GameObjects
{
    [System.Serializable]
    public class Question
    {
        public String question;
        public List<String> answers;
        public int correctAnswerID;
        public QuestionDifficulty questionDifficulty;
    }
}
