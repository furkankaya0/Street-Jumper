using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourPunCallbacks
{
    //public string firstLevel;

    public GameObject optionsScreen;
    public GameObject creditsScreen;
    public GameObject MainMenuScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
        MainMenuScreen.SetActive(true);

    }

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
        MainMenuScreen.SetActive(false);

    }
    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
        MainMenuScreen.SetActive(true);

    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(2);
        PhotonNetwork.LoadLevel("Main Menu");
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void LoadingScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
    public void TurnToLobbyWhileConnected()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Similation1()
    {
        PhotonNetwork.LoadLevel("Level1");
    }
    public void Similation2()
    {
        PhotonNetwork.LoadLevel("Level2");
    }
 
    public void TutorialSCene()
    {
        PhotonNetwork.LoadLevel("Tutorial");
    }
    public void Singleplayer()
    {
        SceneManager.LoadScene("Level Selection");
    }
    public void SingleplayerLevel1()
    {
        SceneManager.LoadScene("Singleplayer_Level1");
    }
    public void SingleplayerLevel2()
    {
        SceneManager.LoadScene("SingleplayerLevel2");

    }

    

}
