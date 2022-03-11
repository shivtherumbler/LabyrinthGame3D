using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 1.5f)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
}
