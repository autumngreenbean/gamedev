using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            GameManager.Instance.FailGame(true, "Multiple WaveManagers detected.");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
