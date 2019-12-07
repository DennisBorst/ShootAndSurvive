using System;
using UnityEngine;

public enum Achievement
{
    ONE,
    TWO,
    THREE
}

public interface IAchievementSystem
{
    // Maybe we need to store information about what achievements are called
    //  for each platform
    //Dictionary<Achievement,string> SKU_Conversion;

    void YouDidWell(Achievement achievement);
}

public class DummyAchievementSystem : IAchievementSystem
{
    public void YouDidWell(Achievement achievement)
    {
        Debug.Log("You did well: " + achievement.ToString());
    }
}

public class PCAchievementSystem : IAchievementSystem
{
    public void YouDidWell(Achievement achievement)
    {
        // It should crash if the system hasn't been built yet
        throw new NotImplementedException();
    }
}

public class XboxAchievementSystem : IAchievementSystem
{
    public void YouDidWell(Achievement achievement)
    {
        throw new NotImplementedException();
    }
}

public static class ServiceManager
{
    static IAchievementSystem instance;
    public static IAchievementSystem GetAchievementSystem()
    {
        if (instance == null)
        {
            //platform-dependent selection
#if UNITY_XBOX
            instance = new XboxAchievementSystem();
#elif UNITY_STANDALONE
            instance = new PCAchievementSystem();
#else
            instance = new DummyAchievementSystem();
#endif
        }

        return instance;
    }
}




public class Preprocessor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // This value was set using the PlayerSettings -> Scripting Define Symbols
#if AAP
        Debug.Log("AAP BESTAAT!");
#else
        Debug.Log("AAP BESTAAT NIET");
#endif //AAP

        IAchievementSystem system = ServiceManager.GetAchievementSystem();
        system.YouDidWell(Achievement.ONE);
        system.YouDidWell(Achievement.TWO);
        system.YouDidWell(Achievement.THREE);
    }
}