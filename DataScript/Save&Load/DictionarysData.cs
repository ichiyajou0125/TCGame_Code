using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DictionaryData", menuName = "GameData/DictionaryData")]
public class DictionarysData : ScriptableObject
{
    public Dictionary<int, Character_Status> Charactersdictionary;
    public Dictionary<int, AttackMenu> Attacksdictionary;
    public Dictionary<string, Dictionary<string ,List<Vector2Int>>> StartPostion;
    public CharacterAtacckRanges CAR;
    public async Task Charactersload(){
        if (Charactersdictionary == null)
        {
            Charactersdictionary = new Dictionary<int, Character_Status>();
        }
        else
        {
            Charactersdictionary.Clear();
        }
        List<Character_Status> allCharacters = new List<Character_Status>();

        var handle = Addressables.LoadAssetsAsync<Character_Status>("CharacterStatus", character =>
        {
            allCharacters.Add(character);
        });
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded){
            allCharacters.Sort((a, b) => a.CharacterID.CompareTo(b.CharacterID));
            foreach(Character_Status status in allCharacters){
                if (!Charactersdictionary.ContainsKey(status.CharacterID))
                {
                    Charactersdictionary.Add(status.CharacterID, status);
                }
            }
        }
    }
    public async Task AttackMenuLoad(){
        if (Attacksdictionary == null)
        {
            Attacksdictionary = new Dictionary<int, AttackMenu>();
        }
        else
        {
            Attacksdictionary.Clear();
        }
        List<AttackMenu> allAttackMenu = new List<AttackMenu>();
        var handle = Addressables.LoadAssetsAsync<AttackMenu>("AttackMenu", Attack =>
        {
            allAttackMenu.Add(Attack);
        });
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded){
            allAttackMenu.Sort((a, b) => a.AttackID.CompareTo(b.AttackID));
            foreach(AttackMenu Attack in allAttackMenu){
                if (!Attacksdictionary.ContainsKey(Attack.AttackID))
                {
                    Attacksdictionary.Add(Attack.AttackID, Attack);
                }
            }
        }
    }
}
