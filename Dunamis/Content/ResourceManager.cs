using System;
using System.Collections.Generic;
using System.Linq;

namespace Dunamis.Content
{
    public class ResourceManager
    {
        List<ILoader> loaders;

        public T Load<T>(string filename)
        {
            if (!HasLoader<ILoader<T>>())
            {
                return default(T); // TODO: throw error here
            }
            else
            {
                return loaders.OfType<ILoader<T>>().FirstOrDefault().Load(filename);
            }
        }
        public void AddLoader(ILoader loader)
        {
            loaders.Add(loader);
        }
        public void RemoveLoader<T>() where T : ILoader
        {
            List<T> removeQueue = loaders.OfType<T>().ToList();
            foreach (T loader in removeQueue)
            {
                loaders.Remove(loader);
            }
        }
        public bool HasLoader<T>() where T : ILoader
        {
            return loaders.OfType<T>().Any();
        }
        public T GetLoader<T>() where T : ILoader
        {
            if (!HasLoader<T>())
            {
                return default(T);
            }
            else
            {
                return loaders.OfType<T>().FirstOrDefault();
            }
        }

        public ResourceManager()
        {
            loaders = new List<ILoader>();
        }
    }
}
