using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoad : MonoBehaviour
{
    public Load load;
    public SaveData SD;
    

    // Start is called before the first frame update
    void Start()
    {
        load.Isloaded += () =>{
            Datamove();
        };
    }

    void Datamove(){
        foreach(FieldData FD in load.SV.fields){
            SD.fields.Add(FD);
        }
    }
}
