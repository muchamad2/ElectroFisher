using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentOption : MonoBehaviour
{
    [SerializeField] private Sprite[] img;
    [SerializeField] private Sprite[] indoImg;
    [SerializeField] private Sprite[] engImg;
    public GameObject btnPrev, btnNext;
    public Image imgSprite;
    private int increment = 0;
    // Start is called before the first frame update

    void Start()
    {
        increment = 0;
        if(indoImg.Length > 0 && engImg.Length > 0){
            if(GameUtility.LangType == Language.Indo)
                img = indoImg;
            else if(GameUtility.LangType == Language.Eng)
                img = engImg;
        }
        if(increment == 0){
            if(btnPrev != null)
                btnPrev.SetActive(false);
            if(btnNext != null)
                btnNext.SetActive(true);
        }
        imgSprite.sprite = img[increment];
    }
    public void SelectionImage(int i){
        increment += i;
        if(increment != 0){
            btnPrev.SetActive(true);
            btnNext.SetActive(true);
        }else{
            btnPrev.SetActive(false);
        }
        if(increment <= 0){
            increment = 0;
        }
        if(increment >= img.Length){
            increment = img.Length - 1;
            btnNext.SetActive(false);
        }
        imgSprite.sprite = img[increment];
    }
    
}
