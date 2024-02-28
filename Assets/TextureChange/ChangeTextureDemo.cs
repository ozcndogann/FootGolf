using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureDemo : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer hair, clotheUp, clotheBottom, shoe;
    string[] colors = new string[] {"lightBrown", "green", "red", "white"};
    public Material[] hairMaterials;
    public Material[] clotheUpMaterials;
    public Material[] clotheBottomMaterials;
    public Material[] shoeMaterials;
    AttributeGetter attributeGetter;

    void Start()
    {
        attributeGetter = gameObject.GetComponent<AttributeGetter>();
        StartCoroutine(ColorPicker());

    }

    IEnumerator ColorPicker()
    {
        yield return new WaitForSeconds(.1f);
        Debug.Log(attributeGetter.hair);
        Debug.Log(attributeGetter.clotheup);
        Debug.Log(attributeGetter.clothebottom);
        Debug.Log(attributeGetter.shoe);
        for (int i = 0; i < 4; i++)
        {
            if (attributeGetter.hair == colors[i])
            {
                Debug.Log("offf");
                hair.material = hairMaterials[i];
            }
            if (attributeGetter.clotheup == colors[i])
            {
                clotheUp.material = clotheUpMaterials[i];
            }
            if (attributeGetter.clothebottom == colors[i])
            {
                clotheBottom.material = clotheBottomMaterials[i];
            }
            if (attributeGetter.shoe == colors[i])
            {
                shoe.material = shoeMaterials[i];
            }
        }
    }
}
