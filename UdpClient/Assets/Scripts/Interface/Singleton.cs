
public class Singleton<T> where T : class, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
                (instance as Singleton<T>).Init();
            }
            return instance;
        }
    }
    public virtual void Init()
    { 
    }
}