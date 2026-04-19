using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Xml;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.Components.MakerChecker
{
    /// <summary>
    /// The matrix XML serializer class.
    /// </summary>
    public static class EntityBaseXmlSerializer
    {
        public const string _SERIALIZER_VERSION = "1";
        private const string _SERIALIZER_INFORMATION_ELEMENT_NAME = "SerializationInformation";
        private const string _SERIALIZER_ROOT_ELEMENT_NAME = "Serialization";
        /// <summary>
        /// 
        /// </summary>
        public enum MakerAndCheckerAction
        {
            Add, Edit, Delete
        }

        /// <summary>
        /// Serializes the specified entity.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <param name="Action">The action of type <see cref="EntityBaseXmlSerializer.MakerAndCheckerAction"/></param>
        /// <returns></returns>
        public static SerializationResult Serialize(EntityBase entity, MakerAndCheckerAction Action)
        {
            return EntityBaseXmlSerializer.Serialize(entity, Action, null);
        }



        public static SerializationResult Serialize(EntityBase entity, MakerAndCheckerAction Action, Dictionary<string, object> AdditionalAttributes)
        {            
            SerializationResult result = new SerializationResult { Action = Action };

            StringBuilder strBuilder = new StringBuilder();
            XmlWriter writer = XmlTextWriter.Create(strBuilder, new XmlWriterSettings() { Indent = true });

            writer.WriteStartDocument();
            writer.WriteStartElement(_SERIALIZER_ROOT_ELEMENT_NAME);
            // Write Info tag
            writer.WriteStartElement(_SERIALIZER_INFORMATION_ELEMENT_NAME);
            writer.WriteStartElement("Version");
            writer.WriteValue(_SERIALIZER_VERSION);
            writer.WriteEndElement();
            writer.WriteStartElement("Action");
            switch (Action)
            {
                case MakerAndCheckerAction.Add: writer.WriteValue("Add"); break;
                case MakerAndCheckerAction.Edit: writer.WriteValue("Edit"); break;
                case MakerAndCheckerAction.Delete: writer.WriteValue("Delete"); break;
                default: writer.WriteValue(string.Empty); break;
            }
            writer.WriteEndElement();
            writer.WriteEndElement();

            // Write <Entity> tag
            writer.WriteStartElement("Entity");
            writer.WriteStartAttribute("type");
            writer.WriteValue(entity.GetType().Name);
            writer.WriteEndAttribute();

            foreach (PropertyInfo fieldInfo in entity.GetType().GetProperties().OrderBy(o => o.Name))
            {
                #region use in case of collections inside entity
                #endregion
                writer.WriteStartElement(fieldInfo.Name);
                switch (fieldInfo.Name.ToUpper())
                {
                    default:
                        object value = fieldInfo.GetValue(entity, null);
                        if (value != null)
                        {
                            if (value is EntityBase)
                            {
                                writer.WriteValue((value as EntityBase).Key);
                            }
                            else if (value is Enum)
                            {
                                writer.WriteValue((value as Enum).ToString());
                            }
                            else
                            {
                                writer.WriteValue(value);
                            }
                        }

                        break;
                }
                writer.WriteEndElement();
            } 

            if (AdditionalAttributes != null && AdditionalAttributes.Count > 0)
            {
                writer.WriteStartElement("Attributes");
                
                foreach( KeyValuePair<string,object> pair in AdditionalAttributes )
                {
                    writer.WriteStartElement("Attribute");
                    writer.WriteStartAttribute("Key");
                    writer.WriteValue(pair.Key);
                    writer.WriteEndAttribute();
                    writer.WriteStartAttribute("Value");
                    writer.WriteValue(pair.Value.ToString());
                    writer.WriteEndAttribute();
                    writer.WriteEndElement();
                }
                
            }
                        

            // Write </Matrix> tag
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            result.Entity = strBuilder.ToString();
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public struct DeserializationResult
        {
            public GarantiaBase Item;
            /// <summary>
            ///   <see cref="object"/>
            /// </summary>
            public Dictionary<string, object> Attributes;
            /// <summary>
            ///   <see cref="EntityBaseXmlSerializer.MakerAndCheckerAction"/>
            /// </summary>
            public MakerAndCheckerAction Action;
        }

        /// <summary>
        /// 
        /// </summary>
        public struct SerializationResult
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public string Entity;
            /// <summary>
            ///   <see cref="EntityBaseXmlSerializer.MakerAndCheckerAction"/>
            /// </summary>
            public MakerAndCheckerAction Action;
        }

        /// <summary>
        /// Deserializes the specified matrix serialized.
        /// </summary>
        /// <param name="EntitySerialized">The matrix serialized of type <see cref="System.String"/></param>
        /// <returns></returns>
        public static DeserializationResult Deserialize(string EntitySerialized)
        {
            DeserializationResult result = new DeserializationResult();
            GarantiaBase m = null;
            XmlDocument _doc = new XmlDocument();
            _doc.LoadXml(EntitySerialized);

            Dictionary<string, object> _Attributes = new Dictionary<string,object>();

            string objAction = _doc.SelectSingleNode(_SERIALIZER_ROOT_ELEMENT_NAME + "/" + _SERIALIZER_INFORMATION_ELEMENT_NAME).Attributes["Action"].Value;

            XmlNodeList lstNodeAttribute = _doc.SelectNodes("Entity/Attributes");
                        
            foreach (XmlNode node in lstNodeAttribute)
            {
                if (node.ChildNodes[0].Attributes[0].Name.ToUpper() == "KEY" && node.ChildNodes[0].Attributes[1].Name.ToUpper() == "VALUE")                
                    _Attributes.Add(node.ChildNodes[0].Attributes["Key"].Value.ToString(), node.ChildNodes[0].Attributes["Value"].Value);
            }            
            
            result.Attributes = _Attributes;

            switch (objAction)
            {
                case "Add": result.Action = MakerAndCheckerAction.Add; break;
                case "Edit": result.Action = MakerAndCheckerAction.Edit; break;
                case "Delete": result.Action = MakerAndCheckerAction.Delete; break;
                default: result.Action = MakerAndCheckerAction.Edit; break;
            }
                    
            result.Item = m;
            return result;
        }
    }
}

/*
<?xml version="1.0" encoding="utf-16"?>
<Matrix>
  <ID>9043</ID>
  <LimitValue>14300000</LimitValue>
  <Comments> </Comments>
  <ExpirationDate>2010-10-31T00:00:00</ExpirationDate>
  <Active>true</Active>
  <Name>ARCOR S.A.I.C.</Name>
  <LastLimit>3100000</LastLimit>
  <Definition ID="578" />
  <Filter>
    <Filter>
      <ID>77802</ID>
      <MatrixID>9043</MatrixID>
      <OperatorID>1</OperatorID>
      <Negate>false</Negate>
      <DataObjectDataTableID>DimCustomer</DataObjectDataTableID>
      <DataObjectColumn>CUSTOMER_NO</DataObjectColumn>
      <Value>&lt;Values&gt;&lt;Value&gt;ARCOARBA0&lt;/Value&gt;&lt;/Values&gt;</Value>
    </Filter>
  </Filter>
  <MatrixState StateID="1" />
  <Matrix_Type MatrixTypeID="0" />
</Matrix>
*/