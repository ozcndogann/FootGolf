using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Switch : MonoBehaviour
{
    public GameObject[] maps;
    public GameObject[] Names;
    public GameObject[] Map_details;
    public Button NextButton;
    public Button PrevButton;

    
    public static int index;

    // Start is called before the first frame update
    void Start()
    {
        
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(index);
        if (index == 2)
        {
            
            NextButton.gameObject.SetActive(false);
            
        }
        else 
        {
            
            NextButton.gameObject.SetActive(true);

        }
        if (index == 0)
        {
            maps[0].gameObject.SetActive(true);
            Names[0].gameObject.SetActive(true);
            Map_details[0].gameObject.SetActive(true);
            PrevButton.gameObject.SetActive(false);
        }
        else
        {
            PrevButton.gameObject.SetActive(true);
        }
    }
    public void Next()
    {
        index += 1;
        for (int i =0;i < maps.Length; i++)
        {
            
            maps[i].gameObject.SetActive(false);
            Names[i].gameObject.SetActive(false);
            Map_details[i].gameObject.SetActive(false);
            maps[index].gameObject.SetActive(true);
            Names[index].gameObject.SetActive(true);
            Map_details[index].gameObject.SetActive(true);

        }
        Debug.Log(index);
    }
    public void Prev()
    {
        index -= 1;
        for (int i = 0; i < maps.Length; i++)
        {
            
            maps[i].gameObject.SetActive(false);
            Names[i].gameObject.SetActive(false);
            Map_details[i].gameObject.SetActive(false);
            maps[index].gameObject.SetActive(true);
            Names[index].gameObject.SetActive(true);
            Map_details[index].gameObject.SetActive(true);

        }
        Debug.Log(index);
    }

}
