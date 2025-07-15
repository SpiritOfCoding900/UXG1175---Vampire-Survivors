using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUpStats : MonoBehaviour
{
    public static PlayerLevelUpStats Instance;

    public int Level = 1;
    public float experience { get; private set; }

    public static float expNeeded;

    public static float previousExperience;



    private void Start()
    {
        Level = 0;
        experience = 0;
    }

    private void Awake()
    {
        Instance = this; // Inserting this into the Static Pigeon hole.
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public static int ExpNeedToLvlUp(int currentLevel)
    {
        if (currentLevel == 0)
            return 0;
        return (currentLevel * currentLevel + currentLevel) * 5;
    }

    public void SetExperience(float expOrbsEarned)
    {
        experience += expOrbsEarned;
        Debug.Log(10f + " Exps Aquired.");

        expNeeded = ExpNeedToLvlUp(Level);
        previousExperience = ExpNeedToLvlUp(Level - 1);

        if(experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(Level);
            previousExperience = ExpNeedToLvlUp(Level - 1);
        }
    }

    public void LevelUp()
    {
        Level++;
    }
}
