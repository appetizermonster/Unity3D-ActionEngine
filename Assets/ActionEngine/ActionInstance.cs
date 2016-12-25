using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	[Flags]
	public enum UpdateType {
		NONE = 0,
		NORMAL = 1,
		UNSCALED = 2,
		FIXED = 4
	}

	public sealed class ActionInstance {

		private class WaitForActionInstance : CustomYieldInstruction {
			private readonly ActionInstance actionInstance_ = null;

			public WaitForActionInstance (ActionInstance instance) {
				actionInstance_ = instance;
			}

			public override bool keepWaiting {
				get {
					return (actionInstance_.State == InstanceState.PLAYING);
				}
			}
		}

		public enum InstanceState {
			READY,
			PLAYING,
			PAUSED,
			KILLED
		}

		private ActionBase action_;
		private InstanceState state_ = InstanceState.READY;

		private UpdateType updateType_ = UpdateType.NORMAL;
		public UpdateType UpdateType { get { return updateType_; } }
		private float oldTime_ = 0f;

		public InstanceState State { get { return state_; } }

		public ActionInstance () {
		}

		internal void _Recycle () {
			if (state_ != InstanceState.KILLED && state_ != InstanceState.READY)
				throw new InvalidOperationException("ActionInstance should be killed before recycling");

			action_ = null;
			state_ = InstanceState.READY;
			updateType_ = UpdateType.NORMAL;
			oldTime_ = 0f;
		}

		internal void _SetAction (ActionBase action) {
			action_ = action;
		}

		public ActionInstance Play (bool unscaled) {
			return Play(unscaled ? UpdateType.UNSCALED : UpdateType.NORMAL);
		}

		public ActionInstance Play (UpdateType updateType) {
			if (state_ != InstanceState.READY)
				throw new Exception("State must be READY");

			updateType_ = updateType;

			state_ = InstanceState.PLAYING;
			oldTime_ = GetLocalTime();

			action_._Begin();

			return this;
		}

		internal void Internal_Update () {
			if (state_ != InstanceState.PLAYING)
				return;

			var curTime = GetLocalTime();
			var dt = (curTime - oldTime_);
			oldTime_ = curTime;

			if (Simulate(dt))
				Complete();
		}

		public void Pause () {
			if (state_ != InstanceState.PLAYING)
				return;

			state_ = InstanceState.PAUSED;
		}

		public void Resume () {
			if (state_ != InstanceState.PAUSED)
				return;

			state_ = InstanceState.PLAYING;
			oldTime_ = GetLocalTime();
		}

		public void Complete () {
			if (state_ == InstanceState.KILLED)
				return;

			if (action_ != null)
				action_._Complete();
			Kill();
		}

		public CustomYieldInstruction WaitForCompletion () {
			return new WaitForActionInstance(this);
		}

		public void Kill () {
			if (action_ != null)
				action_._Kill();

			action_ = null;
			state_ = InstanceState.KILLED;
			updateType_ = UpdateType.NORMAL;
		}

		private bool Simulate (float deltaTime) {
			return action_._Update(deltaTime);
		}

		private float GetLocalTime () {
			if (updateType_ == UpdateType.UNSCALED)
				return Time.unscaledTime;
			else if (updateType_ == UpdateType.FIXED)
				return Time.fixedTime;
			return Time.time;
		}
	}
}
