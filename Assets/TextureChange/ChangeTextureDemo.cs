using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureDemo : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer hair, shirt, skin, head, eye1, eye2; //, clotheBottom, shoe;
    string[] hairColors = new string[] { "grayhair", "redlonghair", "blacklonghair", "longdarkhair" };
    string[] shirtColors = new string[] { "redshirt", "designershirt", "greenshirt" };
    string[] skinColors = new string[] { "mixedskin", "blackskin", "whiteskin" };
    string[] eyeColors = new string[] {"greeneyes", "blueeyes"};
    //string[] lipColors = new string[] { };
    public Material[] hairMaterials;
    public Material[] shirtMaterials;
    public Material[] skinMaterials;
    public Material[] eyeMaterials;
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
                head.material = skinMaterials[i];
            }
            if (attributeGetter.eye == eyeColors[i])
            {
                eye1.material = eyeMaterials[i];
                eye2.material = eyeMaterials[i];
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
