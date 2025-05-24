using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System;
using UnityEngine.PlayerLoop;

public class Character_Function: MonoBehaviour
{
    public Game_Function GF;
    public BattleStageData BSD;

    public void MoveCharacter(GameObject Character,GameObject Wolk_range,int x, int z){
        if(GF.HashHasValue(BSD.NowFactionData.judge_WolkRange_postion,x,z)){
            Character.transform.position = new Vector3(x - 9,Character.transform.position.y,z - 9);
            foreach(Transform child in Wolk_range.transform){
                Destroy(child.gameObject);
            }
        }
    }
    public void MoveEnd(GameObject Character, Vector2Int oldposition, int x, int z){
        BSD.stagedata[oldposition.x][oldposition.y] = false;
        BSD.stagedata[x][z] = true;

        BSD.NowFactionData.PositionToCharacter.Remove(oldposition);
        BSD.NowFactionData.PositionToCharacter[new Vector2Int(x,z)] = Character;
        BSD.NowFactionData.characterToPosition[Character] = new Vector2Int(x,z);
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
    public void judge_nextPostion(int x,int z,List<List<int>> arr_NextPos,ref int NextPos_num){
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
    public void judge_AttackPosition(int x, int z, GameObject AttackRangeGameObject, GameObject AttackPositionObj){
        List<GameObject> opponentCharacters = new List<GameObject>();
        Dictionary<Vector2Int, GameObject> PositionToCharacter = new Dictionary<Vector2Int, GameObject>();
        if(BSD.CharacterAttackRangePop){
            BSD.NowFactionData.subjectCharacter.Clear();
            BSD.NowFactionData.AttackPostions.Clear();
            foreach(Transform child in AttackRangeGameObject.transform){
                Destroy(child.gameObject);
            }
            foreach(Vector2Int Attackpos in BSD.NowFactionData.NowMoveChara.attackMenu.AttackRange){
                GameObject prefab = Instantiate(AttackPositionObj, AttackRangeGameObject.transform);
                if(BSD.NowFactionData.AttackDirection.y == 0 && GF.IsValidIndex(BSD.stagedata,x + Attackpos.x * BSD.NowFactionData.AttackDirection.x,z + Attackpos.y)){
                    prefab.transform.position = new Vector3(x + Attackpos.x * BSD.NowFactionData.AttackDirection.x - 9, -0.01f, z + Attackpos.y - 9);
                    BSD.NowFactionData.AttackPostions.Add(new Vector2Int(x + Attackpos.x * BSD.NowFactionData.AttackDirection.x, z + Attackpos.y));
                }
                if(BSD.NowFactionData.AttackDirection.x == 0 && GF.IsValidIndex(BSD.stagedata,x + Attackpos.y,z + Attackpos.x * BSD.NowFactionData.AttackDirection.y)){
                    prefab.transform.position = new Vector3(x + Attackpos.y - 9, -0.01f, z + Attackpos.x * BSD.NowFactionData.AttackDirection.y - 9);
                    BSD.NowFactionData.AttackPostions.Add(new Vector2Int(x + Attackpos.y, z + Attackpos.x * BSD.NowFactionData.AttackDirection.y));
                }
            }
            if(BSD.NowFactionData == BSD.Ally){
                opponentCharacters = BSD.Enemy.Characters;
                PositionToCharacter = BSD.Enemy.PositionToCharacter;
            }
            else if(BSD.NowFactionData == BSD.Enemy){
                opponentCharacters = BSD.Ally.Characters;
                PositionToCharacter = BSD.Ally.PositionToCharacter;
            }
            foreach(GameObject Character in opponentCharacters){
                foreach(Vector2Int position in BSD.NowFactionData.AttackPostions){
                    if(GF.GetCharacterAtPostion(PositionToCharacter,position) != null){
                        Debug.Log(Character);
                        BSD.NowFactionData.subjectCharacter.Add(Character);
                    }
                }
            }
            foreach(GameObject Character in BSD.NowFactionData.subjectCharacter){
                Debug.Log(Character);
            }
            BSD.CharacterAttackRangePop = false;
        }
    }
    public void walk_range(int x,int z){
        int fornum;
        int LookPos_num = 1;
        BSD.NowFactionData.judge_WolkRange_postion = new HashSet<Vector2Int>();

        List<List<int>> LookPos_arr = new List<List<int>>();
        List<int> row = new List<int> { x - 9, z - 9 };
        LookPos_arr.Add(row);
        BSD.NowFactionData.judge_WolkRange_postion.Add(new Vector2Int( x - 9, z - 9 ));

        BSD.WolkRange_postion = new List<List<int>> { row };

        for (int i = 0; i < BSD.NowFactionData.NowMoveChara.moveRange; i++)
        {
            List<List<int>> NextPos_arr = new List<List<int>>();
            fornum = LookPos_num;
            LookPos_num = 0;
            for (int j = 0; j < fornum; j++)
            {
                judge_nextPostion(LookPos_arr[j][0] + 9, LookPos_arr[j][1] + 9, NextPos_arr, ref LookPos_num);
            }
            LookPos_arr = NextPos_arr;
        }
    }
    public void Move_Cancel(ref GameObject Character,ref Vector2Int oldposition, ref bool CharacterMove){
        Character.transform.position = new Vector3(oldposition.x - 9, Character.transform.position.y, oldposition.y - 9);
        oldposition = new Vector2Int();
        CharacterMove = false;
        Character = null;
    }
    public void CharacterAttackDirection(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            BSD.NowFactionData.AttackDirection = new Vector2Int(-1,0);
            BSD.CharacterAttackRangePop = true;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            BSD.NowFactionData.AttackDirection = new Vector2Int(1,0);
            BSD.CharacterAttackRangePop = true;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            BSD.NowFactionData.AttackDirection = new Vector2Int(0,1);
            BSD.CharacterAttackRangePop = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            BSD.NowFactionData.AttackDirection = new Vector2Int(0,-1);
            BSD.CharacterAttackRangePop = true;
        }
    }
    public void Attack(int x, int z, ref GameObject Move_Character, ref bool Character_move){
        // 攻撃処理

        foreach (GameObject subject in BSD.NowFactionData.subjectCharacter)
        {
            Status SubjectStatus = subject.gameObject.GetComponent<Status>();
            //いい感じにダメー時調整をする
            double D_ATK = BSD.NowFactionData.NowMoveChara.Attack * 0.7 * BSD.NowFactionData.NowMoveChara.attackMenu.AttackPower * 0.3 * 0.1;
            int ATK = (int)D_ATK;
            Debug.Log($"{SubjectStatus.gameObject.name}HP： {SubjectStatus.HP} - {ATK}(double：{D_ATK}) = {SubjectStatus.HP - ATK}");
            SubjectStatus.HP -= ATK;

            if (SubjectStatus.HP <= 0)
            {
                Destroy(SubjectStatus.gameObject);
            }
        }

        BSD.stagedata[BSD.NowFactionData.MoveCharacter_position.x][BSD.NowFactionData.MoveCharacter_position.y] = false;
        BSD.stagedata[x][z] = true;
        BSD.NowFactionData.MoveCharacter_position = new Vector2Int();
        BSD.NowFactionData.characterToPosition[Move_Character] = new Vector2Int(x ,z);
        BSD.NowFactionData.PositionToCharacter.Remove(new Vector2Int(BSD.NowFactionData.MoveCharacter_position.x,BSD.NowFactionData.MoveCharacter_position.y));
        BSD.NowFactionData.PositionToCharacter.Add(new Vector2Int(x,z), Move_Character);
        Character_move = false;
        BSD.NowFactionData.CharacterMoveCount = 0;
        BSD.NowFactionData.CharactersActionEnd[Move_Character] = false;
        Move_Character = null;

        bool AEB = false;
        foreach(var (id,ActionEndBool) in BSD.NowFactionData.CharactersActionEnd)
        {
            if (ActionEndBool == true)
            {
                AEB = true;
            }
        }
        if (AEB != true)
        {
            Debug.Log($"{BSD.NowFactionData.name}Turn-Switch");
            GF.SwitchTurn();
        }

        BSD.NowFactionData.CharacterMoveCount = 0;
    }
}