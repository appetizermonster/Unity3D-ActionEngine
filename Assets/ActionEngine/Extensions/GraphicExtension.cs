using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class GraphicExtension {

		public static ColorTweenAction AEColor (this Graphic obj, Color color, float duration) {
			return AE.Prepare<ColorTweenAction>()
				.SetGetterWithPayload((t) => ((Graphic)t).color)
				.SetSetterWithPayload((t, k) => ((Graphic)t).color = k)
				.SetPayload(obj)
				.SetEndValue(color)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEFade (this Graphic obj, float alpha, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((Graphic)t).color.a)
				.SetSetterWithPayload((t, k) => {
					var _t = (Graphic)t;
					var col = _t.color;
					col.a = k;
					_t.color = col;
				})
				.SetPayload(obj)
				.SetEndValue(alpha)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEFade (this CanvasGroup obj, float alpha, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetterWithPayload((t) => ((CanvasGroup)t).alpha)
				.SetSetterWithPayload((t, k) => ((CanvasGroup)t).alpha = k)
				.SetPayload(obj)
				.SetEndValue(alpha)
				.SetDuration(duration);
		}
	}
}
