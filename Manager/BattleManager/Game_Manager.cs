using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public Character_Function CF;
    public Game_Function GF;
    public RandomEnemy RE;
    public BattleStageData BSD;
    public FactionData Ally;
    public FactionData Enemy;
    public DictionarysData DD;
    public Characters charactersData;
    public GameObject Characters;
    public GameObject LookPrace_frame;
    public GameObject Wolk_Range;
    public GameObject Attack_Range;
    public GameInData GID;
    GameObject Move_Character;
    public Camera MainCamera;
    public GameObject WolkRange_Prefab;
    public GameObject AttackRange_prefab;
    int stage_size = 20;
    int x,z,befor_x,befor_z;
    float clock_time,move_delay;
    bool Character_move;
    public GameObject Enpty_Character;
    bool IsStart;

    void Start()
    {
        IsStart = false;

        RE.IsEnemysSet += () =>{
            Init_game(Characters);
            IsStart = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(IsStart){
            Moving();
            if(Input.GetKeyDown(KeyCode.Space)){
                GF.SwitchTurn();
            }
        }
    }

    void Moving(){
        if(GF.Ismove_place(x, z, befor_x, befor_z)){
            if(BSD.stagedata[x][z] && GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z)) != null){
                Move_Character = GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z));
                BSD.NowFactionData.NowMoveChara = Move_Character.transform.GetComponent<Status>();
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
                CF.walk_range(x,z);
                GF.visualize_WolkRange(WolkRange_Prefab, Wolk_Range);
            }
        }
        if(BSD.stagedata[x][z] && GF.GetCharacterAtPostion(BSD.NowFactionData.PositionToCharacter, new Vector2Int(x,z)) != null && GF.GetBoolAtCharacter(BSD.NowFactionData.CharactersActionEnd,Move_Character)){
            if(Input.GetKeyDown(KeyCode.F)){
                GF.Switch_Bool(ref Character_move);
                GF.GetPositionAtCharacter(BSD.NowFactionData.characterToPosition,Move_Character,ref BSD.NowFactionData.MoveCharacter_position);
            }
        }
        if(Character_move){
            if(Input.GetKeyDown(KeyCode.R)){
                CF.MoveCharacter(Move_Character,Wolk_Range,x,z);
                BSD.NowFactionData.CharacterMoveCount = 1;
                Debug.Log("B : MoveCancel\nA : Attack");
            }
            if(BSD.NowFactionData.CharacterMoveCount == 1){
                if(Input.GetKeyDown(KeyCode.B)){
                    CF.Move_Cancel(ref Move_Character,ref BSD.NowFactionData.MoveCharacter_position,ref Character_move);
                    Move_Character = null;
                    BSD.NowFactionData.MoveCharacter_position = new Vector2Int();
                    Character_move = false;
                    BSD.NowFactionData.CharacterMoveCount = 0;
                }
                if(Input.GetKeyDown(KeyCode.A)){
                    BSD.NowFactionData.AttackPostions = new List<Vector2Int>();
                    BSD.NowFactionData.CharacterMoveCount = 2;
                }
            }
            else if(BSD.NowFactionData.CharacterMoveCount == 2){
                CF.CharacterAttackDirection();
                CF.judge_AttackPosition(x,z,Attack_Range,AttackRange_prefab);
                if(Input.GetKeyDown(KeyCode.B)){
                    foreach(Transform child in Attack_Range.transform){
                        Destroy(child.gameObject);
                    }
                    BSD.NowFactionData.CharacterMoveCount = 1;                    
                }
                if(Input.GetKeyDown(KeyCode.A)){
                    BSD.NowFactionData.CharacterMoveCount = 3;
                }
            }
            if(BSD.NowFactionData.CharacterMoveCount == 3){
                CF.Attack(x,z,ref Move_Character,ref Character_move);
                foreach(Transform child in Attack_Range.transform){
                    Destroy(child.gameObject);
                }
                Character_move = false;
                GF.PrintList(BSD.stagedata);
            }
        }
        if(BSD.NowFactionData.CharacterMoveCount != 2 && BSD.NowFactionData.CharacterMoveCount != 1){
            befor_x = x;
            befor_z = z;
            GF.LookPlace(ref x, ref z, ref clock_time, ref move_delay, LookPrace_frame);
        }
    }

    public void Init_game(GameObject Characters){
        clock_time = 0;
        move_delay = 1.5f;
        Character_move = false;
        BSD.turnBool = true;
        BSD.CharacterAttackRangePop = true;
        BSD.turnNum = 0;
        BSD.stagedata = new List<List<bool>>();
        GF.init_stageList(stage_size,stage_size);
        BSD.Characters = new List<List<GameObject>>();

        BSD.StartPosition = new List<Vector2Int>();
        BSD.StartPosition.Add(new Vector2Int(10,0));
        BSD.StartPosition.Add(new Vector2Int(9,-7));
        BSD.StartPosition.Add(new Vector2Int(6,10));

        BSD.GameCamera = MainCamera;
        BSD.GameCamera.transform.position = new Vector3(LookPrace_frame.transform.position.x + 3.5f, LookPrace_frame.transform.position.y + 7.15f, LookPrace_frame.transform.position.z);
        BSD.GameCamera.transform.rotation = Quaternion.Euler(60.0f,-90.0f,0.0f);

        BSD.Ally = Ally;
        BSD.Enemy = Enemy;
        BSD.NowFactionData = BSD.Ally;
        string NowSceneName = SceneManager.GetActiveScene().name;

        charactersData.Prefab_Characters = new List<GameObject>();
        charactersData.CharacterListAdd();

        GameInField fieldData = GID.fields.Find(f => f.FieldName == NowSceneName);

        List<Character_Status> taget = fieldData.characters;
        foreach(Transform child in Characters.transform){
            BSD.NowFactionData.Characters = new List<GameObject>();
            BSD.NowFactionData.judge_WolkRange_postion = new HashSet<Vector2Int>();
            BSD.NowFactionData.PositionToCharacter = new Dictionary<Vector2Int,GameObject>();
            BSD.NowFactionData.characterToPosition = new Dictionary<GameObject,Vector2Int>();
            BSD.NowFactionData.CharactersActionEnd = new Dictionary<GameObject, bool>();
            BSD.NowFactionData.AttackPostions = new List<Vector2Int>();
            BSD.NowFactionData.subjectCharacter = new List<GameObject>();
            BSD.NowFactionData.CharacterMoveCount = 0;
            int count = 0;
            foreach(var Character in taget){
                GameObject AddCharacter = Instantiate(Enpty_Character,child);
                Instantiate(charactersData.Prefab_Characters[Character.CharacterID], AddCharacter.transform);
                AddCharacter.transform.position = new Vector3(BSD.StartPosition[count].x, 1, BSD.StartPosition[count].y);
                AddCharacter.transform.GetChild(0).gameObject.transform.position = new Vector3(BSD.StartPosition[count].x, 1, BSD.StartPosition[count].y);
                AddCharacter.name = Character.Charactername;
                var Status = AddCharacter.GetComponent<Status>();
                Status.HP = Character.HP;
                Status.Attack = Character.Attack;
                Status.defense = Character.defense;
                Status.attackMenu = DD.Attacksdictionary[Character.AttackMenu];
                Status.moveRange = Character.moveRange;

                BSD.NowFactionData.Characters.Add(AddCharacter);
                BSD.NowFactionData.CharactersActionEnd.Add(AddCharacter, false);
                count++;
            }
            
            BSD.Characters.Add(BSD.NowFactionData.Characters);
            for(int i = 0; i < BSD.NowFactionData.Characters.Count; i++){
                CF.get_postion(BSD.NowFactionData.Characters[i],ref x,ref z);
            }
            foreach(GameObject Character in BSD.NowFactionData.Characters){
                BSD.NowFactionData.CharactersActionEnd[Character] = true;
            }

            taget = GID.fieldEnemys[NowSceneName];
            BSD.StartPosition = new List<Vector2Int>();
            BSD.StartPosition.Add(new Vector2Int(5,-3));

            GF.SwitchTurn();
        }
        x = (int)BSD.Ally.Characters[0].transform.position.x + 9;
        z = (int)BSD.Ally.Characters[0].transform.position.z + 9;
        LookPrace_frame.transform.position = new Vector3(x-9,0,z-9);
        Transform AllyChara = Characters.transform.GetChild(0);
        BSD.NowFactionData.NowMoveChara = AllyChara.GetChild(0).GetComponent<Status>();
        CF.walk_range(x,z);
        GF.visualize_WolkRange(WolkRange_Prefab,Wolk_Range);
    }

    public void AddCharacter(Transform child, FieldData field){

    }
}
