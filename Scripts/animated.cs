using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Animated : MonoBehaviour
{
    // Existing variables (not to be changed)
    private SpriteRenderer _renderer;
    public Sprite idleSprite;
    public Sprite[] animationSprites;
    public float animationTime = 0.25f;
    public bool loop = true;
    public bool idle = true;
    private int _currentFrame;

    private void Awake()
    {
        InitializeRenderer();
    }

    private void OnEnable()
    {
        EnableRenderingAndAnimation();
    }

    private void OnDisable()
    {
        DisableRenderingAndAnimation();
    }

    private void InitializeRenderer()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void EnableRenderingAndAnimation()
    {
        _renderer.enabled = true;
        BeginAnimationCycle();
    }

    private void DisableRenderingAndAnimation()
    {
        _renderer.enabled = false;
        EndAnimationCycle();
    }

    private void BeginAnimationCycle()
    {
        InvokeRepeating(nameof(UpdateAnimationFrame), animationTime, animationTime);
    }

    private void EndAnimationCycle()
    {
        CancelInvoke(nameof(UpdateAnimationFrame));
    }

    private void UpdateAnimationFrame()
    {
        _renderer.sprite = idle ? idleSprite : GetNextAnimationFrame();
    }

    private Sprite GetNextAnimationFrame()
    {
        IncrementAnimationFrame();
        return animationSprites[_currentFrame];
    }

    private void IncrementAnimationFrame()
    {
        _currentFrame = loop ? (_currentFrame + 1) % animationSprites.Length : Mathf.Min(_currentFrame + 1, animationSprites.Length - 1);
    }
}
