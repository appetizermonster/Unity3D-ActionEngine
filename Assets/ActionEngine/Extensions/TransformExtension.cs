using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class TransformExtension {

		#region Transform Helpers

		private static void SetX (this Transform obj, float x) {
			var pos = obj.position;
			pos.x = x;
			obj.position = pos;
		}

		private static void SetY (this Transform obj, float y) {
			var pos = obj.position;
			pos.y = y;
			obj.position = pos;
		}

		private static void SetZ (this Transform obj, float z) {
			var pos = obj.position;
			pos.z = z;
			obj.position = pos;
		}

		private static void SetLocalX (this Transform obj, float x) {
			var localPos = obj.localPosition;
			localPos.x = x;
			obj.localPosition = localPos;
		}

		private static void SetLocalY (this Transform obj, float y) {
			var localPos = obj.localPosition;
			localPos.y = y;
			obj.localPosition = localPos;
		}

		private static void SetLocalZ (this Transform obj, float z) {
			var localPos = obj.localPosition;
			localPos.z = z;
			obj.localPosition = localPos;
		}

		#endregion Helper Functions

		public static Vector3TweenAction AEMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.position)
				.SetSetter((x) => obj.position = x)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.position.x)
				.SetSetter((k) => obj.SetX(k))
				.SetEndValue(x)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.position.y)
				.SetSetter((k) => obj.SetY(k))
				.SetEndValue(y)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.position.z)
				.SetSetter((k) => obj.SetZ(k))
				.SetEndValue(z)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalMove (this Transform obj, Vector3 position, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.localPosition)
				.SetSetter((x) => obj.localPosition = x)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveX (this Transform obj, float x, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.localPosition.x)
				.SetSetter((k) => obj.SetLocalX(k))
				.SetEndValue(x)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveY (this Transform obj, float y, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.localPosition.y)
				.SetSetter((k) => obj.SetLocalY(k))
				.SetEndValue(y)
				.SetDuration(duration);
		}

		public static FloatTweenAction AELocalMoveZ (this Transform obj, float z, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.localPosition.z)
				.SetSetter((k) => obj.SetLocalZ(k))
				.SetEndValue(z)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, Vector3 scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.localScale)
				.SetSetter((x) => obj.localScale = x)
				.SetEndValue(scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, float scale, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.localScale)
				.SetSetter((x) => obj.localScale = x)
				.SetEndValue(Vector3.one * scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AERotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.eulerAngles)
				.SetSetter((x) => obj.eulerAngles = x)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalRotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Prepare<Vector3TweenAction>()
				.SetGetter(() => obj.localEulerAngles)
				.SetSetter((x) => obj.localEulerAngles = x)
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
				.SetGetter(() => obj.position)
				.SetSetter((x) => obj.position = x)
				.SetStrength(strength)
				.SetVibrato(vibrato)
				.SetDuration(duration);
		}
	}
}
