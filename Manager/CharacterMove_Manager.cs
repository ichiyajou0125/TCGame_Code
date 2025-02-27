using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMove_Manager : MonoBehaviour
{
    public Character_Function CF;
    public Game_Function GF;
    public BattleStageData BSD;
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

    void Start()
    {
        LookPrace_frame = GameObject.Find("LookPrace_object");
        Wolk_Range = GameObject.Find("Wolk_Range");
        Characters = GameObject.Find("Characters");
        Init_game(Characters,ref Wolk_width);
    }

    // Update is called once per frame
    void Update()
    {
        Moving(ref Wolk_width);
        if(Input.GetKeyDown(KeyCode.Space)){
            GF.SwitchTurn();
        }
    }

    void Moving(ref int Wolk_Width){
        if(GF.Ismove_place(x, z, befor_x, befor_z)){
            if(BSD.stagedata[x][z] && GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z)) != null){
                Move_Character = GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z));
                Debug.Log($"Moveing : {Move_Character.name}");
            }
            else if(Move_Character != null && !Character_move){
                Move_Character = null;
            }

            if(!Character_move){
                foreach(Transform child in Wolk_Range.transform){
                    Destroy(child.gameObject);
                }
                BSD.NowFactionData.judge_WolkRange_postion = new HashSet<Vector2Int>();
            }
            if(BSD.stagedata[x][z] && !Character_move && GF.GetBoolAtCharacter(BSD.NowFactionData.CharactersActionEnd,Move_Character) && GF.GetBoolAtCharacter(BSD.NowFactionData.CharactersActionEnd,Move_Character)){
                CF.walk_range(x,z,ref Wolk_Width);
                GF.visualize_WolkRange(WolkRange_Prefab, Wolk_Range);
            }
        }
        if(BSD.stagedata[x][z] && GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z)) != null && GF.GetBoolAtCharacter(BSD.NowFactionData.CharactersActionEnd,Move_Character)){
            if(Input.GetKeyDown(KeyCode.F)){
                Debug.Log($"Moveing : {Move_Character.name}");
                GF.Switch_Bool(ref Character_move);
                GF.GetPositionAtCharacter(BSD.NowFactionData.characterToPosition,Move_Character,ref BSD.NowFactionData.MoveCharacter_position);
            }
        }
        if(Character_move){
            if(Input.GetKeyDown(KeyCode.R)){
                CF.MoveCharacter(Move_Character,BSD.NowFactionData.MoveCharacter_position,Wolk_Range,x,z);
                BSD.NowFactionData.CharactersActionEnd[Move_Character] = false;
                Move_Character = null;
                BSD.NowFactionData.MoveCharacter_position = new Vector2Int();
                Character_move = false;
            }
        }
        befor_x = x;
        befor_z = z;
        GF.LookPlace(ref x, ref z, ref clock_time, ref move_delay, LookPrace_frame);
    }

    void Init_game(GameObject Characters,ref int Wolk_Width){
        clock_time = 0;
        move_delay = 1.5f;
        Character_move = false;
        BSD.turnBool = true;
        BSD.turnNum = 0;
        BSD.stagedata = new List<List<bool>>();
        GF.init_stageList(stage_size,stage_size);
        BSD.Characters = new List<List<GameObject>>();
        BSD.NowFactionData = BSD.Ally;
        foreach(Transform child in Characters.transform){
            BSD.NowFactionData.Characters = new List<GameObject>();
            BSD.NowFactionData.judge_WolkRange_postion = new HashSet<Vector2Int>();
            BSD.NowFactionData.PositionToCharacter = new Dictionary<Vector2Int,GameObject>();
            BSD.NowFactionData.characterToPosition = new Dictionary<GameObject,Vector2Int>();
            BSD.NowFactionData.CharactersActionEnd = new Dictionary<GameObject, bool>();
            foreach(Transform grandchild in child.transform){
                BSD.NowFactionData.Characters.Add(grandchild.gameObject);
                BSD.NowFactionData.CharactersActionEnd.Add(grandchild.gameObject, false);
            }
            BSD.Characters.Add(BSD.NowFactionData.Characters);
            for(int i = 0; i < BSD.NowFactionData.Characters.Count; i++){
                CF.get_postion(BSD.NowFactionData.Characters[i],ref x,ref z);
            }
            GF.SwitchTurn();
        }
        x = (int)BSD.Ally.Characters[0].transform.position.x + 9;
        z = (int)BSD.Ally.Characters[0].transform.position.z + 9;
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
        CF.walk_range(x,z,ref Wolk_Width);
        GF.visualize_WolkRange(WolkRange_Prefab,Wolk_Range);
    }
}
