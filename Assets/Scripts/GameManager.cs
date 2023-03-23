using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* This is an example of a singleton. This acts as a global
     * instance of a MonoBehaviour script, which can be applied
     * to a game object, whereas a static script would not be.
     * 
     * This allows you to call any public variable or method from
     * this class by preceeding it with "GameManager.instance."
     * 
     * You may want to put game code in here, or have specific other
     * singletons for other managers, eg. an audio manager, a scene
     * manager, an input manager, so on and so forth. 
     * 
     * If you didn't know about this design pattern, I hope this helps!
     * Of course, you are under no obligation to use it if you prefer
     * abstraction or whatever else, but sometimes it helps to have your
     * game logic and variables in one accessible place.
     * -Rian
     */
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
}
