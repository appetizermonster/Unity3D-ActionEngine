using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class TransformExtension {

		public static Vector3TweenAction AEMove (this Transform obj, Vector3 position, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.position)
				.SetSetter((x) => obj.position = x)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalMove (this Transform obj, Vector3 position, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.localPosition)
				.SetSetter((x) => obj.localPosition = x)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, Vector3 scale, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.localScale)
				.SetSetter((x) => obj.localScale = x)
				.SetEndValue(scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, float scale, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.localScale)
				.SetSetter((x) => obj.localScale = x)
				.SetEndValue(Vector3.one * scale)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AERotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.eulerAngles)
				.SetSetter((x) => obj.eulerAngles = x)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		public static Vector3TweenAction AELocalRotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Action<Vector3TweenAction>()
				.SetGetter(() => obj.localEulerAngles)
				.SetSetter((x) => obj.localEulerAngles = x)
				.SetEndValue(angle)
				.SetDuration(duration);
		}

		public static PathAction AEPath (this Transform obj, float duration, params Vector3[] points) {
			var action = AE.Action<PathAction>()
				.SetSetter((x) => obj.position = x)
				.SetDuration(duration);
			for (var i = 0; i < points.Length; ++i) {
				action.AddPoint(points[i]);
			}
			return action;
		}
		
		public static PathAction AELocalPath (this Transform obj, float duration, params Vector3[] points) {
			var action = AE.Action<PathAction>()
				.SetSetter((x) => obj.localPosition = x)
				.SetDuration(duration);
			for (var i = 0; i < points.Length; ++i) {
				action.AddPoint(points[i]);
			}
			return action;
		}
	}
}
