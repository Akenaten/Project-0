using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAgent : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] int questID;

    private void getAssociatedQuestID(){
        questID = questManager.FindMyQuest(this.gameObject.name);
        if(questID != 0){
            Debug.Log($"Quest has been found!");
        } else {
            Debug.Log($"No quest found for this agent.");
        }
    }
    private void startQuest(){
        questManager.invokeQuestStart(questID);
    }

    public void assignQuestID(int id){
        questID = id;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            getAssociatedQuestID();
        }
    }


}
