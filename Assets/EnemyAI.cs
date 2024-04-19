using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] availableCovers;

    private Material material;
    private Transform bestCoverSpot;

    private float currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Start()
    {
        currentHealth = startingHealth;
        material = GetComponent<MeshRenderer>().material;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Update()
    {
        currentHealth += Time.deltaTime * healthRestoreRate;
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCover()
    {
        return bestCoverSpot;
    }
}
