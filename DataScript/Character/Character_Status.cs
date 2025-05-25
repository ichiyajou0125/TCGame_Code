using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "GameData/CharacterStatus")]
public class Character_Status : ScriptableObject{
    public int CharacterID;
    public string Charactername;
    public int HP;
    public int Attack;
    public int defense;
    public int AttackMenu;
    public int moveRange;
}