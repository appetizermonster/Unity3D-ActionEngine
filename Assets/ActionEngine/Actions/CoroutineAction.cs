using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

namespace ActionEngine {

	public sealed class CoroutineAction : ActionBase {

		Func<IEnumerator> routineGenerator_ = null;
		IEnumerator routine_ = null;
		float waiting_ = 0f;

		public CoroutineAction Coroutine (Func<IEnumerator> routineGenerator) {
			routineGenerator_ = routineGenerator;
			return this;
		}

		protected override void OnBegin () {
			routine_ = routineGenerator_();
			MoveNext(0f);
		}

		protected override void OnKill () {
			routineGenerator_ = null;
			routine_ = null;
			waiting_ = 0f;
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

		private static FieldInfo WaitForSeconds_Seconds_Field = null;
		private static float GetDurationOf (WaitForSeconds waitForSeconds) {
			if (WaitForSeconds_Seconds_Field == null) {
				WaitForSeconds_Seconds_Field = typeof(WaitForSeconds)
					.GetField("m_Seconds", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			return (float)WaitForSeconds_Seconds_Field.GetValue(waitForSeconds);
        }

	}
}
