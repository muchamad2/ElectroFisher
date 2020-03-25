using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentOption : MonoBehaviour
{
    [SerializeField] private Sprite[] img;
    public GameObject btnPrev, btnNext;
    public Image imgSprite;
    private int increment = 0;
    // Start is called before the first frame update
    void Start()
    {
        increment = 0;
        if(increment == 0){
            if(btnPrev != null)
                btnPrev.SetActive(false);
            if(btnNext != null)
                btnNext.SetActive(true);
        }
    }
    public void SelectionImage(int i){
        increment += i;
        if(increment != 0){
            btnPrev.SetActive(true);
        }else{
            btnPrev.SetActive(false);
        }
        if(increment <= 0){
            increment = 0;
        }
        if(increment >= img.Length)
            increment = img.Length - 1;

        imgSprite.sprite = img[increment];
    }
    
}
