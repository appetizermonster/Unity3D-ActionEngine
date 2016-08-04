using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public abstract class ActionBase {

		public enum InternalState {
			PENDING,
			BEGIN
		}

		internal ActionBase () {
		}

		private InternalState state_ = InternalState.PENDING;
		private ActionBase owner_ = null;
		protected IActionPool actionPool_ = null;

		internal void SetActionPool (IActionPool actionPool) {
			actionPool_ = actionPool;
		}

		internal void SetOwner (ActionBase action) {
			if (owner_ != null)
				throw new Exception("This action have been owned by another action");
			owner_ = action;
		}

		internal void Begin () {
			if (state_ != InternalState.PENDING)
				return;

			OnBegin();
			state_ = InternalState.BEGIN;
		}

		internal void Rewind () {
			OnRewind();
		}

		internal void Complete () {
			if (state_ != InternalState.BEGIN)
				return;

			OnComplete();
			state_ = InternalState.PENDING;
		}

		internal void Kill () {
			OnKill();

			owner_ = null;
			actionPool_.Pool(this);
			state_ = InternalState.PENDING;
		}

		internal bool Update (float deltaTime) {
			return OnUpdate(deltaTime);
		}

		protected virtual void OnBegin () {
		}

		protected virtual void OnRewind () {
		}

		protected virtual void OnComplete () {
		}

		protected virtual void OnKill () {
		}

		protected virtual bool OnUpdate (float deltaTime) {
			return true;
		}
	}
}
