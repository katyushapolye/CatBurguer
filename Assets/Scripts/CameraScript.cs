using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // The target object the camera will follow
    public float smoothSpeed = 0.125f; // The smoothness of camera movement

    public float smoothTime = 0.3f; // The smooth time for camera movement
    public Vector3 offset; // Offset from the target's position

    private Vector3 velocity = Vector3.zero;

    public float levelLeftEdge = 0;

    public float levelRightEdge = 0;

    void LateUpdate()
    {
        if (target == null)
        {
            // If the target is null, there's nothing to follow, so return.
            return;
        }

        // Calculate the desired position with an offset

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        
        if(desiredPosition.x < levelLeftEdge){
            desiredPosition.x = levelLeftEdge;
        }

        if(desiredPosition.x > levelRightEdge){
            desiredPosition.x = levelRightEdge;
        }
        

        // Use SmoothDamp to smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
