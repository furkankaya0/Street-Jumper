using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singleplayer_Level2_Player : MonoBehaviour
{
    private Animator _animator;

    // components
    public CharacterController _charController;
    private Transform meshPlayer;
    public GameObject ObjectPosition;

    public Vector3 v_movement;  //Base valuse of movement sides
    private float inputX;
    private float inputZ;
    private float moveSpeed = 0.055f;
    private float gravity;
    private float jumpForce = 0.13f;
    public float waitingsec = 1.5f;
    private string currentAnimation;
    public int checkpoint;

    int resetlendi;

    //Animation States
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_WALK = "Player_walk";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_SPRINT = "Player_Sprint";
    const string ENEMY_PUNCH = "Enemy_Punch";
    const string ENEMY_KICK = "Enemy_Kick";
    const string PLAYER_KNOCKDOWN = "Player_KnockDown";

    //GameUI
    //public GameObject StartPanel;
    private Text Timer;
    private int StartWait;
    public GameObject Victory;
    public GameObject GameOver;
    public int placement;
    public bool TimeRuns = false;

    //Time Calculation
    public  Text TimeText;
    public float TimeCount1;

    //Sounds
    public AudioSource RunningS;
    public AudioSource JumpingS;
    public AudioSource StreetS;


    // TIME DISPLAYYY, SECOND AND MINUTES COUNTER

    void Start()
    {


        placement = 0;
        Victory.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);

        StartWait = 2;
        gravity = 0.5f;
        jumpForce = 0.13f;

        GameObject temPlayer = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = temPlayer.transform.GetChild(0);

        _charController = temPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
        //reset = false;
        resetlendi = 0;
        checkpoint = 0;
        //Timer
        StartCoroutine(SWait());
        //Time
        TimeRuns = true;
        TimeCount1 = 0;
        
        
    }

    void Update()
    {
        /*if (StartWait == 0)
        {
            //StartPanel.gameObject.SetActive(false);
            StopCoroutine(SWait());
        }
        else
        {
            //StartPanel.gameObject.SetActive(true);
        }*/
        //FINISH LINE
        if (placement == 1)
        {
            Victory.gameObject.SetActive(true);
            GameOver.gameObject.SetActive(false);
            TimeRuns = false;
        }
        else if (placement == 2)
        {
            Victory.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);

        }
        //string SW = StartWait.ToString();
        //Timer.text = SW;


        //ANIMATION CONTROL
        //.................................
        //..................................
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");


        if (inputX == 0 && inputZ == 0 && _charController.isGrounded) // animator working system
        {

            if (Input.GetKey(KeyCode.C) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_PUNCH);
            }
            else if (Input.GetKey(KeyCode.V) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_KICK);

            }
            else if (Input.GetKey(KeyCode.K) && checkpoint == 5)
            {
                ChangeAnimationState(PLAYER_KNOCKDOWN);

            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }

        }
        else if (inputZ != 0 && Input.GetKey(KeyCode.LeftShift) && _charController.isGrounded && StartWait == 0)
        {
            ChangeAnimationState(PLAYER_SPRINT);
        }
        else if (_charController.isGrounded)
        {

            if (Input.GetKey(KeyCode.C) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_PUNCH);
            }
            else if (Input.GetKey(KeyCode.V) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_KICK);
            }
            else if (StartWait == 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }

        }
        else if (Input.GetKey(KeyCode.Space))
        {

            ChangeAnimationState(PLAYER_JUMP);
            Delay();
            JumpingS.Play();

        }

        
        // TIME CONTROL
        //......................................
        //.......................................
        if (TimeRuns)
        {
            TimeCount1 += Time.deltaTime;
            TimeDisplay(TimeCount1);
        }
    }
    private void FixedUpdate()
    {
        // CHARACTER MOVEMENTS CONTROL
        //......................................
        //......................................
        if (_charController.isGrounded) // Character being exposed to Gravity if it is not on ground
        {
            v_movement.y = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                v_movement.y = jumpForce;
                //_animator.SetBool("IsInTheAir", true);
            }
            
        }
        else
        {
            v_movement.y -= gravity * Time.deltaTime;
            //_animator.SetBool("IsInTheAir", false);

        }

        if (checkpoint == 8 || StartWait != 0)           //Checkpoint5
        {
            moveSpeed = 0;
        }
        else
        if (Input.GetKey(KeyCode.LeftShift) && _charController.isGrounded)
        {
            moveSpeed = 0.09f;
            //Debug.Log(moveSpeed);
        }
        else
        {
            moveSpeed = 0.055f;
        }

        v_movement = new Vector3(inputX * moveSpeed, v_movement.y, inputZ * moveSpeed);
        _charController.Move(v_movement);


        if (inputX != 0 || inputZ != 0)  // mesh Rotation
        {
            Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);
        }
        //OpRb.transform.position = new Vector3(0, 1 ,3);


        if (checkpoint == 1)        //Checkpoint1
        {
            ObjectPosition.transform.position = new Vector3(-35.23f, 1.43f, -387.52f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }

        }
        else if (checkpoint == 2)          //Checkpoint2
        {
            ObjectPosition.transform.position = new Vector3(-28.9f, 5.21f, -355.83f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
        else if (checkpoint == 3)   //Checkpoint3
        {
            ObjectPosition.transform.position = new Vector3(-30.2f, 9.09f, -289.4f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
        else if (checkpoint == 4) //Checkpoint4
        {
            ObjectPosition.transform.position = new Vector3(-31.83f, 14.7f, -218.71f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
        else if (checkpoint == 5) //Checkpoint5
        {
            ObjectPosition.transform.position = new Vector3(-28.8f, 8.23f, -156.6f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
        else if (checkpoint == 6) //Checkpoint6
        {
            ObjectPosition.transform.position = new Vector3(-28.8f, 7.13f, -89.7f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
        else if (checkpoint == 7) //Checkpoint7
        {
            ObjectPosition.transform.position = new Vector3(-28.96f, 3.65f, -30.78f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                checkpoint = 0;
                resetlendi = 0;
            }
        }
    }

    IEnumerator SWait()
    {
        if (StartWait == 2)
        {
            for (int i = 0; i <2; i++)
            {
                yield return new WaitForSeconds(1);
                StartWait = StartWait - 1;

            }


        }
    }
    

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitingsec);
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Return")
        {
            checkpoint = 1;
        }

        if (other.gameObject.tag == "Checkpoint2")
        {
            checkpoint = 2;
        }
        if (other.gameObject.tag == "Checkpoint3")
        {
            checkpoint = 3;
        }
        if (other.gameObject.tag == "Checkpoint4")
        {
            checkpoint = 4;
        }
        if (other.gameObject.tag == "Checkpoint5")
        {
            checkpoint = 5;
        }
        if (other.gameObject.tag == "Checkpoint6")
        {
            checkpoint = 6;
        }
        if (other.gameObject.tag == "Checkpoint7")
        {
            //reset = true;
            checkpoint = 7;

            if (other.gameObject.tag == "FinishLine")
            {
                checkpoint = 8;
                placement = placement + 1;
            }
            
        }
        

    }
    void TimeDisplay(float TimeToDisplay2)
    {
        TimeToDisplay2 += 1;

        float minutes1 = Mathf.FloorToInt(TimeToDisplay2 / 60);
        float seconds1 = Mathf.FloorToInt(TimeToDisplay2 % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes1, seconds1);
    }
}

// COMMENTS
    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çalışıyor");

            if (Input.GetKey(KeyCode.C) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_PUNCH);
                OpRb.velocity =v_movement* 1;
                Debug.Log("buda çalişiyor");

            }
            else if (Input.GetKey(KeyCode.V) && _charController.isGrounded && moveSpeed < 0.08f)
            {
                ChangeAnimationState(ENEMY_KICK);
                OpRb.AddForce(v_movement);
            }

    }*/

