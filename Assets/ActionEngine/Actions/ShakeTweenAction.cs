using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class ShakeTweenAction : ActionBase {
		private Func<Vector3> getter_ = null;
		private Action<Vector3> setter_ = null;

		private Vector3 baseValue_ = Vector3.zero;

		private Vector3 strength_ = Vector3.one;
		private float vibrato_ = 10f;
		private float duration_ = 0f;
		private EasingFunc easing_ = null;

		private float elapsed_ = 0f;

		#region Parameters

		public ShakeTweenAction SetGetter (Func<Vector3> getter) {
			getter_ = getter;
			return this;
		}

		public ShakeTweenAction SetSetter (Action<Vector3> setter) {
			setter_ = setter;
			return this;
		}

		public ShakeTweenAction SetStrength (Vector3 strength) {
			strength_ = strength;
			return this;
		}

		public ShakeTweenAction SetVibrato (float vibrato) {
			vibrato_ = vibrato;
			return this;
		}

		public ShakeTweenAction SetDuration (float duration) {
			duration_ = duration;
			return this;
		}

		public ShakeTweenAction SetEasing (EasingFunc easing) {
			easing_ = easing;
			return this;
		}

		#endregion Parameters

		protected override sealed void OnBegin () {
			baseValue_ = getter_();
		}

		protected override bool OnUpdate (float deltaTime) {
			if (duration_ <= 0f)
				return true;

			elapsed_ += deltaTime;

			var p = Mathf.Clamp01(elapsed_ / duration_);
			var easedP = (easing_ != null) ? easing_(p) : Easings.QuadOut(p);

			var totalVibrato = vibrato_ * duration_;
			var curStrength = strength_ * (1f - easedP);
			var height = Mathf.Sin(totalVibrato * easedP * Mathf.PI * 2f);

			curStrength.x *= UnityEngine.Random.Range(-1f, 1f) * height;
			curStrength.y *= UnityEngine.Random.Range(-1f, 1f) * height;
			curStrength.z *= UnityEngine.Random.Range(-1f, 1f) * height;

			setter_(baseValue_ + curStrength);
			return (p >= 1f);
		}

		protected override void OnComplete () {
			setter_(baseValue_);
		}

		protected override void OnRewind () {
			elapsed_ = 0f;
		}

		protected override void OnKill () {
			getter_ = null;
			setter_ = null;

			baseValue_ = Vector3.zero;

			strength_ = Vector3.one;
			vibrato_ = 10f;
			duration_ = 0f;
			easing_ = null;

			elapsed_ = 0f;
		}
	}
}
