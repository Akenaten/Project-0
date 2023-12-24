using UnityEngine;

// Add here any data that should be saved, also add in SaveData.cs
public class ProfileData : MonoBehaviour
{
    public float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void SaveProfile ()
    {
        SaveSystem.SaveProfile(this);
    }

    public void LoadProfile ()
    {
        SaveData data = SaveSystem.LoadProfile();
        timer = data.timer;
    }
}
