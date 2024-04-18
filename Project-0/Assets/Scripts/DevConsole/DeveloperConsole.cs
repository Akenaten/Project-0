using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using Unity.VisualScripting;
using System.Linq;
using System;

public class DeveloperConsole : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject backgroundImage;
    public GameObject InputFieldObject;
    public GameObject textBox;
    [SerializeField] private TMP_Text textbox;
    private bool isTextBoxActive;
    private bool isQuestBoxActive = true;
    public List<ManualPages> pages = new List<ManualPages>();
    private Dictionary<string, ManualPages> Pages = new Dictionary<string, ManualPages>();
    private bool currentState = true;
    private QuestBoxDebug questBoxScript;

    //Game Object related variables
    [SerializeField] private QuestManager quest_manager;
    [SerializeField] private GameObject quest_box;
    [SerializeField] private GameObject quest_box_fields;

    // History variables
    public List<string> history = new List<string>();
    public int history_cursor = 0;

    private void verifyReferences()
    {

        List<GameObject> references = new List<GameObject>() { InputFieldObject, backgroundImage, textBox, quest_box, quest_box_fields };

        foreach (GameObject element in references)
        {
            if (element == null)
            {
                Debug.Log($"Element {element.name} is found to be null");
            }
        }


        string textbox_report = textbox ? "textbox variable appears to be assigned." : "textbox variable appears to be null";
        

        questBoxScript = quest_box.GetComponent<QuestBoxDebug>();
        string questBoxScript_report = questBoxScript ? "questBoxScript variable appears to be assigned." : "questBoxScript variable appears to be null";
        

        quest_manager = GameObject.FindAnyObjectByType<QuestManager>();
        string quest_manager_report = questBoxScript ? "quest manager variable appears to be assigned." : "quest manager variable appears to be null";
        

        List<string> report_Book = new List<string>(){textbox_report, questBoxScript_report, quest_manager_report};
        
        foreach(string Entry in report_Book){
            if(Entry.Contains("null")){
                Debug.Log(Entry);
            }
        }
    }
    private void setupManual()
    {

        foreach (ManualPages page in pages)
        {
            Pages[page.name] = page;
        }
    }

    private void loopHistory()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            history_cursor -= 1;


        }
        else
        {
            history_cursor += 1;

        }

        if (history_cursor >= history.Count)
        {
            history_cursor -= 1;
        }
        else if (history_cursor < 0)
        {
            history_cursor = 0;
        }

        inputField.text = history[history_cursor];
    }

    void Start()
    {



        verifyReferences();
        setupManual();
        toggleConsole();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            toggleConsole();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            toggleTextWindows();

        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow)) & currentState)
        {
            loopHistory();
        }
    }

    private void toggleTextWindows()
    {

        if (isTextBoxActive)
        {
            isTextBoxActive = false;
            textbox.text = " ";
        }

        if (isQuestBoxActive)
        {
            isQuestBoxActive = false;
            quest_box.SetActive(false);
            quest_box_fields.SetActive(false);
        }
    }

    private void toggleConsole()
    {

        currentState = !currentState;
        backgroundImage.SetActive(currentState);
        InputFieldObject.SetActive(currentState);
        textBox.SetActive(currentState);
        quest_box.SetActive(false);


    }



    public void Command()
    {

        string command = inputField.text;
        history.Add(command);
        history_cursor = history.Count;
        List<string> arguments = command.Split(" ").ToList();


        switch (arguments[0])
        {

            case "man":
                if (arguments.Count == 1)
                {
                    textbox.text = Pages["man"].text;
                    isTextBoxActive = true;
                }
                else if (arguments.Count == 2 && Pages.ContainsKey(arguments[1]))
                {
                    textbox.text = Pages[arguments[1]].text;
                    isTextBoxActive = true;
                }

                break;


            case "ask":
                textbox.text = pages[0].text;
                isTextBoxActive = true;
                break;



            case "quests":
                if (arguments.Count == 1)
                {
                    Debug.Log("Quests called with no arguments");
                }
                else if (arguments.Count == 2)
                {
                    bool flag = arguments[1].Contains("-") ? true : false;
                    bool isquestID = int.TryParse(arguments[1], out _);

                    if (flag)
                    {
                        switch (arguments[1])
                        {

                            case "-sa":
                                //Debug.Log("Show all quests");
                                List<List<string>> data = quest_manager.presentAllQuests();
                                foreach (var entry in data)
                                {
                                    string info = "";
                                    foreach (var piece in entry)
                                    {
                                        info += piece.PadRight(20);
                                    }
                                    //Debug.Log(info);
                                    textbox.text = info;
                                }


                                break;

                            case "-sq":
                                // Debug.Log("Show a specific quest");
                                break;
                                //OBSOLETE SINCE THE ELSE IF BELOW HANDLES IT
                        }
                    }



                    else if (isquestID)
                    {
                        // Debug.Log($"Requested info on quest by ID of {arguments[1]}");
                        quest_box.SetActive(true);
                        quest_box_fields.SetActive(true);
                        isQuestBoxActive = true;
                        questBoxScript.writeData(quest_manager.fetchQuestDetails(arguments[1]));
                    }


                }

                else if (arguments.Count == 3)
                {
                    List<string> args = new List<string>() {arguments[1], arguments[2]};
                    int QID = 0;
                    string flag = "";
                    foreach (string arg in args)
                    {


                         if (arg.Contains("-"))
                        {
                            flag = arg;
                        }

                        bool isquestID = int.TryParse(arg, out _);
                        if(isquestID){
                            QID = int.Parse(arg);
                        }
                    }

                    switch (flag)
                    {
                        case "-s":
                            quest_manager.invokeQuestStart(QID, DebugMode: true);
                            questBoxScript.writeData(quest_manager.fetchQuestDetails(QID.ToString()));
                            break;

                        case "-p":
                            // Debug.Log("Progress selected quest");
                            //PENDING FUNCTIONALITY
                            break;

                        case "-c":                           
                            quest_manager.invokeQuestComplete(QID);
                            questBoxScript.writeData(quest_manager.fetchQuestDetails(QID.ToString()));
                            break;

                        case "-r": 
                            quest_manager.invokeSetQuestState(QID, "Pending" ,DebugMode: true);
                            questBoxScript.writeData(quest_manager.fetchQuestDetails(QID.ToString()));
                            break;
                    }

                }
                break;

            default:
                Debug.Log("Unknown command");
                break;
        }

        inputField.text = "";
    }






}
