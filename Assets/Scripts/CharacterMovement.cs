using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    public Transform cam;
    public float speed = 6f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public bool groundedPlayer;
    private float jumpHeight = 8.0f;
    Vector3 direction;
    private float gravityValue = -9.81f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        /*
         * Calculate player movement and rotation
         */

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (controller.isGrounded)
        {
            direction = new Vector3(horizontal, 0, vertical).normalized;
            /*
             * Jumping
             */
            if (Input.GetButton("Jump"))
            {
                animator.SetTrigger("Jump");
            }

       


        }
        if (direction.magnitude >= 0.1f)
        {
            // Adjust player rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
        }
      direction.y += gravityValue * Time.deltaTime;
            // Move the player
            controller.Move(direction * speed * Time.deltaTime);
            groundedPlayer = controller.isGrounded;


        UpdateAnimations();

    }

    public void Jump()
    {
        direction.y = jumpHeight;
        animator.ResetTrigger("Jump");
    }

    void UpdateAnimations()
    {
        /*
       * Walk animation
       */

        if (direction.x != 0 || direction.z != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        /*
        * Run animation
        */

        if (Input.GetButton("Run"))
        {
            speed = 8.0f;
            animator.SetBool("Run", true);

        }
        if (Input.GetButtonUp("Run"))
        {
            speed = 6.0f;
            animator.SetBool("Run", false);
        }

        if (controller.isGrounded)
        {
            animator.SetBool("Grounded", true);

        }

        else
        {
            animator.SetBool("Grounded", false);
        }
    }
}
