using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GuidaAutomatica : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 150f;
    private bool isTurning = false;
    private Quaternion targetRotation; 
    int dove = 2;
    int tagg = 0;
    int i = 1;

    void Update()
    {
        if (!isTurning)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tutto"))
        {
            dove = Random.Range(0, 3);
        }
        else if (other.CompareTag("destra"))
        {
            dove = 1;
        }
        else if (other.CompareTag("sinistra"))
        {
            dove = 1;
        }
        else if (other.CompareTag("dxsx"))
        {
            dove = 1;
        }
        else if (other.CompareTag("sxdir"))
        {
            dove = Random.Range(0, 2);
        }
        else if (other.CompareTag("dxdir"))
        {
            dove = Random.Range(0, 2);
        }
        if (i == 1 && dove == 1)
        {
            i++;
            if (other.CompareTag("curva1"))
            {
                tagg = 1;
            }
            else if (other.CompareTag("curva2"))
            {
                tagg = 2;
            }
            else if (other.CompareTag("curva3"))
            {
                tagg = 3;
            }
            else
            {
                tagg = 4;
            }
        }

        if (other.CompareTag("Apposto") && dove == 1)
        {
                if (transform.rotation.eulerAngles.y > 270 && transform.rotation.eulerAngles.y < 330)
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                else if (transform.rotation.eulerAngles.y < 90)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (transform.rotation.eulerAngles.y < 140)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (transform.rotation.eulerAngles.y < 240 && transform.rotation.eulerAngles.y > 170)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            i = 1;
        }
        else if (other.CompareTag("Apposto") && dove == 2)
        {
            if (transform.rotation.eulerAngles.y > 260 && transform.rotation.eulerAngles.y < 280)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (transform.rotation.eulerAngles.y < 360 && transform.rotation.eulerAngles.y > 350)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.rotation.eulerAngles.y < 100)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (transform.rotation.eulerAngles.y > 170 && transform.rotation.eulerAngles.y < 190)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            i = 1;
        }

        if (dove == 0)
        {

        }
        else if(dove == 1)
        {
            if (other.CompareTag($"curva{tagg}"))
            {
                StartTurn(-22f);
            }
        }
        else if(dove == 2)
        {
            if (other.CompareTag($"curvadx1") || other.CompareTag($"curvadx2") || other.CompareTag($"curvadx3") || other.CompareTag($"curvadx4"))
            {
                StartTurn(-22f);
            }
        }
    }

    private void StartTurn(float angle)
    {
        if (!isTurning)
        {
            isTurning = true;
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
        }
    }
}
