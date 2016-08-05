using ActionEngine;
using System.Collections;
using UnityEngine;

public class TestScript : MonoBehaviour {
	public Transform testObj;

	private IEnumerator Start () {
		AE.Repeat(
			AE.TimeScale(
				AE.Sequence(
					testObj.AEMove(new Vector3(0, 5, 0), 1f).Easing(Easings.OutBack),
					testObj.AEMove(new Vector3(-5, 3, 0), 1f).Relative(true).Easing(Easings.OutQuad),
					AE.Coroutine(() => TestCoroutine())
				)
			).TimeScale(0.25f)
		).Loops(5).Play();
		yield break;
	}

	private IEnumerator TestCoroutine () {
		Debug.Log("Start!");
		yield return new WaitForSeconds(1f);
		Debug.Log("1st");
		yield return new WaitForSeconds(1f);
		Debug.Log("2nd");
	}
}
