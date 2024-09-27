using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Slider GameVolumeSlider;
    public Slider CameraSensitivitySlider;
    public GameObject EndScreen;

   

    void Start()
    {
        //Load Ad
        Invoke("LoadAdLoop", 33f);
    }

    void LoadAdLoop()
    {
        //Load Ad
        Invoke("RepeatAdLoop", 33f);
    }

    void RepeatAdLoop()
    {
        Invoke("LoadAdLoop", 33f);
    }

    public void Update()
    {
        AudioListener.volume = GameVolumeSlider.value;
        PlayerController.Instance.MouseSpeed = CameraSensitivitySlider.value * 3f;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
