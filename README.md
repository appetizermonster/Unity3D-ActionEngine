# ActionEngine

Simple Tween-like Engine for Unity3D, which supports Live code editing

**[Currently, Under heavy development]**

## Features
- Play/Pause/Resume/Loop/Complete Playback
- Composite Tween Actions with Coroutines
- Object Pooling for Default
- Live Editing Support with C# AEScript

## Differences with Tween Engine
- Coroutine Support (Partial)
- No Reverse Playback
- No Goto
- No Duration

## Live Editing! (or Hot Reload)
### Demo
[![Demo Video](http://img.youtube.com/vi/Xfc9MXr2Cyg/0.jpg)](http://www.youtube.com/watch?v=Xfc9MXr2Cyg)

Unity's built-in Hot Reload functionality is almost useless.  
So I made it useful for Tween/Action purposes only in a hacky way.

## How to Use
First of All, You'll need to add `AEScriptRunner` Component to GameObject!  
and, Create an AEScript in the Project Window (`Assets/Create/C# AEScript`)  
and Write Code!, then assign the AEScript to `AEScriptRunner` Component!

- Tweening Transform
```csharp
public static class SomethingAEX {

  public static ActionBase Create (IAEScriptContext ctx) {
    var tr = ctx.GetTransform("$cube");
    return tr.AEMove(new Vector3(0, 10, 0), 1.5f).SetEasing(Easings.OutQuad);
  }
  
}
```

- Sequential Action
```csharp
public static class SomethingAEX {

  public static ActionBase Create (IAEScriptContext ctx) {
    var tr = ctx.GetTransform("$cube");
    return AE.Sequence(
      tr.AEMove(new Vector3(0, 10, 0), 1.5f),
      tr.AEMove(new Vector3(0, -5, 0), 0.5f).SetRelative(true)
    );
  }
  
}
```

- Coroutine Action (supports WaitForSeconds)
```csharp
public static class SomethingAEX {

  public static ActionBase Create (IAEScriptContext ctx) {
    return AE.WaitCoroutine(() => TestCoroutine());
  }

  private static IEnumerator TestCoroutine () {
    Debug.Log("Haha!");
    yield return new WaitForSeconds(1.5f);
    Debug.Log("Supports WaitForSeconds!");
  }

}
```

- Complex Action
```csharp
public static class SomethingAEX {

  public static ActionBase Create (IAEScriptContext ctx) {
    var tr = ctx.GetTransform("$cube");
    return AE.Repeat(
      AE.Parallel(
        AE.Sequence(
          tr.AEMove(new Vector3(0, 10, 0), 1.5f),
          tr.AEMove(new Vector3(-10, 0, 0), 0.5f).SetRelative(true)
        ),
        AE.Sequence(
          tr.AEScale(new Vector3(2, 2, 2), 1f),
          tr.AEScale(new Vector3(1, 1, 1), 1f)
        )
      )
    ).SetLoops(5);
  }
  
}
```

## Credits
- Easing Functions  
<http://robotacid.com/documents/code/Easing.cs>  
<http://www.robertpenner.com/easing/>

## License
MIT