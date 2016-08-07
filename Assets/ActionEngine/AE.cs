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

		public static ActionInstance Enqueue (this ActionBase action) {
			return GetInstance().Enqueue(action);
		}

		public static void KillAll () {
			GetInstance().KillAll();
		}

		public static ActionInstance Play (this ActionBase action, bool unscaled = false) {
			return action.Enqueue().Play(unscaled);
		}

		#region Action Shortcuts

		public static CoroutineAction Coroutine (Func<IEnumerator> routineGenerator) {
			return Action<CoroutineAction>().SetCoroutine(routineGenerator);
		}

		public static DebugAction Debug (object message, UnityEngine.Object context = null) {
			return Action<DebugAction>().SetMessage(message, context);
		}

		public static DelayAction Delay (float duration) {
			return Action<DelayAction>().SetDuration(duration);
		}

		public static ParallelAction Parallel (params ActionBase[] actions) {
			var par = Action<ParallelAction>();
			for (var i = 0; i < actions.Length; ++i) {
				par.AddAction(actions[i]);
			}
			return par;
		}

		public static RepeatAction Repeat (ActionBase action) {
			return Action<RepeatAction>().SetAction(action);
		}

		public static ScriptAction Script (Action script) {
			return Action<ScriptAction>().SetScript(script);
		}

		public static SequenceAction Sequence (params ActionBase[] actions) {
			var seq = Action<SequenceAction>();
			for (var i = 0; i < actions.Length; ++i) {
				seq.AddAction(actions[i]);
			}
			return seq;
		}

		public static TimeScaleAction TimeScale (ActionBase action) {
			return Action<TimeScaleAction>().SetAction(action);
		}

		#endregion Action Shortcuts
	}
}
