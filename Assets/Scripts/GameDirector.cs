using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{   //Singleton?

    //Scrolling vars
    Vector3 worldPos = Vector3.zero;
    float leftLimit = 10.5f;
    float downLimit = 10.0f;
    float scrollingSpeed = 2.0f;

    [SerializeField]
    private GameObject WORLD;
    [SerializeField]
    private GameObject CAMERA;
    private CameraScript CameraScript;
    [SerializeField]
    private Transform CAMERA_TARGET;





    //Procedural Gen


    private List<GameObject> ENEMY_POOL = new List<GameObject>();
    [SerializeField]
    private List<Chunk> CHUNKS = new List<Chunk>();
    private List<GameObject> ACTIVE_CHUNKS =  new List<GameObject>();
    [SerializeField]
    GameObject CHUNK_PARENT;

    float lastWidth = 0;
    Vector3 lastPosition = new Vector3();


    //Control Vars

    bool isPlayerDead = false;
    float distance = 0;
    int score = 0;
    float dificultyFactor = 0.0f;
    float dificultyClock = 0.0f;

    //Player vars

    [SerializeField]
    private GameObject PLAYER;

    [SerializeField]
    private GameObject ENEMY;
    [SerializeField]
    private GameObject ENEMY_PARENT;

    //UI

    [SerializeField]
    private TextMeshProUGUI distanceText;
    [SerializeField]
    private TextMeshProUGUI scoreTextText;
    [SerializeField]
    private GameObject GameOverMenu;


    // Start is called before the first frame update
    void Start()
    {
        CameraScript = CAMERA.GetComponent<CameraScript>();
        initGame();
        
    }
   

    // Update is called once per frame
    void Update()
    {

        checkPlayerDeath(); //checks for bounds and player death flag
        scrollLevel(); //scrools the camera
        proceduralLevelGen(); // Recycles assets, gen level,

        increaseDificulty();
        updateUI();





        
    }

    private void scrollLevel()
    {

        this.worldPos.x = CAMERA_TARGET.position.x;
        this.worldPos.x += Time.deltaTime * scrollingSpeed;
        CAMERA_TARGET.position =  this.worldPos;
        CameraScript.levelRightEdge = this.worldPos.x + leftLimit;


        distance += Time.deltaTime * scrollingSpeed;







    }

    private void initGame()
    {
            isPlayerDead = false;
            PLAYER.SetActive(true);
            scrollingSpeed = 2.0f;
            worldPos = new Vector3(0, 0, 0);
            CAMERA_TARGET.position = this.worldPos;
            CAMERA.transform.position = new Vector3(CAMERA_TARGET.transform.position.x,CAMERA_TARGET.transform.position.y,CAMERA.transform.position.z);
            score = 0;
            distance = 0;
            dificultyClock = 0.0f;

            PLAYER.transform.position = new Vector3(-1.5f, 1, 0);
            ACTIVE_CHUNKS.Add(Instantiate(CHUNKS[0].chunkObject, new Vector3(0, 0, 0), Quaternion.identity,CHUNK_PARENT.transform));

            lastWidth = CHUNKS[0].width;

            lastPosition = ACTIVE_CHUNKS[0].transform.position;

            GameOverMenu.SetActive(false);
    

      

    }

    private void proceduralLevelGen()
    {
        //Deletes out of bounds objects

        //Debug.Log("Active Chunks: " + ACTIVE_CHUNKS.Count);
        if(ACTIVE_CHUNKS.Count == 0)
        {
            return;
        }

        for (int i = ACTIVE_CHUNKS.Count - 1; i >= 0; i--)
        {
            var obj = ACTIVE_CHUNKS[i];
            if (obj.transform.position.x + 10 <= worldPos.x - leftLimit)
            {
                ACTIVE_CHUNKS.RemoveAt(i);
                Destroy(obj.gameObject);
            }
        }
       

        for (int i = ENEMY_POOL.Count - 1; i >= 0; i--)
        {
            var obj = ENEMY_POOL[i];
            if (obj.transform.position.x + 10 <= worldPos.x - leftLimit)
            {
                ENEMY_POOL.RemoveAt(i);
                Destroy(obj.gameObject);
            }
        }

        //Generate new objects if they dont exist 
        bool needGen = true;

        while (needGen)
        {
           
            if (ACTIVE_CHUNKS[ACTIVE_CHUNKS.Count-1].transform.position.x >= worldPos.x + leftLimit)
            {
                needGen = false;
                break;
            }

            int rand = UnityEngine.Random.Range(0, CHUNKS.Count);
            int rand2 = UnityEngine.Random.Range(-100, 101);

            Vector3 pos = lastPosition;
            pos.x += lastWidth/2 + CHUNKS[rand].width / 2;
            pos.y += (1.25f*rand2)/100;

            ACTIVE_CHUNKS.Add(Instantiate(CHUNKS[rand].chunkObject, pos, Quaternion.identity, CHUNK_PARENT.transform));
            ACTIVE_CHUNKS[ACTIVE_CHUNKS.Count - 1].tag = "Ground";

            lastWidth = CHUNKS[rand].width;
            lastPosition = pos;

            //Roullet for enemy

            int enemyRnd = UnityEngine.Random.Range(0, 101);
            if(enemyRnd < 20 + (dificultyFactor/100))
            {
                Vector3 ePos = CHUNKS[rand].enemyPosition;
                ePos.x += ACTIVE_CHUNKS[ACTIVE_CHUNKS.Count - 1].transform.position.x;
                ePos.y += ACTIVE_CHUNKS[ACTIVE_CHUNKS.Count - 1].transform.position.y;

                ENEMY_POOL.Add(Instantiate(ENEMY, ePos, Quaternion.identity, ENEMY_PARENT.transform));
                ENEMY_POOL[ENEMY_POOL.Count - 1].GetComponentInChildren<SpiderEnemy>().setMovementRadius(2);

            }


        }
        
       
        //handles everything

    }

    private void increaseDificulty()
    {
        dificultyClock += Time.deltaTime;


        if (dificultyClock >= 5.0f)
        {
            dificultyFactor++;
            scrollingSpeed += 0.1f;
            Debug.Log("Increasing dificulty: " + scrollingSpeed);
            dificultyClock = 0;
        }
        


        
        
       

    }




    public void increasePlayerScore(int amount)
    {
        //increase the player score, externaly or not
        score += amount;    

    }

    private void updateUI() {
        //update UI with current score and shit
        distanceText.text = String.Format("Distance: {0:0.0}", distance);




    }

    

    public void resetGame()
    {

        for (int i = 0; i < ACTIVE_CHUNKS.Count; i++)
        {
            Destroy(ACTIVE_CHUNKS[i].gameObject);

        }
        ACTIVE_CHUNKS.Clear();

        for (int i = 0; i < ENEMY_POOL.Count; i++)
        {
            Destroy(ENEMY_POOL[i].gameObject);

        }
        ENEMY_POOL.Clear();


        initGame();
        //SceneManager.LoadScene(1);

    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);

    }
    private void playerDeath()
    {
        //stop scrolling
        //ask player if continue
        //reset game or back to menu
        scrollingSpeed = 0.0f;
        PLAYER.transform.position = new Vector3(-999, 999, 999); //hides the player;
        PLAYER.SetActive(false);
        

        GameOverMenu.SetActive(true);
        scoreTextText.text = String.Format("Pontuação Final: {0}", (int)(distance + score));


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
            //Debug.Log("Died by level bounds");   
        }

        if (PLAYER.transform.position.y <= worldPos.y -downLimit)
        {
            isPlayerDead = true;
            //Debug.Log("Died by fall");
        }

        if (isPlayerDead)
        {
            playerDeath();

        }







    }
}
