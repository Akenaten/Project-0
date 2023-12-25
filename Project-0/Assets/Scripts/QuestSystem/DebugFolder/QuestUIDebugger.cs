using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUIDebugger : MonoBehaviour
{
    public TextMeshProUGUI QuestName;
    public TextMeshProUGUI QuestDesc;
    public TextMeshProUGUI QuestState;
    
    
    public void setName(Dictionary<string,string> book){
        QuestName.text = book["name"];
        QuestDesc.text = book["desc"];
        QuestState.text = book["state"];
    }


}
