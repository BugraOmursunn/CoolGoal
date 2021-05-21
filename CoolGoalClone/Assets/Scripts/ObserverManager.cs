using UnityEngine.Events;

public static class ObserverManager
{
    public static UnityEvent KickBall = new UnityEvent();
    public static UnityEvent BallHitObstacle = new UnityEvent();
    public static UnityEvent BallHitGoal = new UnityEvent();
    public static UnityEvent BreakableHitSoundEffect = new UnityEvent();
    public static UnityEvent DragStarted = new UnityEvent();
}
