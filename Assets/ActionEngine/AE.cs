using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class AE {
		private static ActionEngineInstance instance_ = null;

		private static ActionEngineInstance GetInstance () {
			if (instance_ == null) {
				var go = new GameObject("[ActionEngineInstance]");
				instance_ = go.AddComponent<ActionEngineInstance>();
				GameObject.DontDestroyOnLoad(go);
			}
			return instance_;
		}

		public static T Action<T>() where T : ActionBase, new() {
			return GetInstance().Action<T>();
		}

		public static ActionInstance Compile (this ActionBase action) {
			return GetInstance().Compile(action);
		}

		public static ActionInstance Play (this ActionBase action) {
			return action.Compile().Play();
		}

		#region Action Shortcuts

		public static SequenceAction Sequence (params ActionBase[] actions) {
			var seq = Action<SequenceAction>();
			for (var i = 0; i < actions.Length; ++i) {
				seq.Add(actions[i]);
			}
			return seq;
		}

		public static RepeatAction Repeat (ActionBase action) {
			return Action<RepeatAction>().Action(action);
		}

		public static ParallelAction Parallel (params ActionBase[] actions) {
			var par = Action<ParallelAction>();
			for (var i = 0; i < actions.Length; ++i) {
				par.Add(actions[i]);
			}
			return par;
		}

		public static DelayAction Delay (float duration) {
			return Action<DelayAction>().Duration(duration);
		}

		public static CoroutineAction Coroutine (Func<IEnumerator> routineGenerator) {
			return Action<CoroutineAction>().Coroutine(routineGenerator);
		}

		#endregion Action Shortcuts
	}
}
