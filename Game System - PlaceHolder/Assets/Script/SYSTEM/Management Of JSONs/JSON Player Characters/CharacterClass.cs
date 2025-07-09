using System.Collections.Generic;

[System.Serializable]
public class CharacterClass
{
    public string className;
    public int hp;
    public int atk;
    public int def;
    public string description;
}

[System.Serializable]
public class CharacterClassList
{
    public List<CharacterClass> classes;
}