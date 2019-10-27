using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
[Serializable]
public struct QuizUIManagerParameters{
    [Header("Answer Options")]
    [SerializeField] float _margin;
    public float Margins {get{return _margin;}}
}
[Serializable]
public struct UIElements{
    [SerializeField] RectTransform _answerContentArea;
    public RectTransform AnswerContentArea{get{return _answerContentArea;}}
    [SerializeField] TextMeshProUGUI _questionInfoTextObject;
    public TextMeshProUGUI QuestionInfo{get{return _questionInfoTextObject;}}
    [SerializeField] TextMeshProUGUI _scoreText;
    public TextMeshProUGUI ScoreText{get{return _scoreText;}}

}
public class QuizUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameEvents events;
    [Header("UI Elements (Prefabs)")]
    [SerializeField] AnswerData answerPrefab;
    [SerializeField] UIElements uIElements;
    [Space]
    [SerializeField] QuizUIManagerParameters parameters;

    List<AnswerData> currentAnswer = new List<AnswerData>();

    private void OnEnable() {
        events.updateQuestionUI += UpadatedQuestionUI;
        
    }
    private void OnDisable()
    {
        events.updateQuestionUI -= UpadatedQuestionUI;
        
    }

    void UpadatedQuestionUI(Question question){
        uIElements.QuestionInfo.text = question.Info;
        //createAnswers;
        CreateAnswers(question);
    }



    void CreateAnswers(Question question){
        EraseAnswer();
        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab,uIElements.AnswerContentArea);
            newAnswer.UpdateData(question.Answers[i].info,i);
            
            newAnswer.Rect.anchoredPosition = new Vector2(0,offset);
            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswerContentArea.sizeDelta = new Vector2(uIElements.AnswerContentArea.sizeDelta.x,
            offset*-1);

            currentAnswer.Add(newAnswer);
        }
    }
    void EraseAnswer(){
        foreach (var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }

}
