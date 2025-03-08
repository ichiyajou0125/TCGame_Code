using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharactersDictionary : ScriptableObject
{
    public Dictionary<int, Character_Status> Charactersdictionary;
    public async Task Charactersload(){
        Charactersdictionary = new Dictionary<int, Character_Status>();
        List<Character_Status> allCharacters = new List<Character_Status>();
        int count = 0;

        Debug.Log("ゲーム開始: Character Statusのロードを開始");

        var handle = Addressables.LoadAssetsAsync<Character_Status>("CharacterStatus", character =>
        {
            allCharacters.Add(character);
        });
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded){
            allCharacters.Sort((a, b) => a.CharacterID.CompareTo(b.CharacterID));
            foreach(Character_Status status in allCharacters){
                Charactersdictionary.Add(count,status);
                count++;
            }
        }

        Debug.Log("ゲーム開始: Character Statusのロードが完了");
    }
}
