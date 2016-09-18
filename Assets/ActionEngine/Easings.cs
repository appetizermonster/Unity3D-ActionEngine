using ActionEngine.Internal;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class Easings {
		public static void DummyFuncForAllocation () {
		}

		public static readonly EasingFunc Linear = (p) => Easing.Linear(p, 0f, 1f, 1f);

		public static readonly EasingFunc ExpoOut = (p) => Easing.ExpoEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc ExpoIn = (p) => Easing.ExpoEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc ExpoInOut = (p) => Easing.ExpoEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc ExpoOutIn = (p) => Easing.ExpoEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc CircOut = (p) => Easing.CircEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc CircIn = (p) => Easing.CircEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc CircInOut = (p) => Easing.CircEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc CircOutIn = (p) => Easing.CircEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuadIn = (p) => Easing.QuadEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuadOut = (p) => Easing.QuadEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuadInOut = (p) => Easing.QuadEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuadOutIn = (p) => Easing.QuadEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc SineIn = (p) => Easing.SineEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc SineOut = (p) => Easing.SineEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc SineInOut = (p) => Easing.SineEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc SineOutIn = (p) => Easing.SineEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc CubicIn = (p) => Easing.CubicEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc CubicOut = (p) => Easing.CubicEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc CubicInOut = (p) => Easing.CubicEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc CubicOutIn = (p) => Easing.CubicEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuartIn = (p) => Easing.QuartEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuartOut = (p) => Easing.QuartEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuartInOut = (p) => Easing.QuartEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuartOutIn = (p) => Easing.QuartEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuintIn = (p) => Easing.QuintEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuintOut = (p) => Easing.QuintEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuintInOut = (p) => Easing.QuintEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc QuintOutIn = (p) => Easing.QuintEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc ElasticIn = (p) => Easing.ElasticEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc ElasticOut = (p) => Easing.ElasticEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc ElasticInOut = (p) => Easing.ElasticEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc ElasticOutIn = (p) => Easing.ElasticEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc BounceIn = (p) => Easing.BounceEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc BounceOut = (p) => Easing.BounceEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc BounceInOut = (p) => Easing.BounceEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc BounceOutIn = (p) => Easing.BounceEaseOutIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc BackIn = (p) => Easing.BackEaseIn(p, 0f, 1f, 1f);

		public static readonly EasingFunc BackOut = (p) => Easing.BackEaseOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc BackInOut = (p) => Easing.BackEaseInOut(p, 0f, 1f, 1f);

		public static readonly EasingFunc BackOutIn = (p) => Easing.BackEaseOutIn(p, 0f, 1f, 1f);

		public static EasingFunc Factory_SoftBounce (float bounces = 4, float stiffness = 3) {
			float alpha = stiffness / 100f;
			float threshold = 0.005f / Mathf.Pow(10, stiffness);
			float limit = Mathf.Floor(Mathf.Log(threshold) / -alpha);
			float omega = (bounces + 0.5f) * Mathf.PI / limit;

			return (float p) => {
				var t = p * limit;
				return 1 - Mathf.Pow(2.718f, -alpha * t) * Mathf.Cos(omega * t);
			};
		}
	}
}
