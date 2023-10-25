using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class GameDirector : MonoBehaviour
{   //Singleton?

    //Scrolling vars
    Vector3 worldPos = Vector3.zero;
    float leftLimit = 9.5f;
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
    int score = 0;

    //Player vars

    [SerializeField]
    private GameObject PLAYER;

    [SerializeField]
    private GameObject ENEMY;
    [SerializeField]
    private GameObject ENEMY_PARENT;


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





        
    }

    private void scrollLevel()
    {

        this.worldPos.x = CAMERA_TARGET.position.x;
        this.worldPos.x += Time.deltaTime * scrollingSpeed;
        CAMERA_TARGET.position =  this.worldPos;
        CameraScript.levelRightEdge = this.worldPos.x + leftLimit;







    }

    private void initGame()
    {
        
            PLAYER.transform.position = new Vector3(-1.5f, 1, 0);
            ACTIVE_CHUNKS.Add(Instantiate(CHUNKS[0].chunkObject, new Vector3(0, 0, 0), Quaternion.identity,CHUNK_PARENT.transform));

            lastWidth = CHUNKS[0].width;

            lastPosition = ACTIVE_CHUNKS[0].transform.position;
    

      

    }

    private void proceduralLevelGen()
    {
        //Deletes out of bounds objects

        //Debug.Log("Active Chunks: " + ACTIVE_CHUNKS.Count);

        foreach(var obj in ACTIVE_CHUNKS)
        {                               //Offset
            if(obj.transform.position.x + 10 <= worldPos.x - leftLimit)
            {
                ACTIVE_CHUNKS.Remove(obj);
                Destroy(obj.gameObject);

                
            }


        }

        foreach (var obj in ENEMY_POOL)
        {                               //Offset
            if (obj.transform.position.x + 10 <= worldPos.x - leftLimit)
            {
                ENEMY_POOL.Remove(obj);
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

            int rand = Random.Range(0, CHUNKS.Count);
            int rand2 = Random.Range(-100, 101);

            Vector3 pos = lastPosition;
            pos.x += lastWidth/2 + CHUNKS[rand].width / 2;
            pos.y += (2*rand2)/100;

            ACTIVE_CHUNKS.Add(Instantiate(CHUNKS[rand].chunkObject, pos, Quaternion.identity, CHUNK_PARENT.transform));

            lastWidth = CHUNKS[rand].width;
            lastPosition = pos;

            //Roullet for enemy

            int enemyRnd = Random.Range(0, 101);
            if(enemyRnd < 80)
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
        scrollingSpeed = 0.0f;
        PLAYER.transform.position = new Vector3(-999, 999, 999); //hides the player;
        PLAYER.SetActive(false);
        
        //Debug.Log("Game ended, final score: " + score);
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
