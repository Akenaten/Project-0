using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform player;
    private ControllerNode playerMovement;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 2, -10);
    public float lookAheadDistance = 3f;
    private Vector3 currentVelocity = Vector3.zero;



	// Use this for initialization
	void Start () {
        playerMovement = player.GetComponent<ControllerNode>();
	}
	
    void FixedUpdate()
    {
        // Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // transform.position = smoothedPosition;

         // Calculate the target position of the camera
            Vector3 targetPosition = player.position + offset;

            // Calculate lookahead position based on player's orientation
            // float playerDirection = player.localScale.x;
            float lookaheadX = lookAheadDistance * playerMovement.playerDirection;
            Vector3 lookaheadPosition = new Vector3(targetPosition.x + lookaheadX, targetPosition.y, targetPosition.z);

            // Smoothly interpolate between the current camera position and the target position
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, lookaheadPosition, ref currentVelocity, smoothSpeed, Mathf.Infinity, Time.fixedDeltaTime);

            // Update the camera position
            transform.position = smoothedPosition;
    }
}
