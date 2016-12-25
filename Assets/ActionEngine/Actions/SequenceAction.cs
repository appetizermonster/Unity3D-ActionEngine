using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public sealed class SequenceAction : ActionBase<SequenceAction> {
		private readonly List<ActionBase> actions_ = new List<ActionBase>(10);
		private int curIndex_ = -1;

		#region Parameters

		public SequenceAction AddAction (ActionBase action) {
			if (action == null)
				return this;

			action._SetOwner(this);
			actions_.Add(action);
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			// Begin initial action
			OnUpdate(0f);
		}

		protected override bool OnUpdate (float deltaTime) {
			if (actions_.Count <= 0)
				return true;

			var dt = deltaTime;
			var curAction = (ActionBase)null;

			if (curIndex_ < 0) {
				curIndex_ = 0;
				curAction = actions_[0];
				curAction._Begin();
			} else if (curIndex_ < actions_.Count) {
				curAction = actions_[curIndex_];
			}

			if (curAction == null)
				return true;

			for (var i = 0; i < 100; ++i) {
				if (curAction._Update(dt) == false)
					break;
				curAction._Complete();

				curIndex_ += 1;
				if (curIndex_ >= actions_.Count)
					return true;

				dt = 0f;

				curAction = actions_[curIndex_];
				curAction._Begin();
			}
			return false;
		}

		protected override void OnComplete () {
			if (curIndex_ >= actions_.Count)
				return;

			if (curIndex_ >= 0)
				actions_[curIndex_]._Complete();

			for (var i = curIndex_ + 1; i < actions_.Count; ++i) {
				actions_[i]._Begin();
				actions_[i]._Complete();
			}
		}

		protected override void OnRewind () {
			curIndex_ = -1;

			for (var i = 0; i < actions_.Count; ++i) {
				actions_[i]._Rewind();
			}
		}

		protected override void OnKill () {
			for (var i = 0; i < actions_.Count; ++i) {
				actions_[i]._Kill();
			}

			actions_.Clear();
			curIndex_ = -1;
		}
	}
}
