using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class ActionInstance {

		public enum InstanceState {
			READY,
			PLAYING,
			PAUSED,
			KILLED
		}

		private ActionBase action_;
		private InstanceState state_ = InstanceState.READY;

		private bool unscaled_ = false;
		private float queuedDelay_ = 0f;

		public InstanceState State { get { return state_; } }

		internal ActionInstance () {
		}

		#region Parameters

		public ActionInstance AddDelay (float delay) {
			queuedDelay_ += delay;
			return this;
		}

		#endregion Parameters

		internal void SetAction (ActionBase action) {
			action_ = action;
		}

		private float oldTime_ = 0f;

		public ActionInstance Play (bool unscaled) {
			if (state_ != InstanceState.READY)
				throw new Exception("State must be READY");

			unscaled_ = unscaled;

			state_ = InstanceState.PLAYING;
			oldTime_ = GetLocalTime();

			action_.Begin();

			return this;
		}

		internal void InternalUpdate () {
			if (state_ != InstanceState.PLAYING)
				return;

			var curTime = GetLocalTime();
			var dt = (curTime - oldTime_);
			oldTime_ = curTime;

			queuedDelay_ -= dt;
			if (queuedDelay_ > 0f)
				return;

			queuedDelay_ = 0f;

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
			action_.Complete();
			Kill();
		}

		public void Kill () {
			if (action_ != null)
				action_.Kill();

			action_ = null;
			state_ = InstanceState.KILLED;

			unscaled_ = false;
			queuedDelay_ = 0f;
		}

		private bool Simulate (float deltaTime) {
			return action_.Update(deltaTime);
		}

		private float GetLocalTime () {
			if (unscaled_)
				return Time.unscaledTime;
			return Time.time;
		}
	}
}
