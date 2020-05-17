public interface IAnimationEndListener
{ 
    AnimationId ListenedAnimationId { get; }
    void HandleListenedAnimationEnd();
}