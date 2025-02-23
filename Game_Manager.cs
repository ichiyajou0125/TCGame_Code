using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Character_Manager CM;
    public GameData gameData;
    GameObject Character;
    public GameObject LookPrace_frame;
    public GameObject WolkRange_Prefab;
    public GameObject Wolk_Range;
    int stage_size = 20;
    int x,z;
    int Wolk_width = 3;
    List<List<int>> WolkRange_postion;
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
        CM.get_postion(Character,gameData.stagedata,ref x,ref z);
        CM.get_postion(Character,gameData.stagedata,ref x, ref z);
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.stagedata[x][z]){
            CM.walk_range(gameData.stagedata,x,z,ref Wolk_width,ref WolkRange_postion);
            CM.visualize_WolkRange(WolkRange_postion,stage_size,WolkRange_Prefab,Wolk_Range);
        }
        else{
            foreach(Transform child in Wolk_Range.transform){
                Destroy(child.gameObject);
            }
        }
        CM.LookPlace(gameData.stagedata,ref x,ref z,LookPrace_frame);
        CM.PrintList(gameData.stagedata);
    }
}
