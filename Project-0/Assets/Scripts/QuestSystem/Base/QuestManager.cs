using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public List<QuestScriptableObject> SOQ = new List<QuestScriptableObject>();
    public List<Quest> Quests = new List<Quest>();

    [Header("Debug Panel")]
    public QuestUIDebugger Debugger;
    public int iota = 0;

    // SetQuestID is used to manually select the ID of the desired quest and test it's behavior upon using any of the debug function keys (S,U,E)
    public int SetQuestID = 0;

    public Action<int> QuestStartEvents;
    public Action<int> QuestEvents;
    public Action<int> QuestEndEvents;


    void Start()
    {
        //CREATE QUEST OBJECTS AND STORE THEM IN A LIST FROM THEIR RESPECTIVE SCRIPTABLE OBJECT QUESTS.
        foreach (var scriptableQuest in SOQ)
        {
           Quest quest = new Quest(scriptableQuest, this); 
           Quests.Add(quest);
        }
        
    }

    //DEBUGGING FUNCTIONALITY
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            CycleQuest();
        }

        if(Input.GetKeyDown(KeyCode.S)){
            invokeQuestStart(SetQuestID);
        }

        if(Input.GetKeyDown(KeyCode.U)){
            invokeEvents(SetQuestID);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            invokeQuestComplete(SetQuestID);
        }
    }



    public int FindMyQuest(string AgentName){
        foreach(Quest quest in Quests){
            if(AgentName == quest.getAgentName()){
                return quest.getQuestID();
            }

        }
        return 0;
    }





    [ContextMenu("Cycle Quest")]
    public void CycleQuest(){
        
        iota++;
        iota = Quests.Count == iota ? 0 : iota;
        updateDetails();
    }

    public void updateDetails(){
        Debugger.setName(Quests[iota].getDets());

    }

    public void invokeQuestStart(int QUEST_ID){
        QuestStartEvents?.Invoke(QUEST_ID);
    }
    public void invokeEvents(int number){
        QuestEvents?.Invoke(number);
    }

    public void invokeQuestComplete(int QUEST_ID){
        QuestEndEvents?.Invoke(QUEST_ID);
    }

    


}
