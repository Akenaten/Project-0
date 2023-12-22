using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuTabButton : MonoBehaviour, IPointerClickHandler//, IPointerExitHandler, IPointerEnterHandler
{
    public TabGroup tabGroup;
    public Image background;
    public RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void OnPointerEnter(PointerEventData enventData)
    // {
    //     tabGroup.OnTabEnter(this);
    // }

    // public void OnPointerExit(PointerEventData enventData)
    // {
    //     tabGroup.OnTabEnter(this);
    // }

    public void OnPointerClick(PointerEventData enventData)
    {
        tabGroup.OnTabSelected(this);
    }
}
