using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class AE {
		private static ActionEngineInstance shortInstance_ = null;

		private static ActionEngineInstance GetShortInstance () {
			if (shortInstance_ == null) {
				var go = new GameObject("[ActionEngineInstance]");
				shortInstance_ = go.AddComponent<ActionEngineInstance>();
			}
			return shortInstance_;
		}

		public static T Prepare<T>() where T : ActionBase, new() {
			return ActionPool.GetInstance().GetAction<T>();
		}

		public static ActionInstance Enqueue (this ActionBase action) {
			return GetShortInstance().Enqueue(action);
		}

		public static void KillAll () {
			GetShortInstance().KillAll();
		}

		/// <summary>
		/// Play the action with short instance, it means that the action will be killed after scene
		/// has changed
		/// </summary>
		public static ActionInstance Play (this ActionBase action, bool unscaled = false) {
			return action.Enqueue().Play(unscaled);
		}

		#region Action Shortcuts

		public static DebugAction Debug (object message, UnityEngine.Object context = null) {
			return Prepare<DebugAction>().SetMessage(message, context);
		}

		public static DelayAction Delay (float duration) {
			return Prepare<DelayAction>().SetDuration(duration);
		}

		public static ParallelAction Parallel (params ActionBase[] actions) {
			var par = Prepare<ParallelAction>();
			for (var i = 0; i < actions.Length; ++i) {
				par.AddAction(actions[i]);
			}
			return par;
		}

		public static RepeatAction Repeat (params ActionBase[] actions) {
			if (actions.Length == 1)
				return Prepare<RepeatAction>().SetAction(actions[0]);
			return Prepare<RepeatAction>().SetAction(Sequence(actions));
		}

		public static ScriptAction Script (Action script) {
			return Prepare<ScriptAction>().SetScript(script);
		}

		public static SequenceAction Sequence (params ActionBase[] actions) {
			var seq = Prepare<SequenceAction>();
			for (var i = 0; i < actions.Length; ++i) {
				seq.AddAction(actions[i]);
			}
			return seq;
		}

		public static TimeScaleAction TimeScale (ActionBase action) {
			return Prepare<TimeScaleAction>().SetAction(action);
		}

		public static FloatTweenAction Tween (Func<float> getter, Action<float> setter, float endValue, float duration) {
			return Prepare<FloatTweenAction>()
				.SetGetter(getter).SetSetter(setter)
				.SetEndValue(endValue).SetDuration(duration);
		}

		public static WaitCoroutineAction WaitCoroutine (Func<object> coroutineGenerator) {
			return Prepare<WaitCoroutineAction>().SetCoroutineGenerator(coroutineGenerator);
		}

		public static WaitUntilAction WaitUntil (Func<bool> predicate) {
			return Prepare<WaitUntilAction>().SetPredicate(predicate);
		}

		#endregion Action Shortcuts
	}
}
