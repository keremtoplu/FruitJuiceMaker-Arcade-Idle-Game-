using System;
using UnityEngine;

namespace Shadout.Controllers
{
	public class CountdownHelper : MonoBehaviour
	{
		#region SerializedFields

		#endregion

		#region Variables

        private const float WAIT_TIME_BEFORE_START = .5f;

		[HideInInspector]
        public float Interval;

        private float timer;

        private bool isCounting;

        private float waitTimer;

		#endregion

		#region Events
		
        public event Action<int> Notified;

		#endregion

		#region Props

		#endregion

		#region Unity Methods

		private void Update()
        {
            if (isCounting == false)
                return;

            waitTimer -= Time.deltaTime;
            if (waitTimer > 0f)
            {
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (Time.deltaTime > Interval)
                {

                    Notified?.Invoke(Mathf.CeilToInt(Time.deltaTime / Interval));
                }
                else
                {
                    Notified?.Invoke(1);
                }

                timer = Interval;
            }
        }

		#endregion

		#region Methods
		
        public void StartCounting()
        {
            isCounting = true;
            waitTimer = WAIT_TIME_BEFORE_START;
        }

        public void StopCounting()
        {
            isCounting = false;
        }

		#endregion

		#region Callbacks

		#endregion
	}
}