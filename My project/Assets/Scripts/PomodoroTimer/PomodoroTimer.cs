using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PomodoroTimer : MonoBehaviour
{
    #region REFERENCES
    [Header("POMODORO TIMER REFERENCES")]
    [SerializeField] private TMP_InputField timerInput;
    [SerializeField] private TMP_InputField breakTimerInput;
    [SerializeField] private Image timerFill;
    [SerializeField] private TMP_Text timerText;

    private float _timeRemaining;
    private float _totalTime;
    private bool _running;
    private bool _onBreak;

    #endregion

    #region POMODORO TIMER FUNCTIONS
    public void StartTimer()
    {
        if (_running) return;

        if (!int.TryParse(timerInput.text, out int workMinutes) || workMinutes < 1 || workMinutes > 60)
        {
            ShowInvalidOutput(timerInput);
            return;
        }

        if (!int.TryParse(breakTimerInput.text, out int  breakMinutes) || breakMinutes < 1 || breakMinutes > 30)
        {
            ShowInvalidOutput(breakTimerInput);
            return;
        }

        _onBreak = false;

        _totalTime = workMinutes * 60f;
        _timeRemaining = _totalTime;
        _running = true;

        StartCoroutine(TimerRoutine(breakMinutes));
    }

    private IEnumerator TimerRoutine(int breakMinutes)
    {
        while (_running)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            timerFill.fillAmount = _timeRemaining / _totalTime;

            int mins = Mathf.FloorToInt(_timeRemaining / 60);
            int secs = Mathf.FloorToInt(_timeRemaining % 60);

            timerText.text = $"{mins:00} : {secs :00}";

            if (_timeRemaining <= 0)
            {
                if (!_onBreak)
                {
                    StartBreak(breakMinutes);
                    continue;
                }
                else
                {
                    FinishPomodoro();
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void StartBreak(int breakMinutes)
    {
        _onBreak = true;

        _totalTime = breakMinutes * 60f;
        _timeRemaining = _totalTime;

        timerFill.transform.DOPunchScale(Vector3.one * 0.2f, 0.4f, 8, 1);
    }

    private void FinishPomodoro()
    {
        _running = false;
        _onBreak = false;

        float workMinutes = float.Parse(timerText.text);

        int rewardCoins = Mathf.FloorToInt(workMinutes / 10f);
        CurrencyManager.instance.AddCoins(rewardCoins * 250);

        timerFill.fillAmount = 1f;
        timerText.text = "";

        timerFill.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 8, 1);
    }

    private void ShowInvalidOutput(TMP_InputField field)
    {
        field.image.color = Color.red;
        field.image.DOColor(Color.white, 0.5f);
        field.text = "";
        field.Select();
        field.ActivateInputField();
    }
    #endregion
}
