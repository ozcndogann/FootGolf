using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class CharacterAttribute
{
    public string trait_type;
    public string value;
}

[Serializable]
public class CharacterData
{
    public int token_id;
    //public string image;
    //public string name;
    //public string description;
    public CharacterAttribute[] attributes;
}

public class AttributeGetter : MonoBehaviour
{
    public string skin, head, hair, shirt, eye, lip, glass, earring;

    void Start()
    {
        // Load character data from JSON file
        CharacterData characterData = LoadCharacterData("Assets/TextureChange/K21.json");

        skin = GetAttributeValue(characterData.attributes, "skins");
        head = GetAttributeValue(characterData.attributes, "skins");
        hair = GetAttributeValue(characterData.attributes, "hairs");
        shirt = GetAttributeValue(characterData.attributes, "shirts");
        eye = GetAttributeValue(characterData.attributes, "eyes");
        //foreach (var attribute in characterData.attributes)
        //{
        //    if (attribute.trait_type == "earrings")
        //    {
        //        Debug.Log("erkek");
        //        earring = GetAttributeValue(characterData.attributes, "earrings");
        //        PlayerPrefs.SetInt("FootballerChooser", 5);
        //    }
        //    else if (attribute.trait_type == "lips")
        //    {
        //        Debug.Log("kadýn");
        //        lip = GetAttributeValue(characterData.attributes, "lips");
        //        PlayerPrefs.SetInt("FootballerChooser", 4);
        //    }
        //}
    }

    CharacterData LoadCharacterData(string filePath)
    {
        // Ensure the file exists
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<CharacterData>(dataAsJson);
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
            return null; // Return null if file does not exist
        }
    }

    string GetAttributeValue(CharacterAttribute[] attributes, string traitType)
    {
        foreach (var attribute in attributes)
        {
            if (attribute.trait_type == traitType)
            {
                return attribute.value;
            }
        }
        return "";
    }
}
