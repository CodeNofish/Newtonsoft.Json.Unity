using System.Reflection;
using UnityEngine;

namespace Newtonsoft.Json.Unity {

  public class CullingGroupEventConverter : PartialConverter<CullingGroupEvent> {
    private const byte DistanceMask = (1 << 7) - 1;

    private static readonly FieldInfo IndexField = typeof(CullingGroupEvent).GetFieldInfoOrThrow("m_Index");
    private static readonly FieldInfo PrevStateField = typeof(CullingGroupEvent).GetFieldInfoOrThrow("m_PrevState");
    private static readonly FieldInfo ThisStateField = typeof(CullingGroupEvent).GetFieldInfoOrThrow("m_ThisState");

    protected override void ReadValue(ref CullingGroupEvent value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.index):
        IndexField.SetValueDirectRef(ref value, reader.ReadAsInt32() ?? 0);
        break;
      case nameof(value.isVisible):
        SetStateField(ThisStateField, ref value, value.currentDistance, reader.ReadAsBoolean() ?? false);
        break;
      case nameof(value.wasVisible):
        SetStateField(PrevStateField, ref value, value.previousDistance, reader.ReadAsBoolean() ?? false);
        break;
      case nameof(value.currentDistance):
        SetStateField(ThisStateField, ref value, reader.ReadAsInt32() ?? 0, value.isVisible);
        break;
      case nameof(value.previousDistance):
        SetStateField(PrevStateField, ref value, reader.ReadAsInt32() ?? 0, value.wasVisible);
        break;
      }
    }

    private static void SetStateField(FieldInfo field, ref CullingGroupEvent value, int distance, bool isVisible) {
      var isVisibleByte = isVisible ? (byte)0x80 : (byte)0;
      var stateByte = (byte)(isVisibleByte | (distance & DistanceMask));
      field.SetValueDirectRef(ref value, stateByte);
    }

    protected override void WriteJsonProperties(JsonWriter writer, CullingGroupEvent value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.index));
      writer.WriteValue(value.index);
      writer.WritePropertyName(nameof(value.isVisible));
      writer.WriteValue(value.isVisible);
      writer.WritePropertyName(nameof(value.wasVisible));
      writer.WriteValue(value.wasVisible);
      writer.WritePropertyName(nameof(value.currentDistance));
      writer.WriteValue(value.currentDistance);
      writer.WritePropertyName(nameof(value.previousDistance));
      writer.WriteValue(value.previousDistance);
    }
  }

}
