using System.Reflection;
using RandomState = UnityEngine.Random.State;

namespace Newtonsoft.Json.Unity {

  public class RandomStateConverter : PartialConverter<RandomState> {
    // If field does not exist it should invalidate the converter for
    // the entirety of the programs lifetime. Which is fine in this case.
    private static readonly FieldInfo S0Field = typeof(RandomState).GetFieldInfoOrThrow("s0");
    private static readonly FieldInfo S1Field = typeof(RandomState).GetFieldInfoOrThrow("s1");
    private static readonly FieldInfo S2Field = typeof(RandomState).GetFieldInfoOrThrow("s2");
    private static readonly FieldInfo S3Field = typeof(RandomState).GetFieldInfoOrThrow("s3");

    protected override void ReadValue(ref RandomState value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case "s0":
        S0Field.SetValueDirectRef(ref value, reader.ReadAsInt32() ?? 0);
        break;
      case "s1":
        S1Field.SetValueDirectRef(ref value, reader.ReadAsInt32() ?? 0);
        break;
      case "s2":
        S2Field.SetValueDirectRef(ref value, reader.ReadAsInt32() ?? 0);
        break;
      case "s3":
        S3Field.SetValueDirectRef(ref value, reader.ReadAsInt32() ?? 0);
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, RandomState value, JsonSerializer serializer) {
      writer.WritePropertyName("s0");
      writer.WriteValue(S0Field.GetValue(value));
      writer.WritePropertyName("s1");
      writer.WriteValue(S1Field.GetValue(value));
      writer.WritePropertyName("s2");
      writer.WriteValue(S2Field.GetValue(value));
      writer.WritePropertyName("s3");
      writer.WriteValue(S3Field.GetValue(value));
    }
  }

}
