using System;
using System.Collections;
using System.Reflection;
using Conway.Threading;
using Chucksoft.Entities;

namespace Chucksoft.Resources
{
    public class Resource  
    {
        private static readonly Hashtable indexedTypes = new Hashtable();
        private static readonly object _lock = new object();

        /// <summary>
        /// Pass in the type, and if it's in the assembly get an instanstated one back.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetResource<T>()
        {
            //Generic variable
            T resource = default(T);           

            //Get the generic Type
            Type genericType = typeof(T);
            Type instance = null;

            //If we have already found the type, no use looking again.
            if (indexedTypes.ContainsKey(genericType.FullName))
            {
                //Get the type by the generic name
                instance = (Type)indexedTypes[genericType.FullName];               
            }
            else
            {
                //Find the type...
                instance = FindTypeInExecutingAssembly(genericType, instance);
            }

            //Create the instance and return it.
            if (instance != null)
            {
                resource = (T)Activator.CreateInstance(instance);
            }

            return resource;
        }


        /// <summary>
        /// Finds the type of "T" in the current assembly.
        /// </summary>
        /// <param name="genericType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        private static Type FindTypeInExecutingAssembly(Type genericType, Type instance)
        {
            //We have to look for it. Lets get the assemebly
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            GallerySettings settings = GallerySettings.Load();

            //Go though all the types looking for the interface.
            foreach (Type type in types)
            {
                //Loads the correct Sql instance, in regards to what's in the settings.
                if (type.GetInterface(genericType.FullName) != null && type.FullName.ToLower().Contains(settings.DataStorage.ToString().ToLower()))
                {
                    instance = type;
                    LockingHelper.SaveTypeInHashTable(type, genericType.FullName, indexedTypes, _lock); 
                    break;
                }
            }
            return instance;
        }

    }
}
