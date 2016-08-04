using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class ActionInstance {

		public enum InstanceState {
			PENDING,
			PLAYING,
			PAUSED,
			KILLED
		}

		private ActionBase action_;
		private bool unscaled_ = false;
		private float timeScale_ = 1f;
		private InstanceState state_ = InstanceState.PENDING;

		public InstanceState State { get { return state_; } }

		internal ActionInstance () {
		}

		internal void SetAction (ActionBase action) {
			action_ = action;
		}

		public ActionInstance SetUnscaled (bool unscaled) {
			unscaled_ = unscaled;
			return this;
		}

		public ActionInstance SetTimeScale (float timeScale) {
			timeScale_ = timeScale;
			return this;
		}

		private float oldTime_ = 0f;

		public ActionInstance Play () {
			if (state_ != InstanceState.PENDING)
				throw new Exception("State must be PENDING");

			oldTime_ = GetLocalTime();
			state_ = InstanceState.PLAYING;
			action_.Begin();

			return this;
		}

		public void Pause () {
			if (action_ != null)
				state_ = InstanceState.PAUSED;
		}

		public void Resume () {
			if (action_ != null)
				state_ = InstanceState.PLAYING;

			oldTime_ = GetLocalTime();
		}

		public void Kill () {
			if (action_ != null)
				action_.Kill();

			state_ = InstanceState.KILLED;
			action_ = null;
		}

		public void Complete () {
			action_.Complete();
			action_.Kill();

			state_ = InstanceState.KILLED;
			action_ = null;
		}

		internal void InternalUpdate () {
			if (state_ != InstanceState.PLAYING)
				return;

			var curTime = GetLocalTime();
			var dt = (curTime - oldTime_) * timeScale_;
			oldTime_ = curTime;

			if (Simulate(dt))
				Complete();
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
