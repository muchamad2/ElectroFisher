using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class QuizManager : MonoBehaviour
{
    private Data data = new Data();
    [SerializeField] Animator timerAnimator = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Color timeHalfOutColor = Color.yellow;
    [SerializeField] Color timeAlmoustOutColor = Color.red;
    private Color timerDefaultColor = Color.white;

    [SerializeField] GameEvents events = null;
    [SerializeField] GameObject emoticon = null;
    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private List<int> CorrectAnswerQuestions = new List<int>();
    private List<int> IncorrectAnswerQuestions = new List<int>();
    private int currentQuestions = 0;
    private int score;

    private int timerStateParaHas = 0;

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

        timerStateParaHas = Animator.StringToHash("TimerState");

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
        if(openEmoticon != null)
            StopCoroutine(openEmoticon);
        var question = getRandomQuestion();
        if (events.updateQuestionUI != null)
        {
            events.updateQuestionUI(question);
        }
        else
        {

        }
        if (question.UseTimer)
        {
            UpdateTime(question.UseTimer);
        }
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
        UpdateScore((isCorrect) ? data.Questions[currentQuestions].AddScore : -data.Questions[currentQuestions].AddScore);
        openEmoticon = OpenEmoticon(isCorrect);
        
        StartCoroutine(openEmoticon);

/*         if (events.displayResolutionScreen != null)
        {
            events.displayResolutionScreen(data.Questions[currentQuestions].AddScore);
        } */
        if (CorrectAnswerQuestions.Count >= 3 && FinishedQuestions.Count >= data.Questions.Length)
        {
            Debug.Log("Change Scene to Platform Scene");
            GameManager.Instance.SceneLoad(1);
        }
        else
        {
            //Display();
            Debug.Log(IncorrectAnswerQuestions.Count);
        }
    }
    IEnumerator OpenEmoticon(bool state){
        if(!emoticon.activeInHierarchy)
            emoticon.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        if(emoticon.activeInHierarchy)
            emoticon.SetActive(false);
        GameManager.Instance.CloseQuiz(state);
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
        var randomIndex = getRandomQuestionIndex();
        currentQuestions = randomIndex;
        return data.Questions[currentQuestions];
    }
    int getRandomQuestionIndex()
    {
        var random = 0;
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
            } while (IncorrectAnswerQuestions.Contains(random));
        }
        return random;
    }
    void LoadData()
    {
        data = Data.Fetch();
    }

    public void UpdateTime(bool state)
    {
        switch (state)
        {
            case true:
                Debug.Log("Active ? "+gameObject.activeInHierarchy);
                startTimer = StartTimer();
                StartCoroutine(startTimer);
                
                timerAnimator.SetInteger(timerStateParaHas,2);
                
                break;
            case false:
                if (startTimer != null)
                    StopCoroutine(startTimer);

                timerAnimator.SetInteger(timerStateParaHas,1);
               
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
            //Debug.Log(timeLeft);
            yield return new WaitForSeconds(1.0f);
            
        }
        Accept();
    }
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if (events.scoreUpdated != null)
        {
            events.scoreUpdated();
        }
    }
}
