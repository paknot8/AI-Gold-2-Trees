using System.Collections;
using TMPro;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public static Blackboard instance;
    private Player player;
    public TextMeshProUGUI indicatorText;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
        indicatorText = GameObject.Find("IndicatorText").GetComponent<TextMeshProUGUI>();
    }

    public Vector3 GetPlayerPosition()
    {
        if (player != null)
        {
            return player.transform.position;
        }
        else
        {
            Debug.LogError("Player_Manager not found!");
            return Vector3.zero;
        }
    }

    public void SetIndicatorText(string newText)
    {
        if (indicatorText != null)
        {
            indicatorText.text = newText;
        }
        else
        {
            Debug.LogError("IndicatorText not assigned!");
        }
    }
}
