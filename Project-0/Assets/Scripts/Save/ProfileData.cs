using UnityEngine;

// Add here any data that should be saved, also add in SaveData.cs
public class ProfileData : MonoBehaviour
{
    public float timer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveProfile();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            LoadProfile();
        }

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
