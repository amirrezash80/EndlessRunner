using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static int difficulty;

    private float score;
    public Text scoreUI;
    private int highscore;
    public Text highscoreUI; // Reference to the new highscore UI text

    public Transform player;
    public PlayerMovement movement;

    public GameObject obstaclePrefab;
    public Transform obstacles;
    public int obstacleStartX = 100;

    public GameObject deathOverlayUI;

    public Fade fade;

    IEnumerator DeathOverlayTransition()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(fade.BeginFade(1));

        deathOverlayUI.SetActive(true);

        yield return new WaitForSeconds(fade.BeginFade(-1));
    }

    IEnumerator SceneTransition(int scene)
    {
        yield return new WaitForSeconds(fade.BeginFade(1));

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void SwitchScene(int scene)
    {
        StartCoroutine(SceneTransition(scene));
    }

    public void InitiateDeath()
    {
        CancelInvoke("Spawn");

        FindObjectOfType<PlayerMovement>().enabled = false;

        foreach (Transform obstacle in obstacles)
        {
            obstacle.gameObject.GetComponent<ObstacleMovement>().enabled = false;
        }

        UpdateHighscore();
        highscoreUI.text = " highscore: " + highscore;

        StartCoroutine(DeathOverlayTransition());
    }

    private void UpdateHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");

        if (score > highscore)
        {
            highscore = (int)score;
            PlayerPrefs.SetInt("Highscore" , highscore);
        }
    }

    private void Spawn()
    {
        int i;

        // Spawn 2 new obstacles
        for (i = -7; i < 7; i += 7)
        {
            Instantiate(obstaclePrefab,
                        new Vector3(Mathf.Floor(Random.Range(i, i + 7)), 1, obstacleStartX),
                        Quaternion.identity, obstacles);
        }
    }

    private void Update()
    {
        if (FindObjectOfType<PlayerMovement>().enabled)
        {
            score += Time.deltaTime * 10;
            scoreUI.text = "Score: " + (int)score;
            highscoreUI.text = "High Score: " + highscore; // Update high score display
        }

        if (Input.GetKey("r"))
        {
            SwitchScene(1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (deathOverlayUI.activeSelf)
            {
                SwitchScene(0);
            }
            else
            {
                InitiateDeath();
            }
            return;
        }
    }

    private void Start()
    {
        fade.BeginFade(-1);

        // Initialize highscore at the start of the game
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreUI.text = "High Score: " + highscore;

        // Invoke obstacle spawning, frequency depends on difficulty
        InvokeRepeating("Spawn", 1f, 0.5f );
    }
}
