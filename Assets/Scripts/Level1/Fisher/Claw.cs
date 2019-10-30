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
                }
                Destroy(childObject);
                gameObject.SetActive(false);
                retracting = false;
            }
            if (retracting)
            {
                if (GameManager.Instance.isCorrectAnswers)
                {
                    childObject = GameManager.Instance.getChildObject();
                    childObject.transform.SetParent(this.transform);
                    GameManager.Instance.isCorrectAnswers = false;
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
            GameManager.Instance.OpenQuiz(other.gameObject);
        }
    }
}
