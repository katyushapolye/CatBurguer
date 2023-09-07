using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float gravity;


    [SerializeField]
    [Range(0.0f,10.0f)]
    private float jumpHeight;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float movementSensitivity;


    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float movementFalloff;

    [SerializeField]
    private float maxSpeed;



    private Rigidbody2D rigidbody2D;



    private Vector3 speedAxis = Vector3.zero;

    float xInput = 0;
    void Start()
    {

        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleJump();


        this.rigidbody2D.velocity = speedAxis;


    }


    void handleMovement()
    {
        //Some interpolation 4 good mesure
        //target speed

        //raw input for us to do the smoothing later
        xInput = Input.GetAxisRaw("Horizontal");

        //float smoothValue = Mathf.SmoothStep(-maxSpeed, maxSpeed, xInput);


        speedAxis.x = xInput*movementSensitivity;
        speedAxis.y = rigidbody2D.velocity.y;

        this.rigidbody2D.velocity = speedAxis;

    }


    void handleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            speedAxis.y = jumpHeight;
            return;
               
        } 
    }





}
