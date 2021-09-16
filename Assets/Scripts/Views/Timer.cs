using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class Timer : MonoBehaviour
    {
        private Text timerText;
        private int time;
        private void Awake()
        {
            timerText = GetComponent<Text>();
        }
        
        /// <summary>
        /// make a counter timer which call the method every second
        /// </summary>
        private void Start()
        {
            InvokeRepeating(nameof(SetTimerText), 0, 1);
        }

        /// <summary>
        /// calculate the elapsed time
        /// </summary>
        private void SetTimerText()
        {
            time++;
            timerText.text = TimeSpan.FromSeconds(time).ToString();
        }
    }
}