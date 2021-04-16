using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float cycles = Time.time / period; // Continually grows over time

        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        // Recaclculated to go from 0 to 1 so the
        // starting point isn't the mid point of movement
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
