using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerDiedEventHandler();
    public static event PlayerDiedEventHandler PlayerDied;

    public float Speed { get; set; } = 7;
    private float jumpVelocity = 9;
    private float secondJumpVelocity = 7;
    private float horizontalMovement;

    [SerializeField]
    private float maxX, minX;

    private const string WALK_ANIMITION = "Walk";
    private const string IDLE_ANIMITION = "Idle";
    private const string JUMP_ANIMITION = "Jump";
    private const string GROUND_TAG = "Ground";
    private const string ENEMY_TAG = "Enemy";
    private string currentState = IDLE_ANIMITION;
    
    public static bool facingRight = true;
    private bool pressedJump = false;
    private bool secondJump = false;
    private bool holdingSpace = false;
    private int numberOfJumps = 0;
    private bool onGround = true;

    private Rigidbody2D playerBody;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            pressedJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumps == 1)
        {
            secondJump = true;
        }
        if (Input.GetKey(KeyCode.Space) && !onGround)
        {
            holdingSpace = true;
        }
    }

    private void FixedUpdate()
    {
        CheckAndMove();
        CheckAndJump();
        JumpHigherIfHoldingSpace();
        AddForceWhileFalling();
        Animate();
    }

    private void JumpHigherIfHoldingSpace()
    {
        if (holdingSpace && playerBody.velocity.y > 0)
        {
            playerBody.AddForce(7 * Vector2.up);
            holdingSpace = false;
        }
    }

    private void AddForceWhileFalling()
    {
        if (playerBody.velocity.y < 0)
        {
            playerBody.AddForce(Vector2.down * 10);
        }
    }

    private void CheckAndMove()
    {
        CheckIfInBorder();
        if (Dash.dashing)
        {
            return;
        }
        playerBody.velocity = new Vector2(horizontalMovement * Speed, playerBody.velocity.y);
    }

    private void CheckIfInBorder()
    {
        if (transform.position.x >= maxX && horizontalMovement == 1)
        {
            transform.position = new Vector3(maxX, transform.position.y, 0);
            horizontalMovement = 0;
        }

        else if (transform.position.x <= minX && horizontalMovement == -1)
        {
            transform.position = new Vector3(minX, transform.position.y, 0);
            horizontalMovement = 0;
        }
    }

    private void CheckAndJump()
    {
        if (pressedJump && onGround)
        {
            AddJumpVelocity(jumpVelocity);
            onGround = false;
            pressedJump = false;
            numberOfJumps++;
        }

        if (secondJump)
        {
            AddJumpVelocity(secondJumpVelocity);
            secondJump = false;
            numberOfJumps++;
        }
    }

    private void AddJumpVelocity(float jumpVelocity)
    {
        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            onGround = true;
            numberOfJumps = 0;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            PlayerDied?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            PlayerDied?.Invoke();
        }
    }

    private void Animate()
    {
        if (!onGround)
        {
            ChangeState(JUMP_ANIMITION);
            CheckAndCorrectFaceDirection();
        }

        else if (horizontalMovement == 1 || horizontalMovement == -1)
        {
            ChangeState(WALK_ANIMITION);
            CheckAndCorrectFaceDirection();
        }

        else
        {
            ChangeState(IDLE_ANIMITION);
        }
    }

    private void CheckAndCorrectFaceDirection()
    {
        if ((horizontalMovement == 1 && !facingRight) || (horizontalMovement == -1 && facingRight))
        {
            Rotate180();
            facingRight = !facingRight;
        }
    }

    private void Rotate180()
    {
        transform.Rotate(Vector3.up, 180);
    }

    private void ChangeState(string newState)
    {
        if (currentState.Equals(newState)) return;

        animator.Play(newState);
        currentState = newState;
    }
}