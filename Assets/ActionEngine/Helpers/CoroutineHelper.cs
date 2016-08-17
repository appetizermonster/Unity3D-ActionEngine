using UnityEngine;

namespace ActionEngine {

	public sealed class CoroutineHelper : MonoBehaviour {
		private static CoroutineHelper instantInstance_ = null;

		public static CoroutineHelper Get () {
			if (instantInstance_ == null) {
				var go = new GameObject("[ActionEngine.CoroutineHelper]");
				instantInstance_ = go.AddComponent<CoroutineHelper>();
			}

			return instantInstance_;
		}
	}
}
