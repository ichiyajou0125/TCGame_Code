using System.Security.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public GameInData GID;
    public DictionarysData DD;
    public Load load;
    private List<Character_Status> Enemys;
    public event Action IsEnemysSet;
    void Start()
    {
        load.Isloaded += () =>{
            ResetEnemy();
            IsEnemysSet?.Invoke();
        };
    }

    public void ResetEnemy()
    {
        GID.fieldEnemys = new Dictionary<string, List<Character_Status>>();

        for (int i = 1; i < 3; i++)
        {
            Enemys = new List<Character_Status>();

            string FieldName = $"Field{i}";
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            int CharaNumRondom = UnityEngine.Random.Range(1, 7);
            for (int CharaNum = 0; CharaNum < CharaNumRondom; CharaNum++)
            {
                int RandomChara = UnityEngine.Random.Range(0, 4);
                Enemys.Add(DD.Charactersdictionary[RandomChara]);
                Debug.Log($"Field = Field{i}\nCharaNum = {CharaNum} , Enemy = {DD.Charactersdictionary[RandomChara].Charactername}");
            }
            GID.fieldEnemys.Add(FieldName, Enemys);
        }
    }
}