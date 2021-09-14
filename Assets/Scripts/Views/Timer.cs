using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class Timer : MonoBehaviour
    {
        private Text timerText;
        private int counter;
        private void Awake()
        {
            timerText = GetComponent<Text>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SetTimerText), 0, 1);
        }

        private void SetTimerText()
        {
            counter++;
            timerText.text = TimeSpan.FromSeconds(counter).ToString();
        }
    }
}