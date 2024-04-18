using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBoxDebug : MonoBehaviour
{
    public List<TMP_Text> textfields;
    void Start()
    {
        foreach (var item in textfields)
        {
            Debug.Log(item.name);
        }
    }


    void Update()
    {

    }

    public void writeData(Dictionary<string, string> book = null)
    {
        if (book != null)
        {
            foreach (var field in textfields)
            {
                switch (field.name)
                {
                    case "ID":
                        field.text = book["ID"];
                        break;

                    case "Title":
                        field.text = book["name"];
                        break;

                    case "Status":
                        field.text = book["state"];
                        break;

                    case "Description":
                        field.text = book["desc"];
                        break;

                    default:
                        break;
                }
            }


        } else {
            Debug.Log("QuestBoxDebug's writeData function has been called without a Quest's dictionary");
        }

    }
}
