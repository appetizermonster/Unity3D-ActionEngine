using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class WaitCoroutineAction : ActionBase<WaitCoroutineAction> {
		private Func<object> coroutineGenerator_ = null;

		#region Parameters

		public WaitCoroutineAction SetCoroutineGenerator (Func<object> coroutineGenerator) {
			coroutineGenerator_ = coroutineGenerator;
			return this;
		}

		#endregion Parameters

		private Coroutine runningCoroutine_ = null;
		private bool completed_ = false;

		protected override void OnBegin () {
			var coroutineOrEnumerator = coroutineGenerator_();
			runningCoroutine_ = CoroutineHelper.Get().StartCoroutine(RunCoroutineAndMarkCompleted(coroutineOrEnumerator));
			completed_ = false;
		}

		private IEnumerator RunCoroutineAndMarkCompleted (object coroutineOrEnumerator) {
			Coroutine coroutine = null;
			if (coroutineOrEnumerator is Coroutine)
				coroutine = (Coroutine)coroutineOrEnumerator;
			else if (coroutineOrEnumerator is IEnumerator)
				coroutine = CoroutineHelper.Get().StartCoroutine((IEnumerator)coroutineOrEnumerator);
			yield return coroutine;
			completed_ = true;
		}

		protected override bool OnUpdate (float deltaTime) {
			return completed_;
		}

		protected override void OnKill () {
			if (runningCoroutine_ != null)
				CoroutineHelper.Get().StopCoroutine(runningCoroutine_);

			completed_ = false;
		}
	}
}
