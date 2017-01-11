using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject player;

    public AudioSource hurtSound;

    public string mainMenu;

	// Use this for initialization
	void Start () {
        // TODO: Clean this up
        // Spawn player
        GameObject player1 = (GameObject)Instantiate(player);
        player1.GetComponent<Player>().Initialize("Testi", 5, 10, 30, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.Space, new Vector2(Screen.width / 10, Screen.height - Screen.height / 10));
        player1.tag = "Player1";
        player1.transform.position = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
    // TODO: Cleanup to work with new HUD and player controller system
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player1") != null && GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().health <= 0)
        {
            // Player died
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
}
