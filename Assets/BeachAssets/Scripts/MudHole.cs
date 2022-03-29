using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudHole : MonoBehaviour
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
            //isGrounded = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Collider>().isTrigger = true;
            other.transform.position = Vector3.Lerp(other.transform.position, transform.position - Vector3.up, Time.deltaTime * 2);
            Invoke("Retry", 0.5f);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("BeachLevel");
        Time.timeScale = 1;
    }
}
