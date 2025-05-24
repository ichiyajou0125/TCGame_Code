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
    public event Action Isloaded;
    public void Awake()
    {
        CAR.AttackRangesload();
        loadJson();
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

    public void PostionInit(){
        DD.StartPostion = new Dictionary<string, List<Vector2Int>>();

        foreach(var field in SV.fields){

        }
    }
}
