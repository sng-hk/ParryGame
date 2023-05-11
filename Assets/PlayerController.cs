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
    private float jumpTimer;

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

    [Header("Player Input")]
    Vector2 _moveInput;

    [Header("Events")]
    [Space]

    public UnityEngine.Events.UnityEvent OnLandEvent;


    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Start()
    {

    }


    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        Velocity = RB.velocity.x;

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        #region Jump
        /*onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);*/
        onGround = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, groundLayer);
        if (Input.GetKeyDown(KeyCode.X))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        #endregion

        #region Animator
        animator.SetFloat("HorizontalInput", Mathf.Abs(_moveInput.x));

        animator.SetFloat("VerticalVel", RB.velocity.y);
        animator.SetBool("OnGround", onGround);
        #endregion
    }

    private void FixedUpdate()
    {
        #region Run
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.x * maxSpeed;
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
        if(Mathf.Abs(_moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(RB.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(RB.velocity.x);
            RB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Jump
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        modifyPhysics();
        #endregion

        
    }

    void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, 0);
        RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
        /*StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));*/
    }

    void modifyPhysics()
    {   
        if (onGround)
        {           
            RB.gravityScale = 0;
        }
        else
        {
            RB.gravityScale = gravity;
            RB.drag = linearDrag * 0.15f;
            if (RB.velocity.y < 0)
            {
                RB.gravityScale = gravity * fallMultiplier;
            }
            else if (RB.velocity.y > 0 && !Input.GetKey(KeyCode.X))
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}