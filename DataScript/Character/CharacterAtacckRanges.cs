using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterAtacckRanges : ScriptableObject
{
    public Dictionary<int,List<Vector2Int>> AttackRangeDictionary;

    public void AttackRangesload(){
        AttackRangeDictionary = new Dictionary<int, List<Vector2Int>>();
        List<Vector2Int> AttackRanges = new List<Vector2Int>();
        AttackRanges.Clear();
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRangeDictionary.Add(0,AttackRanges);
        AttackRanges.Clear();

        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRangeDictionary.Add(1,AttackRanges);
        AttackRanges.Clear();

        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRanges.Add(new Vector2Int(3,0));
        AttackRangeDictionary.Add(2,AttackRanges);
        AttackRanges.Clear();
        
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRanges.Add(new Vector2Int(3,0));
        AttackRangeDictionary.Add(3,AttackRanges);
        AttackRanges.Clear();
        
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRanges.Add(new Vector2Int(2,-1));
        AttackRanges.Add(new Vector2Int(2,1));
        AttackRanges.Add(new Vector2Int(3,0));
        AttackRangeDictionary.Add(4,AttackRanges);
        AttackRanges.Clear();
        
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRanges.Add(new Vector2Int(2,-1));
        AttackRanges.Add(new Vector2Int(2,1));
        AttackRangeDictionary.Add(5,AttackRanges);
        AttackRanges.Clear();

        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(1,1));
        AttackRanges.Add(new Vector2Int(1,-1));
        AttackRangeDictionary.Add(6,AttackRanges);
        AttackRanges.Clear();
        
        
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(-1,0));
        AttackRanges.Add(new Vector2Int(0,-1));
        AttackRanges.Add(new Vector2Int(0,1));
        AttackRangeDictionary.Add(7,AttackRanges);
        AttackRanges.Clear();
        
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(2,0));
        AttackRanges.Add(new Vector2Int(2,-1));
        AttackRanges.Add(new Vector2Int(2,1));
        AttackRanges.Add(new Vector2Int(3,0));
        AttackRangeDictionary.Add(8,AttackRanges);
        AttackRanges.Clear();
        
        AttackRanges.Add(new Vector2Int(1,0));
        AttackRanges.Add(new Vector2Int(1,1));
        AttackRanges.Add(new Vector2Int(1,-1));
        AttackRanges.Add(new Vector2Int(0,1));
        AttackRanges.Add(new Vector2Int(0,-1));
        AttackRanges.Add(new Vector2Int(-1,0));
        AttackRanges.Add(new Vector2Int(-1,1));
        AttackRanges.Add(new Vector2Int(-1,-1));
        AttackRangeDictionary.Add(9,AttackRanges);
        AttackRanges.Clear();

        AttackRangeDictionary.Add(10,AttackRanges);
    }
}
