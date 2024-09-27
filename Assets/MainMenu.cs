using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string SceneName;
    
    //public GameObject Level1;
    //public GameObject Level2;
    //public void OpenSelectLevel()
    //{
    //    //Load Ad
    //    Ads.Instance.ShowInterstitialExec();
    //    Level1.SetActive(false);
    //    Level2.SetActive(true);
    //}

    public void Play()
    {
        //Load Ad
        Advertisements.Instance.ShowInterstitial();
        GlobalGameManager.Instance.Call();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }


}
