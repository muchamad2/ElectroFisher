using UnityEngine;

public class Fish : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    Vector2 velocity;
    bool isRight;
    private void Start()
    {
        isRight = false;
        velocity = Vector2.right;
        Flip();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
            rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);
    }
    private void OnBecameInvisible()
    {
        isRight = !isRight;
        velocity *= -Vector2.right;
        Flip();
    }
    void Flip()
    {
        Vector3 right = transform.localScale;
        right.x *= -1;
        transform.localScale = right;
    }
}