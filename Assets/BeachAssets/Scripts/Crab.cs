using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crab : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float time = 1;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.transform.position = Vector3.Lerp(other.transform.position, transform.position, Time.deltaTime * 5);
            transform.LookAt(gameObject.transform);
            time += time * Time.deltaTime;
            if (time > 1.5f)
            {
                other.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 10, 5, ForceMode.Impulse);
                other.transform.localScale -= Vector3.one * (Time.deltaTime * 5);
                Invoke("Retry", 1.5f);
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("BeachLevel");
        Time.timeScale = 1;
    }
}
