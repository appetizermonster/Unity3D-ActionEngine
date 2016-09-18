using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class TransformExtension {

		public static void DummyForAlloc () {
		}

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

		private static readonly Func<object, Vector3> AEMove_Getter = (t) => ((Transform)t).position;
		private static readonly Action<object, Vector3> AEMove_Setter = (t, k) => ((Transform)t).position = k;

		public static Vector3TweenAction AEMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AEMove_Getter)
				.SetSetterWithPayload(AEMove_Setter)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEMoveX_Getter = (t) => ((Transform)t).position.x;
		private static readonly Action<object, float> AEMoveX_Setter = (t, k) => ((Transform)t).SetX(k);

		public static FloatTweenAction AEMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEMoveX_Getter)
				.SetSetterWithPayload(AEMoveX_Setter)
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEMoveY_Getter = (t) => ((Transform)t).position.y;
		private static readonly Action<object, float> AEMoveY_Setter = (t, k) => ((Transform)t).SetY(k);

		public static FloatTweenAction AEMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEMoveY_Getter)
				.SetSetterWithPayload(AEMoveY_Setter)
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEMoveZ_Getter = (t) => ((Transform)t).position.z;
		private static readonly Action<object, float> AEMoveZ_Setter = (t, k) => ((Transform)t).SetZ(k);

		public static FloatTweenAction AEMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEMoveZ_Getter)
				.SetSetterWithPayload(AEMoveZ_Setter)
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		private static readonly Func<object, Vector3> AELocalMove_Getter = (t) => ((Transform)t).localPosition;
		private static readonly Action<object, Vector3> AELocalMove_Setter = (t, k) => ((Transform)t).localPosition = k;

		public static Vector3TweenAction AELocalMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AELocalMove_Getter)
				.SetSetterWithPayload(AELocalMove_Setter)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AELocalMoveX_Getter = (t) => ((Transform)t).localPosition.x;
		private static readonly Action<object, float> AELocalMoveX_Setter = (t, k) => ((Transform)t).SetLocalX(k);

		public static FloatTweenAction AELocalMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AELocalMoveX_Getter)
				.SetSetterWithPayload(AELocalMoveX_Setter)
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AELocalMoveY_Getter = (t) => ((Transform)t).localPosition.y;
		private static readonly Action<object, float> AELocalMoveY_Setter = (t, k) => ((Transform)t).SetLocalY(k);

		public static FloatTweenAction AELocalMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AELocalMoveY_Getter)
				.SetSetterWithPayload(AELocalMoveY_Setter)
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AELocalMoveZ_Getter = (t) => ((Transform)t).localPosition.z;
		private static readonly Action<object, float> AELocalMoveZ_Setter = (t, k) => ((Transform)t).SetLocalZ(k);

		public static FloatTweenAction AELocalMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AELocalMoveZ_Getter)
				.SetSetterWithPayload(AELocalMoveZ_Setter)
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		private static readonly Func<object, Vector3> AEScale_Getter = (t) => ((Transform)t).localScale;
		private static readonly Action<object, Vector3> AEScale_Setter = (t, k) => ((Transform)t).localScale = k;

		public static Vector3TweenAction AEScale (this Transform obj, Vector3 scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AEScale_Getter)
				.SetSetterWithPayload(AEScale_Setter)
				.SetPayload(obj)
				.SetEndValue(scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, float scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AEScale_Getter)
				.SetSetterWithPayload(AEScale_Setter)
				.SetPayload(obj)
				.SetEndValue(Vector3.one * scale)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEScaleX_Getter = (t) => ((Transform)t).localScale.x;
		private static readonly Action<object, float> AEScaleX_Setter = (t, k) => ((Transform)t).SetScaleX(k);

		public static FloatTweenAction AEScaleX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEScaleX_Getter)
				.SetSetterWithPayload(AEScaleX_Setter)
				.SetPayload(obj)
				.SetEndValue(x)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEScaleY_Getter = (t) => ((Transform)t).localScale.y;
		private static readonly Action<object, float> AEScaleY_Setter = (t, k) => ((Transform)t).SetScaleY(k);

		public static FloatTweenAction AEScaleY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEScaleY_Getter)
				.SetSetterWithPayload(AEScaleY_Setter)
				.SetPayload(obj)
				.SetEndValue(y)
				.SetDuration(duration);
		}

		private static readonly Func<object, float> AEScaleZ_Getter = (t) => ((Transform)t).localScale.z;
		private static readonly Action<object, float> AEScaleZ_Setter = (t, k) => ((Transform)t).SetScaleZ(k);

		public static FloatTweenAction AEScaleZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEScaleZ_Getter)
				.SetSetterWithPayload(AEScaleZ_Setter)
				.SetPayload(obj)
				.SetEndValue(z)
				.SetDuration(duration);
		}

		private static readonly Func<object, Vector3> AERotate_Getter = (t) => ((Transform)t).eulerAngles;
		private static readonly Action<object, Vector3> AERotate_Setter = (t, k) => ((Transform)t).eulerAngles = k;

		public static Vector3TweenAction AERotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AERotate_Getter)
				.SetSetterWithPayload(AERotate_Setter)
				.SetPayload(obj)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		private static readonly Func<object, Vector3> AELocalRotate_Getter = (t) => ((Transform)t).localEulerAngles;
		private static readonly Action<object, Vector3> AELocalRotate_Setter = (t, k) => ((Transform)t).localEulerAngles = k;

		public static Vector3TweenAction AELocalRotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetterWithPayload(AELocalRotate_Getter)
				.SetSetterWithPayload(AELocalRotate_Setter)
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

		private static readonly Func<object, Vector3> AEShakePosition_Getter = (t) => ((Transform)t).position;
		private static readonly Action<object, Vector3> AEShakePosition_Setter = (t, k) => ((Transform)t).position = k;

		public static ShakeTweenAction AEShakePosition (this Transform obj, Vector3 strength, float vibrato, float duration) {
			return AE.Prepare<ShakeTweenAction>()
				.SetGetterWithPayload(AEShakePosition_Getter)
				.SetSetterWithPayload(AEShakePosition_Setter)
				.SetPayload(obj)
				.SetStrength(strength)
				.SetVibrato(vibrato)
				.SetDuration(duration);
		}
	}
}
