using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using System;

public class Load : MonoBehaviour
{
    public DictionarysData DD;
    public CharacterAtacckRanges CAR;
    public SaveData SV;
    public GameInData GID;
    private string FieldName;
    private string SubjectName;
    private Dictionary<string, List<Vector2Int>> Positions; 
    private List<Vector2Int> STPosition;
    public event Action Isloaded;
    public void Awake()
    {
        CAR.AttackRangesload();
        loadJson();
        PostionInit();
    }
    async void Start()
    {
        await DD.Charactersload();
        await DD.AttackMenuLoad();


        Isloaded?.Invoke();
    }

    public void loadJson(){
        string path = Path.Combine(Application.dataPath, "script/code/DataScript/Jsons/Characters.json");

        GID.fieldEnemys = new Dictionary<string, List<Character_Status>>();
        GID.fields = new List<GameInField>();

        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            SV = JsonUtility.FromJson<SaveData>(jsonText);
        }
        foreach(var field in SV.fields){
            GameInField GIF = new GameInField();
            List<Character_Status> CharactersStatus = new List<Character_Status>();
            GIF.FieldName = field.FieldName;
            GIF.isTerritory = field.isTerritory;

            foreach(var Character in field.characters){
                Character_Status CS = ScriptableObject.CreateInstance<Character_Status>();
                CS.CharacterID = Character.CharacterID;
                CS.Charactername = Character.Charactername;
                CS.Attack = Character.Attack;
                CS.defense = Character.defense;
                CS.HP = Character.HP;
                CS.AttackMenu = Character.AttackMenu;
                CS.moveRange = Character.moveRange;
                CharactersStatus.Add(CS);
            }
            GIF.characters = CharactersStatus;

            GID.fields.Add(GIF);
        }
    }

    public void PostionInit()
    {
        DD.StartPostion = new Dictionary<string, Dictionary<string, List<Vector2Int>>>();

        Positions = new Dictionary<string, List<Vector2Int>>();
        STPosition = new List<Vector2Int>();
        FieldName = "Field1";
        SubjectName = "Ally";

        STPosition.Add(new Vector2Int(10, 0));
        STPosition.Add(new Vector2Int(9, -7));
        STPosition.Add(new Vector2Int(6, 10));
        STPosition.Add(new Vector2Int(10, 1));
        STPosition.Add(new Vector2Int(10, -1));
        STPosition.Add(new Vector2Int(8, 7));
        Positions.Add(SubjectName, STPosition);

        STPosition = new List<Vector2Int>();
        SubjectName = "Enemy";
        STPosition.Add(new Vector2Int(5, -3));
        STPosition.Add(new Vector2Int(3, 2));
        STPosition.Add(new Vector2Int(3, 1));
        STPosition.Add(new Vector2Int(4, -1));
        STPosition.Add(new Vector2Int(2, 0));
        STPosition.Add(new Vector2Int(3, -2));
        Positions.Add(SubjectName, STPosition);

        DD.StartPostion.Add(FieldName, Positions);

        Positions = new Dictionary<string, List<Vector2Int>>();
        STPosition = new List<Vector2Int>();
        FieldName = "Field2";
        SubjectName = "Ally";

        STPosition.Add(new Vector2Int(9, -10));
        STPosition.Add(new Vector2Int(9, -9));
        STPosition.Add(new Vector2Int(9, -8));
        STPosition.Add(new Vector2Int(10, 9));
        STPosition.Add(new Vector2Int(10, 8));
        STPosition.Add(new Vector2Int(10, 7));
        Positions.Add(SubjectName, STPosition);

        STPosition = new List<Vector2Int>();
        SubjectName = "Enemy";
        STPosition.Add(new Vector2Int(-9, 10));
        STPosition.Add(new Vector2Int(-9, 9));
        STPosition.Add(new Vector2Int(-9, 8));
        STPosition.Add(new Vector2Int(-10, -9));
        STPosition.Add(new Vector2Int(-10, -8));
        STPosition.Add(new Vector2Int(-10, -7));
        Positions.Add(SubjectName, STPosition);
        DD.StartPostion.Add(FieldName, Positions);
    }
}
