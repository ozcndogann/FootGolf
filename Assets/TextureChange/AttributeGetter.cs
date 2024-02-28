using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Include the System.IO namespace

public class AttributeGetter : MonoBehaviour
{
    public string hair, clotheup, clothebottom, shoe;
    void Start()
    {
        // Call the function to load attributes from the file
        Dictionary<string, string> attributes = LoadAttributes("Assets/TextureChange/Attributes.txt");

        // Example usage: print all attributes
        foreach (var attribute in attributes)
        {
            //Debug.Log($"{attribute.Key}: {attribute.Value}");
            //Debug.Log(attributes["hair"]);
        }
        hair = attributes["hair"];
        clotheup = attributes["clotheup"];
        clothebottom = attributes["clothebottom"];
        shoe = attributes["shoe"];
        //Debug.Log(attributes["hair"]);
        //Debug.Log(attributes["clotheup"]);
        //Debug.Log(attributes["clothebottom"]);
        //Debug.Log(attributes["shoe"]);
    }

    Dictionary<string, string> LoadAttributes(string filePath)
    {
        Dictionary<string, string> attributes = new Dictionary<string, string>();

        // Ensure the file exists
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split each line into attribute name and value
                    string[] parts = line.Split(new string[] { ": " }, System.StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        // Add the attribute and its value to the dictionary
                        attributes[parts[0]] = parts[1];
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
       
        return attributes;
    }
}
