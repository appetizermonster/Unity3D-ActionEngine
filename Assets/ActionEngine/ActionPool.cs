using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public interface IActionPool {

		void Pool (ActionBase action);
	}

	public sealed class ActionPool : IActionPool {
		private static ActionPool instance_ = null;

		public static ActionPool GetInstance () {
			if (instance_ == null)
				instance_ = new ActionPool();
			return instance_;
		}

		private static readonly Dictionary<Type, Queue<ActionBase>> actionQueues_ = new Dictionary<Type, Queue<ActionBase>>();

		public void Preallocate<T>(int count = 1) where T : ActionBase, new() {
			var type = typeof(T);
			var pool = GetActionQueue(type);

			for (var i = 0; i < count; ++i) {
				var action = new T();
				action._SetActionPool(this);

				pool.Enqueue(action);
			}
		}

		public T GetAction<T>() where T : ActionBase, new() {
			var type = typeof(T);
			var pool = GetActionQueue(type);

			if (pool.Count <= 0) {
				var action = new T();
				action._SetActionPool(this);
				return action;
			}

			var pooledAction = (T)pool.Dequeue();
			return pooledAction;
		}

		public void Pool (ActionBase action) {
			var type = action.GetType();
			var pool = GetActionQueue(type);
			pool.Enqueue(action);
		}

		private Queue<ActionBase> GetActionQueue (Type type) {
			var pool = (Queue<ActionBase>)null;
			if (actionQueues_.TryGetValue(type, out pool) == false) {
				pool = new Queue<ActionBase>();
				actionQueues_.Add(type, pool);
			}
			return pool;
		}
	}
}
