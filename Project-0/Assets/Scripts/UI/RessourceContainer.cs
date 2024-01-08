using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceContainer : MonoBehaviour
{

    public Sprite   fullContainer, emptyContainer;
    private Image containerImage;

    private void Awake()
    {
        containerImage = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContainerImage(bool status)
    {
        if  (status == true)
            containerImage.sprite = fullContainer;
        else
            containerImage.sprite = emptyContainer;
    }
}
