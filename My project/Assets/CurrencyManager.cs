using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    #region CURRENCY MANAGER REFERENCES
    public static CurrencyManager instance;
    public int CabinCoins {  get; private set; }

    private float workTimer;
    private int tasksCompletedToday;

    private DateTime lastResetDate;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        lastResetDate = DateTime.Now.Date;
        LoadCurrency();
    }

    private void Update()
    {
        if (DateTime.Now.Date != lastResetDate)
        {
            tasksCompletedToday = 0;
            lastResetDate = DateTime.Now.Date;
        }
    }
    #endregion

    #region CURRENCY MANAGER FUNCTIONS
    public void TasksCompleted()
    {
        if (tasksCompletedToday <= 3)
        {
            AddCoins(100);
            tasksCompletedToday++;
            SaveCurrency();
        }

        if (tasksCompletedToday > 3)
        {
            return;
        }
    }

    public void RewardSession()
    {
        AddCoins(250);
        SaveCurrency();
    }

    public void AddCoins(int amount)
    {
        CabinCoins += amount;

        CurrencyUI.instance.UpdateCoins(CabinCoins);

        if(ShopCurrencyUI.instance != null)
        {
            ShopCurrencyUI.instance.UpdateCurrency(CabinCoins);
        }

        if (amount > 0)
        {
            CurrencyUI.instance.ShowPopup(amount);
        }

        SaveCurrency();
    }

    private void SaveCurrency()
    {
        CurrencySaveData data = new CurrencySaveData
        {
            cabinCoins = CabinCoins,
            tasksCompletedToday = tasksCompletedToday,
            lastResetDate = lastResetDate.ToString("yyyy-MM-dd")
        };

        SaveSystem.SaveCurrency(data);
    }

    private void LoadCurrency()
    {
        CurrencySaveData data = SaveSystem.LoadCurrency();

        if (data == null)
        {
            CabinCoins = 0;
            tasksCompletedToday = 0;
            lastResetDate = DateTime.Now.Date;
            return;
        }

        CabinCoins = data.cabinCoins;
        tasksCompletedToday = data.tasksCompletedToday;
        lastResetDate = DateTime.Parse(data.lastResetDate);

        CurrencyUI.instance.UpdateCoins(CabinCoins);
    }
    #endregion
}
