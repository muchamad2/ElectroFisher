using UnityEngine;

[CreateAssetMenu(fileName = "QuizEvents", menuName = "ElectroFisher/QuizEvents", order = 0)]
public class GameEvents : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(Question question);
    public UpdateQuestionUICallback updateQuestionUI;
    public delegate void UpdateQuestionAnswerCallback(AnswerData pickedAnswer);
    public UpdateQuestionAnswerCallback updateQuestionAnswer;

    public delegate void DisplayResolutionScreenCallback(int score);
    public DisplayResolutionScreenCallback displayResolutionScreen;

    public delegate void ScoreUpdatedCallback();
    public ScoreUpdatedCallback scoreUpdated;

    public int CurrentFinalScore;

}