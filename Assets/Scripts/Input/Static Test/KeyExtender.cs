using UnityEngine;

namespace InputExtender
{
    public static class KeyExtender
    {
        public static float timer;
        public static float lastTimer;
        public static float currentTimer;
        private static int clickCounter = 0;

        public static bool isDoubleTap (KeyCode key)
        {
            if (Input.GetKeyUp(key))
            {
                clickCounter += 1;

                if (clickCounter == 1)
                {
                    lastTimer = Time.unscaledTime;
                }
                
                else if (clickCounter == 2)
                {
                    currentTimer = Time.unscaledTime;
                    
                    float difference = currentTimer - lastTimer;

                    if (difference <= 0.3f)
                    {
                        clickCounter = 0;
                        return true;
                    }
                    else
                    {
                        Debug.Log("asd");
                        clickCounter = 0;
                    }
                }
            }
            return false;
        }
    }
}