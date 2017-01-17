using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject slug;

    public string mainMenu;

    private List<GameObject> characterList = new List<GameObject>();

	void Start () {
        SpawnPlayer("Testi", "Player", Vector3.zero, 5, 10, 30, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.Space, new Vector2(Screen.width / 10, (Screen.height - Screen.height / 10)));
        SpawnCharacter(slug, "Enemy", new Vector2(0, 0), 5, 10);
        // TODO: Smooth the camera movement
        //Camera.main.transform.SetParent(characterList[0].transform);
    }
	
	void Update () {
 
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenu);
        }

        LerpCamera(characterList[0].GetComponent<Character>(), 0.35f);
    }

    void SpawnPlayer(string _playerName, string tag, Vector3 playerPos, int _health, float _moveSpeed, float _jumpForce, KeyCode _left, KeyCode _right, KeyCode _jump, KeyCode _throwBall, Vector2 hudPos)
    {
        GameObject newPlayer = (GameObject)Instantiate(player);
        newPlayer.GetComponent<Player>().Initialize(_playerName, _health, _moveSpeed, _jumpForce, _left, _right, _jump, _throwBall, hudPos);
        newPlayer.tag = tag;
        newPlayer.transform.position = playerPos;
        characterList.Add(newPlayer);
    }

    void SpawnCharacter(GameObject prefab, string tag, Vector2 position, int health, int moveSpeed)
    {
        GameObject newCharacter = (GameObject)Instantiate(prefab);
        newCharacter.GetComponent<Character>().Initialize(health, moveSpeed);
        newCharacter.tag = tag;
        newCharacter.transform.position = position;
        characterList.Add(newCharacter);
    }

    void LerpCamera(Character cameraTarget, float cameraOffsetX)
    {
        // TODO: Feels a but choppy, have to investigate

        // Find out velocity of character, will be used to check the direction of movement
        Vector2 velocityOfTarget = cameraTarget.GetVelocity();
        // Get raw vector for where to put camera
        Vector3 targetPos = Vector3.Lerp(Camera.main.transform.position, cameraTarget.transform.position, 0.05f);
        // Move the camera a bit to the side so players have more room in front of them to see what is going on.
        if(velocityOfTarget.x > 0)
        {
            targetPos.x += cameraOffsetX;
        } else if (velocityOfTarget.x < 0)
        {
            targetPos.x += -cameraOffsetX;
        }

        // Set target z back to original value so camera can actually see something
        targetPos.z = -10;
        // Apply finished vector to camera position.
        Camera.main.transform.position = targetPos;
    }
}
