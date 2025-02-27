using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionData : ScriptableObject
{
    public List<GameObject> Characters;
    public HashSet<Vector2Int> judge_WolkRange_postion;
    public Vector2Int MoveCharacter_position;
    public Dictionary<GameObject, bool> CharactersActionEnd;
    public Dictionary<Vector2Int,GameObject> PositionToCharacter;
    public Dictionary<GameObject, Vector2Int> characterToPosition;
}