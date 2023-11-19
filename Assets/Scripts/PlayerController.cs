using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; set; }
    /*ShieldController shield;*/

    [Header("HP")]
    public float player_max_helth_point = 6;
    public float player_helth_point = 6;

    [Header("Run")]
    public float maxSpeed = 15.0f;
    public float lerpAmount = 0.5f;
    float runAccelAmount = 19f;
    float runDeccelAmount = 19f;

    [Header("Vertical Movement")]
    public float jumpDelay = 0.25f;
    public float jumpForce = 5f;
    public float fallMultiplier = 4f;
    [SerializeField] public float maxFallSpeed;
    private float jumpTimer;
    private float coyoteTime = 5f;
    private float coyoteTimeCounter;
    [SerializeField] private int jumpCounter;
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool canDoubleJump;

    bool isFacingRight = true;
    [SerializeField]
    Vector2 start_pos;

    [Header("Physics")]
    public float linearDrag = 5f;
    public float gravity = 1f;
    public float Velocity;
    public float frictionAmount;
    public Rigidbody2D RB;

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

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 12f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.3f;
    private Vector3 dashDir;
    private float dashDistance = 6f;

    public SoundManager sound_manager;
    private float respawn_delay;

    private void Awake()
    {
        Pause.ResumeGame();
        RB = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        instance = this;
    }

    void Start()
    {        
        start_pos = transform.position;
        respawn_delay = 0.7f;
        /*shield = GetComponent<ShieldController>();*/
    }

    void Update()
    {
        if (Pause.GameStop == 0)
        {
            return;
        }

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
            /*if (Input.GetKeyDown(KeyCode.대시키) && canDash)*/
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
            sound_manager.SfxPlayer(SoundManager.sfx.dash);
            StartCoroutine(nameof(Dash));
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

            /*if (Input.GetKeyDown(KeyCode.점프키))*/
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                //JumpBuffer
                jumpTimer = Time.time + jumpDelay;
                if (canDoubleJump)
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
            if (doubleJump)
            {
                doubleJump = false;
            }
        }

        modifyPhysics();
        #endregion
    }
    
    void Jump()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.jump);
        RB.velocity = new Vector2(RB.velocity.x, 0f);
        RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0f;
        coyoteTimeCounter = 0f;
        Instantiate(jumpParticle, transform.position, Quaternion.identity);
        canDoubleJump = !canDoubleJump;
    }   

    public void modifyPhysics()
    {
        if (onGround)
        {
            RB.gravityScale = 0f;
        }
        else
        {
            // press and hold jump key
            RB.gravityScale = gravity;
            RB.drag = linearDrag * 0.15f;

            // fall down
            if (RB.velocity.y < 0f)
            {
                RB.gravityScale = gravity * fallMultiplier;
                if (RB.velocity.y < maxFallSpeed)
                {
                    RB.velocity = new Vector2(RB.velocity.x, maxFallSpeed);
                }
            }
            else if (RB.velocity.y > 0f && !Input.GetKey(KeyCode.X)) // after unhold jump key
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = RB.gravityScale;
        SetGravityScale(0);

        #region Particle
        GameObject go = Instantiate(runParticle, transform.position, Quaternion.identity);
        Vector2 scale = go.transform.localScale;
        scale.x *= (isFacingRight ? 1 : -1); /*방향에 따라 파티클의 x 스케일을 결정. */
        go.transform.localScale = scale;
        go.transform.position += dashAnimationOffset * (isFacingRight ? 1 : -1);
        #endregion

        /*방향키의 입력 방향에 따라 대시 방향 결정*/
        /*대시 방식을 속도를 지정하는게 아닌 위치를 강제로 이동하는 방식으로 변경*/
        /*RB.velocity = dir * dashingPower; 속도 지정 방식*/

        #region Animator
        animator.SetBool("IsDash", true);
        sr.flipX = true;
        #endregion

        Vector3 startDashPoint = transform.position;

        #region Dash Direction
        /*Vector3 endDashPoint = startDashPoint + new Vector3(6f, 0, 0) * (isFacingRight ? 1 : -1);*/
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f && Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.01f)
        {
            dashDir = Vector3.right * (isFacingRight ? 1 : -1);
        }
        else
        {
            dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        }
        dashDir.Normalize();
        #endregion
        Vector3 endDashPoint = startDashPoint + dashDir * dashDistance;

        float currentTime = 0f;
        float dashTime = 0.35f;
        for (; currentTime < dashTime; currentTime += Time.deltaTime)
        {
            float t = currentTime / dashTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); /*ease in*/
            /*t = t * t * t * (t * (6f * t - 15f) + 10f);*/ /*smooth step*/
            RB.velocity = Vector3.Lerp((endDashPoint - startDashPoint) / dashTime, Vector3.zero, t);
            yield return null;
        }

        /*yield return new WaitForSeconds(dashingTime);*/
        SetGravityScale(originalGravity);
        isDashing = false;

        animator.SetBool("IsDash", false);
        sr.flipX = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public IEnumerator Respawn()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(respawn_delay);
        player_helth_point = 6;
        transform.position = start_pos;
        sr.enabled = true;
    }


    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }*/
}