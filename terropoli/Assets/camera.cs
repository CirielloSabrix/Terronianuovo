using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class camera : MonoBehaviour
{

    
    public GameObject car;


    
    
    NavMeshAgent agentCar;
  
   

    public Camera cameraPrincipale;
    public Camera povCar;
 

    private Dictionary<GameObject, (NavMeshAgent agent, Camera povCamera)> carMap;

    void Start()
    {
        // Inizializzo i NavMeshAgent dai veicoli
        
     
        agentCar = car.GetComponent<NavMeshAgent>();

        // Mappa dei veicoli con il relativo NavMeshAgent e telecamera POV
        carMap = new Dictionary<GameObject, (NavMeshAgent, Camera)>()
        {
           
            {car, (agentCar, povCar) },
          
        
        };
    }

    void Update()
    {
        // Se clicca con il pulsante sinistro del mouse
        if (Input.GetMouseButtonDown(0))
        {
            HandleCarSelection();
        }

        // Ripristina la visuale principale quando si preme ESC
        if (Input.GetKey(KeyCode.Escape))
        {
            ResetToMainCamera();
        }
    }

    // Funzione per gestire la selezione e il passaggio alla telecamera dell'auto cliccata
    void HandleCarSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            // Cerca se l'oggetto cliccato è nella mappa delle macchine
            if (carMap.ContainsKey(hit.collider.gameObject))
            {
                (NavMeshAgent agent, Camera povCamera) = carMap[hit.collider.gameObject];
                SwitchToPOVCamera(povCamera, agent);
            }
        }
    }

    // Attiva la telecamera della macchina selezionata e disattiva il NavMeshAgent
    void SwitchToPOVCamera(Camera povCamera, NavMeshAgent agent)
    {
        // Disattiva la telecamera principale
        cameraPrincipale.enabled = false;

        // Disattiva gli agenti e le altre telecamere POV
        foreach (var car in carMap.Values)
        {
            car.agent.enabled = false;
            car.povCamera.enabled = false;
        }

        // Attiva la telecamera e disabilita l'agente della macchina selezionata
        povCamera.enabled = true;
        agent.enabled = false;
    }

    // Ripristina la telecamera principale e riattiva gli agenti delle macchine
    void ResetToMainCamera()
    {
        cameraPrincipale.enabled = true;

        // Disattiva tutte le telecamere POV e riattiva gli agenti
        foreach (var car in carMap.Values)
        {
            car.povCamera.enabled = false;
            car.agent.enabled = true;
        }
    }
}