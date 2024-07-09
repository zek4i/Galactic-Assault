using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [Tooltip("how fast the player ship moves")] 
    [SerializeField] float controlSpeed = 0;
    [Tooltip("how fast the player ship moves side to side on screen")]
    [SerializeField] float xRange = 12f;
    [Tooltip("how fast the player ship moves up and down on screen")]
    [SerializeField] float yRange = 10f;

    [Header("Laser Gun Array")]
    [SerializeField] GameObject[] lasers;


    [Header("Screen Position Based Tuning")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] float positionPitchfactor = -2f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Screen Input Based Tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float ControlRollFactor = -10f;
    [SerializeField] InputAction fire;
    float xThrow, yThrow; //not encapsulated can be used in any method

    void Start()
    {

    }
    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable(); //these are to makwe sure our movement is only working when we need is to 
        fire.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    void ProcessRotation()
    {
        float PitchDueToPosition = transform.localPosition.y * positionPitchfactor;
        float PitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = PitchDueToPosition + PitchDueToControlThrow; //current pos on y (to rotate)* pitch factor
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * ControlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //quaternion is used for rotation
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x; //float can take only 11 valur so we specify x cux it only takes x value then
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXpos = transform.localPosition.x + xOffset; //current position + the desired position
        float clammedXPos = Mathf.Clamp(newXpos, -xRange, xRange); //from -ve to +ve it helps to make ship stop instead of going off screen

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clammedYPos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clammedXPos, clammedYPos, transform.localPosition.z);
    }
    void ProcessFiring()
    {
        if (fire.IsPressed()) //or fire.IsPressed() --> both work
        {
            SetLasersActive(true); //passing parameter to make code more brief and clean
        }
        else
        {
            SetLasersActive(false);
        }
    }
    void SetLasersActive(bool isActive) //naming it anything like here "isActive"
    {
        foreach (GameObject laser in lasers) //goes through each of the things in the collection and the laser part can be named anything you weant
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission; //acessing emission and creating a reference variable
            emissionModule.enabled = isActive; //laser.setactive = true (instead of enabling the laser we are making a reference for emission in lasers)
        }
    }
    // void DeactivateLasers()
    // {
    //     foreach (GameObject laser in lasers) 
    //     {
    //         var emissionModule = laser.GetComponent<ParticleSystem>().emission;
    //         emissionModule.enabled = false;
    //     }
    // } instead of writing this we use bool


}
