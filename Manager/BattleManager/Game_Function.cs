using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Game_Function : MonoBehaviour
{
    public BattleStageData BSD;
    public void init_stageList(int width, int height){
        for(int i = 0; i < height; i++){
            List<bool> row = new List<bool>();
            for(int j = 0; j < width; j++){
                row.Add(false);
            }
            BSD.stagedata.Add(row);
        }
        Debug.Log("stage初期化完了");
    }
    public bool Ismove_place(int x, int z, int befor_x, int befor_z){
        if(befor_x != x || befor_z != z){
            return true;
        }
        else{
            return false;
        }
    }
    public void LookPlace(ref int x, ref int z,ref float clock_time, ref float move_delay, GameObject LookPrace_frame){
        PlaceMove(ref x,ref z,ref clock_time,ref move_delay);
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
    }
    public void PlaceMove(ref int x, ref int z, ref float click_time, ref float move_delay)
    {
        bool isKeyPressed = false;
        float initial_delay = 1.0f;
        float repeat_delay = 0.25f;

        int moveX = 0;
        int moveZ = 0;

        if (Input.GetKey(KeyCode.UpArrow) && IsValidIndex(BSD.stagedata, x - 1, z))
        {
            moveX = -1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && IsValidIndex(BSD.stagedata, x + 1, z))
        {
            moveX = 1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && IsValidIndex(BSD.stagedata, x, z + 1))
        {
            moveZ = 1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && IsValidIndex(BSD.stagedata, x, z - 1))
        {
            moveZ = -1;
            isKeyPressed = true;
        }

        if (isKeyPressed)
        {
            if (click_time == 0)
            {
                x += moveX;
                z += moveZ;
                BSD.GameCamera.transform.position = new Vector3(BSD.GameCamera.transform.position.x + moveX,BSD.GameCamera.transform.position.y,BSD.GameCamera.transform.position.z + moveZ);
                move_delay = initial_delay; 
            }
            else if (click_time >= move_delay)
            {
                x += moveX;
                z += moveZ;
                BSD.GameCamera.transform.position = new Vector3(BSD.GameCamera.transform.position.x + moveX,BSD.GameCamera.transform.position.y,BSD.GameCamera.transform.position.z + moveZ);
                click_time = 0f;
                move_delay = repeat_delay;
            }
            click_time += Time.deltaTime;
        }
        else
        {
            click_time = 0f;
            move_delay = initial_delay;
        }
    }

    public void visualize_WolkRange(GameObject WolkRange_Prefab,GameObject Wolk_Range){
        for(int i = 0; i < BSD.WolkRange_postion.Count; i++){
            GameObject Prefab = Instantiate(WolkRange_Prefab,Wolk_Range.transform);
            Prefab.transform.position = new Vector3(BSD.WolkRange_postion[i][0],-0.01f,BSD.WolkRange_postion[i][1]);
        }
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
    public bool HashHasValue(HashSet<Vector2Int> Hash, int x, int z)
    {
        return Hash.Contains(new Vector2Int(x - 9, z - 9));
    }
    public GameObject GetCharacterAtPostion(Dictionary<Vector2Int,GameObject> PositionToCharacter, Vector2Int position){
        if(PositionToCharacter.TryGetValue(position, out GameObject character)) {
            return character;
        }
        return null;
    }
    public void GetPositionAtCharacter(Dictionary<GameObject,Vector2Int> CharacterToPosition, GameObject Character,ref Vector2Int position){
        if(CharacterToPosition.TryGetValue(Character, out Vector2Int trypostion)){
            position = trypostion;
        }
    }
    public bool GetBoolAtCharacter(Dictionary<GameObject, bool> CharactersActionEnd, GameObject Character){
        if(Character == null){
            return false;
        }
        else if(CharactersActionEnd.TryGetValue(Character, out bool Action)){
            return Action;
        }
        return false;
    }
    public bool IsValidIndex(List<List<bool>> grid, int row, int col)
    {
        return row >= 0 && row < grid.Count &&         // 行数チェック
               col >= 0 && col < grid[row].Count;      // 列数チェック
    }
    public void SwitchTurn()
    {
        BSD.turnBool = (BSD.turnBool == true) ? false : true;
        if (BSD.turnBool)
        {
            BSD.Enemy = BSD.NowFactionData;
            BSD.NowFactionData = BSD.Ally;
        }
        else if (!BSD.turnBool)
        {
            BSD.Ally = BSD.NowFactionData;
            BSD.NowFactionData = BSD.Enemy;
        }
        BSD.NowFactionData.AttackDirection = new Vector2Int(-1, 0);
        BSD.NowFactionData.CharacterMoveCount = 0;

        if (BSD.NowFactionData.Characters.Count != 0)
        {
            if (BSD.NowFactionData.Characters[0] != null)
            {
                foreach (GameObject Character in BSD.NowFactionData.Characters)
                {
                    BSD.NowFactionData.CharactersActionEnd[Character] = true;
                }
            }
        }
    }
    public void Switch_Bool(ref bool Character_move){
        if(Character_move){
            Character_move = false;
        }
        else if(!Character_move){
            Character_move = true;
        }
    }
}
