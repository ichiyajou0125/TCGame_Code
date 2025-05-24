using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<FieldData> fields;
}

[Serializable]
public class FieldData
{
    public string FieldName;
    public bool isTerritory;
    public List<CharacterStatusData> characters;
}

[Serializable]
public class CharacterStatusData 
{
    public int CharacterID;
    public string Charactername;
    public int HP;
    public int Attack;
    public int defense;
    public int AttackMenu;
    public int moveRange;
}