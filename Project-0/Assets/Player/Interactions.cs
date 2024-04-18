using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PLAIN TESTING SCRIPT TO STIMULATE INTERACTIONS. SHOULD BE INCOROPORATED INTO A DIFFERENT SCRIPT.
public class Interactions : MonoBehaviour
{
    private QuestAgent agent;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Trigger box detected.");
        if(other.GetComponent<QuestAgent>() != null){
            agent = other.GetComponent<QuestAgent>();
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            if(agent){
                agent.Interact();
            }
        }
    }
}
