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
    public string skin, hair, shirt;

    void Start()
    {
        // Load character data from JSON file
        CharacterData characterData = LoadCharacterData("Assets/TextureChange/K16.json");

        skin = GetAttributeValue(characterData.attributes, "skins");
        hair = GetAttributeValue(characterData.attributes, "hairs");
        shirt = GetAttributeValue(characterData.attributes, "shirts");
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
