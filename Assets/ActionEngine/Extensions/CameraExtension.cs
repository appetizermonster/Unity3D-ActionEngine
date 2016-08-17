using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class CameraExtension {

		public static FloatTweenAction AEFov (this Camera cam, float fov, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => cam.fieldOfView)
				.SetSetter((x) => cam.fieldOfView = x)
				.SetEndValue(fov)
				.SetDuration(duration);
		}

	}
}
