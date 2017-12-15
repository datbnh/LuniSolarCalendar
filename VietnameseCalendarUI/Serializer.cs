using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Augustine.VietnameseCalendar.UI
{
    public static class Serializer
    {
        // textual serializing
        public static void SerializeObject<T>(this T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }
            XmlDocument xmlDocument = new XmlDocument();
            DataContractSerializer serializer = new DataContractSerializer(serializableObject.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }

        public static T DeSerializeObject<T>(string fileName)
        {
            T objectOut = default(T);
            if (string.IsNullOrEmpty(fileName)) { return objectOut; }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;
            using (StringReader read = new StringReader(xmlString))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objectOut = (T)serializer.ReadObject(reader);
                    reader.Close();
                }

                read.Close();
            }
            return objectOut;
        }

        public static void LoadFromFile<T>(string fileName, out T serializableObject)
        {
            T deserialized;
            try { deserialized = Serializer.DeSerializeObject<T>(fileName); }
            catch (Exception ex) { throw new Exception("Cannot parse data from file.", ex); }
            serializableObject = deserialized;
        }

        public static void SaveToFile<T>(this T serializableObject, string fileName)
        {
            try { Serializer.SerializeObject<T>(serializableObject, fileName); }
            catch (Exception ex) { throw new Exception("Cannot save data to file.", ex); }
        }


        // binary serializing
        public static void BinarySerializeObject<T>(this T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }
            using (FileStream stream = File.Open(fileName, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(serializableObject.GetType());
                using (XmlDictionaryWriter binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream))
                {
                    serializer.WriteObject(binaryDictionaryWriter, serializableObject);
                    binaryDictionaryWriter.Close();
                }
                stream.Close();
            }
        }

        public static T BinaryDeSerializeObject<T>(string fileName)
        {
            T objectOut = default(T);
            if (string.IsNullOrEmpty(fileName)) { return objectOut; }
            using (FileStream stream = File.Open(fileName, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                using (XmlDictionaryReader binaryDictionaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
                {
                    objectOut = (T)serializer.ReadObject(binaryDictionaryReader);
                    binaryDictionaryReader.Close();
                }
                stream.Close();
            }
            return objectOut;
        }


        public static void LoadFromBinaryFile<T>(string fileName, out T serializableObject)
        {
            T deserialized;
            try { deserialized = Serializer.BinaryDeSerializeObject<T>(fileName); }
            catch (Exception ex) { throw new Exception("Cannot parse data from file.", ex); }
            serializableObject = deserialized;
        }

        public static void SaveToBinaryFile<T>(this T serializableObject, string fileName)
        {
            try { Serializer.BinarySerializeObject<T>(serializableObject, fileName); }
            catch (Exception ex) { throw new Exception("Cannot save data to file.", ex); }
        }

    }
}
