using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public sealed class PathAction : ActionBase {

		public delegate float EaseFunction (float p);

		private Action<Vector3> setter_ = null;

		private float duration_ = 0f;
		private EaseFunction easing_ = null;

		private readonly List<Vector3> points_ = new List<Vector3>();
		private float elapsed_ = 0f;

		#region Parameters

		public PathAction SetSetter (Action<Vector3> setter) {
			setter_ = setter;
			return this;
		}

		public PathAction AddPoint (Vector3 point) {
			points_.Add(point);
			return this;
		}

		public PathAction SetDuration (float duration) {
			duration_ = duration;
			return this;
		}

		public PathAction SetEasing (EaseFunction easing) {
			easing_ = easing;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			if (points_.Count < 4)
				throw new InvalidOperationException("Points must be at least 4 points");
		}

		protected override bool OnUpdate (float deltaTime) {
			elapsed_ += deltaTime;

			var p = 1f;
			if (duration_ > 0)
				p = Mathf.Clamp01(elapsed_ / duration_);

			var easedP = p;
			if (easing_ != null)
				easedP = easing_(p);

			ApplySpline(easedP);

			return (p >= 1f);
		}

		protected override void OnComplete () {
			ApplySpline(1f);
		}

		protected override void OnRewind () {
			elapsed_ = 0f;
		}

		protected override void OnKill () {
			setter_ = null;

			duration_ = 0f;
			easing_ = null;

			points_.Clear();
			elapsed_ = 0f;
		}

		private void ApplySpline (float p) {
			var totalCount = points_.Count - 2;

			var idx = Mathf.FloorToInt(p * totalCount) + 1;
			if (idx >= totalCount) {
				setter_(points_[totalCount]);
				return;
			}

			var i0 = idx - 1;
			var i1 = idx;
			var i2 = idx + 1;
			var i3 = idx + 2;

			var p0 = points_[i0];
			var p1 = points_[i1];
			var p2 = points_[i2];
			var p3 = points_[i3];

			var p_p1 = (i1 - 1) / (float)totalCount;
			var p_p2 = (i2 - 1) / (float)totalCount;

			var diff = (p_p2 - p_p1);
			var t = (diff <= 0) ? 1 : Mathf.Clamp01((p - p_p1) / diff);

			var point = CatmullRom(t, p0, p1, p2, p3);
			setter_(point);
		}

		private Vector3 CatmullRom (float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
			var a = 0.5f * (2f * p1);
			var b = 0.5f * (p2 - p0);
			var c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
			var d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);
			return a + (b * t) + (c * t * t) + (d * t * t * t);
		}
	}
}
