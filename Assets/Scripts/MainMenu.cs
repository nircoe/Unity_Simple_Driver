using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text pleaseWaitText;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    private DateTime energyReady;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = $"High Score \n {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(energy==0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if(energyReadyString == string.Empty)
            {
                return;
            }

            energyReady = DateTime.Parse(energyReadyString);
            if(DateTime.Now > energyReady)
            {
                pleaseWaitText.text = string.Empty;
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                pleaseWaitText.text = $"Lifes Recharging\n {energyRechargeDuration}s";
            }
        }

        energyText.text = $"Lifes : {energy}";
    }

    private void Update() 
    {
        TimeSpan timeRemain = energyReady.Subtract(DateTime.Now);
        if (timeRemain.Seconds <= 0)
        {
            pleaseWaitText.text = string.Empty;
            Start();
        }
        else
        {
            pleaseWaitText.text = $"Lifes Recharging\n {timeRemain.Seconds}s";
        }
    }

    public void Play()
    {
        if (energy < 1)
        {
            return;
        }
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if(energy==0)
        {
            DateTime energyRecharge = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyRecharge.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyRecharge);
#endif

        }
        SceneManager.LoadScene(1);
    }
}
