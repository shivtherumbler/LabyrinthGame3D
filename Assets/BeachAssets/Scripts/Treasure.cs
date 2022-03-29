using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public float keycollected = 0;
    public int levelId = 1;
    public LevelManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null)
        {
            manager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (keycollected == 3)
            {
                gameObject.GetComponent<Animator>().enabled = true;
                keycollected = 0;
                Invoke("NextLevel", 3);
            }
        }
    }

    public void NextLevel()
    {
        manager.DeleteLevel(0);
        levelId++;
        manager.LoadLevel(levelId);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-8.17000008f, 2.88000011f, 7.09000015f);
        //isGrounded = false;
    }
}
