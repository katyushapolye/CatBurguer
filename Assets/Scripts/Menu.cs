using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
 
{
    [SerializeField] GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        FindObjectOfType<AudioHandler>().Play("bgm");

        
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
