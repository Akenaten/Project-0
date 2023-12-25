using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour, IQuestObject
{
    private int questID;
    [SerializeField] QuestManager questManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Quest object has notified questManager of event.");
            questManager.invokeEvents(questID);
            
        }
    }

    public void onQuestAssigned(int qID){
        Debug.Log("Quest ID has been assigned.");
        questID = qID;
    }

    public void onQuestCompletion(){

    }
}
