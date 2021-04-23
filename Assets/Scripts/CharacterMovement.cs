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
    public Vector3 jumpDirection;
    public Vector3 gravity;
    public GameObject followTarget;
    PlayerStats stats;


    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        jumpDirection = Vector3.zero;
        gravity = new Vector3(0, -9.8f, 0);
        stats = GetComponent<PlayerStats>();

    }

    private void Update()
    {

        /*
         * Calculate player movement and rotation
         */
        if (!groundedPlayer) jumpDirection += gravity * Time.deltaTime;
        controller.Move(jumpDirection.normalized);
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
        jumpDirection = transform.up*2;
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
            stats.speed = 2.0f;
            animator.SetBool("Run", true);

        }
        if (!GetComponent<PlayerInput>().running)
        {
            stats.speed = 1.0f;
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

    public void SetGravitationalPull(Vector3 _gravity)
    {
        gravity = _gravity;
        jumpDirection = Vector3.zero;

    }

    public void ResetGravitationalPull()
    {
        gravity = new Vector3(0, -9.8f, 0);
        jumpDirection = Vector3.zero;
    }

}
