using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    public Transform cam;
    float horizontal;
    float vertical;
    public bool groundedPlayer;
    public Vector3 direction;
    Vector3 jumpDirection;
    private float gravityValue = -20f;
    public GameObject followTarget;
    PlayerStats stats;


    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        jumpDirection = Vector3.zero;
        stats = GetComponent<PlayerStats>();

    }

    private void Update()
    {

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

        controller.Move(direction * stats.speed * Time.deltaTime);
      
    }

    public void Jump()
    {
        jumpDirection.y = stats.jumpHeight;
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
            stats.speed = 12.0f;
            animator.SetBool("Run", true);

        }
        if (!GetComponent<PlayerInput>().running)
        {
            stats.speed = 6.0f;
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
