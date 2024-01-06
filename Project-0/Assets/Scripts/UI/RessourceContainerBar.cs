using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceContainerBar : MonoBehaviour
{
    public GameObject   containerPrefab;
    public  int          container, maxContainer;
    private List<RessourceContainer> containers = new List<RessourceContainer>();


    public void DrawContainers()
    {
        ClearContainers();
        for (int i = 0; i < maxContainer; i++)
        {
            CreateEmptyContainer();
        }

        for (int i = 0; i < containers.Count; i++)
        {
            containers[i].SetContainerImage((i < container));
        }

    }
    public void CreateEmptyContainer()
    {
        GameObject newContainer = Instantiate(containerPrefab, transform.position, transform.rotation, transform);

        RessourceContainer containerComponent = newContainer.GetComponent<RessourceContainer>();
        containerComponent.SetContainerImage(false);
        containers.Add(containerComponent);
    }
    public void ClearContainers()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        containers = new List<RessourceContainer>();
    }

    public void Add()
    {
        if (container < maxContainer)
        {
            container++;
            DrawContainers();
        }
    }

    
    public void Remove()
    {
        if (container > 0)
        {
            container--;
            DrawContainers();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DrawContainers();
    }
}
