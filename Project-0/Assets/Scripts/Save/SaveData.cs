using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float timer;

    public SaveData (ProfileData profileData)
    {
        timer = profileData.timer;
    }
}
