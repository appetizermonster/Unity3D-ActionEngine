using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class GraphicExtension {

		public static void DummyForAlloc () {
		}

		private static Func<object, Color> AEColor_Getter = (t) => ((Graphic)t).color;
		private static Action<object, Color> AEColor_Setter = (t, k) => ((Graphic)t).color = k;

		public static ColorTweenAction AEColor (this Graphic obj, Color color, float duration) {
			return AE.Prepare<ColorTweenAction>()
				.SetGetterWithPayload(AEColor_Getter)
				.SetSetterWithPayload(AEColor_Setter)
				.SetPayload(obj)
				.SetEndValue(color)
				.SetDuration(duration);
		}

		private static Func<object, float> AEFade_Getter = (t) => ((Graphic)t).color.a;

		private static Action<object, float> AEFade_Setter = (t, k) => {
			var _t = ((Graphic)t);
			var c = _t.color;
			c.a = k;
			_t.color = c;
		};

		public static FloatTweenAction AEFade (this Graphic obj, float alpha, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEFade_Getter)
				.SetSetterWithPayload(AEFade_Setter)
				.SetPayload(obj)
				.SetEndValue(alpha)
				.SetDuration(duration);
		}

		private static Func<object, float> AEFade2_Getter = (t) => ((CanvasGroup)t).alpha;
		private static Action<object, float> AEFade2_Setter = (t, k) => ((CanvasGroup)t).alpha = k;

		public static FloatTweenAction AEFade (this CanvasGroup obj, float alpha, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload(AEFade2_Getter)
				.SetSetterWithPayload(AEFade2_Setter)
				.SetPayload(obj)
				.SetEndValue(alpha)
				.SetDuration(duration);
		}
	}
}
