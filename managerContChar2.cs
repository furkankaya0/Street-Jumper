using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class managerContChar2 : MonoBehaviour
{
    PhotonView photonView;
    public static bool dorumu = false;
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
    public float waitingsec = 1.0f;
    private string currentAnimation;
    public static int checkpoint;

    int resetlendi;

    //Animation States
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_WALK = "Player_walk";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_SPRINT = "Player_Sprint";
    const string ENEMY_PUNCH = "Enemy_Punch";
    const string ENEMY_KICK = "Enemy_Kick";
    const string PLAYER_KNOCKDOWN = "Player_KnockDown";

    /*GameUI
    public GameObject StartPanel;
    public Text Timer;
    public GameObject Victory;
    public GameObject GameOver;
    public bool timerIsRunning = false;

    //Time Calculation
    public Text TimeText;
    public static float TimeCount;

    //Sounds
    public AudioSource RunningS;
    public AudioSource JumpingS;
    public AudioSource StreetS;*/
    //�

    public static int placement;
    public static int StartWait;


    void Start()
    {
    
        

        placement = 0;
        /*Victory.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);

        StartWait = 5;*/
        gravity = 0.5f;
        jumpForce = 0.13f;

        GameObject temPlayer = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = temPlayer.transform.GetChild(0);
        photonView = GetComponent<PhotonView>();

        _charController = temPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
        //reset = false;
        resetlendi = 0;
        checkpoint = 0;
        //
        
        StartWait = 5;

        //StartCoroutine(STARTWAIT());
        /*Time
        /timerIsRunning = true;
        TimeCount = 0;*/

        //view = GetComponent<PhotonView>();
        if (photonView.IsMine == true)
        {
            
            dorumu = true;
        }
        else if (photonView.IsMine == false)
        {
            dorumu = false;
        }
    }

    public void Update()
    {
        if (photonView.IsMine)
        {
            
            
            if (GameController2.StartWait == 0)
            {
                StopAllCoroutines();
                inputX = Input.GetAxis("Horizontal");
                inputZ = Input.GetAxis("Vertical");
            }

            //ANIMATION CONTROL
            //.................................
            //..................................
            
            //inputX = Input.GetAxis("Horizontal");
            //putZ = Input.GetAxis("Vertical");


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
            
            else 
            if (inputZ != 0 && Input.GetKey(KeyCode.LeftShift) && _charController.isGrounded)
             //else if (inputZ != 0 && Input.GetKey(KeyCode.LeftShift) && _charController.isGrounded && GameController2.StartWait == 0)
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
                else
                {
                    ChangeAnimationState(PLAYER_WALK);
                }

            }
            else if (Input.GetKey(KeyCode.Space))
            {

                ChangeAnimationState(PLAYER_JUMP);
                Delay();
                //JumpingS.Play();
            }

        }


    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;

    }
    public void FixedUpdate()
    {
        if (photonView.IsMine)
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
                }
            }
            else
            {
                v_movement.y -= gravity * Time.deltaTime;
            }


            if (Input.GetKey(KeyCode.LeftShift) && _charController.isGrounded)
            {
                moveSpeed = 0.09f;
                Debug.Log(moveSpeed);
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
            


            if (checkpoint == 1)                //Checkpoint1
            {
                ObjectPosition.transform.position = new Vector3(1.1f, 2.099f, 0.0f);
                resetlendi = 1;
                if (resetlendi == 1)
                {
                    checkpoint = 0;
                    resetlendi = 0;
                }

            }
            else if (checkpoint == 2)          //Checkpoint2
            {
                ObjectPosition.transform.position = new Vector3(-2.0f, 6.0f, 27.55f);
                resetlendi = 1;
                if (resetlendi == 1)
                {
                    checkpoint = 0;
                    resetlendi = 0;
                }
            }
            else if (checkpoint == 3)           //Checkpoint3
            {
                ObjectPosition.transform.position = new Vector3(-2.0f, 3.63f, 68.50719f);
                resetlendi = 1;
                if (resetlendi == 1)
                {
                    checkpoint = 0;
                    resetlendi = 0;
                }
            }
            else if (checkpoint == 4)           //Checkpoint4
            {
                ObjectPosition.transform.position = new Vector3(-2.0f, 6.0f, 107.0f);
                resetlendi = 1;
                if (resetlendi == 1)
                {
                    checkpoint = 0;
                    resetlendi = 0;
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
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
            if (other.gameObject.tag == "FinishLine")
            {
                checkpoint = 5;
            }
        }


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitingsec);
    }


}

