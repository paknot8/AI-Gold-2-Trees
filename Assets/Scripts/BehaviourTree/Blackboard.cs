using TMPro;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public static Blackboard instance; // Can be called anywhere.
    private Player player;
    private TextMeshProUGUI indicatorText;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
        indicatorText = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
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
