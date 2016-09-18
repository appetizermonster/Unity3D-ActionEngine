using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class LightExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, float> AEIntensity_Getter = (t) => ((Light)t).intensity;
		private static Action<object, float> AEIntensity_Setter = (t, k) => ((Light)t).intensity = k;

		public static FloatTweenAction AEIntensity (this Light obj, float intensity, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEIntensity_Getter)
				.SetSetterWithPayload(AEIntensity_Setter)
				.SetPayload(obj)
				.SetEndValue(intensity)
				.SetDuration(duration);
		}
	}
}
