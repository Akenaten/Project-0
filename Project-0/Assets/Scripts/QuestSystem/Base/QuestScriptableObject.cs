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
    public QuestType questType;
    [Header("Travel Type Parameters")]
    public string questAgentName;
    public string questGoalName;

}
