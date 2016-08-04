using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class TransformExtension {

		public static Vector3TweenAction AEMove (this Transform obj, Vector3 position, float duration) {
			return AE.Action<Vector3TweenAction>()
				.Getter(() => obj.position)
				.Setter((x) => obj.position = x)
				.EndValue(position)
				.Duration(duration);
		}

		public static Vector3TweenAction AELocalMove (this Transform obj, Vector3 position, float duration) {
			return AE.Action<Vector3TweenAction>()
				.Getter(() => obj.localPosition)
				.Setter((x) => obj.localPosition = x)
				.EndValue(position)
				.Duration(duration);
		}

		public static Vector3TweenAction AEScale (this Transform obj, Vector3 scale, float duration) {
			return AE.Action<Vector3TweenAction>()
				.Getter(() => obj.localScale)
				.Setter((x) => obj.localScale = x)
				.EndValue(scale)
				.Duration(duration);
		}

		public static Vector3TweenAction AERotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Action<Vector3TweenAction>()
				.Getter(() => obj.eulerAngles)
				.Setter((x) => obj.eulerAngles = x)
				.EndValue(angle)
				.Duration(duration);
		}

		public static Vector3TweenAction AELocalRotate (this Transform obj, Vector3 angle, float duration) {
			return AE.Action<Vector3TweenAction>()
				.Getter(() => obj.localEulerAngles)
				.Setter((x) => obj.localEulerAngles = x)
				.EndValue(angle)
				.Duration(duration);
		}
	}
}
