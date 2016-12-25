using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class ScrollRectExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, float> AEHorizontalNormalizedPosition_Getter = (t) => ((ScrollRect)t).horizontalNormalizedPosition;
		private static Action<object, float> AEHorizontalNormalizedPosition_Setter = (t, k) => ((ScrollRect)t).horizontalNormalizedPosition = k;

		public static FloatTweenAction AEHorizontalNormalizedPosition (this ScrollRect obj, float position, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEHorizontalNormalizedPosition_Getter)
				.SetSetterWithPayload(AEHorizontalNormalizedPosition_Setter)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}

		private static Func<object, float> AEVerticalNormalizedPosition_Getter = (t) => ((ScrollRect)t).verticalNormalizedPosition;
		private static Action<object, float> AEVerticalNormalizedPosition_Setter = (t, k) => ((ScrollRect)t).verticalNormalizedPosition = k;

		public static FloatTweenAction AEVerticalNormalizedPosition (this ScrollRect obj, float position, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEVerticalNormalizedPosition_Getter)
				.SetSetterWithPayload(AEVerticalNormalizedPosition_Setter)
				.SetPayload(obj)
				.SetEndValue(position)
				.SetDuration(duration);
		}
	}
}