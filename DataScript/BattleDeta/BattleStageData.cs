using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "StageData/BattleStageData")]
public class BattleStageData : ScriptableObject
{
    public List<List<bool>> stagedata;
    public List<List<int>> WolkRange_postion;
    public List<List<GameObject>> Characters;
    public List<Vector2Int> StartPosition;
    public bool turnBool;
    public bool  CharacterAttackRangePop;
    public int turnNum;
    public Camera GameCamera;
    public FactionData Ally;
    public FactionData Enemy;
    public FactionData NowFactionData;
}
