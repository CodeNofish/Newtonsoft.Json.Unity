using System;

namespace Newtonsoft.Json.Unity {

  public abstract class PartialConverter<T> : JsonConverter where T : new() {
    protected abstract void ReadValue(ref T value, string name, JsonReader reader, JsonSerializer serializer);

    protected abstract void WriteJsonProperties(JsonWriter writer, T value, JsonSerializer serializer);


    public override bool CanConvert(Type objectType) {
      return objectType == typeof(T) ||
             (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>) && objectType.GenericTypeArguments[0] == typeof(T));
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
      if (reader.TokenType == JsonToken.Null) {
        var isNullableStruct = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
        return isNullableStruct ? null : default(T);
      }

      return InternalReadJson(reader, serializer, existingValue);
    }

    private T? InternalReadJson(JsonReader reader, JsonSerializer serializer, object? existingValue) {
      if (reader.TokenType != JsonToken.StartObject)
        throw reader.CreateSerializationException($"Failed to read type '{typeof(T).Name}'. Expected object start, got '{reader.TokenType}' <{reader.Value}>");

      reader.Read();

      if (!(existingValue is T value)) value = new T();

      string previousName = string.Empty;

      while (reader.TokenType == JsonToken.PropertyName) {
        if (reader.Value is string name) {
          if (name == previousName) {
            throw reader.CreateSerializationException($"Failed to read type '{typeof(T).Name}'. Possible loop when reading property '{name}'");
          }
          previousName = name;
          ReadValue(ref value, name, reader, serializer);
        } else {
          reader.Skip();
        }

        reader.Read();
      }

      return value;
    }

    /// <summary>
    /// Write the specified properties of the object.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
      if (value == null) {
        writer.WriteNull();
        return;
      }

      writer.WriteStartObject();

      var typed = (T)value;
      WriteJsonProperties(writer, typed, serializer);

      writer.WriteEndObject();
    }
  }

}
