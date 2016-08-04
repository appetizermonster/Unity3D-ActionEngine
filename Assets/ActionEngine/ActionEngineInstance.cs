using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public interface IActionPool {

		void Pool (ActionBase action);
	}

	public sealed class ActionEngineInstance : MonoBehaviour, IActionPool {
		private readonly Dictionary<Type, Queue<ActionBase>> actionPools_ = new Dictionary<Type, Queue<ActionBase>>();

		public T Action<T>() where T : ActionBase, new() {
			var type = typeof(T);
			var pool = GetActionPool(type);

			if (pool.Count <= 0) {
				var action = new T();
				action.SetActionPool(this);
				return action;
			}

			var pooledAction = (T)pool.Dequeue();
			return pooledAction;
		}

		public void Pool (ActionBase action) {
			var type = action.GetType();
			var pool = GetActionPool(type);
			pool.Enqueue(action);
		}

		private Queue<ActionBase> GetActionPool (Type type) {
			var pool = (Queue<ActionBase>)null;
			if (actionPools_.TryGetValue(type, out pool) == false) {
				pool = new Queue<ActionBase>();
				actionPools_.Add(type, pool);
			}
			return pool;
		}

		public ActionInstance Compile (ActionBase action) {
			var actionInstance = new ActionInstance();
			actionInstance.SetAction(action);
			playingInstances_.Add(actionInstance);
			return actionInstance;
		}

		private readonly List<ActionInstance> playingInstances_ = new List<ActionInstance>();
		private readonly List<ActionInstance> removalList_ = new List<ActionInstance>();

		private void Update () {
			removalList_.Clear();

			for (var i = 0; i < playingInstances_.Count; ++i) {
				var actionInstance = playingInstances_[i];
				actionInstance.InternalUpdate();

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
