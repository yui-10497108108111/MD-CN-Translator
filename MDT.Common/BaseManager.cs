using System;
public abstract class BaseManager<T> where T : new()
{
    private static readonly Lazy<T> lazy = new Lazy<T>();
    public static T Instance
    {
        get
        {
            return lazy.Value;
        }
    }
}

