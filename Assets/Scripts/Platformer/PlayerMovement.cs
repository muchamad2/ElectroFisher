using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;
    

    public float runSpeed = 40f;
    private float setUpSpeed;
    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        setUpSpeed = runSpeed;
    }
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crest"))
        {
            GameManager.Instance.OpenCrest(0);
        }
    }
}
