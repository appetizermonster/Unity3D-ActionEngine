using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class DelayAction : ActionBase<DelayAction> {
		private float duration_ = 0f;
		private float elapsed_ = 0f;

		#region Parameters

		public DelayAction SetDuration (float duration) {
			duration_ = duration;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			elapsed_ = 0f;
		}

		protected override bool OnUpdate (float deltaTime) {
			elapsed_ += deltaTime;
			return (elapsed_ >= duration_);
		}

		protected override void OnKill () {
			duration_ = 0f;
			elapsed_ = 0f;
		}
	}
}
