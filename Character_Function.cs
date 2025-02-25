using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System;

public class Character_Function: MonoBehaviour
{
    public Game_Function GF;

    public void MoveCharacter(List<List<bool>> stage,
                              Dictionary<Vector2Int,GameObject> PositionToCharacter,
                              Dictionary<GameObject,Vector2Int> CharacterToPosition,
                              HashSet<Vector2Int> JageHash_WolkRange,
                              GameObject Character,
                              Vector2Int oldposition,
                              GameObject Wolk_range,
                              int x, 
                              int z){
        if(GF.HashHasValue(JageHash_WolkRange,x,z)){
            Character.transform.position = new Vector3(x - 9,Character.transform.position.y,z - 9);
            stage[oldposition.x][oldposition.y] = false;
            stage[x][z] = true;

            Debug.Log($"old: x = {oldposition.x} , z = {oldposition.y}\nnew: x = {x} , z = {z}");

            PositionToCharacter.Remove(oldposition);
            PositionToCharacter[new Vector2Int(x,z)] = Character;
            CharacterToPosition[Character] = new Vector2Int(x,z);
            foreach(Transform child in Wolk_range.transform){
                Destroy(child.gameObject);
            }
        }
    }
    public void get_postion(GameObject Character,
                            List<List<bool>> stage,
                            Dictionary<Vector2Int,GameObject> PositionToCharacter,
                            Dictionary<GameObject,Vector2Int> CharacterToPosition,
                            ref int x,
                            ref int z){
        float floatx = Character.transform.position.x;
        float floatz = Character.transform.position.z;
        x = (int)floatx + 9;
        z = (int)floatz + 9;
        Vector2Int position = new Vector2Int(x,z);
        PositionToCharacter.Add(position,Character);
        CharacterToPosition.Add(Character,position);
        stage[x][z] = true;
    }
    public void Jage_nextPostion(List<List<bool>> stage,
                             List<List<int>> WolkRange_postion,
                             ref HashSet<Vector2Int> JageHash_WolkRange,
                             int x,
                             int z,
                             List<List<int>> arr_NextPos,
                             ref int NextPos_num){
        int[] jage_case = { -1, 1 };
        List<int> row;

        for (int i = 0; i < 2; i++)
        {
            if (IsValidIndex(stage, x + jage_case[i], z) && !GF.HashHasValue(JageHash_WolkRange,x + jage_case[i],z) && !stage[x + jage_case[i]][z])
            {
                row = new List<int> { x + jage_case[i] - 9, z - 9 };
                WolkRange_postion.Add(row);
                arr_NextPos.Add(row);
                JageHash_WolkRange.Add(new Vector2Int( x + jage_case[i] - 9, z - 9));
                NextPos_num++;
            }
            if (IsValidIndex(stage, x, z + jage_case[i]) && !GF.HashHasValue(JageHash_WolkRange,x,z + jage_case[i]) && !stage[x][z + jage_case[i]])
            {
                row = new List<int> { x - 9, z + jage_case[i] - 9 };
                WolkRange_postion.Add(row);
                arr_NextPos.Add(row);
                JageHash_WolkRange.Add(new Vector2Int( x - 9, z  + jage_case[i] - 9));
                NextPos_num++;
            }
        }
    }
    public void walk_range(List<List<bool>> stage,
                           ref List<List<int>> WolkRange_postion,
                           ref HashSet<Vector2Int> JageHash_WolkRange,
                           int x,
                           int z, 
                           ref int Wolk_width){
        int fornum;
        int LookPos_num = 1;
        JageHash_WolkRange = new HashSet<Vector2Int>();

        List<List<int>> LookPos_arr = new List<List<int>>();
        List<int> row = new List<int> { x - 9, z - 9 };
        LookPos_arr.Add(row);
        JageHash_WolkRange.Add(new Vector2Int( x - 9, z - 9 ));

        WolkRange_postion = new List<List<int>> { row };

        for (int i = 0; i < Wolk_width; i++)
        {
            List<List<int>> NextPos_arr = new List<List<int>>();
            fornum = LookPos_num;
            LookPos_num = 0;
            for (int j = 0; j < fornum; j++)
            {
                Jage_nextPostion(stage, WolkRange_postion,ref JageHash_WolkRange, LookPos_arr[j][0] + 9, LookPos_arr[j][1] + 9, NextPos_arr, ref LookPos_num);
            }
            LookPos_arr = NextPos_arr;
        }
    }
    public bool IsValidIndex(List<List<bool>> grid, int row, int col)
    {
        return row >= 0 && row < grid.Count &&         // 行数チェック
               col >= 0 && col < grid[row].Count;      // 列数チェック
    }
    public void jage_CharacterMove(ref bool Character_move){
        if(Character_move){
            Character_move = false;
        }
        else if(!Character_move){
            Character_move = true;
        }
    }
}