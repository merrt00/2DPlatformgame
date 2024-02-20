using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuManager : MonoBehaviour
{
    public GameObject inGameScreen, pauseScreen,settingsScreen;
    public AudioMixer mainMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        inGameScreen.SetActive(false);
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void ResumeButton()
    {
        Time .timeScale = 1f;
        pauseScreen.SetActive(false);
        inGameScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void ReplayButton()
    {
        Time.timeScale = 1f;    
        SceneManager.LoadScene(1);
    }
    public void ExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void SettingsButton()
    {
        Time.timeScale = 0f;
        inGameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
    public void CloseButton()
    {
        Time.timeScale = 1f;
        inGameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }


    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

}
