using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class GameController2 : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    //public GameObject playerPrefab2;
    public GameObject StartPanel;
    //7
    public GameObject GameOVerPanel;
    public Text GameOverText;


    public GameObject Victory;
    public GameObject GameOver;
    public int placement;
    public bool TimeRuns = false;
    private int placemment;
    private int checkpoint;

    //Sounds
    public AudioSource RunningS;
    public AudioSource JumpingS;
    public AudioSource StreetS;

    public Text Timer;
    public static int time;
    public static int StartWait;


    //Time Calculation
    public Text TimeText;
    public static float TimeCount;
    public bool timerIsRunning = false;
    bool key = true;



    void Start()
    {
        GameOVerPanel.SetActive(false);

        StartWait = 5;

        placement = 0;
        Victory.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        //StartCoroutine(SWait());
        TimeCount = 0;
        
        Vector3 spawnPosition = new Vector3(1.3f,5f, 138.5f);  //frist player's spawn position
        Vector3 spawnPosition2 = new Vector3(0f, 5f, 138.5f);   //second player's spawn position

        JumpingS.GetComponent<AudioSource>();

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {

        PhotonNetwork.Instantiate(this.playerPrefab.name,spawnPosition, Quaternion.identity, 0);
        //PhotonNetwork.LocalPlayer.SetPlayerNumber(1);
        
        }
        
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            
        PhotonNetwork.Instantiate(this.playerPrefab.name,spawnPosition2, Quaternion.identity, 0);
        
        }

        /*if(key == true)
        {
            StartCoroutine(SWait());
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && key == true)
        {
            StartCoroutine(SWait());
            key = false;
            timerIsRunning = true;


        }
        if (StartWait == 0)
        {
            StartPanel.gameObject.SetActive(false);
            StopCoroutine(SWait());
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
        }
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
        string SW = StartWait.ToString();
        Timer.text = SW;

        if (timerIsRunning)
        {
            TimeCount += Time.deltaTime;
            DisplayTime(TimeCount);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            JumpingS.Play();
        }

        if(managerContChar2.placement>1)
        {
            Victory.SetActive(true);
        }


        if (managerContChar2.checkpoint == 5)
        {
            GameOVerPanel.SetActive(true);
            GameOverText.text = TimeText.text;
            PhotonNetwork.LoadLevel("Level2");
        }

    }

    public void DisplayTime(float TimeToDisplay)
    {
        TimeToDisplay += 1;

        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator SWait()
    {
        if (StartWait == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(1);
                StartWait = StartWait - 1;
            }


        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void OnTriggerEnter(Collider other)
    {

    }
}
