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
        //Debug.Log("CM: x = " + x + ", z = " + z);
    }
    public void Jage_nextPostion(List<List<bool>> stage,
                                 List<List<int>> WolkRange_postion,
                                 int x,
                                 int z,
                                 List<List<int>>  arr_NextPos,
                                 ref int NextPos_num){
        int[] jage_case = {-1,1};
        List<int> row;
        for(int i = 0; i < 2; i++){
            List<int> nextLook_postion = new List<int> { x + jage_case[i], z };
            if(IsValidIndex(stage,x + jage_case[i],z) && list_jage(WolkRange_postion,nextLook_postion)){ //左・右のIndexが有効で、かつ探索されていない
                row = new List<int>();
                row.Add(x + jage_case[i] - 9);
                row.Add(z - 9);
                WolkRange_postion.Add(row);
                arr_NextPos.Add(row);

                NextPos_num++;
            }
            nextLook_postion = new List<int>{x, z + jage_case[i]};
            if(IsValidIndex(stage,x,z + jage_case[i]) && list_jage(WolkRange_postion,nextLook_postion)){ //上・下のIndexが有効で、かつ探索されていない
                row = new List<int>();
                row.Add(x - 9);
                row.Add(z + jage_case[i] - 9);
                WolkRange_postion.Add(row);
                arr_NextPos.Add(row);

                NextPos_num++;
            }
        }
    }
    public void walk_range(List<List<bool>> stage,int x, int z,ref int Wolk_width,ref List<List<int>> WolkRange_postion){
        int fornum;
        int LookPos_num = 1;//今見ている場所が何個あるのか

        List<List<int>> LookPos_arr = new List<List<int>>();
        List<int> row = new List<int>{x-9,z-9};
        LookPos_arr.Add(row);

        WolkRange_postion = new List<List<int>>();

        for(int i = 0; i < Wolk_width; i++){//配列に次の移動できる場所を格納。それを参照して次の移動マスを判定する
            List<List<int>> NextPos_arr = new List<List<int>>();
            fornum = LookPos_num;
            LookPos_num = 0;
            for(int j = 0; j < fornum; j++){
                Jage_nextPostion(stage,WolkRange_postion,LookPos_arr[j][0] + 9,LookPos_arr[j][1] + 9,NextPos_arr,ref LookPos_num);
            }
            LookPos_arr = NextPos_arr;
        }
    }
    public void LookPlace(List<List<bool>> stage, ref int x, ref int z, GameObject LookPrace_frame){
        if(Input.GetKeyDown(KeyCode.UpArrow) && IsValidIndex(stage,x - 1,z)){
            x--;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && IsValidIndex(stage,x + 1,z)){
            x++;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && IsValidIndex(stage,x,z + 1)){
            z++;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && IsValidIndex(stage,x,z - 1)){
            z--;
        }
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
    }
    public void visualize_WolkRange(List<List<int>> WolkRange_postion,int stage_size,GameObject WolkRange_Prefab,GameObject Wolk_Range){
        for(int i = 0; i < WolkRange_postion.Count; i++){
            GameObject Prefab = Instantiate(WolkRange_Prefab,Wolk_Range.transform);
            Prefab.transform.position = new Vector3(WolkRange_postion[i][0],-0.01f,WolkRange_postion[i][1]);
        }
    }
    public bool list_jage(List<List<int>> postion_list, List<int> next_postion)
    {
        foreach (List<int> child in postion_list)
        {
            if (child[0] == next_postion[0] && child[1] == next_postion[1])
            {
                return false;
            }
        }
        return true;
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