using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AnswerData : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject;
    [SerializeField] Image toogle;
    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle;
    [SerializeField] Sprite checkedToggle;
    [Header("References")]
    [SerializeField]GameEvents events;

    private RectTransform _rect;
    public RectTransform Rect{
        get{
            if(_rect == null){
                _rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return _rect;
        }
    }
    private int _answerIndex = -1;
    public int AnswerIndex{get{return _answerIndex;}}
    private bool _checked = false;
    public void UpdateData(string info,int index,float size=0f,bool autofont = true){
        infoTextObject.text = info;
        if(!autofont){
            infoTextObject.enableAutoSizing = autofont;
            infoTextObject.fontSize = size;
        }
        _answerIndex = index;
    }
    public void Reset(){
        _checked = false;
        UpdateUI();
    }
    public void SwitchState(){
        _checked = !_checked;
        UpdateUI();
        if(events.updateQuestionAnswer != null){
            events.updateQuestionAnswer(this);
        }
    }
    void UpdateUI(){
        toogle.sprite = (_checked) ? checkedToggle : uncheckedToggle;
    }
}