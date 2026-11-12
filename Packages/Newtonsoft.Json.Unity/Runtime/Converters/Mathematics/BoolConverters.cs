#if HAVE_MODULE_MATHEMATICS
using Unity.Mathematics;

namespace Newtonsoft.Json.Unity.Mathematics {
  
  public class Bool2Converter : PartialConverter<bool2> {
    protected override void ReadValue(ref bool2 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsBoolean() ?? false;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, bool2 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
    }
  }
  
  public class Bool3Converter : PartialConverter<bool3> {
    protected override void ReadValue(ref bool3 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.z):
        value.z = reader.ReadAsBoolean() ?? false;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, bool3 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
      writer.WritePropertyName(nameof(value.z));
      writer.WriteValue(value.z);
    }
  }
  
  public class Bool4Converter : PartialConverter<bool4> {
    protected override void ReadValue(ref bool4 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.z):
        value.z = reader.ReadAsBoolean() ?? false;
        break;
      case nameof(value.w):
        value.w = reader.ReadAsBoolean() ?? false;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, bool4 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
      writer.WritePropertyName(nameof(value.z));
      writer.WriteValue(value.z);
      writer.WritePropertyName(nameof(value.w));
      writer.WriteValue(value.w);
    }
  }

}
#endif // HAVE_MODULE_MATHEMATICS
