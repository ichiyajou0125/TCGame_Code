using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapFeildObjectManager : MonoBehaviour
{
    public MapData MD;
    public Canvas UICanvas;
    public GameObject MapPlayer;
    private string MoveSceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isPlayerLook();
        SceneMove();
    }

    public void isPlayerLook()
    {
        if (MD.LoocObj == this.gameObject)
        {
            UICanvas.transform.rotation = MapPlayer.transform.rotation;
        }
        else
        {
            UICanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SceneMove()
    {
        if (MD.LoocObj == this.gameObject)
        {
            MoveSceneName = MD.LoocObj.name.Replace("Object", "");
            Debug.Log($"MoveSceneName = {MoveSceneName}");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(MoveSceneName);
            }
        }
    }
}
