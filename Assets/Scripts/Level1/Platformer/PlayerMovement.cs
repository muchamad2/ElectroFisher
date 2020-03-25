using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;
    public Animator anim;

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
            anim.SetFloat("Speed",Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                anim.SetBool("Jump",true);
            }
        }
    }
    public void onLanding(){
        anim.SetBool("Jump",false);
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
            PlatformManager.Instance.TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crest"))
        {
            PlatformManager.Instance.OpenCrest(1);
        }
    }
}
