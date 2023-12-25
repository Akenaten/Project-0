using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Quest : BaseQuest
{
    private QuestManager MANAGER;

    public Quest(QuestScriptableObject QSO, QuestManager qman){
        this.MANAGER = qman;
        this.ID = QSO.QuestID;
        this.Name = QSO.QuestName;
        this.Desc = QSO.Description;
        this.Status = "Pending";

        this.MANAGER.QuestStartEvents += onQuestStart;
        this.MANAGER.QuestEvents += onQuestUpdate;
        this.MANAGER.QuestEndEvents += onQuestCompletion;
        
        switch(QSO.questType){
            case QuestScriptableObject.QuestType.Talk:
            this.QuestType = "Talk";
            break;
            case QuestScriptableObject.QuestType.Travel:
            this.QuestType = "Travel";
            setupTravel(QSO.questGoalName, QSO.questAgentName);
            break;
            default:
            this.QuestType = "Undefined";
            break;
        }

    }



    public void setupTravel(string goalName, string questAgentName){
        GameObject goal = GameObject.Find(goalName);
        GameObject agent = GameObject.Find(questAgentName);
        IQuestObject goalScript = goal.GetComponent<IQuestObject>();
        goalScript.onQuestAssigned(ID);
        agent.GetComponent<QuestAgent>().assignQuestID(this.ID);
    }


    public override void onQuestStart(int quest_id){
        if(this.ID == quest_id && this.Status != "Active"){
            this.Status = "Active";
            MANAGER.updateDetails();
        }
    }

    public override void onQuestUpdate(int quest_id){
        if(quest_id == this.ID && this.Status != "Pending"){
            Debug.Log($"Quest of name: {this.Name} has been progressed!");
            MANAGER.updateDetails();
        }

    }

    public override void onQuestCompletion(int quest_id){
        this.Desc = "Quest Completed";
        this.Status = "Completed";
        MANAGER.updateDetails();
    }




//PASS REQUIRED INFORMATION TO THE UI DEBUGGER
    public Dictionary<string,string> getDets(){
        Dictionary<string,string> book = new Dictionary<string,string>();
        book["name"] = this.Name;
        book["desc"] = this.Desc;
        book["state"] = this.Status;
        return book;
    }
}
