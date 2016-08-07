using UnityEngine;
using ActionEngine;

public static class SecondAEX {
	
	public static ActionBase Create (IAEScriptContext ctx) {
		var sphere2 = ctx.GetTransform("$sphere_2");
		var duration = ctx.GetFloat("$duration");

		Debug.Log(duration);
		return 
			AE.Repeat(
				AE.Sequence(
					AE.Debug("playing SecondAEX"),
					sphere2.AEMove(new Vector3(-5, 0, 0), 1.5f).SetEasing(Easings.BackOut),
					sphere2.AEPath(duration,
						new Vector3(0, 5, 0),
						new Vector3(-5, 0, 0),
						new Vector3(-3, 2, 0),
						new Vector3(3, -2, 0),
						new Vector3(5, 0, 0),
						new Vector3(0, -5, 0)
					)
				)
			).SetLoops(3);
	}

}