using UnityEngine;
using UnityEngine.UI;


public class MyCharacterControler : MonoBehaviour
{
    // Penalty
    [System.Serializable] protected struct Limit
    {
        public int limit;
        public float powerOrSeconds;
    }
    [SerializeField] protected Limit[] runLimit = new Limit[3];
    [SerializeField] protected Limit[] weightLimit = new Limit[3];

    // Endurance
    [SerializeField] protected float endurance;
    [SerializeField] protected int enduranceRegenSpeed;
    [SerializeField] protected int enduranceWasteSpeed;
    [SerializeField] protected Slider enduranceSlider;

        protected float realEndurance;
        protected bool conditionEndurance;
        protected float enduranceTimer;

    // Information about movement;
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float dashDuration;
    [SerializeField] protected float liftSpeed;
    [SerializeField] protected float crouchSpeed;

        protected int stand; // 0 - Stand, 1 - Sit, 2 - Lie
        protected int moveType; // 0 - Idle, 1 - Walk, 2 - Crouch
                                // 3 - Ready,  4 - Steady, 5 - GO (3,4,5 stands for run stage)
                                // 6 - Dash, 7 - Fall,  8 - Grab, 9 - Solo lifting
                                // 10 - Over jump
        protected float weightModifficator;

        // Over jump/lift staff
        protected float liftHeight;
        protected float liftRealSpeed;
        protected float liftTimer;
        
        protected float liftDuration;
        protected bool liftFirstStage;
        protected bool liftSecondStage;

        // Dash staff
        protected float doubleTapTimerLeftDash;
        protected float doubleTapTimerRightDash;
        protected float dashTimer;
        protected bool dashStatus;
        

        // Run staff
        protected float runTimer;
        


    // Injurities
    protected bool injurityLeg;



    // Weight
    [SerializeField] protected float weight;
    [SerializeField] protected Animator animator;

    // Grab
    Vector2 grabBodyLastPos;

    // Other
    [SerializeField] protected GameObject injContr;
    protected Rigidbody2D rb;
    protected BoxCollider2D characterCollider2;
    protected Rigidbody2D grabBody;
    protected InjurityController injurityController;

    protected float direction;
    protected float inputHorizontal;
    protected float inputVertical;

    protected bool capsLock;


    // Start is called before the first frame update
    void Start()
    {
        injurityController = injContr.GetComponent<InjurityController>();
        rb = transform.GetComponent<Rigidbody2D>(); 
        characterCollider2 = transform.GetComponent<BoxCollider2D>();
        capsLock = false;

        // Endurance preparation
        realEndurance = endurance;
        enduranceSlider.value = realEndurance;
        conditionEndurance = false;

        // Movement preparation
        stand = 0;
        moveType = 0;
        direction = 1;
        weightModifficator = 1;


        // Timers preparation
        runTimer = 0;
        enduranceTimer = 10;
        doubleTapTimerLeftDash = 0;
        doubleTapTimerRightDash = 0;
        dashTimer = 0;
        dashStatus = false;
        liftTimer = 0;
        liftFirstStage = false;
        liftSecondStage = false;
    }

    void Update()
    {
        // Input
        inputVertical = Input.GetAxisRaw("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        bool keyLeftUp = Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
        bool keyRightUp = Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
        bool keyShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool keySit = Input.GetKeyDown(KeyCode.C);
        bool keyLie = Input.GetKeyUp(KeyCode.Z);
        bool keyPlayerCooperate = Input.GetKey(KeyCode.F);

        // Capslock check
        if (Input.GetKeyUp(KeyCode.CapsLock))
            capsLock = !capsLock;

        // Input calculations;
        if ((moveType != 6) && (moveType !=9) && (moveType != 10) && GroundCheck())
        {
            MoveCheck(inputHorizontal, keyLeftUp, keyRightUp, keyShift, keyPlayerCooperate);
            StandCheck(keySit, keyLie);
        }


        // Endurance check
        Endurance();
        enduranceTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Move
        Moving(inputHorizontal);
    }


    public void WeightModChange(float weight)
    {
        if (weight < weightLimit[0].limit)
            weightModifficator = weightLimit[0].powerOrSeconds;
        foreach(var item in weightLimit)
        {
            if (weight < item.limit)
            {
                weightModifficator = item.powerOrSeconds;
                break;
            }

        }
    }

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


    public bool GroundCheck() // Returns true if the character is on the ground
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
        moveType = 7;
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

    public void MoveCheck(float inputHorizontal, bool keyLeftUp,
                                  bool keyRightUp, bool keyShift, bool keyPlayerCooperate) 
    {
        if ((inputHorizontal != 0) && (stand != 2))
        {
            // Rotate character if he changed direction
            if (inputHorizontal * direction < 0)
                transform.Rotate(0, 180, 0);
            direction = inputHorizontal > 0 ? 1 : -1;

            // Run
            if (keyShift && !injurityController.InjurityCheck("Legs") && (realEndurance > 5))
            {
                if (runTimer == 0)
                {
                    moveType = 3;
                    stand = 0;
                }
                else if (runTimer < 2)
                    moveType = 4;
                else
                    moveType = 5;
                runTimer += Time.deltaTime;
            }
            // Grab
            else if (keyPlayerCooperate)
            {
                if (WallCheck(transform.right, 3f, -characterCollider2.bounds.extents.y) && GrabCheck())
                    moveType = 8;
                else
                    moveType = 1;
            }
            // Crouch
            else if (capsLock)
                moveType = 2;
            // Walk
            else
                moveType = 1;
        }
        // Lift
        else if (keyPlayerCooperate)
        {
            liftHeight = HeightOfObstecle();
            if(liftHeight != 0)
            {
                liftDuration = (liftHeight+0.5f) / liftSpeed;
                liftTimer = Time.time;
                if (liftHeight < characterCollider2.bounds.size.y)
                    moveType = 9;
            }
            return;

        }
        // Over Jump
        else if (inputVertical == 1)
        {
            if (OverJumpCheck())
            {
                liftDuration = HeightOfObstecle() / liftSpeed;
                liftTimer = Time.time;
                moveType = 10;
            }
        }
        else
            moveType = 0;


        // Check run status
        if (!keyShift)
            runTimer = 0;
        // Double tap check, for left and right
        if (moveType<3)
        {
            if (keyLeftUp && (stand != 2) && (direction < 0))
                if ((Time.time - doubleTapTimerLeftDash < 0.3f) && (realEndurance > 30))
                {
                    moveType = 6;
                    dashTimer = Time.time;
                    stand = 0;
                    return;
                }
                else
                    doubleTapTimerLeftDash = Time.time;

            else if (keyRightUp && (stand != 2) && (direction > 0))
                if ((Time.time - doubleTapTimerRightDash < 0.3f) && (realEndurance > 30))
                {
                    moveType = 6;
                    dashTimer = Time.time;
                    stand = 0;
                    return;
                }
                else
                    doubleTapTimerRightDash = Time.time;
            
        }
        if (injurityController.InjurityCheck("Legs") && (inputHorizontal != 0))
        {
            moveType = 2;
        }
    }
    
    public void StandCheck(bool keySit, bool keyLie)
    {
        // Sit
        if (keySit)
        {
            stand = stand == 1 ? (WallCheck(transform.up, 3f, characterCollider2.bounds.extents.y) ? 0 : 1) : 1;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Lie
        if (keyLie)
        {
            stand = stand == 2 ? 0 : ((WallCheck(transform.right, 3f, -characterCollider2.bounds.extents.y) && WallCheck(-transform.right, 3f, -characterCollider2.bounds.extents.y)) ? 2 : 0);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }



    public void Endurance() // Endurance
    {
        if ((moveType == 0) && (enduranceTimer < 1))         
        {
            realEndurance += enduranceRegenSpeed;
            enduranceTimer = 10;
        }
        else if ((moveType == 5 ) && (enduranceTimer < 7))
        {
            realEndurance -= enduranceWasteSpeed;
            enduranceTimer = 10;
        }
        enduranceSlider.value = realEndurance;
    }
    public void Moving(float inputHorizontal)
    {
        if(stand != 2) // If character liyng we can't do anything
        {
            switch (moveType) // Check move 
            {
                case 0: // Standing here... (or sitting)
                    {
                        if (stand == 0)
                            animator.Play("Idle");
                        else
                        {
                            animator.Play("Sitting");
                        }
                        break;
                    }
                case 1: // walking or crawling
                    {
                        if (stand == 0)
                        {
                            if (WallCheck(transform.right, 2.5f, 0f))
                                animator.Play("Walking");
                            else
                                animator.Play("Blocked move");
                            rb.velocity = new Vector2(inputHorizontal * walkSpeed * weightModifficator, rb.velocity.y);
                        }
                        else
                        {
                            animator.Play("Down Crouching");
                            rb.velocity = new Vector2(inputHorizontal * crouchSpeed * weightModifficator, rb.velocity.y);
                        }

                        break;
                    }
                case 2: // slow walk or slow crawling
                    {
                            if (stand == 0)
                            {
                                if (WallCheck(transform.right, 2.5f, 0f))
                                    animator.Play("Crouching");
                                else
                                    animator.Play("Blocked move");
                                rb.velocity = new Vector2(inputHorizontal * crouchSpeed * weightModifficator, rb.velocity.y);
                            }
                            else
                            {
                                animator.Play("Down Crouching");
                                rb.velocity = new Vector2(inputHorizontal * crouchSpeed * weightModifficator * 0.6f, rb.velocity.y);
                            }
                        break;
                    }
                case 3: // First run stage
                    {
                        if (WallCheck(transform.right, 3f, 0))
                        {
                            animator.Play("Running");
                            rb.velocity = new Vector2(inputHorizontal * runSpeed * 0.5f * weightModifficator, rb.velocity.y);
                        }
                        else
                        {
                            stand = 0;
                            animator.Play("Blocked move");
                            runTimer = 0;
                        }
                        break;
                    }
                case 4: // Second run stage
                    {
                        if (WallCheck(transform.right, 3f, 0))
                        {
                            rb.velocity = new Vector2(inputHorizontal * runSpeed * weightModifficator * 0.7f, rb.velocity.y);
                            animator.Play("Running");
                        }
                        else
                        {
                            stand = 0;
                            animator.Play("Blocked move");
                            runTimer = 0;
                        }
                        break;
                    }
                case 5: // Third run stage
                    {
                        if (WallCheck(transform.right, 3f, 0))
                        {
                            rb.velocity = new Vector2(inputHorizontal * runSpeed * weightModifficator, rb.velocity.y);
                            animator.Play("Running");
                        }
                        else
                        {
                            stand = 0;
                            animator.Play("Blocked move");
                            runTimer = 0;
                        }
                        break;
                    }
                case 6: // Dash
                    {
                        animator.Play("Dash");
                        if (dashStatus)
                        {
                            if (Time.time - dashTimer > dashDuration)
                            {
                                moveType = 0;
                                rb.velocity = new Vector2(0, rb.velocity.y);
                                dashStatus = false;
                            }
                        }
                        else 
                        {
                            rb.velocity = new Vector2(direction * dashSpeed * weightModifficator, rb.velocity.y);
                            dashStatus = true;
                        }
                        break;
                    }
                case 7: // Fall
                    {
                        stand = 0;
                        animator.Play("Jumping Fall");
                        break;
                    }
                case 8: // Grabb
                    {
                        animator.Play("Grabbing");
                        rb.velocity = new Vector2(inputHorizontal * crouchSpeed, rb.velocity.y);
                        grabBody.transform.Translate(new Vector2(inputHorizontal * crouchSpeed * Time.fixedDeltaTime,0),Space.World);
                        break;
                    }
                case 9: // Lifting
                    {
                        if (liftFirstStage)
                        {
                            if (Time.time - liftTimer > liftDuration)
                            {
                                if (liftSecondStage)

                                {
                                    liftDuration = characterCollider2.bounds.size.x/ liftSpeed;
                                    liftTimer = Time.time;
                                    liftSecondStage = false;
                                    animator.Play("Down Crouching");
                                }
                                else
                                {
                                    rb.isKinematic = false;
                                    moveType = 0;
                                    liftFirstStage = false;
                                }
                            }
                            else if(liftSecondStage)
                                rb.transform.Translate(Vector2.up * liftSpeed * Time.fixedDeltaTime, Space.World); 
                            else
                                rb.transform.Translate(transform.right * liftSpeed * Time.fixedDeltaTime, Space.World);
                        }
                        else
                        {
                            rb.isKinematic = true;
                            animator.Play("Climbing");
                            liftFirstStage = true;
                            liftSecondStage = true;
                        }
                       
                        break;
                    }
                case 10: // Over jump
                    {
                        if (liftFirstStage)
                        {
                            if (Time.time - liftTimer > liftDuration)
                            {
                                if (liftSecondStage)

                                {
                                    liftDuration = (characterCollider2.bounds.size.x + 3f) / liftSpeed;
                                    liftTimer = Time.time;
                                    liftSecondStage = false;
                                    animator.Play("Down Crouching");
                                }
                                else
                                {
                                    rb.isKinematic = false;
                                    moveType = 0;
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
                            animator.Play("Climbing");
                            liftFirstStage = true;
                            liftSecondStage = true;
                        }

                        break;
                    }
            }  
        }
        else
        {
            animator.Play("Liyng");
        }
    }
}
