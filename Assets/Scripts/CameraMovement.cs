using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraDistance = 0;
    public float cameraSpeed = 1f; // New variable to control the speed of the camera movement
    private Transform playerTransform;
    private Vector3 previousPosition = Vector3.zero; // Initialize to Vector3.zero

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player"); // Get player GameObject once
        playerTransform = player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (previousPosition == Vector3.zero) // Check against Vector3.zero
        {
            previousPosition = playerTransform.position;
        }

        var direction = (playerTransform.position - previousPosition) * cameraDistance;
        direction.Normalize();
        previousPosition = playerTransform.position;

        var targetLocation = playerTransform.position + new Vector3(direction.x, direction.y, -10f);
        float step = cameraSpeed * Time.deltaTime; // Use delta time and a speed variable for Lerp
        transform.position = Vector3.Lerp(transform.position, targetLocation, step);
    }
}
