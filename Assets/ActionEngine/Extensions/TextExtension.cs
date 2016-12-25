using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class TextExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, Color> AEColor_Getter = (t) => ((Text)t).color;
		private static Action<object, Color> AEColor_Setter = (t, k) => ((Text)t).color = k;

		public static ColorTweenAction AEColor (this Text obj, Color color, float duration) {
			return AE.Prepare<ColorTweenAction>()
				.SetGetterWithPayload(AEColor_Getter)
				.SetSetterWithPayload(AEColor_Setter)
				.SetPayload(obj)
				.SetEndValue(color)
				.SetDuration(duration);
		}
	}
}
