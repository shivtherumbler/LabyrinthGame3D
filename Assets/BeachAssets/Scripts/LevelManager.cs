using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelId;
    Transform levelParent;
    public GameObject player;
    private const string levelParentTag = "Level";
    private const string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        levelId = 1;
        levelParent = GameObject.FindGameObjectWithTag(levelParentTag).transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
        LoadLevel(levelId);
        player.SetActive(true);
    }

    public void LoadLevel(int levelId)
    {
        GameObject levelObject = Instantiate(Resources.Load<GameObject>("Levels/Level_" + levelId));
        levelObject.transform.SetParent(levelParent);
    }

    public void DeleteLevel(int levelId)
    {
        Destroy(levelParent.GetChild(levelId).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
