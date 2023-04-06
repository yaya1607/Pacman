
using System;
using UnityEngine;

public abstract class Ghost_Behavior : MonoBehaviour
{
    public float passedDuration = 0;
    public Ghost ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        
        CancelInvoke();
        Invoke(nameof(Disable),duration) ;
    }

    public virtual void Disable()
    {
        this.enabled = false;
        passedDuration = 0;
        CancelInvoke();
    }
    private void Update()
    {
        passedDuration += Time.deltaTime;
    }
    public float DurationRemaining()
    {
        if (!this.enabled)
            return 0;
        return duration - passedDuration;
    }
}
