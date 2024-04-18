using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Page", menuName = "Page")]
public class ManualPages : ScriptableObject

{

    [TextArea(0,10)]
    public  string text;
}
