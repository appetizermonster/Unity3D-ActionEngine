using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class DelayAction : ActionBase {
		private float duration_ = 0f;
		private float elapsed_ = 0f;

		public DelayAction Duration (float duration) {
			duration_ = duration;
			return this;
		}

		protected override void OnKill () {
			duration_ = 0f;
			elapsed_ = 0f;
		}

		protected override void OnBegin () {
			elapsed_ = 0f;
		}

		protected override void OnRewind () {
			elapsed_ = 0f;
		}

		protected override bool OnUpdate (float deltaTime) {
			elapsed_ += deltaTime;
			return (elapsed_ >= duration_);
		}
	}
}
