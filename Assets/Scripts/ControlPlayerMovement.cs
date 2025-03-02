using UnityEngine;

public class ControlPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float jumpForce = 10f; // Jump force to apply when jumping
    private Animator animator;  // Reference to the Animator component
    private Rigidbody2D rb;     // Reference to the Rigidbody2D component
    private bool isGrounded = true; // Flag to check if the player is on the ground

    // Add a reference to the ground check object
    public Transform groundCheck;  // Reference to a small empty GameObject at the player's feet
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    // Add a reference to the LayerMask that defines which layers are considered "Ground"
    public LayerMask groundLayer;

    void Start()
    {
        // Get the Animator and Rigidbody2D components attached to this GameObject
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get horizontal input (A/D or Left/Right arrow keys)
        float move = Input.GetAxis("Horizontal");

        // Apply movement using Rigidbody2D's velocity
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y); // Keep current y velocity (gravity) when moving horizontally

        // Set the "Speed" parameter (float) to control walking/running animation
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Trigger jump animation if the player presses the spacebar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Apply a jump force upwards
            animator.SetTrigger("Jump"); // Trigger the Jump animation
        }

        // Set bool for idle/walking state based on movement
        if (move != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Flip character's sprite based on movement direction
        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);  // Flip horizontally
        }
    }

    // Optionally, you can create a method to visually debug the ground check.
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
