using UnityEngine;

namespace Newtonsoft.Json.Unity {
  
  public class ResolutionConverter : PartialConverter<Resolution> {
    protected override void ReadValue(ref Resolution value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.width):
        value.width = reader.ReadAsInt32() ?? 0;
        break;
      case nameof(value.height):
        value.height = reader.ReadAsInt32() ?? 0;
        break;
      case nameof(value.refreshRateRatio):
        value.refreshRateRatio = reader.ReadViaSerializer<RefreshRate>(serializer);
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, Resolution value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.width));
      writer.WriteValue(value.width);
      writer.WritePropertyName(nameof(value.height));
      writer.WriteValue(value.height);
      writer.WritePropertyName(nameof(value.refreshRateRatio));
      serializer.Serialize(writer, value.refreshRateRatio);
    }
  }

}
