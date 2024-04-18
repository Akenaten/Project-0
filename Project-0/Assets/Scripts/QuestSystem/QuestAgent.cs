using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAgent : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] int questID;
    public BoxCollider2D InteractionBox;
    private bool withinInteractRange = false;

    private void Start() {
        StartCoroutine("GetQuestID");
    }


    
    private void getAssociatedQuestID(){
        questID = questManager.FindMyQuest(this.gameObject.name);
        if(questID != 0){
            Debug.Log($"Quest has been found!");
        } else {
            Debug.Log($"No quest found for this agent.");
        }
    }


    // Starts the quest associated with this agent.
    private void startQuest(){
        questManager.invokeQuestStart(questID, this.gameObject.name);
    }

    public void assignQuestID(int id){
        questID = id;
    }

    private void Update() {

        //DEBUG OPTION. 
        if(Input.GetKeyDown(KeyCode.F)){
            getAssociatedQuestID();
        }

        if(Input.GetKeyDown(KeyCode.I)){
            if (this.gameObject.name == questManager.SetAgentName){
                startQuest();
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            withinInteractRange = true;
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            withinInteractRange = false;
        }
    }

    public void Interact(){
        if(withinInteractRange){
            Debug.Log($"Player interacted with Agent {this.gameObject.name}");
            Debug.Log("Now starting quest:...");
            startQuest();
        }
    }

    IEnumerator GetQuestID(){
        Debug.Log("Coroutine started.");
        while(!questManager.questsCompiled){
            yield return new WaitForSeconds(1);
        }
        getAssociatedQuestID();
    }


}
