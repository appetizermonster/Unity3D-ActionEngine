using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class TransformExtension {

		#region Helpers

		private static void SetLocalX (this Transform tr, float x) {
			tr.localPosition = tr.localPosition.SetX(x);
		}

		private static void SetLocalY (this Transform tr, float y) {
			tr.localPosition = tr.localPosition.SetY(y);
		}

		private static void SetLocalZ (this Transform tr, float z) {
			tr.localPosition = tr.localPosition.SetZ(z);
		}

		private static void SetScaleX (this Transform tr, float x) {
			tr.localScale = tr.localScale.SetX(x);
		}

		private static void SetScaleY (this Transform tr, float y) {
			tr.localScale = tr.localScale.SetY(y);
		}

		private static void SetScaleZ (this Transform tr, float z) {
			tr.localScale = tr.localScale.SetZ(z);
		}

		private static void SetX (this Transform tr, float x) {
			tr.position = tr.position.SetX(x);
		}

		private static void SetY (this Transform tr, float y) {
			tr.position = tr.position.SetY(y);
		}

		private static void SetZ (this Transform tr, float z) {
			tr.position = tr.position.SetZ(z);
		}

		#endregion Helpers

		public static Vector3TweenAction AEMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).position)
				.SetSetterWithPayload((t, k) => ((Transform)t).position = k)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).position.x)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetX(k))
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).position.y)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetY(k))
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).position.z)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetZ(k))
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localPosition)
				.SetSetterWithPayload((t, k) => ((Transform)t).localPosition = k)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localPosition.x)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetLocalX(k))
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localPosition.y)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetLocalY(k))
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localPosition.z)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetLocalZ(k))
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, Vector3 scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localScale)
				.SetSetterWithPayload((t, k) => ((Transform)t).localScale = k)
				.SetPayload(obj)
				.SetEndValue(scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, float scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localScale)
				.SetSetterWithPayload((t, k) => ((Transform)t).localScale = k)
				.SetPayload(obj)
				.SetEndValue(Vector3.one * scale)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEScaleX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localScale.x)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetScaleX(k))
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEScaleY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localScale.y)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetScaleY(k))
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEScaleZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localScale.z)
				.SetSetterWithPayload((t, k) => ((Transform)t).SetScaleZ(k))
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AERotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).eulerAngles)
				.SetSetterWithPayload((t, k) => ((Transform)t).eulerAngles = k)
				.SetPayload(obj)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalRotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).localEulerAngles)
				.SetSetterWithPayload((t, k) => ((Transform)t).localEulerAngles = k)
				.SetPayload(obj)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		public static PathAction AEPath (this Transform obj, float duration, params Vector3[] points) {
			var action = AE.Prepare<PathAction>()
				.SetSetter((x) => obj.position = x)
				.SetDuration(duration);
			for (var i = 0; i < points.Length; ++i) {
				action.AddPoint(points[i]);
			}
			return action;
		}

		public static PathAction AELocalPath (this Transform obj, float duration, params Vector3[] points) {
			var action = AE.Prepare<PathAction>()
				.SetSetter((x) => obj.localPosition = x)
				.SetDuration(duration);
			for (var i = 0; i < points.Length; ++i) {
				action.AddPoint(points[i]);
			}
			return action;
		}

		public static ShakeTweenAction AEShakePosition (this Transform obj, Vector3 strength, float vibrato, float duration) {
			return AE.Prepare<ShakeTweenAction>()
				.SetGetterWithPayload((t) => ((Transform)t).position)
				.SetSetterWithPayload((t, k) => ((Transform)t).position = k)
				.SetPayload(obj)
				.SetStrength(strength)
				.SetVibrato(vibrato)
				.SetDuration(duration);
		}
	}
}
