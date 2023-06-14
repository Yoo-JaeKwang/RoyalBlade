using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class ViewController : MonoBehaviour
{
    protected View View { get; set; }
    protected Presenter Presenter { get; set; }
    private void Awake() => Initialize();
    private void Start() => Presenter.OnInitialize(View);
    private void OnDestroy() => Presenter.OnRelease();
    protected abstract void Initialize();
}