using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class GameControllerLevel2 : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    //public GameObject playerPrefab2;

    public GameObject Victory;
    public GameObject GameOver;
    public int placement;
    public bool TimeRuns = false;


    //Sounds
    public AudioSource RunningS;
    public AudioSource JumpingS;
    public AudioSource StreetS;

    public static int time;


    //Time Calculation
    public Text TimeText;
    public float TimeCount;
    public bool timerIsRunning = false;
    bool key = true;



    void Start()
    {
        placement = managerContChar2.placement;

        placement = 0;
        Victory.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        //StartCoroutine(SWait());
        TimeCount = GameController2.TimeCount;

        Vector3 spawnPosition = new Vector3(-31, 0.59f, -383f);  //frist player's spawn position

        JumpingS.GetComponent<AudioSource>();


         PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPosition, Quaternion.identity, 0);
         //PhotonNetwork.LocalPlayer.SetPlayerNumber(1);



        /*if(key == true)
        {
            StartCoroutine(SWait());
        }*/
    }

    // Update is called once per frame
    void Update()
    {


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


        if (timerIsRunning)
        {
            TimeCount += Time.deltaTime;
            DisplayTime(TimeCount);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            JumpingS.Play();
        }



    }

    public void DisplayTime(float TimeToDisplay)
    {
        TimeToDisplay += 1;

        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void OnTriggerEnter(Collider other)
    {

    }
}
