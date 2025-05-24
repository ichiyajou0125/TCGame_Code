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
    private List<string> FieldName;
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
        FieldName = new List<string>();
        Enemys = new List<Character_Status>();
        GID.fieldEnemys = new Dictionary<string, List<Character_Status>>();

        FieldName.Add("Field1");
        Enemys.Add(DD.Charactersdictionary[3]);
        foreach(var field in FieldName){
            GID.fieldEnemys.Add(field,Enemys);
        }        
    }
}
