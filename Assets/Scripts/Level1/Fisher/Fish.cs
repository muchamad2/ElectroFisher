using UnityEngine;

public class Fish : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    Vector2 velocity;
    private void Start()
    {
        velocity = Vector2.right;
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
            rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);
    }
    private void OnBecameInvisible()
    {
        velocity *= -Vector2.right;
    }
}