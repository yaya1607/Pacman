
using System;
using UnityEngine;

public abstract class Ghost_Behavior : MonoBehaviour
{
    public float passedDuration;
    public Ghost ghost { get; private set; }
    public float initialDuration;
    public float duration ;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(this.initialDuration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        this.passedDuration = 0;
        this.duration = duration;
        CancelInvoke();
        Invoke(nameof(Disable),duration) ;
    }

    public virtual void Disable()
    {
        this.enabled = false;
        this.passedDuration = 0;
        CancelInvoke();
    }
    protected virtual void Update()
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
