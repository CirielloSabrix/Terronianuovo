using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sigiuda : MonoBehaviour
{
    public float speed = 10f;         // Velocità della macchina
    public float turnSpeed = 150f;     // Velocità di rotazione quando si gira (gradi per secondo)
    private bool isTurning = false;   // Flag per capire se la macchina sta girando
    private Quaternion targetRotation; // Rotazione verso cui la macchina deve puntare

    void Update()
    {
        // Movimento della macchina in avanti
        if (!isTurning)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // Rotazione della macchina
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Se ha completato la rotazione, smetti di girare
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
            }
        }
    }

    // Questo metodo verrà chiamato quando la macchina entra in un trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurnLeft"))
        {
            StartTurn(-90f); // Gira a sinistra
        }
        else if (other.CompareTag("TurnRight"))
        {
            StartTurn(45f);  // Gira a destra
        }
        else if (other.CompareTag("TurnBack"))
        {
            StartTurn(180f); // Fa inversione a U
        }
    }

    // Inizia la rotazione verso la direzione specificata
    private void StartTurn(float angle)
    {
        if (!isTurning)
        {
            isTurning = true;

            // Calcola la rotazione target in base alla direzione corrente
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
        }
    }
}

