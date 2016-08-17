using System;

namespace ActionEngine {

	public sealed class WaitUntilAction : ActionBase {
		private Func<bool> predicate_ = null;

		#region Parameters

		public WaitUntilAction SetPredicate (Func<bool> predicate) {
			predicate_ = predicate;
			return this;
		}

		#endregion Parameters

		protected override bool OnUpdate (float deltaTime) {
			return predicate_();
		}

		protected override void OnKill () {
			predicate_ = null;
		}
	}
}
