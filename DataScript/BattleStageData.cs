using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "BattleStageData")]
public class BattleStageData : ScriptableObject
{
    public List<List<bool>> stagedata;
    public List<List<int>> WolkRange_postion;
    public List<List<GameObject>> Characters;
    public bool turnBool;
    public bool  CharacterAttackRangePop;
    public int turnNum;
    public FactionData Ally;
    public FactionData Enemy;
    public FactionData NowFactionData;
}
