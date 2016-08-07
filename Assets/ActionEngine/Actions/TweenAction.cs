using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class QuaternionTweenAction : TweenActionBase<QuaternionTweenAction, Quaternion> {

		public override Quaternion Lerp (Quaternion a, Quaternion b, float p) {
			return Quaternion.LerpUnclamped(a, b, p);
		}

		public override Quaternion Add (Quaternion a, Quaternion b) {
			return a * b;
		}
	}

	public sealed class Vector2TweenAction : TweenActionBase<Vector2TweenAction, Vector2> {

		public override Vector2 Lerp (Vector2 a, Vector2 b, float p) {
			return Vector2.LerpUnclamped(a, b, p);
		}

		public override Vector2 Add (Vector2 a, Vector2 b) {
			return a + b;
		}
	}

	public sealed class Vector3TweenAction : TweenActionBase<Vector3TweenAction, Vector3> {

		public override Vector3 Lerp (Vector3 a, Vector3 b, float p) {
			return Vector3.LerpUnclamped(a, b, p);
		}

		public override Vector3 Add (Vector3 a, Vector3 b) {
			return a + b;
		}
	}

	public sealed class Vector4TweenAction : TweenActionBase<Vector4TweenAction, Vector4> {

		public override Vector4 Lerp (Vector4 a, Vector4 b, float p) {
			return Vector4.LerpUnclamped(a, b, p);
		}

		public override Vector4 Add (Vector4 a, Vector4 b) {
			return a + b;
		}
	}

	public sealed class FloatTweenAction : TweenActionBase<FloatTweenAction, float> {

		public override float Lerp (float a, float b, float p) {
			return Mathf.LerpUnclamped(a, b, p);
		}

		public override float Add (float a, float b) {
			return a + b;
		}
	}

	public sealed class IntTweenAction : TweenActionBase<IntTweenAction, int> {

		public override int Lerp (int a, int b, float p) {
			var a_f = (float)a;
			var b_f = (float)b;
			var v = Mathf.LerpUnclamped(a_f, b_f, p);
			return (int)v;
		}

		public override int Add (int a, int b) {
			return a + b;
		}
	}

	public sealed class ColorTweenAction : TweenActionBase<ColorTweenAction, Color> {

		public override Color Lerp (Color a, Color b, float p) {
			return Color.LerpUnclamped(a, b, p);
		}

		public override Color Add (Color a, Color b) {
			return a + b;
		}
	}

	public abstract class TweenActionBase<ConcreteClass, T> : ActionBase where ConcreteClass : ActionBase {

		public delegate float EaseFunction (float p);

		private Func<T> getter_ = null;
		private Action<T> setter_ = null;

		private T startValue_ = default(T);
		private T endValue_ = default(T);
		private T finalValue_ = default(T);

		private float duration_ = 0f;
		private bool relative_ = false;
		private EaseFunction easing_ = null;

		private float elapsed_ = 0f;

		#region Parameters

		public ConcreteClass SetGetter (Func<T> getter) {
			getter_ = getter;
			return (ConcreteClass)((object)this);
		}

		public ConcreteClass SetSetter (Action<T> setter) {
			setter_ = setter;
			return (ConcreteClass)((object)this);
		}

		public ConcreteClass SetEndValue (T endValue) {
			endValue_ = endValue;
			return (ConcreteClass)((object)this);
		}

		public ConcreteClass SetDuration (float duration) {
			duration_ = duration;
			return (ConcreteClass)((object)this);
		}

		public ConcreteClass SetRelative (bool relative) {
			relative_ = relative;
			return (ConcreteClass)((object)this);
		}

		public ConcreteClass SetEasing (EaseFunction easing) {
			easing_ = easing;
			return (ConcreteClass)((object)this);
		}

		#endregion Parameters

		protected override sealed void OnBegin () {
			startValue_ = getter_();

			if (relative_) {
				finalValue_ = Add(startValue_, endValue_);
			} else {
				finalValue_ = endValue_;
			}
		}

		protected override bool OnUpdate (float deltaTime) {
			elapsed_ += deltaTime;

			var p = 1f;
			if (duration_ > 0)
				p = Mathf.Clamp01(elapsed_ / duration_);

			var easedP = p;
			if (easing_ != null)
				easedP = easing_(p);

			var curValue = Lerp(startValue_, finalValue_, easedP);
			setter_(curValue);

			return (p >= 1f);
		}

		protected override void OnComplete () {
			setter_(finalValue_);
		}

		protected override void OnRewind () {
			elapsed_ = 0f;
		}

		protected override void OnKill () {
			getter_ = null;
			setter_ = null;

			startValue_ = default(T);
			endValue_ = default(T);
			finalValue_ = default(T);

			duration_ = 0f;
			relative_ = false;
			easing_ = null;

			elapsed_ = 0f;
		}

		#region Abstract Methods

		public abstract T Lerp (T a, T b, float p);

		public abstract T Add (T a, T b);

		#endregion Abstract Methods
	}
}
