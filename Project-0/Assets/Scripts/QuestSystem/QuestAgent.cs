using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAgent : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] int questID;

    private void startQuest(){
        questManager.invokeQuestStart(questID);
    }

    public void assignQuestID(int id){
        questID = id;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.S)){
            Debug.Log("Agent is starting quest...");
            startQuest();
        }
    }


}
