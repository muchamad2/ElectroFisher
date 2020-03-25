using UnityEngine;

[System.Serializable]
public struct InText
{
    [SerializeField] private string _info;
    public string Info { get { return _info; } }
}
[CreateAssetMenu(fileName = "TextScript", menuName = "ElectroFisher/TextScript", order = 0)]
public class TextScript : ScriptableObject
{
    public enum LangType { Ind, Eng }
    public LangType language = LangType.Ind;
    [Header("Initiate Content Text")]

    [SerializeField] private InText _next;
    public InText Next { get { return _next; } }
    [SerializeField] private InText _languageSelect;
    public InText LanguageSelect { get { return _languageSelect; } }
    [Header("Main Content Text")]
    [SerializeField] private InText _play;
    public InText Play { get { return _play; } }
    [SerializeField] private InText _quit;
    public InText BtnQuit { get { return _quit; } }

    [Header("Login Content Text")]
    [SerializeField] private InText _labelPlayerName;
    public InText LabelPlayerName { get { return _labelPlayerName; } }
    [SerializeField] private InText _labelPlayerGrade;
    public InText LabelPlayerGrade { get { return _labelPlayerGrade; } }
    [SerializeField] private InText _titleBtnLogin;
    public InText TitleBtnLogin { get { return _titleBtnLogin; } }
    [Space]
    [Header("In Game Text")]
    [SerializeField] InText _titleBtnHome;
    public InText BtnHome { get { return _titleBtnHome; } }
    [SerializeField] InText _labelQuitText;
    public InText LabelQuitText { get { return _labelQuitText; } }
    [SerializeField] InText _titleYes;
    public InText Yes { get { return _titleYes; } }
    [SerializeField] InText _titleNo;
    public InText No { get { return _titleNo; } }
    [SerializeField] InText _answerText;
    public InText AnswerText { get { return _answerText; } }
    [SerializeField] InText _closeText;
    public InText CloseText { get { return _closeText; } }
}