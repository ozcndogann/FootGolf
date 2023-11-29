using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// bu kodun de�i�mesi gerek full versiyonda ��nk� �uan 2 index'e g�re ayarland�
public class Switch_Player : MonoBehaviour
{
    public GameObject[] maps;
    public Button NextButton;
    public Button PrevButton;
    public Button PlayerSelect;
    public GameObject Selected;
    PlayerSwipe swipeScript;
    

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
        if (index == maps.Length - 1)
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
            //Ekran değişirken dönmeyi sıfırlıyorum
            swipeScript = maps[i].gameObject.GetComponent<PlayerSwipe>();
            swipeScript.resetMove();
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
        PlayerPrefs.SetInt("FootballerChooser", index);

    }
    
        }
