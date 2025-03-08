using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class FactionData : ScriptableObject
{
    public int CharacterMoveCount;
    public List<GameObject> Characters;
    public List<GameObject> subjectCharacter;
    public List<Vector2Int> AttackPostions;
    public HashSet<Vector2Int> judge_WolkRange_postion;
    public Vector2Int MoveCharacter_position;
    public Vector2Int AttackDirection;
    public Dictionary<GameObject, bool> CharactersActionEnd;
    public Dictionary<Vector2Int,GameObject> PositionToCharacter;
    public Dictionary<GameObject, Vector2Int> characterToPosition;
}