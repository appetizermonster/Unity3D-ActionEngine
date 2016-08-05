using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ActionEngine {

	public sealed class CoroutineAction : ActionBase {
		private Func<IEnumerator> routineGenerator_ = null;
		private IEnumerator routine_ = null;
		private float waiting_ = 0f;

		#region Parameters

		public CoroutineAction Coroutine (Func<IEnumerator> routineGenerator) {
			routineGenerator_ = routineGenerator;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			routine_ = routineGenerator_();
			MoveNext(0f);
		}

		protected override bool OnUpdate (float deltaTime) {
			return MoveNext(deltaTime);
		}

		protected override void OnComplete () {
			if (routine_ == null)
				return;

			for (var i = 0; i < 500; ++i) {
				var completed = !routine_.MoveNext();
				if (completed)
					break;
			}

			routine_ = null;
		}

		protected override void OnKill () {
			routineGenerator_ = null;
			routine_ = null;
			waiting_ = 0f;
		}

		private bool MoveNext (float deltaTime) {
			if (routine_ == null)
				return true;

			if (waiting_ > 0) {
				waiting_ -= deltaTime;
				return false;
			}

			var completed = !routine_.MoveNext();
			if (completed) {
				routine_ = null;
				return true;
			}

			if (routine_.Current is WaitForSeconds) {
				var waitForSeconds = (WaitForSeconds)routine_.Current;
				var duration = GetDurationOf(waitForSeconds);
				waiting_ = duration;
			}
			return false;
		}

		#region Utility Methods

		private static FieldInfo WaitForSeconds_Seconds_Field = null;

		private static float GetDurationOf (WaitForSeconds waitForSeconds) {
			if (WaitForSeconds_Seconds_Field == null) {
				WaitForSeconds_Seconds_Field = typeof(WaitForSeconds)
					.GetField("m_Seconds", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			return (float)WaitForSeconds_Seconds_Field.GetValue(waitForSeconds);
		}

		#endregion Utility Methods
	}
}
