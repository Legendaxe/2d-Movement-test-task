using UnityEngine;


public class MyCharacterControler : MonoBehaviour
{
    
    // Information about movement;
    [Header("Movement settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runAccelorationTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float liftSpeed;
    [SerializeField] private float crouchSpeed;
    [Header("Movement Animations")]
    [SerializeField] private string StandAnimation = "Stand";
    [SerializeField] private string LiteRunAnimation = "Stand";
    [SerializeField] private string MediumRunAnimation = "Stand";
    [SerializeField] private string HardRunAnimation = "Stand";
    [SerializeField] private string CrouchAnimation = "Stand";
    [SerializeField] private string DownCrouchAnimation = "Stand";
    [SerializeField] private string DashAnimation = "Stand";
    [SerializeField] private string ReDashAnimation = "Stand";
    [SerializeField] private string SitAnimation = "Stand";
    [SerializeField] private string LieAnimation = "Stand";
    [SerializeField] private string FallAnimation = "Stand";
    [SerializeField] private string GrabAnimation = "Stand";
    [SerializeField] private string BlockAnimation = "Stand";
    [SerializeField] private string ClimbAnimation = "Stand";


    private int stand; // 0 - Stand, 1 - Sit, 2 - Lie



    private MoveType movetype; 

    public enum MoveType
    {
        Idle,
        Walk,
        Crouch,
        Ready,
        Steady,
        GO,
        Dash,
        ReDash,
        Fall,
        Grab,
        SoloLifting,
        OverJump
    }


    // Over jump/lift staff
    private float liftTimer;

    private float liftDuration;
    private bool liftFirstStage;
    private bool liftSecondStage;

    // Dash staff

    private float dashTimer;
    private bool dashStatus;
    private float reDashTimer;
    private bool reDashStatus;



    // Other
    
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private BoxCollider2D characterCollider2;
    private Rigidbody2D grabBody;
    private PlayerInputProcessor playerInput;


    private float direction;



  


    // Start is called before the first frame update
    private void Start()
    {
       
        rb = transform.GetComponent<Rigidbody2D>(); 
        characterCollider2 = transform.GetComponent<BoxCollider2D>();
        playerInput = transform.GetComponent<PlayerInputProcessor>();

        

        // Movement preparation
        stand = 0;
        movetype = MoveType.Idle;
        direction = 1;


        // Timers preparation
        
        dashTimer = 0;
        dashStatus = false;
        reDashStatus = false;
        liftTimer = 0;
        liftFirstStage = false;
        liftSecondStage = false;
    }


    public  MoveType Movetype { get { return movetype; } set { movetype = value; } }
    public int Stand { get { return stand; } set { stand = value; } }
    public float LiftSpeed { get { return liftSpeed; } }
    public float RunAccelorationTime { get { return runAccelorationTime; } }
    public float RunMaxSpeed { get { return runSpeed; } }
    public float LiftTimer { set { liftTimer = value; } }
    public float LiftDuration { set { liftDuration = value; } }
    public float DashTimer { set { dashTimer = value; } }
    public float HeightOfCharacter { get { return characterCollider2.bounds.size.y; } }
    public float HalfOfCharacterHeight { get { return characterCollider2.bounds.extents.y; } }


    public float HeightOfObstecle()
    {
        int bitMask = (1 << 7); // Ignoring all except the ground
        RaycastHit2D hitted = Physics2D.Raycast(characterCollider2.bounds.center, transform.right, characterCollider2.bounds.extents.x +.05f, bitMask);
        if (hitted)
        {
            return hitted.collider.bounds.size.y; 
        }
        return 0;
    }

    public bool OverJumpCheck()
    {
        int bitMask = (1 << 7); // Ignoring all except the ground
        RaycastHit2D hitted = Physics2D.Raycast(characterCollider2.bounds.center, transform.right, characterCollider2.bounds.extents.x + .05f, bitMask);
        if (hitted)
        {
            if ((hitted.collider.bounds.size.x < 3f) && (hitted.collider.bounds.size.y < characterCollider2.bounds.size.y + 2f))
                return true;
        }
        return false;
    }


    public bool GrabCheck()
    {
        int bitMask = (1 << 8); // Ignoring all except the other players
        RaycastHit2D hitted = Physics2D.Raycast(characterCollider2.bounds.center, Vector2.down, characterCollider2.bounds.extents.y - 1f,bitMask);
        if (hitted)
        {
            grabBody = hitted.rigidbody;
            return true;
        }
        return false;
    }


    public bool IsGrounded() // Returns true if the character is on the ground
    {
       
        int bitMask = ( 1 << 7); // ignoring all except the ground
        Vector2 temp = new Vector2(characterCollider2.bounds.center.x ,characterCollider2.bounds.center.y);
        RaycastHit2D hitted = Physics2D.Raycast(temp, Vector2.down, characterCollider2.bounds.extents.y + 1f, bitMask);
        if (hitted)
            return true;
        temp.x = temp.x - direction * characterCollider2.bounds.extents.x;
        hitted = Physics2D.Raycast(temp, Vector2.down, characterCollider2.bounds.extents.y + 1f, bitMask);
        if (hitted)
            return true;
        temp.x = temp.x + 2* direction * characterCollider2.bounds.extents.x;
        hitted = Physics2D.Raycast(temp, Vector2.down, characterCollider2.bounds.extents.y + 1f, bitMask);
        if (hitted)
            return true;
        return false;
    }

    public bool WallCheck(Vector2 checkDirection, float distance, float height)  // Returns true if there isn't an obstacle in the selected direction
    {
        int bitmask = ~ ((1 << 8) | (1 << 6)); // ignoring players layers
        Vector2 firstVector = new Vector2(characterCollider2.bounds.center.x, characterCollider2.bounds.center.y + height);
        RaycastHit2D hitted = Physics2D.Raycast(firstVector, checkDirection.normalized, distance, bitmask);
        
        if (hitted)
            return false;
        else
            return true;
    }
    public void RotateCharacter(float x, float y, float z) => transform.Rotate(x,y,z);



    public void StandOrSit()
    {
        if (stand == 0)
            animator.Play(StandAnimation);
        else

            animator.Play(SitAnimation);
    }

    public void WalkOrCrawl(Vector2 inputHorizontal)
    {
        if (stand == 0)
        {
            if (WallCheck(transform.right, 2.5f, 0f))
                animator.Play(StandAnimation);
            else
                animator.Play(BlockAnimation);
            rb.velocity = new Vector2(inputHorizontal.x * walkSpeed, inputHorizontal.y);
        }
        else
        {
            animator.Play(DownCrouchAnimation);
            rb.velocity = new Vector2(inputHorizontal.x * crouchSpeed, inputHorizontal.y);
        }
    }
    public void SlowWalkOrSlowCrawl(Vector2 inputHorizontal)
    {
        if (stand == 0)
        {
            if (WallCheck(transform.right, 2.5f, 0f))
                animator.Play(CrouchAnimation);
            else
                animator.Play(BlockAnimation);
            rb.velocity = new Vector2(inputHorizontal.x * crouchSpeed, inputHorizontal.y);
        }
        else
        {
            animator.Play(DownCrouchAnimation);
            rb.velocity = new Vector2(inputHorizontal.x * crouchSpeed * 0.6f, inputHorizontal.y);
        }
    }
    public void FirstRunStage(Vector2 inputHorizontal)
    {
        if (WallCheck(transform.right, 3f, 0))
        {
            animator.Play(LiteRunAnimation);
            rb.velocity = inputHorizontal;
        }
        else
        {
            stand = 0;
            animator.Play(BlockAnimation);
        }
    }
    public void SecondRunStage(Vector2 inputHorizontal)
    {
        if (WallCheck(transform.right, 3f, 0))
        {
            rb.velocity = inputHorizontal;
            animator.Play(MediumRunAnimation);
        }
        else
        {
            animator.Play(BlockAnimation);
        }
    }
    public void ThirdRunStage(Vector2 inputHorizontal)
    {
        if (WallCheck(transform.right, 3f, 0))
        {
            rb.velocity = inputHorizontal;
            animator.Play(HardRunAnimation);
        }
        else
        {
            animator.Play(BlockAnimation);
        }
    }



    public void Dash(Vector2 inputHorizontal)
    {
        if (dashStatus)
        {
            if (Time.time - dashTimer > dashDuration)
            {
                dashStatus = false;
                movetype = MoveType.Steady;
            }
            else if (IsGrounded())
            {
                rb.velocity = new Vector2(transform.right.x * runSpeed * 0.7f, inputHorizontal.y);
            }
            else
            {
                movetype = MoveType.Fall;
                dashStatus = false;
            }
        }
        else
        {
            dashStatus = true;
            animator.Play(DashAnimation);
        }
    }

    public void ReDash()
    {
        if (reDashStatus)
        {
            if(reDashTimer < .5f)
            {
                reDashTimer += Time.fixedDeltaTime;
                rb.velocity = new Vector2(rb.velocity.x*0.5f, rb.velocity.y);
            }
            else
            {
                reDashStatus = false;
                RotateCharacter(0, 180, 0);
                movetype = MoveType.Steady;
            }
        }
        else
        {
            animator.Play(ReDashAnimation);
            reDashTimer = 0;
            reDashStatus = true;
        }
    }

    public void Fall()
    {
        stand = 0;
        animator.Play(FallAnimation);
    }

    public void Grab(Vector2 inputHorizontal)
    {
        animator.Play(GrabAnimation);
        rb.velocity = inputHorizontal * crouchSpeed;
        grabBody.velocity = inputHorizontal * crouchSpeed;
    }

    public void Lift()
    {
        if (liftFirstStage)
        {
            if (Time.time - liftTimer > liftDuration)
            {
                if (liftSecondStage)

                {
                    liftDuration = characterCollider2.bounds.size.x / liftSpeed;
                    liftTimer = Time.time;
                    liftSecondStage = false;
                }
                else
                {
                    rb.isKinematic = false;
                    movetype = MoveType.Idle;
                    liftFirstStage = false;
                }
            }
            else if (liftSecondStage)
                rb.transform.Translate(Vector2.up * liftSpeed * Time.fixedDeltaTime, Space.World);
            else
                rb.transform.Translate(transform.right * liftSpeed * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            rb.isKinematic = true;
            animator.Play(ClimbAnimation);
            rb.velocity = Vector2.zero;
            liftFirstStage = true;
            liftSecondStage = true;
        }
    }

    public void JumpOver()
    {
        if (liftFirstStage)
        {
            if (Time.time - liftTimer > liftDuration)
            {
                if (liftSecondStage)

                {
                    liftDuration = (characterCollider2.bounds.extents.x + 3f) / liftSpeed;
                    liftTimer = Time.time;
                    liftSecondStage = false;
                }
                else
                {
                    rb.isKinematic = false;
                    movetype = MoveType.Idle;
                    liftFirstStage = false;
                }
            }
            else if (liftSecondStage)
                rb.transform.Translate(Vector2.up * liftSpeed * Time.fixedDeltaTime, Space.World);
            else
                rb.transform.Translate(transform.right * liftSpeed * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            rb.isKinematic = true;
            animator.Play(ClimbAnimation);
            rb.velocity = Vector2.zero;
            liftFirstStage = true;
            liftSecondStage = true;
        }
    }
    public void Lying()
    {
        animator.Play(LieAnimation);
    }

}
