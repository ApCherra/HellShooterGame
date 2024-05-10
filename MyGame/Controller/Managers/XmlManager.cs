using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGame.Controller.Managers
{
    public class XmlManager<T>
    {
        public Type Type;

        public XmlManager() 
        {
            Type = typeof(T);
        }

        /// <summary>
        /// Loads xml file for serialization
        /// </summary>
        /// <param name="path"></param>
        /// <returns> </returns>
        public T Load(string path) {
            T instance;
            using(TextReader reader = new StreamReader(path)) {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        /// <summary>
        /// Saves object to xml file
        /// </summary>
        /// <param name="path"> Path to xml file </param> 
        /// <param name="obj"> Object to be saved </param> 
        public void Save(string path, object obj) {
            using(TextWriter writer = new StreamWriter(path)) {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}