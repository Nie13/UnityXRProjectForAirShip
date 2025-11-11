using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFromVR : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraRig;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public Animator animator;

    bool jumpPressed;

    bool halted = true;

    float velocityPercent = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!halted)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            // Jumping
            if (jumpPressed && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                // Animate jumping
                animator.SetTrigger("Jumping");
                jumpPressed = false;
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            
            Vector3 direction = new Vector3(velocityPercent, 0f, 0f).normalized;

            if (Mathf.Abs(velocityPercent) > 0.15f)
            {
                Vector3 moveDir = Vector3.left;
                controller.Move(moveDir.normalized * speed * velocityPercent * Time.deltaTime);
                animator.SetBool("Running", true);
            }
            else
                animator.SetBool("Running", false);

        } else
        {
            animator.SetBool("Running", false);
        }
    }

    public void jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!jumpPressed && isGrounded && !halted)
        {
            jumpPressed = true;
        }
    }

    public void halt()
    {
        halted = !halted;
    }

    public void movementControl(float percent)
    {
        velocityPercent = (percent - 0.5f) * 2f;
    }

    public void adjustJumpHeight(float percent)
    {
        jumpHeight = 1f * percent;
    }

    
}
