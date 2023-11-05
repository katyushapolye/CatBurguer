using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
 
{
    [SerializeField] GameObject optionsMenu;

    [SerializeField] 
    Image audioButtom;

    static public bool isAudioEnabled = true;


    
    public Sprite enabledSprite;
    
    public Sprite disabledSprite;




    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        if (isAudioEnabled)
        {
            audioButtom.sprite = enabledSprite;
            FindObjectOfType<AudioHandler>().Play("bgm");

            return;
        }
        audioButtom.sprite = disabledSprite;
        FindObjectOfType<AudioHandler>().StopAllAudio();


    }

    // Update is called once per frame
    

    public void StartGame()
    {
  
   

        
        SceneManager.LoadScene(1);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void ToggleAudio()
    {
        isAudioEnabled = !isAudioEnabled;
        if (isAudioEnabled)
        {
            audioButtom.sprite = enabledSprite;
            FindObjectOfType<AudioHandler>().Play("bgm");

            return;
        }
        audioButtom.sprite = disabledSprite;
        FindObjectOfType<AudioHandler>().StopAllAudio();


    }


    public void changeResolution(int resolution)
    {
        bool fullScreen = false; // Set to false for windowed mode
        switch (resolution)
        {
            case 0:
                Screen.SetResolution(1024, 576, fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, fullScreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, fullScreen);
                break;
            default:
                Debug.LogError("Invalid resolution index");
                break;
        }
    }
}
