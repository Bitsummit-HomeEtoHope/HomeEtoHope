using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
   private static T _instance;

   public static T Intance => _instance;

   protected void Awake()
   {
      if (_instance != null)
         Destroy(gameObject);
      else
         _instance = (T)this;
   }
}