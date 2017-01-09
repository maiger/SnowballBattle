using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public Text PlayerName;
    public GameObject health;

    public void Initialize(string playerName, int healthAmount)
    {
        PlayerName.text = playerName;
        SetHealth(healthAmount);
    }

    public void SetHealth(int healthAmount)
    {
        // TODO: Do not just destroy all old icons, reuse when possible

        Debug.Log("Setting up health");

        // Destroy all children
        foreach (Transform child in transform)
        {
            Debug.Log("Destroying old health images");
            GameObject.Destroy(child.gameObject);
        }

        // Spawn the correct amount of health icons
        for (int i = 0; i < healthAmount; i++)
        {
            // TODO: Cleanup
            GameObject healthIconClone = (GameObject)Instantiate(health, new Vector3(0, 0, 0), Quaternion.identity);
            healthIconClone.transform.SetParent(gameObject.transform);
            healthIconClone.transform.localPosition = new Vector3(i * 50, -50, 0);
            healthIconClone.SetActive(true);
            healthIconClone.GetComponent<Image>().enabled = true;
        }
    }
}
