using UnityEngine;
using TMPro;
using System;

public class Cooldown : MonoBehaviour
{
    [Header("=== SETTING ===")]
    [SerializeField] private float duration = 300f; 

    [Header("=== UI ===")]
    [SerializeField] private TMP_Text timeText;

    private float timer;
    private bool isRunning;

    public Action OnComplete;

    private void Start()
    {
        StartCooldown(duration);
    }

    public void StartCooldown(float time)
    {
        duration = time;
        timer = duration;
        isRunning = true;
        UpdateUI();
    }

    public void StopCooldown()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            isRunning = false;
            OnComplete?.Invoke();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timeText == null) return;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}