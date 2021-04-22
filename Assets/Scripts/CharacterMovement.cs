using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    public Transform cam;
    public float speed = 6f;
    float horizontal;
    float vertical;
    public bool groundedPlayer;
    private float jumpHeight = 15.0f;
    public Vector3 direction;
    Vector3 jumpDirection;
    private float gravityValue = -20f;
    public GameObject followTarget;


    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        jumpDirection = Vector3.zero;

    }

    private void Update()
    {
        //if (controller.isGrounded && Input.GetButton("Jump"))
        //{
        //    /*
        //     * Jumping
        //     */

        //    animator.SetTrigger("Jump");

        //}

        /*
         * Calculate player movement and rotation
         */
        jumpDirection.y += gravityValue * Time.deltaTime;
        controller.Move(jumpDirection * Time.deltaTime);
        groundedPlayer = controller.isGrounded;
        horizontal = GetComponent<PlayerInput>()._move.x;
        vertical = GetComponent<PlayerInput>()._move.y;
        UpdateAnimations();
        Vector3 forwardMove = transform.forward * GetComponent<PlayerInput>()._move.y; 
        Vector3 sideMove = transform.right * GetComponent<PlayerInput>()._move.x;
        direction = forwardMove + sideMove;

        controller.Move(direction * speed * Time.deltaTime);
      
    }

    public void Jump()
    {
        jumpDirection.y = jumpHeight;
        animator.ResetTrigger("Jump");
    }

    void UpdateAnimations()
    {
        /*
       * Walk animation
       */

        if (horizontal != 0 || vertical != 0)
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

        if (GetComponent<PlayerInput>().running)
        {
            speed = 12.0f;
            animator.SetBool("Run", true);

        }
        if (!GetComponent<PlayerInput>().running)
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
