using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class CameraExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, float> AEFov_Getter = (t) => ((Camera)t).fieldOfView;
		private static Action<object, float> AEFov_Setter = (t, k) => ((Camera)t).fieldOfView = k;

		public static FloatTweenAction AEFov (this Camera cam, float fov, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEFov_Getter)
				.SetSetterWithPayload(AEFov_Setter)
				.SetPayload(cam)
				.SetEndValue(fov)
				.SetDuration(duration);
		}
	}
}
