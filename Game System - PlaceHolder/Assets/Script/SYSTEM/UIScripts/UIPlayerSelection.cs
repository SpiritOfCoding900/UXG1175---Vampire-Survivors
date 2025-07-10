using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterCardUI
{
    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text speedText;
    public TMP_Text descriptionText;
}


public class UIPlayerSelection : MonoBehaviour
{
    public List<CharacterCardUI> characterCards; // Assign 3 elements in the inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.CloseAll();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        var classList = CharacterLoader.Instance.myClassList;

        for (int i = 0; i < characterCards.Count; i++)
        {
            if (i < classList.classes.Count)
            {
                var data = classList.classes[i];
                var ui = characterCards[i];

                ui.nameText.text = data.className;
                ui.hpText.text = data.MaxHP.ToString();
                ui.speedText.text = data.moveSpeed.ToString();
                ui.descriptionText.text = data.description;

                
            }
        }

        
    }

    public void ChosenWarriorClass() => ChooseClass(0);
    public void ChosenRangerClass() => ChooseClass(1);
    public void ChosenMageClass() => ChooseClass(2);

    private void ChooseClass(int index)
    {
        Time.timeScale = 1f;

        var data02 = CharacterLoader.Instance.myClassList.classes[index];
        Player.Instance.MaxHP = data02.MaxHP;
        Player.Instance.moveSpeed = data02.moveSpeed;

        UIManager.Instance.CloseAll();
        // Do something with index, like pass it to GameManager or store selected class
    }
}
