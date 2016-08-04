using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class RepeatAction : ActionBase {
		public const int INFINITY = -1;

		private int loops_ = 1;
		private int curLoop_ = 0;
		private ActionBase action_ = null;

		public RepeatAction Loops (int loops) {
			loops_ = loops;
			return this;
		}

		public RepeatAction Action (ActionBase action) {
			action.SetOwner(this);
			action_ = action;
			return this;
		}

		protected override void OnKill () {
			if (action_ != null)
				action_.Kill();

			loops_ = 1;
			curLoop_ = 0;
			action_ = null;
		}

		protected override void OnBegin () {
			action_.Begin();
		}

		protected override void OnRewind () {
			action_.Rewind();
			curLoop_ = 0;
		}

		protected override bool OnUpdate (float deltaTime) {
			for (var i = 0; i < 100; ++i) {
				if (action_.Update(deltaTime) == false)
					break;

				action_.Complete();

				curLoop_ += 1;

				if (loops_ != INFINITY && curLoop_ >= loops_)
					return true;

				action_.Rewind();
				action_.Begin();
			}
			return false;
		}

		protected override void OnComplete () {
			action_.Complete();
		}
	}
}
