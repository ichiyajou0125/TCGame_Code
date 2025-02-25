using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Game_Function : MonoBehaviour
{
    public Character_Function CF;
    public void init_stageList(List<List<bool>> stage, int width, int height){
        for(int i = 0; i < height; i++){
            List<bool> row = new List<bool>();
            for(int j = 0; j < width; j++){
                row.Add(false);
            }
            stage.Add(row);
        }
    }
    public bool Ismove_place(int x, int z, int befor_x, int befor_z){
        if(befor_x != x || befor_z != z){
            return true;
        }
        else{
            return false;
        }
    }
        public void LookPlace(List<List<bool>> stage, ref int x, ref int z,ref float clock_time, ref float move_delay, GameObject LookPrace_frame){
        PlaceMove(stage,ref x,ref z,ref clock_time,ref move_delay);
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
    }
    public void PlaceMove(List<List<bool>> stage, ref int x, ref int z, ref float click_time, ref float move_delay)
    {
        bool isKeyPressed = false;
        float initial_delay = 1.0f;
        float repeat_delay = 0.25f;

        int moveX = 0;
        int moveZ = 0;

        if (Input.GetKey(KeyCode.UpArrow) && CF.IsValidIndex(stage, x - 1, z))
        {
            moveX = -1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && CF.IsValidIndex(stage, x + 1, z))
        {
            moveX = 1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && CF.IsValidIndex(stage, x, z + 1))
        {
            moveZ = 1;
            isKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && CF.IsValidIndex(stage, x, z - 1))
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
                move_delay = initial_delay; 
            }
            else if (click_time >= move_delay)
            {
                x += moveX;
                z += moveZ;
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

    public void visualize_WolkRange(List<List<int>> WolkRange_postion,int stage_size,GameObject WolkRange_Prefab,GameObject Wolk_Range){
        for(int i = 0; i < WolkRange_postion.Count; i++){
            GameObject Prefab = Instantiate(WolkRange_Prefab,Wolk_Range.transform);
            Prefab.transform.position = new Vector3(WolkRange_postion[i][0],-0.01f,WolkRange_postion[i][1]);
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
}
