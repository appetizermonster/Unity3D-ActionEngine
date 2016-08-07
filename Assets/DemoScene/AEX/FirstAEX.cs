using ActionEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FirstAEX {

	public static ActionBase Create (IAEScriptContext ctx) {
		var cube = ctx.GetTransform("$cube");
		var sphere = ctx.GetTransform("$sphere");
		var secondAEX = ctx.GetAEScript("$second_aex");

		return
			AE.Sequence(
				// Basic Tween
				sphere.AEScale(2f, 1f).SetEasing(Easings.BounceOut),
				AE.Parallel(
					cube.AEMove(new Vector3(0, -3, 0), 3.5f).SetEasing(Easings.BounceInOut),
					sphere.AEMove(new Vector3(0, 3, 0), 4.5f).SetEasing(Easings.ElasticOut)
				),
				AE.Parallel(
					cube.AEMove(new Vector3(0, 3, 0), 2.5f).SetRelative(true).SetEasing(Easings.BackOut),
					sphere.AEMove(new Vector3(0, -3, 0), 3.5f).SetRelative(true).SetEasing(Easings.QuadOut)
				),
				sphere.AEScale(1f, 1f).SetEasing(Easings.BounceOut),
				// Coroutine
				AE.Coroutine(() => DelayCoroutine()),
				// Play another AEX
				secondAEX.Create(new Dictionary<string, object> {
					{"$duration", 10f }
				}),
				AE.Debug("All Completed!")
			);
	}

	private static IEnumerator DelayCoroutine () {
		Debug.Log("Coroutine Started: " + Time.time);
		yield return new WaitForSeconds(2f);
		Debug.Log("Coroutine End: " + Time.time);
	}
}
