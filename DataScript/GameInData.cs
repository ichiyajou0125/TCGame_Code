using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInData",menuName = "StageData/GameInData")]
public class GameInData : ScriptableObject
{
    public List<GameInField> fields;
    public Dictionary<string, List<Character_Status>> fieldEnemys;
}

public class GameInField
{
    public string FieldName;
    public bool isTerritory;
    public List<Character_Status> characters;
}