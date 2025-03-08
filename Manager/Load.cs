using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Load : MonoBehaviour
{
    public CharactersDictionary CD;
    async void Start()
    {
        await CD.Charactersload();
    }
}
