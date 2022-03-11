using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    InputModule currentModule;
    Vector2 input;
    Vector3 move;
    private float moveSpeed = 4f;
    public Rigidbody rb;
    public bool isGrounded;
    public float time = 1;
    public float keycollected = 0;
    public GameObject treasure;
    public LevelManager manager;
    public int levelId = 1;
    public bool isPaused;
    public GameObject pausemenu;

    void Start()
    {
        move = Vector3.zero;
        rb = GetComponent<Rigidbody>();

#if UNITY_EDITOR
        currentModule = new KeyboardInput();
#elif UNITY_ANDROID || UNITY_IOS
                currentModule = new MobileInput();
#else
                currentModule = new KeyboardInput();
#endif

    }

    private void FixedUpdate()
    {

        if (isGrounded)
        {
            input = currentModule.GetInput();

            move.x = input.x;
            move.z = input.y;
            rb.velocity = move * moveSpeed * input.magnitude;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;

        }

        if(collision.gameObject.tag == "Treasure")
        {
            if(keycollected == 3)
            {
                collision.gameObject.GetComponent<Animator>().enabled = true;
                keycollected = 0;
                Invoke("NextLevel", 3);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bomb")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("attack01");
        }

        if(other.tag == "Key")
        {
            keycollected++;
            Destroy(other.gameObject);
        }

        
    }

    public void PausePlay()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausemenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pausemenu.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Water")
        {
            isGrounded = false;
            rb.isKinematic = true;
            gameObject.GetComponent<Collider>().isTrigger = true;
            transform.position = Vector3.Lerp(transform.position, other.transform.position - Vector3.up, Time.deltaTime * 2);
            Invoke("Retry", 0.5f);
        }

        if (other.tag == "Bomb")
        {
            transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * 5);
            time += time * Time.deltaTime;
            if (time > 2.5f)
            {
                rb.AddExplosionForce(100, other.transform.position, 10, 5, ForceMode.Impulse);
                transform.localScale -= Vector3.one * (Time.deltaTime * 2);
                Invoke("Retry", 2);
            }
        }
        if(other.tag == "Crab")
        {
            //transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * 5);
            other.transform.LookAt(gameObject.transform);
            time += time * Time.deltaTime;
            if (time > 1.5f)
            {
                rb.AddExplosionForce(50, other.transform.position, 10, 5, ForceMode.Impulse);
                transform.localScale -= Vector3.one * (Time.deltaTime * 5);
                Invoke("Retry", 1.5f);
            }
        }

        if (other.tag == "Bullet")
        {
            //transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * 5);
            //time += time * Time.deltaTime;
            //if (time > 2f)
            //{
            rb.AddExplosionForce(50, other.transform.position, 10, 5, ForceMode.Impulse);
            transform.localScale -= Vector3.one * (Time.deltaTime * 2);
            Invoke("Retry", 2);
            //}
        }
    }
        /*private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Obstacle")
            {
                Handheld.Vibrate();
            }
        }*/
    
    public void Open(GameObject open)
    {
        open.SetActive(true);
    }

    public void Close(GameObject close)
    {
        close.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        manager.DeleteLevel(0);
        levelId++;
        manager.LoadLevel(levelId);
        gameObject.transform.position = new Vector3(-8.17000008f, 2.88000011f, 7.09000015f);
        isGrounded = false;
    }
}