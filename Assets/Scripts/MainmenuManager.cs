using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    public GameObject inGameScreen, settingsScreen;
    public AudioMixer mainMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsButton()
    {
        inGameScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
    public void CloseButton()
    {
        inGameScreen.SetActive(true);
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
    public void QuitButton()
    {
        Application.Quit();
    }
}
