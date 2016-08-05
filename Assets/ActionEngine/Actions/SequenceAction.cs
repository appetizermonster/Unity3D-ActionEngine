using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public sealed class SequenceAction : ActionBase {
		private readonly List<ActionBase> actions_ = new List<ActionBase>();
		private int curIndex_ = -1;

		#region Parameters

		public SequenceAction Add (ActionBase action) {
			action.SetOwner(this);
			actions_.Add(action);
			return this;
		}

		#endregion Parameters

		protected override bool OnUpdate (float deltaTime) {
			if (actions_.Count <= 0)
				return true;

			var dt = deltaTime;
			var curAction = (ActionBase)null;

			if (curIndex_ < 0) {
				curIndex_ = 0;
				curAction = actions_[0];
				curAction.Begin();
			} else {
				curAction = actions_[curIndex_];
			}

			for (var i = 0; i < 100; ++i) {
				if (curAction.Update(dt) == false)
					break;
				curAction.Complete();

				curIndex_ += 1;
				if (curIndex_ >= actions_.Count)
					return true;

				dt = 0f;

				curAction = actions_[curIndex_];
				curAction.Begin();
			}
			return false;
		}

		protected override void OnComplete () {
			if (curIndex_ >= actions_.Count)
				return;

			if (curIndex_ >= 0)
				actions_[curIndex_].Complete();

			for (var i = curIndex_ + 1; i < actions_.Count; ++i) {
				actions_[i].Begin();
				actions_[i].Complete();
			}
		}

		protected override void OnRewind () {
			curIndex_ = -1;

			for (var i = 0; i < actions_.Count; ++i) {
				actions_[i].Rewind();
			}
		}

		protected override void OnKill () {
			for (var i = 0; i < actions_.Count; ++i) {
				actions_[i].Kill();
			}

			actions_.Clear();
			curIndex_ = -1;
		}
	}
}
