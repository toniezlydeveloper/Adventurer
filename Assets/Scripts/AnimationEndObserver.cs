using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationEndObserver : MonoBehaviour
{
    [SerializeField] private int animationId;

    private readonly List<IAnimationEndListener> _listeners = new List<IAnimationEndListener>();
    
    private AnimationId _previousAnimationId;
    
    private void Update()
    {
        if (!Enum.IsDefined(typeof(AnimationId), animationId))
        {
            Debug.Log("Invalid animation ID has been set!");
            return;
        }
        
        if ((AnimationId) animationId == _previousAnimationId)
        {
            return;
        }
        
        NotifyListeners();
        _previousAnimationId = (AnimationId) animationId;
    }

    public void Subscribe(IAnimationEndListener listener)
    {
        _listeners.Add(listener);
    }

    public void Unsubscribe(IAnimationEndListener listener)
    {
        _listeners.Remove(listener);
    }

    private void NotifyListeners()
    {
        foreach (IAnimationEndListener listener in _listeners.Where(listener => _previousAnimationId == listener.ListenedAnimationId))
        {
            listener.HandleListenedAnimationEnd();
        }
    }
}