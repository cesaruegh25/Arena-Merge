using UnityEngine;
using UnityEngine.InputSystem;

public class move : MonoBehaviour
{
    public float scale = 0.1f;
    public Vector2 moveInput;
    public Rigidbody2D rb;
    public Vector2 lastDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveInput != Vector2.zero && GameManager.Instance.isGame)
        {
            rb.linearVelocity = new Vector2(moveInput.x * 5, moveInput.y * 5);
            transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * 5);
        }

        if (moveInput != Vector2.zero)
        {
            lastDirection = moveInput.normalized;
        }
        if (moveInput.x < 0)
        {
            transform.localScale =
                new Vector3(-scale, scale, scale);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale =
                new Vector3(scale, scale, scale);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
