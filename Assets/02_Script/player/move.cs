using UnityEngine;
using UnityEngine.InputSystem;

public class move : MonoBehaviour
{
    public float scale = 0.1f;
    public Vector2 moveInput;
    public Rigidbody2D rb;
    public Vector2 lastDirection;
    public Animator animator;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play("Idle");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveInput != Vector2.zero && GameManager.Instance.isGame)
        {
            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("move", isMoving);
            }
            float speed = GameManager.Instance.speed;
            rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
            //transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * speed);
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetBool("move", isMoving);
            }
            rb.linearVelocity = Vector2.zero;
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
    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            pauseManager.Instance.ButtonPause();
        }

    }
}
