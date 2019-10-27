using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject claw;
    public bool isShooting;
    public Animator fisherAnimation;
    public Claw clawScript;
    private void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && !isShooting)
            {
                launchClaw();
            }
        }

    }

    void launchClaw()
    {
        
        fisherAnimation.speed = 0;
        Vector2 down = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down, 100);
        if (hit && !isShooting)
        {
            isShooting = true;
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
