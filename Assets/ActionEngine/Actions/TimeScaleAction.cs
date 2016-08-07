using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class TimeScaleAction : ActionBase {
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
			action_.Begin();
		}

		protected override bool OnUpdate (float deltaTime) {
			return action_.Update(deltaTime * timeScale_);
		}

		protected override void OnComplete () {
			action_.Complete();
		}

		protected override void OnRewind () {
			action_.Rewind();
		}

		protected override void OnKill () {
			action_.Kill();

			action_ = null;
			timeScale_ = 1f;
		}
	}
}
