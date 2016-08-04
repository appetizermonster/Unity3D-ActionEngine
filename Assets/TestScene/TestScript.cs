using UnityEngine;
using System.Collections;
using ActionEngine;

public class TestScript : MonoBehaviour {

	public Transform testObj;

	IEnumerator Start () {
		AE.Sequence(
			testObj.AEMove(new Vector3(0, 10, 0), 1f).Easing(Easings.OutBack),
			testObj.AEMove(new Vector3(-10, 0, 0), 1f).Relative(true).Easing(Easings.OutQuad)
		).Play();

		yield return new WaitForSeconds(1.5f);

		AE.Repeat(
			AE.Sequence(
				testObj.AEMove(new Vector3(0, 5, 0), 1f).Easing(Easings.OutBack),
				testObj.AEMove(new Vector3(-5, 3, 0), 1f).Relative(true).Easing(Easings.OutQuad),
				AE.Coroutine(() => TestCoroutine())
			)
		).Loops(5).Play();
	}
	
	private IEnumerator TestCoroutine () {
		Debug.Log("Start!");
		yield return new WaitForSeconds(1f);
		Debug.Log("1st");
		yield return new WaitForSeconds(1f);
		Debug.Log("2nd");
	}

}
