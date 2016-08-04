using ActionEngine.Internal;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class Easings {

		public static float OutExpo (float p) {
			return Easing.ExpoEaseOut(p, 0f, 1f, 1f);
		}

		public static float InExpo (float p) {
			return Easing.ExpoEaseIn(p, 0f, 1f, 1f);
		}

		public static float InOutExpo (float p) {
			return Easing.ExpoEaseInOut(p, 0f, 1f, 1f);
		}

		public static float OutInExpo (float p) {
			return Easing.ExpoEaseOutIn(p, 0f, 1f, 1f);
		}

		public static float InQuad (float p) {
			return Easing.QuadEaseIn(p, 0f, 1f, 1f);
		}

		public static float OutQuad (float p) {
			return Easing.QuadEaseOut(p, 0f, 1f, 1f);
		}

		public static float OutBack (float p) {
			return Easing.BackEaseOut(p, 0f, 1f, 1f);
		}

		public static float InBack (float p) {
			return Easing.BackEaseIn(p, 0f, 1f, 1f);
		}
	}
}
