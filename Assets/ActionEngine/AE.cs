using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public static class AE {
		private static ActionEngineInstance sceneInstance_ = null;
		private static ActionEngineInstance foreverInstance_ = null;

		private static ActionEngineInstance GetSceneInstance () {
			if (sceneInstance_ == null) {
				var go = new GameObject("[ActionEngineInstance]");
				sceneInstance_ = go.AddComponent<ActionEngineInstance>();
			}
			return sceneInstance_;
		}

		private static ActionEngineInstance GetForeverInstance () {
			if (foreverInstance_ == null) {
				var go = new GameObject("[ActionEngineInstance.Forever]");
				GameObject.DontDestroyOnLoad(go);
				foreverInstance_ = go.AddComponent<ActionEngineInstance>();
			}
			return foreverInstance_;
		}

		public static void PreloadSceneInstance () {
			GetSceneInstance();
		}

		public static void PreloadForeverInstance () {
			GetForeverInstance();
		}

		public static void PreallocateAll (int allocationCount = 50) {
			// Extensions
			CameraExtension.DummyForAlloc();
			GraphicExtension.DummyForAlloc();
			LightExtension.DummyForAlloc();
			RectTransformExtension.DummyForAlloc();
			TransformExtension.DummyForAlloc();

			// Tween Actions
			Preallocate<QuaternionTweenAction>(allocationCount);
			Preallocate<Vector2TweenAction>(allocationCount);
			Preallocate<Vector3TweenAction>(allocationCount);
			Preallocate<Vector4TweenAction>(allocationCount);
			Preallocate<FloatTweenAction>(allocationCount);
			Preallocate<IntTweenAction>(allocationCount);
			Preallocate<ColorTweenAction>(allocationCount);

			// Normal Actions
			Preallocate<DebugAction>(allocationCount);
			Preallocate<DelayAction>(allocationCount);
			Preallocate<ParallelAction>(allocationCount);
			Preallocate<PathAction>(allocationCount);
			Preallocate<RepeatAction>(allocationCount);
			Preallocate<ScriptAction>(allocationCount);
			Preallocate<SequenceAction>(allocationCount);
			Preallocate<ShakeTweenAction>(allocationCount);
			Preallocate<TimeScaleAction>(allocationCount);
			Preallocate<WaitCoroutineAction>(allocationCount);
			Preallocate<WaitUntilAction>(allocationCount);
		}

		public static void Preallocate<T>(int count = 50) where T : ActionBase, new() {
			ActionPool.GetInstance().Preallocate<T>(count);
		}

		public static T Prepare<T>() where T : ActionBase, new() {
			return ActionPool.GetInstance().GetAction<T>();
		}

		public static ActionInstance Enqueue (this ActionBase action, ActionInstance recycleInstance = null) {
			return GetSceneInstance().Enqueue(action, recycleInstance);
		}

		public static ActionInstance EnqueueAsForever (this ActionBase action, ActionInstance recycleInstance = null) {
			return GetForeverInstance().Enqueue(action, recycleInstance);
		}

		public static void KillAll () {
			GetSceneInstance().KillAll();
		}

		public static ActionInstance Play (this ActionBase action, UpdateType updateType = UpdateType.Normal, ActionInstance recycleInstance = null) {
			return action.Enqueue(recycleInstance).Play(updateType);
		}

		public static ActionInstance PlayUnscaled (this ActionBase action, ActionInstance recycleInstance = null) {
			return action.Enqueue(recycleInstance).Play(UpdateType.Unscaled);
		}

		public static ActionInstance PlayFixed (this ActionBase action, ActionInstance recycleInstance = null) {
			return action.Enqueue(recycleInstance).Play(UpdateType.Fixed);
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

		public static Vector3TweenAction Tween (Func<Vector3> getter, Action<Vector3> setter, Vector3 endValue, float duration) {
			return Prepare<Vector3TweenAction>()
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
