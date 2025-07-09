using UnityEngine;
using System.Collections.Generic;

public class CharacterLoader : MonoBehaviour
{
    public CharacterClassList classList;

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("characterClasses");
        if (jsonFile != null)
        {
            classList = JsonUtility.FromJson<CharacterClassList>(jsonFile.text);
            foreach (CharacterClass c in classList.classes)
            {
                Debug.Log($"Loaded Class: {c.className} - HP: {c.hp}, ATK: {c.atk}, DEF: {c.def}");
            }
        }
        else
        {
            Debug.LogError("Could not find characterClasses.json in Resources folder.");
        }
    }
}
