using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;
    private float currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Start(){
        currentHealth = startingHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Update(){
        currentHealth += Time.deltaTime * healthRestoreRate;
    }
}
