using UnityEngine;

public class DoubleTap : Singleton<DoubleTap>
{
    private const float DOUBLE_CLICK_TIME = .2f;

    private float lastTapTime;
    
    public void CheckDoubleTap(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap <= DOUBLE_CLICK_TIME)
            {

            }
            else
            {

            }
            lastTapTime = Time.time;
        }
    }
}