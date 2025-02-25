using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    public List<List<bool>> stagedata;
    public List<GameObject> My_Characters;
    public Dictionary<Vector2Int,GameObject> PositionToCharacter;
    public Dictionary<GameObject, Vector2Int> characterToPosition;
}
