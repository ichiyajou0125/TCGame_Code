using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System;

public class Character_Manager : MonoBehaviour
{
    public void get_postion(GameObject Character,List<List<bool>> stage,ref int x,ref int z){
        float floatx = Character.transform.position.x;
        float floatz = Character.transform.position.z;
        x = (int)floatx + 9;
        z = (int)floatz + 9;
        stage[x][z] = true;
        Debug.Log("CM: x = " + x + ", z = " + z);
    }
        public void Jage_nextPostion(List<List<bool>> stage,int x,int z, int[,] arr_NextPos, ref int NextPos_num){
        int[] jage_case = {-1,1};
        for(int i = 0; i < 2; i++){
            if(IsValidIndex(stage,x + jage_case[i],z) && stage[x + jage_case[i]][z] == false){ //左・右のIndexが有効で、かつ探索されていない
                stage[x + jage_case[i]][z] = true;
                arr_NextPos[NextPos_num,0] = x + jage_case[i];
                arr_NextPos[NextPos_num,1] = z;
                NextPos_num++;
            }
            if(IsValidIndex(stage,x,z + jage_case[i]) && stage[x][z + jage_case[i]] == false){ //上・下のIndexが有効で、かつ探索されていない
                stage[x][z + jage_case[i]] = true;
                arr_NextPos[NextPos_num,0] = x;
                arr_NextPos[NextPos_num,1] = z + jage_case[i];
                NextPos_num++;
            }
        }
    }
    public void walk_range(List<List<bool>> stage,int x, int z,int stage_size,GameObject WolkSize_Prefab){
        int fornum;
        int LookPos_num = 1;//今見ている場所が何個あるのか
        int[,] LookPos_arr = new int[1,2];
        LookPos_arr[0,0] = x;
        LookPos_arr[0,1] = z;
        for(int i = 0; i < 3; i++){//配列に次の移動できる場所を格納。それを参照して次の移動マスを判定する
            int[,] NextPos_arr = new int[LookPos_num + 4,2];
            fornum = LookPos_num;
            LookPos_num = 0;
            for(int j = 0; j < fornum; j++){
                Jage_nextPostion(stage,LookPos_arr[j,0],LookPos_arr[j,1],NextPos_arr,ref LookPos_num);
            }
            LookPos_arr = new int[LookPos_num+4,2];
            LookPos_arr = NextPos_arr;
        }

        for(int i = 0; i < stage_size; i++){
            for(int j = 0; j < stage_size; j++){
                if(stage[i][j]){
                    Instantiate(WolkSize_Prefab,new Vector3(i-9,-0.04f,j-9),Quaternion.identity);
                }
            }
        }
    }
    bool IsValidIndex(List<List<bool>> grid, int row, int col)
    {
        return row >= 0 && row < grid.Count &&         // 行数チェック
               col >= 0 && col < grid[row].Count;      // 列数チェック
    }

    public void PrintList(List<List<bool>> grid)
    {
        string output = "";
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                output += grid[i][j] + " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }

}
