using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MenuAttributeGetter : MonoBehaviour
{
    public string lip, earring;

    void Start()
    {
        CharacterData characterData = LoadCharacterData("Assets/TextureChange/E41.json");

        foreach (var attribute in characterData.attributes)
        {
            if (attribute.trait_type == "earrings")
            {
                Debug.Log("erkek");
                earring = GetAttributeValue(characterData.attributes, "earrings");
                PlayerPrefs.SetInt("FootballerChooser", 5);
            }
            else if (attribute.trait_type == "lips")
            {
                Debug.Log("kadýn");
                lip = GetAttributeValue(characterData.attributes, "lips");
                PlayerPrefs.SetInt("FootballerChooser", 4);
            }
        }
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
