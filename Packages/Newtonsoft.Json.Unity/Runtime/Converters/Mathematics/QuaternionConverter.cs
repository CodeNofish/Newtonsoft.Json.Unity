#if HAVE_MODULE_MATHEMATICS
using Unity.Mathematics;

namespace Newtonsoft.Json.Unity.Mathematics {

  public class QuaternionConverter : PartialConverter<quaternion> {
    protected override void ReadValue(ref quaternion value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.value.x):
        value.value.x = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.value.y):
        value.value.y = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.value.z):
        value.value.z = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.value.w):
        value.value.w = reader.ReadAsFloat() ?? 0f;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, quaternion value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.value.x));
      writer.WriteValue(value.value.x);
      writer.WritePropertyName(nameof(value.value.y));
      writer.WriteValue(value.value.y);
      writer.WritePropertyName(nameof(value.value.z));
      writer.WriteValue(value.value.z);
      writer.WritePropertyName(nameof(value.value.w));
      writer.WriteValue(value.value.w);
    }
  }

}
#endif // HAVE_MODULE_MATHEMATICS
