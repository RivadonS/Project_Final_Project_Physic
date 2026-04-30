using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 moveInput;

    [Header("Game State")]
    public bool hasKey = false;

    void Start()
    {
        //For get component from player
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("🚨 จับได้แล้ว! สคริปต์ PlayerController ไปโผล่อยู่ที่ Object ชื่อ: '" + gameObject.name + "' แต่มันไม่มี Rigidbody2D ครับ!");
        }
    }

    void Update()
    {
        if (groundCheck == null)
        {
            Debug.LogError("🚨 จับได้แล้ว! ลืมลาก Ground Check ใส่ในช่องของ Object ชื่อ: '" + gameObject.name + "' ครับ!");
            return;
        }

        //Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void FixedUpdate()
    {
        //Move the player
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("กำลังกดปุ่มทิศทาง: " + moveInput);
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("กดปุ่มกระโดด! เท้าติดพื้นไหม?: " + isGrounded);

        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
            Debug.Log("Got the Key!");
        }

        if (collision.CompareTag("Trap") || collision.CompareTag("Lava"))
        {
            Die();
        }

        if (collision.CompareTag("Win"))
        {
            if (hasKey)
            {
                Debug.Log("Level Cleared!");
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("You need the key first!");
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
