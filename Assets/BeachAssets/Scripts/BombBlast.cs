using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombBlast : MonoBehaviour
{
    public float time = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<Animator>().SetTrigger("attack01");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = Vector3.Lerp(other.transform.position, transform.position, Time.deltaTime * 5);
            time += time * Time.deltaTime;
            if (time > 2.5f)
            {
                other.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10, 5, ForceMode.Impulse);
                other.transform.localScale -= Vector3.one * (Time.deltaTime * 2);
                Invoke("Retry", 2);
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("BeachLevel");
        Time.timeScale = 1;
    }
}
