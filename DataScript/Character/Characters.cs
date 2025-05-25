using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "GameData/Characters")]
public class Characters : ScriptableObject
{
    public List<GameObject> Prefab_Characters;
    public GameObject Character_1;
    public GameObject Character_2;
    public GameObject Character_3;
    public GameObject Character_4;

    public void CharacterListAdd(){
        Prefab_Characters = new List<GameObject>();
        Prefab_Characters.Add(Character_1);
        Prefab_Characters.Add(Character_2);
        Prefab_Characters.Add(Character_3);
        Prefab_Characters.Add(Character_4);
    }
}