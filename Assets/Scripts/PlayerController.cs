using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; set; }

    [Header("Run")]
    public float maxSpeed = 15.0f;
    public float lerpAmount = 0.5f;
    float runAccelAmount = 19f;
    float runDeccelAmount = 19f;

    [Header("Vertical Movement")]
    public float jumpDelay = 0.25f;
    public float jumpForce = 5f;
    public float fallMultiplier = 4f;
    [SerializeField] private float maxFallSpeed;
    private float jumpTimer;
    private float coyoteTime = 0.3f;
    private float coyoteTimeCounter;
    [SerializeField] private int jumpCounter;
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool canDoubleJump;

    bool isFacingRight = true;

    [Header("Physics")]
    public float linearDrag = 5f;
    public float gravity = 1f;
    public float Velocity;
    public float frictionAmount;
    Rigidbody2D RB;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundLength = 0.6f;
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.17f, 0.02f);
    public bool onGround = false;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject jumpParticle;
    [SerializeField] private GameObject runParticle;
    [SerializeField] private Vector3 dashAnimationOffset = new Vector3(-0.68f, 0f, 0f);

    [Header("Sprite Renderer")]
    SpriteRenderer sr;

    [Header("Player Input")]
    Vector2 moveInput;

    [Header("Events")]
    [Space]
    public UnityEngine.Events.UnityEvent OnLandEvent;

    [Header("TrailRenderer")]
    [SerializeField] private TrailRenderer tr;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 12f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    private Vector2 dashDir;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        instance = this;
    }

    void Start()
    {
        tr.emitting = false;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        Velocity = RB.velocity.x;

        if (moveInput.x != 0)
        {
            CheckDirectionToFace(moveInput.x > 0);
        }

        #region Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            if (moveInput != Vector2.zero)
            {
                dashDir = moveInput;
            }
            else
            {
                dashDir = isFacingRight ? Vector2.right : Vector2.left;
            }
            StartCoroutine(nameof(Dash), dashDir);
        }
        #endregion

        #region Jump Check
        onGround = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, groundLayer);

        // CoyoteTime
        if (onGround)
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //JumpBuffer
            jumpTimer = Time.time + jumpDelay;
            if(canDoubleJump)
            {
                doubleJump = true;
            }
        }
        #endregion

        #region Animator
        animator.SetFloat("HorizontalInput", Mathf.Abs(moveInput.x));
        animator.SetFloat("VerticalVel", RB.velocity.y);
        animator.SetBool("OnGround", onGround);
        #endregion

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        #region Run
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = moveInput.x * maxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - RB.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

        /*
         * For those interested here is what AddForce() will do
         * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
         * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
        */
        #endregion
        #endregion

        #region Friction
        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(RB.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(RB.velocity.x);
            RB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Jump
        if (jumpTimer > Time.time && coyoteTimeCounter > 0f || doubleJump)
        {
            Jump();
            if(doubleJump)
            {
                doubleJump = false;
            }
        }

        modifyPhysics();
        #endregion
    }

    void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, 0f);
        RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0f;
        coyoteTimeCounter = 0f;
        Instantiate(jumpParticle, transform.position, Quaternion.identity);
        canDoubleJump = !canDoubleJump;        
    }

    void modifyPhysics()
    {
        if (onGround)
        {
            RB.gravityScale = 0f;
        }
        else
        {
            RB.gravityScale = gravity;
            RB.drag = linearDrag * 0.15f;
            if (RB.velocity.y < 0f)
            {
                /*RB.gravityScale = gravity * fallMultiplier;*/
                RB.gravityScale = gravity * fallMultiplier;
                if (RB.velocity.y < maxFallSpeed)
                {
                    RB.velocity = new Vector2(RB.velocity.x, maxFallSpeed);
                }
            }
            else if (RB.velocity.y > 0f && !Input.GetKey(KeyCode.X))
            {
                RB.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
        {
            Turn();
        }
    }

    void Turn()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }

    void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }

    private IEnumerator Dash(Vector2 dir)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = RB.gravityScale;
        SetGravityScale(0);

        GameObject go = Instantiate(runParticle, transform.position, Quaternion.identity);
        Vector2 scale = go.transform.localScale;
        scale.x *= (isFacingRight ? 1 : -1); // 방향에 따라 파티클의 x 스케일을 결정. 
        go.transform.localScale = scale;
        go.transform.position += dashAnimationOffset * (isFacingRight ? 1 : -1);

        RB.velocity = dir * dashingPower;

        animator.SetBool("IsDash", true);
        sr.flipX = true;

        yield return new WaitForSeconds(dashingTime);
        SetGravityScale(originalGravity);
        isDashing = false;

        animator.SetBool("IsDash", false);
        sr.flipX = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }*/
}