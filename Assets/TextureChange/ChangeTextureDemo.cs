using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureDemo : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer hair, shirt, skin, head, eyeL, eyeR; //, clotheBottom, shoe;
    string[] hairColors = new string[] { "redlonghair", "blacklonghair" };
    string[] shirtColors = new string[] { "redshirt", "designershirt" };
    string[] skinColors = new string[] { "blackskin", "whiteskin" };
    string[] eyeColors = new string[] {"greeneyes", "blueeyes"};
    string[] headColors = new string[] { "blackskin", "whiteskin" };
    //string[] lipColors = new string[] { };
    public Material[] hairMaterials;
    public Material[] shirtMaterials;
    public Material[] skinMaterials;
    public Material[] eyeLeftMaterials;
    public Material[] eyeRightMaterials;
    public Material[] headMaterials;
    //public Material[] lipMaterials;
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
        Debug.Log(attributeGetter.shirt);
        //Debug.Log(attributeGetter.clothebottom);
        //Debug.Log(attributeGetter.shoe);
        for (int i = 0; i < 4; i++)
        {
            if (attributeGetter.hair == hairColors[i])
            {
                Debug.Log("offf");
                hair.material = hairMaterials[i];
            }
            if (attributeGetter.shirt == shirtColors[i])
            {
                shirt.material = shirtMaterials[i];
            }
            if (attributeGetter.skin == skinColors[i])
            {
                skin.material = skinMaterials[i];
            }
            if (attributeGetter.skin == headColors[i])
            {
                head.material = headMaterials[i];
            }
            if (attributeGetter.eye == eyeColors[i])
            {
                eyeL.material = eyeLeftMaterials[i];
                eyeR.material = eyeRightMaterials[i];
            }
            //if (attributeGetter.clothebottom == colors[i])
            //{
            //    clotheBottom.material = clotheBottomMaterials[i];
            //}
            //if (attributeGetter.shoe == colors[i])
            //{
            //    shoe.material = shoeMaterials[i];
            //}
        }
    }
}
