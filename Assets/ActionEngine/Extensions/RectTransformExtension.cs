using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class RectTransformExtension {

		public static FloatTweenAction AESizeDeltaX (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.sizeDelta.x)
				.SetSetter((x) => obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x))
				.SetEndValue(size)
				.SetDuration(duration);
		}

		public static FloatTweenAction AESizeDeltaY (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.sizeDelta.y)
				.SetSetter((x) => obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x))
				.SetEndValue(size)
				.SetDuration(duration);
		}
	}
}
