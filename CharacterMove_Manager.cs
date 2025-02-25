using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMove_Manager : MonoBehaviour
{
    public Character_Function CF;
    public Game_Function GF;
    public GameData gameData;
    GameObject Characters;
    GameObject LookPrace_frame;
    GameObject Wolk_Range;
    GameObject Move_Character;
    public GameObject WolkRange_Prefab;
    int stage_size = 20;
    int x,z,befor_x,befor_z;
    float clock_time,move_delay;
    int Wolk_width = 3;
    bool Character_move;
    List<List<int>> WolkRange_postion;
    List<bool> CharactersMoved;
    HashSet<Vector2Int> jage_WolkRange_postion;
    Vector2Int MoveCharacter_position;

    void Start()
    {
        LookPrace_frame = GameObject.Find("LookPrace_object");
        Wolk_Range = GameObject.Find("Wolk_Range");
        Characters = GameObject.Find("Characters");
        Init_stage(Characters,ref Wolk_width);
    }

    // Update is called once per frame
    void Update()
    {
        Moving(ref Wolk_width);
    }

    void Moving(ref int Wolk_Width){
                if(GF.Ismove_place(x, z, befor_x, befor_z)){
            if(!Character_move){
                foreach(Transform child in Wolk_Range.transform){
                    Destroy(child.gameObject);
                }
                jage_WolkRange_postion = new HashSet<Vector2Int>();
            }
            if(gameData.stagedata[x][z] && !Character_move){
                CF.walk_range(gameData.stagedata,ref WolkRange_postion,ref jage_WolkRange_postion,x,z,ref Wolk_Width);
                GF.visualize_WolkRange(WolkRange_postion, stage_size, WolkRange_Prefab, Wolk_Range);
            }
        }
        if(gameData.stagedata[x][z]){
            if(Input.GetKeyDown(KeyCode.F)){
            CF.jage_CharacterMove(ref Character_move);
            Move_Character = GF.GetCharacterAtPostion(gameData.PositionToCharacter,new Vector2Int(x,z));
            GF.GetPositionAtCharacter(gameData.characterToPosition,Move_Character,ref MoveCharacter_position);
            }
        }
        if(Character_move){
            if(Input.GetKeyDown(KeyCode.R)){
                CF.MoveCharacter(gameData.stagedata,gameData.PositionToCharacter,gameData.characterToPosition,jage_WolkRange_postion,Move_Character,MoveCharacter_position,Wolk_Range,x,z);
                Move_Character = new GameObject();
                MoveCharacter_position = new Vector2Int();
                Character_move = false;
                GF.PrintList(gameData.stagedata);
            }
        }
        befor_x = x;
        befor_z = z;
        GF.LookPlace(gameData.stagedata, ref x, ref z, ref clock_time, ref move_delay, LookPrace_frame);
    }

    void Init_stage(GameObject Characters,ref int Wolk_Width){
        clock_time = 0;
        move_delay = 1.5f;
        Character_move = false;
        gameData.My_Characters = new List<GameObject>();
        jage_WolkRange_postion = new HashSet<Vector2Int>();
        gameData.PositionToCharacter = new Dictionary<Vector2Int,GameObject>();
        gameData.characterToPosition = new Dictionary<GameObject,Vector2Int>();
        CharactersMoved = new List<bool>();
        foreach(Transform child in Characters.transform){
            gameData.My_Characters.Add(child.gameObject);
            CharactersMoved.Add(true);
        }
        gameData.stagedata = new List<List<bool>>();
        GF.init_stageList(gameData.stagedata,stage_size,stage_size);
        for(int i = 0; i < gameData.My_Characters.Count; i++){
            CF.get_postion(gameData.My_Characters[i],gameData.stagedata,gameData.PositionToCharacter,gameData.characterToPosition,ref x,ref z);
        }
        x = (int)gameData.My_Characters[0].transform.position.x + 9;
        z = (int)gameData.My_Characters[0].transform.position.z + 9;
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
        CF.walk_range(gameData.stagedata,ref WolkRange_postion,ref jage_WolkRange_postion,x,z,ref Wolk_Width);
        GF.visualize_WolkRange(WolkRange_postion,stage_size,WolkRange_Prefab,Wolk_Range);
    }
}
