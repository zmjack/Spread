#if DEBUG
using System.Xml.Serialization;
using System.Xml;

namespace Spread;

public class XmlSerializerPool
{
    private readonly XmlSerializerNamespaces _ns;

    public XmlSerializerPool()
    {
        _ns = new XmlSerializerNamespaces();
        _ns.Add(string.Empty, string.Empty);
    }

    public XmlSerializerPool(XmlSerializerNamespaces? ns)
    {
        _ns = ns;
    }

    public Dictionary<Type, XmlSerializer> XmlSerializers = [];

    public XmlSerializer this[Type type]
    {
        get
        {
            if (XmlSerializers.TryGetValue(type, out var serializer))
            {
                return serializer;
            }
            else
            {
                var _serializer = new XmlSerializer(type);
                XmlSerializers[type] = _serializer;
                return _serializer;
            }
        }
    }

    public void Serialize<T>(XmlWriter writer, T value)
    {
        this[typeof(T)].Serialize(writer, value, _ns);
    }

    public void Deserialize<T>(XmlWriter writer, T value)
    {
        this[typeof(T)].Serialize(writer, value, _ns);
    }
}
#endif
