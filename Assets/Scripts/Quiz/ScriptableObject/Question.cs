using System.Collections.Generic;
using UnityEngine;
public enum AnswerType{Single, Multi}

[System.Serializable]
public class Answer{
    public string info;
    public bool isCorrect;

    public Answer(){}
}
[System.Serializable]
public class Question
{
    [TextArea]
    public string Info = string.Empty;
    public Answer[] Answers = null;
    public bool UseTimer = false;
    public int Timer = 0;
    [TextArea]
    public string Explanation = string.Empty;
    public AnswerType AnswerType = AnswerType.Single;
    public  int AddScore = 1;
    public List<int> GetCorrectListAnswers(){
        List<int> CorrectAnswer = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
           if(Answers[i].isCorrect){
               CorrectAnswer.Add(i);
           } 
        }
        return CorrectAnswer;
    }
}