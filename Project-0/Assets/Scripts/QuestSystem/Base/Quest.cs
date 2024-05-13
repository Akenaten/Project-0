using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Quest : BaseQuest
{
    private QuestManager MANAGER;
    string AgentName = "none";
    string GoalName = "none";
    
    private List<QuestStage> stages;
    
    public int quest_stage_tracker = 0;
    private Dictionary<string, string> Rewards = new Dictionary<string, string>();

    private List<String> Agents = new List<String>();

    public Quest(QuestScriptableObject QSO, QuestManager qman)
    {
        this.MANAGER = qman;
        this.ID = QSO.QuestID;
        this.Name = QSO.QuestName;
        this.Desc = QSO.Description;
        this.Status = "Pending";
        this.Rewards["XP"] = QSO.XPReward.ToString();
        this.Rewards["Gold"] = QSO.GoldReward.ToString();
        this.stages = QSO.stages;
        setupStageIndexes();
        setupQuestAgents(QSO.otherAgents);

        this.MANAGER.QuestStartEvents += onQuestStart;
        this.MANAGER.QuestEvents += onQuestUpdate;
        this.MANAGER.QuestEndEvents += onQuestCompletion;
        this.MANAGER.QuestStateSet += onQuestStateSetting;

        switch (QSO.questType)
        {
            case QuestScriptableObject.QuestType.Talk:
                this.QuestType = "Talk";
                break;
            case QuestScriptableObject.QuestType.Travel:
                this.QuestType = "Travel";
                // AgentName = QSO.questAgentName;
                GoalName = QSO.questGoalName;
                // setupTravel(QSO.questGoalName, QSO.questAgentName);
                break;
            default:
                this.QuestType = "Undefined";
                break;
        }

    }

    private void setupStageIndexes(){

        for(int i = 1; i < stages.Count; i++){
            stages[i].stage_number = i;
        }


    }

    private void setupQuestAgents(List<string> AgentNames){
        foreach(QuestStage stage in stages){
            Agents.Add(stage.goal_name);
        }

        foreach(string entry in AgentNames){
            Agents.Add(entry);
        }
    }



    public void setupTravel(string goalName, string questAgentName)
    {
        GameObject goal = GameObject.Find(goalName);
        GameObject agent = GameObject.Find(questAgentName);
        IQuestObject goalScript = goal.GetComponent<IQuestObject>();
        goalScript.onQuestAssigned(ID);
        agent.GetComponent<QuestAgent>().assignQuestID(this.ID);
    }


    public override void onQuestStart(int quest_id)
    {
        Debug.Log($"This quest has {stages.Count} stages");

        if (this.ID == quest_id && this.Status != "Active" && this.Status != "Completed")
        {
            Debug.Log($"Function invoked through Quest Manager with a given ID of: {quest_id}");
            this.Status = "Active";
            MANAGER.updateDetails();
        }
    }

    public override void onQuestUpdate(int quest_id, string trigger_name = null)
    {
        if(this.Status != "Pending" && this.Status != "Completed"){

            if (quest_id == this.ID)
            {   
                if(quest_stage_tracker >= 0 && trigger_name == stages[quest_stage_tracker].goal_name){

                    Debug.Log($"Quest of name: {this.Name} has been progressed through interaction with {trigger_name}");
                    quest_stage_tracker = quest_stage_tracker == stages.Count-1 ? -1 : quest_stage_tracker + 1;
                    if(quest_stage_tracker < 0){
                        Debug.Log("Quest has been completed");
                    }
                    MANAGER.updateDetails();
                    
                }

            }
        }

    }

    public override void onQuestCompletion(int quest_id)
    {
        if (quest_id == this.ID && this.Status != "Completed" && this.Status == "Active")
        {
            Debug.Log("Quest completed.");
            //this.Desc = "Quest Completed";
            this.Status = "Completed";
            Debug.Log($"Player has gained {Rewards["XP"]} experience!");
            Debug.Log($"Player has earned {Rewards["Gold"]} gold!");
            MANAGER.updateDetails();
        }

    }

    public void onQuestStateSetting(int quest_id, string state){
        if(quest_id == this.ID){
            this.Status = state;
        }
    }

    //PROVIDE AGENT NAME AND QUEST ID
    public int getQuestID(){
        return this.ID;
    }

    public string getAgentName(){
        return this.AgentName;
    }

    public string getQuestGoal(){
        return this.GoalName;
    }

    public bool isAgentInQuest(string AgentName){
        if(Agents.Contains(AgentName)){
            return true;
        }

        return false;
    }




    //PASS REQUIRED INFORMATION TO THE UI DEBUGGER
    public Dictionary<string, string> getDets()
    {
        Dictionary<string, string> book = new Dictionary<string, string>();
        book["ID"] = this.ID.ToString();
        book["name"] = this.Name;
        book["desc"] = this.Desc;
        book["state"] = this.Status;
        return book;
    }

    public List<string> returnCondensedInfo(){
        List<string> info = new List<string>();
        info.Add(this.ID.ToString());
        info.Add(this.Name);
        info.Add(this.Status);

        return info;
    }
}
