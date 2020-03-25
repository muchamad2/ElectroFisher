using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransasition : MonoBehaviour
{
    private  static SceneTransasition _instance = null;
    public static SceneTransasition Instance{
        get{
            return _instance;   
        }
    }
    public Animator animator;
    private void Start()
    {
        if(_instance == null || _instance != this){
            _instance = this;
        }
    }
    public void LoadScene(dynamic index){
        StartCoroutine(SceneLoad(index));
    }
    IEnumerator SceneLoad(dynamic index){
        animator.SetTrigger("end");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(index);
    }
}
