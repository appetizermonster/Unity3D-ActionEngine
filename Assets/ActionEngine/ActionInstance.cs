using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

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

		private bool unscaled_ = false;

		public InstanceState State { get { return state_; } }

		internal ActionInstance () {
		}

		internal void _SetAction (ActionBase action) {
			action_ = action;
		}

		private float oldTime_ = 0f;

		public ActionInstance Play (bool unscaled) {
			if (state_ != InstanceState.READY)
				throw new Exception("State must be READY");

			unscaled_ = unscaled;

			state_ = InstanceState.PLAYING;
			oldTime_ = GetLocalTime();

			action_._Begin();

			return this;
		}

		internal void _InternalUpdate () {
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

			unscaled_ = false;
		}

		private bool Simulate (float deltaTime) {
			return action_._Update(deltaTime);
		}

		private float GetLocalTime () {
			if (unscaled_)
				return Time.unscaledTime;
			return Time.time;
		}
	}
}
