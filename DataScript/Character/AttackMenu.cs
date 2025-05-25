using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackMenu", menuName = "GameData/AttackMenu")]
public class AttackMenu : ScriptableObject
{
    public int AttackID;
    public string AttackName;
    public int AttackPower;
    public List<Vector2Int> AttackRange;
}

