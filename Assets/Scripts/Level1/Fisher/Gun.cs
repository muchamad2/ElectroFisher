using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject claw;
    public LayerMask layer;
    public bool isShooting;
    public Animator fisherAnimation;
    public FisherAnimEvent fisherAnimEvent;
    public Claw clawScript;

    private void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && !isShooting)
            {
                if(fisherAnimEvent.AreaEvent == true){
                    launchClaw();
                }
            }
        }

    }

    void launchClaw()
    {
        isShooting = true;
        fisherAnimation.speed = 0;
        Vector2 down = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down, 100,layer);
        if (hit)
        {
            claw.SetActive(true);
            clawScript.ClawTarget(hit.point);
        }
    }
    public void CollectObject()
    {
        isShooting = false;
        fisherAnimation.speed = 1;
    }

}
