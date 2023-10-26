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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
  
   

        
        SceneManager.LoadScene(1);
    }

    public void changeResolution(int resolution)
    {
        switch (resolution)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
