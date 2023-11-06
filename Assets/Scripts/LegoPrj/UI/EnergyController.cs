using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    public int maxEnergy = 20;
    public float refillTime = 15;

    private int _currentEnergy;
    private float _countTime;

    private string _formatDate = "yyyy-MM-dd HH-mm-ss";

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.UpdateAmountEnergy, SubtractionEnergyPlay);

        _currentEnergy = PlayerPrefsManager.AmountEnergy;
        if(PlayerPrefsManager.TimeRefillEnergy == "")
        {
            _currentEnergy = maxEnergy;
            PlayerPrefsManager.AmountEnergy = _currentEnergy;
        }
        else
        {
            DateTime oldDate = DateTime.ParseExact(PlayerPrefsManager.TimeRefillEnergy, _formatDate, CultureInfo.InvariantCulture);
            DateTime today = DateTime.Now;
            if(today < oldDate)
            {
                PlayerPrefsManager.AmountEnergy = 0;
                PlayerPrefsManager.TimeRefillEnergy = DateTime.Now.ToString(_formatDate);
                return;
            }
            TimeSpan timeDifference = today - oldDate;
            float minuteDifference = (float)Math.Abs(timeDifference.TotalMinutes) + PlayerPrefsManager.TimeCountEnergy / 60;
            int plusEnergy = (int)minuteDifference / (int)refillTime;
            if (_currentEnergy < maxEnergy)
            {
                _currentEnergy = Mathf.Clamp(PlayerPrefsManager.AmountEnergy + plusEnergy, 0, maxEnergy);
                PlayerPrefsManager.AmountEnergy = _currentEnergy;
                _countTime = (minuteDifference - plusEnergy * refillTime) * 60;

            }
            else
            {
                _currentEnergy = PlayerPrefsManager.AmountEnergy;
            }
        }
        EventDispatcher.Instance.PostEvent(EventID.UpdateEnergyText, _currentEnergy);
        PlayerPrefsManager.TimeRefillEnergy = DateTime.Now.ToString(_formatDate);

    }

    private void Update()
    {
        PlayerPrefsManager.TimeCountEnergy = _countTime;
        PlayerPrefsManager.TimeRefillEnergy = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        if(_currentEnergy < maxEnergy)
        {
            _countTime += Time.unscaledDeltaTime;
            if(_countTime >= refillTime * 60)
            {
                _currentEnergy++;
                PlayerPrefsManager.AmountEnergy = _currentEnergy;
                EventDispatcher.Instance.PostEvent(EventID.UpdateEnergyText, _currentEnergy);
                _countTime = 0;
            }
        }
    }

    void SubtractionEnergyPlay(object obj)
    {
        _currentEnergy += (int)obj;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, int.MaxValue);
        PlayerPrefsManager.AmountEnergy = _currentEnergy;
        EventDispatcher.Instance.PostEvent(EventID.UpdateEnergyText, _currentEnergy);
    }
}
