using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using _2ToTango;
using System;
using System.IO;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	
	private bool gameOver;
	private bool restart;
	private int score;

    public JiraTicketCollection Tickets = new JiraTicketCollection();
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

        // Load Jira Tickets
        var path = Directory.GetCurrentDirectory() + "\\Assets\\Data\\Tickets.xml";
        Tickets = JiraTicketCollection.Load(path);
	}
	
	void Update ()
	{
        if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
        if (Input.GetKeyDown(KeyCode.Q) ||
            Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
                var index = UnityEngine.Random.Range(0, hazards.Length);
                GameObject hazard = hazards [index];
                AssignJiraTicket(hazard, i);

                Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

    private void AssignJiraTicket(GameObject hazard, int i)
    {
        var ticket = GetTicket();
        var ticketKey = (ticket != null) ? ticket.Key : "ADVB-????";
        var tm = hazard.GetComponent<TextMesh>();
        if (tm != null)
        {
            tm.text = ticketKey + i.ToString();
        }
        else
        {
            tm = hazard.AddComponent<TextMesh>();
            tm.text = ticketKey + i.ToString();
        }
    }

    public JiraTicket GetTicket()
    {
        if (Tickets != null &&
            Tickets.Tickets != null)
        {
            int index = UnityEngine.Random.Range(0, this.Tickets.Tickets.Count);
            return Tickets.Tickets[index];
        }
        else return null;
    }
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "You SUCK - Failed Jira Tasks!";
		gameOver = true;
	}
}