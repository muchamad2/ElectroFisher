﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
[Serializable]
public struct QuizUIManagerParameters
{
    [Header("Answer Options")]
    [SerializeField] float _margin;
    public float Margins { get { return _margin; } }
}
[Serializable]
public struct QuizUIElements
{
    [SerializeField] RectTransform _answerContentArea;
    public RectTransform AnswerContentArea { get { return _answerContentArea; } }
    [SerializeField] TextMeshProUGUI _questionInfoTextObject;
    public TextMeshProUGUI QuestionInfo { get { return _questionInfoTextObject; } }
    [SerializeField] TextMeshProUGUI _scoreText;
    public TextMeshProUGUI ScoreText { get { return _scoreText; } }
    [SerializeField] TextMeshProUGUI _explanationText;
    public TextMeshProUGUI ExplanationInfo { get { return _explanationText; } }
    [SerializeField] TextMeshProUGUI _answerText;
    public TextMeshProUGUI AnswerInfo { get { return _answerText; } }
    [SerializeField] TextMeshProUGUI _closeText;
    public TextMeshProUGUI CloseInfo { get { return _closeText; } }
}
public class QuizUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameEvents events;
    [SerializeField] Image emoticonImage;
    [SerializeField] Sprite emoLose;
    [SerializeField] Sprite emoWin;
    [Header("Additional References")]
    [SerializeField] bool isActiveAdditional;
    [SerializeField] TextScript indoTextScript;
    [SerializeField] TextScript engTextScript;

    [Header("UI Elements (Prefabs)")]
    [SerializeField] AnswerData answerPrefab;
    [SerializeField] QuizUIElements uIElements;
    [Space]
    [SerializeField] QuizUIManagerParameters parameters;

    List<AnswerData> currentAnswer = new List<AnswerData>();

    private void OnEnable()
    {
        events.updateQuestionUI += UpadatedQuestionUI;
        events.updatedAnswer += UpdateEmoticonAnswer;
    }
    private void OnDisable()
    {
        events.updateQuestionUI -= UpadatedQuestionUI;
        events.updatedAnswer -= UpdateEmoticonAnswer;

    }
    private void Start()
    {
        if (isActiveAdditional)
        {
            if (GameUtility.LangType == Language.Indo)
            {
                uIElements.AnswerInfo.text = indoTextScript.AnswerText.Info;
                uIElements.CloseInfo.text = indoTextScript.CloseText.Info;
            }
            else
            {
                uIElements.AnswerInfo.text = engTextScript.AnswerText.Info;
                uIElements.CloseInfo.text = engTextScript.CloseText.Info;
            }
        }
    }
    void UpadatedQuestionUI(Question question)
    {
        uIElements.QuestionInfo.text = question.Info;
        uIElements.ExplanationInfo.text = question.Explanation;
        //createAnswers;
        CreateAnswers(question);
    }



    void CreateAnswers(Question question)
    {
        EraseAnswer();
        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.AnswerContentArea);
            if (GameManager.Instance.isQuestionOnly)
                newAnswer.UpdateData(question.Answers[i].info, i, 24f, false);
            else
                newAnswer.UpdateData(question.Answers[i].info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);
            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswerContentArea.sizeDelta = new Vector2(uIElements.AnswerContentArea.sizeDelta.x,
            offset * -1);

            currentAnswer.Add(newAnswer);
        }
    }
    void UpdateEmoticonAnswer(bool state)
    {
        switch (state)
        {
            case true:
                emoticonImage.sprite = emoWin;
                break;
            case false:
                emoticonImage.sprite = emoLose;
                break;
        }
    }
    void EraseAnswer()
    {
        foreach (var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }

}
