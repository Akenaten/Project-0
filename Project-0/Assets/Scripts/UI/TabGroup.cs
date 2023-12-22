using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<MenuTabButton> tabButtons;
    private float height = 0;
    private float selectedHeight = 0;
    private float width = 0;
    private float selectedWidth = 0;
    public float sizeMultiplier = 1.5f;
    public float lerpDuration = 0.5f;
    public MenuTabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(MenuTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<MenuTabButton>();
        }
        tabButtons.Add(button);
        if (height == 0 || width == 0 || selectedHeight == 0 || selectedWidth == 0)
        {
            height = button.rt.rect.height;
            selectedHeight = button.rt.rect.height * sizeMultiplier;
            width = button.rt.rect.width;
            selectedWidth = button.rt.rect.width * sizeMultiplier;
        }
    }

    // public void OnTabEnter(TabButton button)
    // {
    //     ResetTabs();
    // }

    // public void OntabExit(TabButton button)
    // {
    //     ResetTabs();
    // }

    public void OnTabSelected(MenuTabButton button)
    {
        selectedTab = button;
        ResetTabs();
        StartCoroutine(UpdateHeightAndWidth(button, selectedHeight, selectedWidth));
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(MenuTabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue ;}
            StartCoroutine(UpdateHeightAndWidth(button, height, width));
        }
    }

    IEnumerator UpdateHeightAndWidth(MenuTabButton button, float targetHeight, float targetWidth)
    {
        float elapsedTime = 0f;
        float startHeight = button.rt.rect.height;
        float startWidth = button.rt.rect.width;

        while (elapsedTime < lerpDuration)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, elapsedTime / lerpDuration);
            float newWidth = Mathf.Lerp(startWidth, targetWidth, elapsedTime / lerpDuration);

            // Update the RectTransform's height
            button.rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
            button.rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the final height is set
         button.rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
         button.rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }
}
