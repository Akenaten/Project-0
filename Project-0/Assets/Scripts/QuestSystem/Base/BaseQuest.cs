using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuest {
    [SerializeField] protected int ID;
    [SerializeField] protected string Status;
    [SerializeField] protected string Name;
    [SerializeField] protected string Desc;
    [SerializeField] protected string QuestType;

    public abstract void onQuestStart(int value);

    public abstract void onQuestUpdate(int value);

    

    public abstract void onQuestCompletion(int value);
}
