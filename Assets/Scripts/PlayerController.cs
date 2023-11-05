using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float gravity;


    [SerializeField]
    [Range(0.0f, 50.0f)]
    private float jumpHeight;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float movementSensitivity;


    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float movementFalloff;

    [SerializeField]
    private float maxSpeed;


    [SerializeField]
    private Animator animator;
    private Rigidbody2D rigidbody2D;



    private bool grounded;
    public int maxJumps = 2; // Set the maximum number of jumps here.

    private int remainingJumps= 1;
    private GameDirector gameDirector;



    private Vector3 speedAxis = Vector3.zero;

    float xInput = 0;
    void Start()
    {


        gameDirector = FindFirstObjectByType<GameDirector>();

        rigidbody2D = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //jumpCooldown += Time.deltaTime;


        handleMovement();
        handleJump();



        this.rigidbody2D.velocity = speedAxis;
        animator.SetInteger("Movement", (int)this.rigidbody2D.velocity.x);


    }

    public Vector3 getSpeed()
    {
        return this.rigidbody2D.velocity;
    }


    void handleMovement()
    {
        //Some interpolation 4 good mesure
        //target speed

        //raw input for us to do the smoothing later
        xInput = Input.GetAxisRaw("Horizontal");


        speedAxis.x = xInput * movementSensitivity;
        speedAxis.y = rigidbody2D.velocity.y;

        this.rigidbody2D.velocity = speedAxis;

    }


    void handleJump()
    {
        //Add ground check and double jmp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if((grounded == true || remainingJumps>0)){
            FindObjectOfType<AudioHandler>().Play("jump");
            //jumpCooldown = 0;
            //Debug.Log("Jump");
            speedAxis.y = jumpHeight;
            animator.SetTrigger("Jump");
            
            if(!grounded){
                remainingJumps--;
            }
            return;
            }
            

        }
    }

    public void bumpUp()
    {
        speedAxis.y = 10;
        this.rigidbody2D.velocity = speedAxis;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Debug.Log("Got killed by Bullet");
            gameDirector.murderPlayer();
            
        }
        if(collision.tag == "Ground")
        {
            Debug.Log("Grounded");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            remainingJumps=1;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }

    }






}
