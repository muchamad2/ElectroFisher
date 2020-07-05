using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
public class TextWriter : MonoBehaviour
{
    public static TextWriter instance;
    private List<TextWriterSingle> textWriterSingleList;
    private void Awake() {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    public static void AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacter,bool destroyBeforeAdd, Action onComplete){
        if(destroyBeforeAdd){
            instance.RemoveWriter(uiText);
        }
        instance.AddWriter(uiText,textToWrite,timePerCharacter,invisibleCharacter,onComplete);
    }
    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacter, Action onComplete)
    {
        TextWriterSingle text = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacter,onComplete);
        textWriterSingleList.Add(text);
    }
    public void RemoveWriter(TextMeshProUGUI uiText){
        for(int i=0;i<textWriterSingleList.Count;i++){
            if(textWriterSingleList[i].GetUIText() == uiText){
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance){
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }


    public class TextWriterSingle
    {

        private TextMeshProUGUI uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        bool invisibleCharacter;
        private Action onComplete;

        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacter,Action onComplete)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.onComplete = onComplete;
            characterIndex = 0;
            this.invisibleCharacter = invisibleCharacter;
        }

        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacter)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    characterIndex = textToWrite.Length;
                    if(onComplete != null) onComplete();
                    return true;
                }
            }
            return false;
        }

        public TextMeshProUGUI GetUIText(){
            return uiText;
        }
        public bool isActive(){
            return characterIndex < textToWrite.Length;
        }
        public void WriteAllText(){
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
        }
    }
}
