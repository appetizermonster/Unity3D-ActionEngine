using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class RectTransformExtension {

		public static FloatTweenAction AESizeDeltaX (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((RectTransform)t).sizeDelta.x)
				.SetSetterWithPayload((t, k) => ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, k))
				.SetPayload(obj)
				.SetEndValue(size)
				.SetDuration(duration);
		}

		public static FloatTweenAction AESizeDeltaY (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((RectTransform)t).sizeDelta.y)
				.SetSetterWithPayload((t, k) => ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, k))
				.SetPayload(obj)
				.SetEndValue(size)
				.SetDuration(duration);
		}
	}
}
