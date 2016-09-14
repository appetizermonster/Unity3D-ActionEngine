using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public sealed class ActionEngineInstance : MonoBehaviour {

		private void OnDestroy () {
			KillAll();
		}

		public ActionInstance Enqueue (ActionBase action, ActionInstance recycleInstance = null) {
			var actionInstance = recycleInstance;
			if (actionInstance == null)
				actionInstance = new ActionInstance();
			else
				actionInstance._Recycle();

			actionInstance._SetAction(action);

			playingInstances_.Add(actionInstance);
			return actionInstance;
		}

		public void KillAll () {
			for (var i = 0; i < playingInstances_.Count; ++i) {
				var actionInstance = playingInstances_[i];
				actionInstance.Kill();
			}
			playingInstances_.Clear();
		}

		private readonly List<ActionInstance> playingInstances_ = new List<ActionInstance>(100);
		private readonly List<ActionInstance> removalList_ = new List<ActionInstance>(100);

		private void Update () {
			removalList_.Clear();

			for (var i = 0; i < playingInstances_.Count; ++i) {
				var actionInstance = playingInstances_[i];
				actionInstance._InternalUpdate();

				if (actionInstance.State == ActionInstance.InstanceState.KILLED)
					removalList_.Add(actionInstance);
			}

			for (var i = 0; i < removalList_.Count; ++i) {
				var actionInstance = removalList_[i];
				playingInstances_.Remove(actionInstance);
			}
		}
	}
}
