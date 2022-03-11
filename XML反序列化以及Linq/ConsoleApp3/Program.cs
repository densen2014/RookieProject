// See https://aka.ms/new-console-template for more information
using System.Xml.Serialization;


Console.WriteLine("Reading with Stream");
// Create an instance of the XmlSerializer.
XmlSerializer serializer =
new XmlSerializer(typeof(config));

// Declare an object variable of the type to be deserialized.
config i;

using (Stream reader = new FileStream(@"天然气中压架空.xml", FileMode.Open))
{
    // Call the Deserialize method to restore the object's state.
    i = (config)serializer.Deserialize(reader);
}
Console.WriteLine("-----源");
foreach (var item in i.attribute)
{
    Console.WriteLine(item.name);
}

var xx=i.attribute.Skip(10);
var xx2=i.attribute.Take(10);
var xx3 = xx.Concat(xx2);

Console.WriteLine("------排序");
foreach (var item in xx3)
{
    Console.WriteLine(item.name);
}

// 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class config
{

    private configField[] attributeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("field", IsNullable = false)]
    public configField[] attribute
    {
        get
        {
            return this.attributeField;
        }
        set
        {
            this.attributeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class configField
{

    private string nameField;

    private object notEmptyField;

    private string typeField;

    private string formatField;

    private byte widthField;

    private byte heightField;

    private string checkedNameField;

    private configFieldLI[] ulField;

    private object readOnlyField;

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public object notEmpty
    {
        get
        {
            return this.notEmptyField;
        }
        set
        {
            this.notEmptyField = value;
        }
    }

    /// <remarks/>
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    public string format
    {
        get
        {
            return this.formatField;
        }
        set
        {
            this.formatField = value;
        }
    }

    /// <remarks/>
    public byte width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    public byte height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }

    /// <remarks/>
    public string checkedName
    {
        get
        {
            return this.checkedNameField;
        }
        set
        {
            this.checkedNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("li", IsNullable = false)]
    public configFieldLI[] ul
    {
        get
        {
            return this.ulField;
        }
        set
        {
            this.ulField = value;
        }
    }

    /// <remarks/>
    public object readOnly
    {
        get
        {
            return this.readOnlyField;
        }
        set
        {
            this.readOnlyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class configFieldLI
{

    private byte keyField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte key
    {
        get
        {
            return this.keyField;
        }
        set
        {
            this.keyField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

