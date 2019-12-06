using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOvertext;
    public Text winText;
    public Text hardText;
    public Text timeText;
    public Text timerText;
    public Text actualTimetext;


    private bool gameOver;
    private bool restart;
    private int score;

    public AudioClip win;
    public AudioClip lose;
    private AudioSource audioSource;

    public float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    private bool tAttack = false;


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOvertext.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        audioSource = GetComponent<AudioSource>();
        hardText.text = "Press Space for Hard Mode (Double Time)!";
        timeText.text = "Press G for Time Attack!";
        timerText.text = "";
        timer = mainTimer;
        actualTimetext.text = "";


    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hardText.text = "DOUBLE TIME!!!";
            Time.timeScale = 2f;
        }

        
    }

    public void ResetBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce =false;
    }

    private void Update()
    {

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.T))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("CompletedChallenge3");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            tAttack = true;
            UpdateScore();
            timeText.text = "Time Attack!!!";
            actualTimetext.text = "Time: ";
        }

        if (tAttack == true)
        {
            

            if (timer >= 0.0f && canCount)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F");

            }

            else if (timer <= 0.0f && !doOnce)
            {
                canCount = false;
                doOnce = true;
                timerText.text = "0.00";
                timer = 0.0f;
            }

            if (timerText.text == "0.00")
            {
                gameOvertext.text = "Game Over!";
                gameOver = true;

                audioSource.clip = lose;
                audioSource.Play();
                audioSource.loop = false;
            }
        }
        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            score = 0;
        }

        ScoreText.text = "Points: " + score;
        if (tAttack == false)
        {
            if (score >= 100)
            {
                winText.text = "You win! Game created by Gary Carrasco!";
                gameOver = true;
                restart = true;

                audioSource.clip = win;
                audioSource.Play();
                audioSource.loop = false;
            }
        }

    
    }

    public void GameOver()
    {
        if (score >= 100)
        {
            gameOvertext.text = "";
        }
        else
        {
            gameOvertext.text = "Game Over!";
            gameOver = true;

            audioSource.clip = lose;
            audioSource.Play();
            audioSource.loop = false;
        }
    }
}