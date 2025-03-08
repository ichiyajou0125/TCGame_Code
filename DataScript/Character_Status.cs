using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Character_Status : ScriptableObject{
    public int CharacterID;
    public string Charactername;
    public int HP;
    public int Attack;
    public int defense;
    public List<Vector2Int> AttackRange;
    public int moveRange;
}