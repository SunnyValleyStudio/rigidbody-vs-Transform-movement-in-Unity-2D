using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKinematicMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float speed = 500;
    [SerializeField]
    private float jumpSpeed = 8;
    [SerializeField]
    private float maxSpeedVertical = 3;

    Vector2 movementVector = Vector2.zero;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    bool isGrounded = false;

    bool isGroundedCheckStop = false;

    public Sounds playerSopunds;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSopunds = GetComponentInChildren<Sounds>();
    }

    private void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal") * this.speed;

        HandleMovementDirectionSpriteFlip();

        HandleGroundedCheck();

        HandleJumpInput();
    }

    private void HandleMovementDirectionSpriteFlip()
    {
        if (movementVector.x > 0)
            spriteRenderer.flipX = false;
        else if (movementVector.x < 0)
            spriteRenderer.flipX = true;
    }

    private void HandleGroundedCheck()
    {
        isGrounded = IsGrounded();
        if (isGrounded)
        {
            if (animator.GetBool("Jumping"))
                playerSopunds.PlayLandSound();
            animator.SetBool("Jumping", false);
            animator.SetFloat("InputX", Mathf.Abs(movementVector.x));
            movementVector.y = 0;
        }

        Debug.DrawRay(transform.position, Vector2.down * 0.5f);
    }
    private bool IsGrounded()
    {
        if (isGroundedCheckStop)
            return false;
        RaycastHit2D result = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        return result.collider != null;
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerSopunds.PlayJumpSound();
            animator.SetBool("Jumping", true);
            isGrounded = false;
            isGroundedCheckStop = true;
            movementVector.y = jumpSpeed;
            StartCoroutine(ResetGroundedCheckStop());
        }
    }

    private IEnumerator ResetGroundedCheckStop()
    {
        yield return new WaitForSeconds(0.5f);
        isGroundedCheckStop = false;
    }

    private void FixedUpdate()
    {
        if (isGrounded == false)
        {
            movementVector.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        }
        rb2d.MovePosition(rb2d.position + movementVector * Time.fixedDeltaTime);
    }

}
