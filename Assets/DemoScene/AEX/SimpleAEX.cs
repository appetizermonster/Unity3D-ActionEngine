using UnityEngine;
using ActionEngine;

public static class SimpleAEX {
	
	public static ActionBase Create (IAEScriptContext ctx) {
		return AE.Debug("SimpleAEX has created");
	}

}