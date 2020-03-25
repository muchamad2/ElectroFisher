using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class QuizManager : MonoBehaviour
{
    private Data data = new Data();
    [Header("References")]
    [SerializeField] Animator timerAnimator = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Color timeHalfOutColor = Color.yellow;
    [SerializeField] Color timeAlmoustOutColor = Color.red;
    [SerializeField] private string filename;

    public string Filename { get => filename; set => filename = value; }

    private Color timerDefaultColor = Color.white;

    [SerializeField] GameEvents events = null;
    [SerializeField] GameObject emoticon = null;
    [SerializeField] GameObject explanation = null;
    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private List<int> CorrectAnswerQuestions = new List<int>();
    private List<int> IncorrectAnswerQuestions = new List<int>();
    private int currentQuestions = 0;



    private IEnumerator startTimer;
    private IEnumerator openEmoticon;
    private void OnEnable()
    {
        events.updateQuestionAnswer += UpdateAnswers;
    }
    private void OnDisable()
    {
        events.updateQuestionAnswer -= UpdateAnswers;
    }
    private void Start()
    {
        LoadData();

        events.CurrentFinalScore = 0;



        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

    }

    public void UpdateAnswers(AnswerData newAnswer)
    {
        if (data.Questions[currentQuestions].AnswerType == AnswerType.Single)
        {
            if (PickedAnswers.Count == 0)
                PickedAnswers.Add(newAnswer);
            else
            {
                for (int i = 0; i < PickedAnswers.Count; i++)
                {
                    if (PickedAnswers[i] != newAnswer)
                    {
                        PickedAnswers[i].Reset();
                    }
                    PickedAnswers.Clear();
                    PickedAnswers.Add(newAnswer);
                }

            }

        }
        else
        {
            bool alredyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alredyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }
    public void Display()
    {
        EraseAnswers();
        if (openEmoticon != null)
            StopCoroutine(openEmoticon);
        var question = getRandomQuestion();
        if (events.updateQuestionUI != null)
        {
            events.updateQuestionUI(question);
        }
        UpdateTime(question.UseTimer);
    }

    public void Accept()
    {
        UpdateTime(false);
        bool isCorrect = CheckedAnswer();
        FinishedQuestions.Add(currentQuestions);

        if (isCorrect)
        {
            CorrectAnswerQuestions.Add(currentQuestions);
            if (IncorrectAnswerQuestions.Contains(currentQuestions))
            {
                IncorrectAnswerQuestions.Remove(currentQuestions);
            }
        }
        else if (!isCorrect)
        {
            IncorrectAnswerQuestions.Add(currentQuestions);
        }

        openEmoticon = OpenEmoticon(isCorrect);

        StartCoroutine(openEmoticon);

        /*         if (events.displayResolutionScreen != null)
                {
                    events.displayResolutionScreen(data.Questions[currentQuestions].AddScore);
                } */
        if (!GameManager.Instance.isQuestionOnly)
        {
            if (CorrectAnswerQuestions.Count >= 3 && FinishedQuestions.Count >= data.Questions.Length)
            {
                Debug.Log("Change Scene to Platform Scene");
                GameManager.Instance.SceneLoad(2);
            }
        }
        else
        {
            UpdateScore(isCorrect);
        }
        events.updatedAnswer(isCorrect);
    }
    IEnumerator OpenEmoticon(bool state)
    {

        emoticon.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        emoticon.SetActive(false);
        
        if(explanation != null)
            explanation.SetActive(state);

        if (!GameManager.Instance.isQuestionOnly)
            FisherManager.Instance.CloseQuiz(state);
        else
        {
            if (FinishedQuestions.Count == data.Questions.Length)
                QAManager.Instance.ShowTotalScore();
            else
                Display();
        }

    }

    private bool CheckedAnswer()
    {
        if (!CompareAnswers())
            return false;
        return true;
    }
    private bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> correct = data.Questions[currentQuestions].GetCorrectListAnswers();
            List<int> picked = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var first = correct.Except(picked).ToList();
            var second = picked.Except(correct).ToList();
            return !first.Any() && !second.Any();
        }
        return false;
    }

    Question getRandomQuestion()
    {
        if (data.Questions.Length > 1)
        {
            var randomIndex = getRandomQuestionIndex();
            currentQuestions = randomIndex;
            return data.Questions[currentQuestions];
        }
        else
            return data.Questions[0];
    }
    int getRandomQuestionIndex()
    {
        int random = 0;
        if (FinishedQuestions.Count < data.Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, data.Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestions);
        }
        else if (CorrectAnswerQuestions.Count < 3)
        {
            do
            {
                random = UnityEngine.Random.Range(0, data.Questions.Length);
            } while (IncorrectAnswerQuestions.Contains(random) || random == currentQuestions);
        }
        return random;
    }
    void LoadData()
    {
        data = Data.Fetch(Filename);
    }

    public void UpdateTime(bool state)
    {
        switch (state)
        {
            case true:

                startTimer = StartTimer();
                StartCoroutine(startTimer);

                timerAnimator.SetInteger("TimerState", 2);

                break;
            case false:
                if (startTimer != null)
                    StopCoroutine(startTimer);

                timerAnimator.SetInteger("TimerState", 1);

                break;
        }

    }
    IEnumerator StartTimer()
    {
        var totalTime = data.Questions[currentQuestions].Timer;
        var timeLeft = totalTime;
        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {

            timeLeft--;

            if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timeHalfOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timeAlmoustOutColor;
            }
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);

        }
        Accept();
    }
    public void CloseExplanationArea(){
        explanation.SetActive(false);
    }
    private void UpdateScore(bool state)
    {
        if (events.scoreUpdated != null)
        {
            events.scoreUpdated(state);
        }
    }
}
