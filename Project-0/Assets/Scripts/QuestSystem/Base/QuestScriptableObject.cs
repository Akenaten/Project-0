using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]


public class QuestScriptableObject : ScriptableObject
{
    public enum QuestType {
        Talk,
        Travel
    };


    public int QuestID;
    public string QuestName;
    public string Description;
    public int XPReward;
    public int GoldReward;
    public List<QuestStage> stages = new List<QuestStage>();
    

    public QuestType questType;
    [Header("Travel Type Parameters")]
    public List<string> otherAgents;
    public string questGoalName;

}


