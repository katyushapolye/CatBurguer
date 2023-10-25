using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{   //Singleton?

    //Scrolling vars
    Vector3 worldPos = Vector3.zero;
    float leftLimit = 9.5f;
    float downLimit = 10.0f;
    float scrollingSpeed = 1.0f;

    [SerializeField]
    private GameObject WORLD;
    [SerializeField]
    private GameObject CAMERA;
    private CameraScript CameraScript;
    [SerializeField]
    private Transform CAMERA_TARGET;


    //Control Vars

    bool isPlayerDead = false;
    int score = 0;

    //Player vars

    [SerializeField]
    private GameObject PLAYER;


    // Start is called before the first frame update
    void Start()
    {
        CameraScript = CAMERA.GetComponent<CameraScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

        checkPlayerDeath(); //checks for bounds and player death flag
        scrollLevel(); //scrools the camera
        proceduralLevelGen(); // Recycles assets, gen level,





        
    }

    private void scrollLevel()
    {

        this.worldPos.x = CAMERA_TARGET.position.x;
        this.worldPos.x += Time.deltaTime * scrollingSpeed;
        CAMERA_TARGET.position =  this.worldPos;
        CameraScript.levelRightEdge = this.worldPos.x + leftLimit;







    }

    private void proceduralLevelGen()
    {
        //handles everything

    }

    private void increaseDificulty()
    {
        //increase dificulty variables

    }


    public void increasePlayerScore(int amount)
    {
        //increase the player score, externaly or not
        score += amount;    

    }

    private void updateUI() {
        //update UI with current score and sshit
    }

    private void resetGame()
    {
        //reset everything

    }

    private void backToMenu()
    {
        //load menu scene

    }
    private void playerDeath()
    {
        //stop scrolling
        //ask player if continue
        //reset game or back to menu
        scrollingSpeed = 0;
        PLAYER.transform.position = new Vector3(-999, 999, 999); //hides the player;

        Debug.Log("Game ended, final score: " + score);
    }

    public void murderPlayer()
    {
        this.isPlayerDead = true;
    }

    


    private void checkPlayerDeath()
    {
 

        if(PLAYER.transform.position.x <= worldPos.x - leftLimit)
        {
            isPlayerDead = true;
            Debug.Log("Died by level bounds");   
        }

        if (PLAYER.transform.position.y <= worldPos.y -downLimit)
        {
            isPlayerDead = true;
            Debug.Log("Died by fall");
        }

        if (isPlayerDead)
        {
            playerDeath();

        }







    }
}
