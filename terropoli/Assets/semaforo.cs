using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semaforo : MonoBehaviour
{

    public GameObject Luce;

    public Transform Posverde;
    public Transform Posgiallo;
    public Transform Posrosso;

    private bool verde;
    private bool giallo;
    private bool rosso;

    private void Start()
    {
        verde = true;

    }

    void Update()
    {
        if (verde)
        {
            Luce.transform.position = Posverde.position;
            Luce.GetComponent<Light>().color = Color.green;
            StartCoroutine(Luceverde());
            rosso = false;
        }
        if (giallo)
        {
            Luce.transform.position = Posgiallo.position;
            Luce.GetComponent<Light>().color = Color.yellow;
            StartCoroutine(Lucegialla());
            verde = false;

        }
        if (rosso)
        {
            Luce.transform.position = Posrosso.position;
            Luce.GetComponent<Light>().color = Color.red;
            StartCoroutine(Lucerossa());
            giallo = false;

        }
    }
    IEnumerator Luceverde()
    {
        yield return new WaitForSeconds(10);
        giallo = true;
    }
    IEnumerator Lucegialla()
    {
        yield return new WaitForSeconds(4);
        rosso = true;
    }
    IEnumerator Lucerossa()
    {
        yield return new WaitForSeconds(10);
        verde = true;
    }
}





