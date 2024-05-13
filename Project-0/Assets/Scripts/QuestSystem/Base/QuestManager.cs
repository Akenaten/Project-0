using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class QuestManager : MonoBehaviour
{
    public List<QuestScriptableObject> SOQ = new List<QuestScriptableObject>();
    public List<Quest> Quests = new List<Quest>();
    public bool questsCompiled = false; // Is used to determine wether an agent / goal will ask to retrieve their quest. To avoid potential asking before a quest has been made.


    [Header("Debug Panel")]
    public QuestUIDebugger Debugger;
    public int iota = 0;

    // The text area displaying the debug buttons.
    public GameObject HelpText;

    // SetQuestID is used to manually select the ID of the desired quest and test it's behavior upon using any of the debug function keys (S,U,E)
    public int SetQuestID = 0;
    public string SetAgentName;
    

    public Action<int> QuestStartEvents;
    public Action<int, string> QuestEvents;
    public Action<int> QuestEndEvents;
    public Action<int, string> QuestStateSet;


    void Start()
    {
        //CREATE QUEST OBJECTS AND STORE THEM IN A LIST FROM THEIR RESPECTIVE SCRIPTABLE OBJECT QUESTS.
        foreach (var scriptableQuest in SOQ)
        {
            Quest quest = new Quest(scriptableQuest, this);
            Quests.Add(quest);
        }
        questsCompiled = true;

    }

    //DEBUGGING FUNCTIONALITY
    #region DEBUG FUNCTIONS
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CycleQuest();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            invokeQuestStart(SetQuestID);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            invokeEvents(SetQuestID);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            invokeQuestComplete(SetQuestID);
        }

        // if (Input.GetKeyDown(KeyCode.BackQuote))
        // {
        //     HelpText.SetActive(HelpText.activeSelf ? false : true);
        // }
    }

    public List<List<string>> presentAllQuests(){

        List<List<string>> questdata = new();

        foreach(Quest quest in Quests){
            questdata.Add(quest.returnCondensedInfo());
        }

        return questdata;
    }

    public Dictionary<string,string> fetchQuestDetails(string questID){
        Debug.Log("FetchQuestDetails has been called");
        foreach(Quest quest in Quests){
            if (quest.getQuestID().ToString() == questID){
                return quest.getDets();
            }
            else {
                Debug.Log("No quest found with the provided criteria");
            }
            
        }

        return null;
    }
    #endregion

    //FUNCTION THAT IS CALLED FROM QUEST AGENTS IN ORDER TO RETRIEVE THE QUEST ID TIED TO THEM.
    public int FindMyQuest(string AgentName)
    {
        foreach (Quest quest in Quests)
        {
            // AgentName == quest.getAgentName()
            if (quest.isAgentInQuest(AgentName ) || AgentName == quest.getQuestGoal())
            {
                return quest.getQuestID();
            }

        }
        return 0;
    }





    [ContextMenu("Cycle Quest")]
    public void CycleQuest()
    {

        iota++;
        iota = Quests.Count == iota ? 0 : iota;
        updateDetails();
    }

    public void updateDetails()
    {
        if(Debugger){
            Debugger.setName(Quests[iota].getDets());

        }

    }

    public void invokeQuestStart(int QUEST_ID, string AgentName = null, bool DebugMode = false)
    {
        if(DebugMode){
            QuestStartEvents?.Invoke(QUEST_ID);
        }

        else if (QUEST_ID == 0)
        {
            if (AgentName != null)
            {
                Debug.Log($"Agent {AgentName} has not being assigned a Quest ID. Their current Quest ID is 0.");
            }
            else {
                Debug.Log("An Agent has not been assigned a proper Quest ID (non zero). Agent unknown.");
            }
        }
        else
        {
            QuestStartEvents?.Invoke(QUEST_ID);

        }
    }
    public void invokeEvents(int number, string trigger_name = null)
    {
        // Debug.Log($"Quest Progression has been called by {trigger_name}");
        QuestEvents?.Invoke(number, trigger_name);
    }

    public void invokeQuestComplete(int QUEST_ID)
    {
        QuestEndEvents?.Invoke(QUEST_ID);
    }

    public void invokeSetQuestState(int QUEST_ID, string State, bool DebugMode = false){
        QuestStateSet?.Invoke(QUEST_ID, State);

    }



}
