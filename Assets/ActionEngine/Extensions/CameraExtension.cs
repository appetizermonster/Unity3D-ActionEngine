using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class CameraExtension {

		public static FloatTweenAction AEFov (this Camera cam, float fov, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Camera)t).fieldOfView)
				.SetSetterWithPayload((t, k) => ((Camera)t).fieldOfView = k)
				.SetPayload(cam)
				.SetEndValue(fov)
				.SetDuration(duration);
		}
	}
}
