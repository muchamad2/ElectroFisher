using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField]TextMeshProUGUI scoreTxt;
    [SerializeField]TextMeshProUGUI healthTxt;

    public void UpdateUI(int health,int score){
        scoreTxt.text = ""+score;
        healthTxt.text = ""+health;
    }
}
