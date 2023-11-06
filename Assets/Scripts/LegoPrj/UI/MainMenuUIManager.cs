using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : UIContainer
{
    public Button startBtn, settingBtn, energyBtn;
    public Text energyAmounttxt;
    public InputField nameField;


    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.UpdateEnergyText, UpdateEnergyText);
        
        startBtn.onClick.AddListener(StartOnClick);
        settingBtn.onClick.AddListener(SettingsOnClick);
        energyBtn.onClick.AddListener(EnergyOnclick);
        nameField.onValueChanged.AddListener(delegate { OnChangeValueName(); });

    }

    private void Start()
    {
        energyAmounttxt.text = PlayerPrefsManager.AmountEnergy.ToString();
    }

    void StartOnClick()
    {
        if (PlayerPrefsManager.AmountEnergy >= ConstantsGame.EnergyPlay)
        {
            GameManager.Instance.StartEndless();
            EventDispatcher.Instance.PostEvent(EventID.UpdateAmountEnergy, -ConstantsGame.EnergyPlay);
            Hide();
            GameManager.Instance.SetNamePlayer(nameField.text);
        }
    }

    void SettingsOnClick()
    {
        PopupManager.instance.ShowSettings();
    }

    void EnergyOnclick()
    {
        EventDispatcher.Instance.PostEvent(EventID.UpdateAmountEnergy, 20);
    }

    void UpdateEnergyText(object obj)
    {
        energyAmounttxt.text = PlayerPrefsManager.AmountEnergy.ToString();
    }

    public void OnChangeValueName()
    {

    }

}
