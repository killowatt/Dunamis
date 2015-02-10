using System;
using System.Collections.Generic;
using System.Linq;

namespace Dunamis.Content
{
    public class ResourceManager
    {
        HashSet<ILoader> loaders; 

        public T Load<T>(string filename)
        {
            if (!HasLoader<ILoader<T>>())
            {
                throw new ArgumentException("The loader of type " + typeof(T) + " was not found.");
            }
            return loaders.OfType<ILoader<T>>().FirstOrDefault().Load(filename);
        }
        public void AddLoader(ILoader loader)
        {
            loaders.Add(loader); // TODO: convert to bool?
        }
        public void RemoveLoader<T>() where T : ILoader
        {
            loaders.Remove(loaders.OfType<T>().FirstOrDefault());
        }
        public bool HasLoader<T>() where T : ILoader
        {
            return loaders.OfType<T>().Any();
        }
        public T GetLoader<T>() where T : ILoader
        {
            if (!HasLoader<ILoader<T>>())
            {
                throw new ArgumentException("The loader of type " + typeof(T) + " was not found.");
            }
            return loaders.OfType<T>().FirstOrDefault(); // TODO: if default, wat?
        }
        public ResourceManager()
        {
            loaders = new HashSet<ILoader>();
        }
    }
}
