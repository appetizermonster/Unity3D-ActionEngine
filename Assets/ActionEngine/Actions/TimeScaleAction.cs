using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class TimeScaleAction : ActionBase<TimeScaleAction> {
		private ActionBase action_ = null;
		private float timeScale_ = 1f;

		#region Parameters

		public TimeScaleAction SetAction (ActionBase action) {
			action_ = action;
			return this;
		}

		public TimeScaleAction SetTimeScale (float timeScale) {
			timeScale_ = timeScale;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			action_._Begin();
		}

		protected override bool OnUpdate (float deltaTime) {
			return action_._Update(deltaTime * timeScale_);
		}

		protected override void OnComplete () {
			action_._Complete();
		}

		protected override void OnRewind () {
			action_._Rewind();
		}

		protected override void OnKill () {
			action_._Kill();

			action_ = null;
			timeScale_ = 1f;
		}
	}
}
