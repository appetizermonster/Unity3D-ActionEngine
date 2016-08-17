using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEngine {

	public static class GraphicExtension {

		public static ColorTweenAction AEColor (this Graphic obj, Color color, float duration) {
			return AE.Prepare<ColorTweenAction>()
				.SetGetter(() => obj.color)
				.SetSetter((x) => obj.color = x)
				.SetEndValue(color)
				.SetDuration(duration);
		}

		public static FloatTweenAction AEFade (this Graphic obj, float alpha, float duration) {
			return AE.Prepare<FloatTweenAction>()
				.SetGetter(() => obj.color.a)
				.SetSetter((x) => {
					var col = obj.color;
					col.a = x;
					obj.color = col;
				})
				.SetEndValue(alpha)
				.SetDuration(duration);
		}
		
	}
}
