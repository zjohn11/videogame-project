using UnityEngine;

public class ControlPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    private Animator animator;  // Reference to the Animator component

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrow keys)
        float move = Input.GetAxis("Horizontal");  // Horizontal movement input

        // Move the player horizontally by adjusting its position
        transform.Translate(Vector2.right * move * moveSpeed * Time.deltaTime);

        // Set the "Speed" parameter (float) to control walking/running animation
        animator.SetFloat("Speed", Mathf.Abs(move));  // Use Mathf.Abs to avoid negative speed values

        // Trigger jump animation if the player presses the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");  // Trigger the Jump animation
        }

        // Set bool for idle/walking state
        if (move != 0)
        {
            animator.SetBool("isWalking", true); // Assuming you have an "isWalking" bool parameter
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}