using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Character_Manager CM;
    public GameData gameData;
    GameObject Character;
    public GameObject WolkRange_Prefab;

    int stage_size = 20;
    int x,z;

    void Start()
    {
        Character = GameObject.Find("Capsule");
        gameData.stagedata = new List<List<bool>>();
        for(int i = 0; i < stage_size; i++){
            List<bool> row = new List<bool>();
            for(int j = 0; j < stage_size; j++){
                row.Add(false);
            }
            gameData.stagedata.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            CM.get_postion(Character,gameData.stagedata,ref x,ref z);
            CM.walk_range(gameData.stagedata,x,z,stage_size,WolkRange_Prefab);
            CM.PrintList(gameData.stagedata);
        }
    }
}
