using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public abstract class ActionBase {

		public enum InternalState {
			READY_TO_BEGIN,
			BEGIN
		}

		internal ActionBase () {
		}

		private ActionBase owner_ = null;
		private IActionPool actionPool_ = null;
		private InternalState state_ = InternalState.READY_TO_BEGIN;

		internal InternalState State { get { return state_; } }

		internal void SetActionPool (IActionPool actionPool) {
			actionPool_ = actionPool;
		}

		internal void SetOwner (ActionBase action) {
			if (owner_ != null)
				throw new Exception("This action have been owned by another action");
			owner_ = action;
		}

		internal void Begin () {
			if (state_ != InternalState.READY_TO_BEGIN)
				return;

			OnBegin();
			state_ = InternalState.BEGIN;
		}

		internal bool Update (float deltaTime) {
			if (state_ != InternalState.BEGIN)
				return true;
			return OnUpdate(deltaTime);
		}

		internal void Complete () {
			if (state_ != InternalState.BEGIN)
				return;

			OnComplete();
			state_ = InternalState.READY_TO_BEGIN;
		}

		internal void Rewind () {
			OnRewind();
		}

		internal void Kill () {
			OnKill();

			// Reset All States
			owner_ = null;
			actionPool_.Pool(this);
			state_ = InternalState.READY_TO_BEGIN;
		}

		/// <summary>
		/// Should make all internal states begin
		/// </summary>
		protected virtual void OnBegin () {
		}

		/// <summary>
		/// Should update all internal states using deltaTime
		/// </summary>
		/// <param name="deltaTime">deltaTime</param>
		/// <returns>true if Action has completed</returns>
		protected virtual bool OnUpdate (float deltaTime) {
			return true;
		}

		/// <summary>
		/// Should make all internal states complete
		/// </summary>
		protected virtual void OnComplete () {
		}

		/// <summary>
		/// Should make all internal states ready to replay, and this will be called after <see cref="OnComplete"/>
		/// </summary>
		protected virtual void OnRewind () {
		}

		/// <summary>
		/// Should reset all internal states in Here
		/// </summary>
		protected virtual void OnKill () {
		}
	}
}
