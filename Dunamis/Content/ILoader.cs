using System;

namespace Dunamis.Content
{
    public interface ILoader
    {
    }
    public interface ILoader<T> : ILoader
    {
        T Load(string filename);
    }
}
