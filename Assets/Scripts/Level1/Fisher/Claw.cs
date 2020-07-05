using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public Transform origin;
    public float speed = 4f;
    public Gun gun;

    private Vector2 target;
    private int fishValue = 1;
    private GameObject childObject;
    private LineRenderer lineRenderer;
    private bool hitFish;
    private bool hitTrash = false;
    private bool retracting;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            float step = speed * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            lineRenderer.SetPosition(0, origin.position);
            lineRenderer.SetPosition(1, transform.position);
            if (transform.position == origin.position && retracting)
            {
                gun.CollectObject();
                if (hitFish)
                {
                    hitFish = false;
                }else if(hitTrash){
                    FisherManager.Instance.UpdateHealth();
                    hitTrash = false;
                }
                Destroy(childObject);
                gameObject.SetActive(false);
                retracting = false;
            }
            if (retracting)
            {
                if (FisherManager.Instance.isCorrectAnswers)
                {
                    childObject = FisherManager.Instance.getChildObject();
                    childObject.transform.SetParent(this.transform);
                    FisherManager.Instance.isCorrectAnswers = false;
                }else{
                    //hitFish = false;
                }
            }
        }

    }

    public void ClawTarget(Vector2 pos)
    {
        target = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        retracting = true;
        target = origin.position;
        if (other.gameObject.CompareTag("Fish") && !hitFish)
        {
            hitFish = true;
            FisherManager.Instance.OpenQuiz(other.gameObject);
        }else if(other.gameObject.CompareTag("Sampah") && !hitTrash){
            hitTrash = true;
            childObject = other.gameObject;
            childObject.transform.SetParent(this.transform);
        }
    }
}
