using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testere_kontrol : MonoBehaviour
{
    Rigidbody rgd;
    public float hiz = 3;
    
    
    float minx=-7.9f;
    float maxx=9.4f;
    public GameObject[] wood;

    public GameObject efekt;
    float spawnTime=0;

    public kameraShake kameraShake;

    public AudioSource sawvoice;

    public Text score;
    int skor=0;
    public Text highscore;

    GameObject pausebuton,devambuton,tekrarbuton,quitbuton;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        pausebuton = GameObject.FindGameObjectWithTag("pause");
        devambuton = GameObject.FindGameObjectWithTag("devam");
        tekrarbuton = GameObject.FindGameObjectWithTag("tekrar");
        quitbuton = GameObject.FindGameObjectWithTag("quit");

        devambuton.SetActive(false);
        tekrarbuton.SetActive(false);
        quitbuton.SetActive(false);

        score.text = "SCORE : " + skor;
        highscore.text = "HIGH SCORE : " + PlayerPrefs.GetInt("HighScore").ToString();
        rgd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        highscore.text = "HIGH SCORE : " + PlayerPrefs.GetInt("HighScore").ToString();

        if (PlayerPrefs.GetInt("HighScore") < skor)
        {
            PlayerPrefs.SetInt("HighScore", skor);
            highscore.text = "HIGH SCORE : " + PlayerPrefs.GetInt("HighScore").ToString();
        }


        spawn();
        move();
    }
    private void FixedUpdate()
    {
        transform.Rotate(0,0,6);
    }
    void move()//testere hareketi
    {
        /*float horizontal = Input.GetAxis("Horizontal");
        Vector3 vec = new Vector3(horizontal, 0, 0);
        vec.Normalize();
        rgd.velocity = vec * hiz;
        transform.position = new Vector3(Mathf.Clamp(rgd.position.x, minx, maxx),
        rgd.position.y, rgd.position.z);*/

        if (Input.anyKey)
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
               transform.position = new Vector3(Mathf.Clamp(rgd.position.x - 0.3f, minx, maxx),
               rgd.position.y, rgd.position.z);
            }
            if (Input.mousePosition.x > Screen.width / 2)
            {
                transform.position = new Vector3(Mathf.Clamp(rgd.position.x + 0.3f, minx, maxx),
                rgd.position.y, rgd.position.z);
            }
        }
        



    }
    void spawn()//odunların spawnlanması
    {
        
        spawnTime += Time.deltaTime;
        if (spawnTime > 3)
        {
            
            int random = Random.Range(0, wood.Length);
            Instantiate(wood[random],new Vector3(Random.Range(-4,5), 7, 53),Quaternion.identity);
            
            spawnTime = 0;
        }
        
    }
    
    void OnTriggerEnter(Collider other)//yeşil parçaların kırılması ve yok olması
    {
        
        if (other.gameObject.tag=="yesil")
        {
            skor += 10;
            score.text = "SCORE : " + skor;
            sawvoice.Play();
            StartCoroutine(kameraShake.Shake(0.2f,0.4f));
            
            GameObject efektim = Instantiate(efekt, other.transform.position, Quaternion.identity);
            Destroy(efektim, 5);
            Destroy(other.gameObject, 0.025f);
 
        }
        
    }
    public void butonsecim(int buton)
    {
        if (buton==0)
        {
            Time.timeScale = 0;
            pausebuton.GetComponent<Button>().interactable = false;
            devambuton.SetActive(true);
            quitbuton.SetActive(true);
            tekrarbuton.SetActive(true);
        }
        else if (buton == 1)
        {
            Time.timeScale = 1;
            pausebuton.GetComponent<Button>().interactable = true;
            devambuton.SetActive(false);
            quitbuton.SetActive(false);
            tekrarbuton.SetActive(false);
        }
        else if (buton==2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (buton == 3)
        {
            Application.Quit();
        }
    }
}
