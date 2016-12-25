using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class RectTransformExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, float> AESizeDeltaX_Getter = (t) => ((RectTransform)t).sizeDelta.x;
		private static Action<object, float> AESizeDeltaX_Setter = (t, k) => ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, k);

		public static FloatTweenAction AESizeDeltaX (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AESizeDeltaX_Getter)
				.SetSetterWithPayload(AESizeDeltaX_Setter)
				.SetPayload(obj)
				.SetEndValue(size)
				.SetDuration(duration);
		}

		private static Func<object, float> AESizeDeltaY_Getter = (t) => ((RectTransform)t).sizeDelta.y;
		private static Action<object, float> AESizeDeltaY_Setter = (t, k) => ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, k);

		public static FloatTweenAction AESizeDeltaY (this RectTransform obj, float size, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AESizeDeltaY_Getter)
				.SetSetterWithPayload(AESizeDeltaY_Setter)
				.SetPayload(obj)
				.SetEndValue(size)
				.SetDuration(duration);
		}

		private static Func<object, Vector2> AEAnchorPos_Getter = (t) => ((RectTransform)t).anchoredPosition;
		private static Action<object, Vector2> AEAnchorPos_Setter = (t, k) => ((RectTransform)t).anchoredPosition = k;

		public static Vector2TweenAction AEAnchorPos (this RectTransform obj, Vector2 anchorPos, float duration) {
			return AE.Prepare<Vector2TweenAction>()
				.SetGetterWithPayload(AEAnchorPos_Getter)
				.SetSetterWithPayload(AEAnchorPos_Setter)
				.SetPayload(obj)
				.SetEndValue(anchorPos)
				.SetDuration(duration);
		}
	}
}
