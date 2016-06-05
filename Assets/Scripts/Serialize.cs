using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public abstract class SerialBase
{
  protected string fileName;
  protected FileStream fs;
  protected BinaryFormatter bf;

  public SerialBase(string fileName)
  {
    this.fileName = fileName;
    Init();
  }

  protected virtual void Init() { bf = new BinaryFormatter(); }
}

public class Serializer : SerialBase
{
  public Serializer(string fileName) : base(fileName) { }

  protected override void Init()
  {
    base.Init();

    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
  }

  public Serializer Save<T>(T data)
  {
    bf.Serialize(fs, data);
    return this;
  }
}

public class Deserializer : SerialBase
{
  public Deserializer(string fileName) : base(fileName) { }

  public bool IsValid = true;

  protected override void Init()
  {
    base.Init();

    try
    {
      fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    }
    catch (FileNotFoundException)
    {
      IsValid = false;
    }
  }

  public Deserializer Load<T>(ref T member)
  {
    member = (T)bf.Deserialize(fs);
    return this;
  }
}
