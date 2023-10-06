using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// bu kodun deðiþmesi gerek full versiyonda çünkü þuan 2 index'e göre ayarlandý
public class Switch_Player : MonoBehaviour
{
    public GameObject[] maps;
    public Button NextButton;
    public Button PrevButton;
    public Button PlayerSelect;
    public GameObject Selected;
    

    public static int index;

    // Start is called before the first frame update
    void Start()
    {

        index = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetInt("FootballerChooser", index);
        Debug.Log(index);
        if (index == 1)
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
      
        for (int i = 0; i < maps.Length; i++)
        {

            maps[i].gameObject.SetActive(false);
            maps[index].gameObject.SetActive(true);


        }
        Selected.SetActive(false);
        if (PlayerPrefs.GetInt("FootballerChooser") == index)
        {
            Selected.SetActive(true);
        }
        Debug.Log(index);
    }
    public void Prev()
    {
        index -= 1;
 
        for (int i = 0; i < maps.Length; i++)
        {

            maps[i].gameObject.SetActive(false);
            maps[index].gameObject.SetActive(true);


        }
        Selected.SetActive(false);
        if(PlayerPrefs.GetInt("FootballerChooser")== index)
        {
            Selected.SetActive(true);
        }
        Debug.Log(index);
    }
    public void PLayerSelection()
    {
        Selected.SetActive(true);

    }
    
        }
