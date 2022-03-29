using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonBlast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * 5);
            //time += time * Time.deltaTime;
            //if (time > 2f)
            //{
            other.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 10, 5, ForceMode.Impulse);
            other.transform.localScale -= Vector3.one * (Time.deltaTime * 2);
            Invoke("Retry", 2f);
            //}
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("BeachLevel");
        Time.timeScale = 1;
    }
}
