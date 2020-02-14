using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunGameStudio.Tools
{
    public class Timer
    {
        public Timer(float _timeToWait)
        {
            timeToWait = _timeToWait;
            timerCanStart = false;
        }

        float timeToWait = -1;
        float currentTime = 0;

        bool timerCanStart = false;
        public bool TimerCanStart
        {
            get { return timerCanStart; }
        }

        public float CurrentTime
        {
            get { return currentTime; }
        }

        public void SetTimeToreach(float _value)
        {
            timeToWait = _value;
        }
        public void StartTimer()
        {
            Debug.Log("Timer started");
            timerCanStart = true;
        }

        public void PauseTimer()
        {
            Debug.Log("Timer paused");
            timerCanStart = false;
        }

        public void ResetTimer()
        {
            //if()
            currentTime = 0;
        }

        public bool hasReachTime()
        {
            if (timerCanStart == true)
            {
                if (currentTime < timeToWait)
                {
                    currentTime += Time.deltaTime;
                }
            }
            if (timeToWait == 0)
            {
                return false;
            }
            else if (currentTime >= timeToWait)
            {
                currentTime = 0;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
