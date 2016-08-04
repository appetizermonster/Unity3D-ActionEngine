# ActionEngine

Simple, powerful, productive Action Engine for Unity3D, inspired by DOTween, Cocos2d

## Features
- Play/Pause/Resume/Loop/Complete Playback
- Composite Tween Actions with Coroutines
- Object Pooling for Default

## Differences with Tween Engine
- Coroutine Support (Partial)
- No Reverse Playback
- No Goto
- No Duration

## How to Use
- Tweening Transform
```csharp
using ActionEngine;

Transform tr;
tr.AEMove(new Vector3(0, 10, 0), 1.5f).Easing(Easings.OutQuad).Play();
```

- Sequential Action
```csharp
using ActionEngine;

Transform tr;
AE.Sequence(
  tr.AEMove(new Vector3(0, 10, 0), 1.5f),
  tr.AEMove(new Vector3(0, -5, 0), 0.5f).Relative(true)
).Play();
```

- Coroutine Action (supports WaitForSeconds)
```csharp
using ActionEngine;

void Start () {
  AE.Coroutine(
    () => TestCoroutine()
  ).Play();
}

IEnumerator TestCoroutine () {
  Debug.Log("Haha!");
  yield return new WaitForSeconds(1.5f);
  Debug.Log("Supports WaitForSeconds!");
}
```

- Complex Action
```csharp
using ActionEngine;

Transform tr;
AE.Repeat(
  AE.Parallel(
    AE.Sequence(
      tr.AEMove(new Vector3(0, 10, 0), 1.5f),
      tr.AEMove(new Vector3(-10, 0, 0), 0.5f).Relative(true)
    ),
    AE.Sequence(
      tr.AEScale(new Vector3(2, 2, 2), 1f),
      tr.AEScale(new Vector3(1, 1, 1), 1f)
    )
  )
).Loops(5).Play();
```

## Credits
- Easing Functions  
<http://robotacid.com/documents/code/Easing.cs>  
<http://www.robertpenner.com/easing/>

## License
MIT