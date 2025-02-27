using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System;

public class Character_Function: MonoBehaviour
{
    public Game_Function GF;
    public BattleStageData BSD;

    

    public void MoveCharacter(GameObject Character,Vector2Int oldposition,GameObject Wolk_range,int x, int z){
        if(GF.HashHasValue(BSD.NowFactionData.judge_WolkRange_postion,x,z)){
            Character.transform.position = new Vector3(x - 9,Character.transform.position.y,z - 9);
            BSD.stagedata[oldposition.x][oldposition.y] = false;
            BSD.stagedata[x][z] = true;

            Debug.Log($"old: x = {oldposition.x} , z = {oldposition.y}\nnew: x = {x} , z = {z}");

            BSD.NowFactionData.PositionToCharacter.Remove(oldposition);
            BSD.NowFactionData.PositionToCharacter[new Vector2Int(x,z)] = Character;
            BSD.NowFactionData.characterToPosition[Character] = new Vector2Int(x,z);
            foreach(Transform child in Wolk_range.transform){
                Destroy(child.gameObject);
            }
        }
    }
    public void get_postion(GameObject Character,ref int x,ref int z){
        float floatx = Character.transform.position.x;
        float floatz = Character.transform.position.z;
        x = (int)floatx + 9;
        z = (int)floatz + 9;
        Vector2Int position = new Vector2Int(x,z);
        BSD.NowFactionData.PositionToCharacter.Add(position,Character);
        BSD.NowFactionData.characterToPosition.Add(Character,position);
        BSD.stagedata[x][z] = true;
    }
    public void Jage_nextPostion(int x,int z,List<List<int>> arr_NextPos,ref int NextPos_num){
        int[] jage_case = { -1, 1 };
        List<int> row;

        for (int i = 0; i < 2; i++)
        {
            if (GF.IsValidIndex(BSD.stagedata, x + jage_case[i], z) && !GF.HashHasValue(BSD.NowFactionData.judge_WolkRange_postion,x + jage_case[i],z) && !BSD.stagedata[x + jage_case[i]][z])
            {
                row = new List<int> { x + jage_case[i] - 9, z - 9 };
                BSD.WolkRange_postion.Add(row);
                arr_NextPos.Add(row);
                BSD.NowFactionData.judge_WolkRange_postion.Add(new Vector2Int( x + jage_case[i] - 9, z - 9));
                NextPos_num++;
            }
            if (GF.IsValidIndex(BSD.stagedata, x, z + jage_case[i]) && !GF.HashHasValue(BSD.NowFactionData.judge_WolkRange_postion,x,z + jage_case[i]) && !BSD.stagedata[x][z + jage_case[i]])
            {
                row = new List<int> { x - 9, z + jage_case[i] - 9 };
                BSD.WolkRange_postion.Add(row);
                arr_NextPos.Add(row);
                BSD.NowFactionData.judge_WolkRange_postion.Add(new Vector2Int( x - 9, z  + jage_case[i] - 9));
                NextPos_num++;
            }
        }
    }
    public void walk_range(int x,int z, ref int Wolk_width){
        int fornum;
        int LookPos_num = 1;
        BSD.NowFactionData.judge_WolkRange_postion = new HashSet<Vector2Int>();

        List<List<int>> LookPos_arr = new List<List<int>>();
        List<int> row = new List<int> { x - 9, z - 9 };
        LookPos_arr.Add(row);
        BSD.NowFactionData.judge_WolkRange_postion.Add(new Vector2Int( x - 9, z - 9 ));

        BSD.WolkRange_postion = new List<List<int>> { row };

        for (int i = 0; i < Wolk_width; i++)
        {
            List<List<int>> NextPos_arr = new List<List<int>>();
            fornum = LookPos_num;
            LookPos_num = 0;
            for (int j = 0; j < fornum; j++)
            {
                Jage_nextPostion(LookPos_arr[j][0] + 9, LookPos_arr[j][1] + 9, NextPos_arr, ref LookPos_num);
            }
            LookPos_arr = NextPos_arr;
        }
    }
}