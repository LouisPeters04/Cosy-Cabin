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
    [SerializeField] private Image timerFill;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image inputButton;

    private float _timeRemaining;
    private bool _running;

    #endregion

    #region POMODORO TIMER FUNCTIONS
    public void StartTimer()
    {
        if (_running) return;

        if (!int.TryParse(timerInput.text, out int minutes))
        {
            ShowInvalidOutput();
            return;
        }

        if (minutes < 1 || minutes > 60)
        {
            ShowInvalidOutput();
            return;
        }

        minutes = Mathf.Clamp(minutes, 1, 60);

        _timeRemaining = minutes * 60f;
        _running = true;

        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        while (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

            float t = _timeRemaining / 3600f;
            timerFill.fillAmount = _timeRemaining / (float.Parse(timerInput.text) * 60f);

            int mins = Mathf.FloorToInt(_timeRemaining / 60);
            int secs = Mathf.FloorToInt(_timeRemaining % 60);

            timerText.text = $"{mins:00} : {secs :00}";

            yield return null;
        }

        _running = false;
        TimerFinished();
    }

    private void TimerFinished()
    {
        timerText.text = "00:00";

        timerFill.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 8, 1);
    }

    private void ShowInvalidOutput()
    {
        inputButton.color = Color.red;
        inputButton.DOColor(Color.white, 1f);
    }
    #endregion
}
