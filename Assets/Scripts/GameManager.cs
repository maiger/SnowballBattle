using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // TODO: Maybe create a class out of players if things get more complex in the future
    public GameObject player1;
    public GameObject player2;

    public int P1Life;
    public int P2Life;

    // TODO: Pass the winner information a bit more smartly to the game over screen..
    public GameObject p1Wins;
    public GameObject p2Wins;

    public GameObject[] p1Sticks;
    public GameObject[] p2Sticks;

    public AudioSource hurtSound;

    public string mainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    // TODO: Cleanup to work with new HUD and player controller system
	void Update () {
        if (P1Life <= 0 || GameObject.FindGameObjectWithTag("Player1") != null && GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().health <= 0)
        {
            player1.SetActive(false);
            p2Wins.SetActive(true);
        }

        if (P2Life <= 0)
        {
            player2.SetActive(false);
            p1Wins.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenu);
        }
    }

    // TODO: Remove
    public void HurtP1()
    {
        //P1Life -= 1;

        for(int i = 0; i < p1Sticks.Length; i++)
        {
            if(P1Life > i)
            {
                p1Sticks[i].SetActive(true);
            } else
            {
                p1Sticks[i].SetActive(false);
            }

            hurtSound.Play();
        }
    }

    public void HurtP2()
    {
        P2Life -= 1;

        for (int i = 0; i < p2Sticks.Length; i++)
        {
            if (P2Life > i)
            {
                p2Sticks[i].SetActive(true);
            }
            else
            {
                p2Sticks[i].SetActive(false);
            }
        }

        hurtSound.Play();
    }
}
