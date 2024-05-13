using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour, IQuestObject
{
    private int questID;
    public BoxCollider2D interactionBox; //Manually attach a box collider component that will serve as the quest trigger for progression/completion.
    [SerializeField] QuestManager questManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            onQuestCompletion();
        }
    }

    public void onQuestAssigned(int qID){
        Debug.Log("Quest ID has been assigned.");
        questID = qID;
    }



    public void onQuestCompletion(){
        questManager.invokeQuestComplete(questManager.FindMyQuest(this.gameObject.name));

    }

    public void ProgressQuest(){
        questManager.invokeEvents(questManager.FindMyQuest(this.gameObject.name), this.name);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            ProgressQuest();
        }
    }
}
