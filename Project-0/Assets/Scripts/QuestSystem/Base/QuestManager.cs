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

    public Action<int> QuestStartEvents;
    public Action<int> QuestEvents;
    public Action<int> QuestEndEvents;


    void Start()
    {
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

    


}
