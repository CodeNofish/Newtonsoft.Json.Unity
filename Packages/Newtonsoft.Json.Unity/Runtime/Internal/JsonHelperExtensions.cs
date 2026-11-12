using System;
using System.Globalization;
using System.Text;

namespace Newtonsoft.Json.Unity {

  internal static class JsonHelperExtensions {
    private static JsonSerializationException NewJsonSerializationException(string message, string path, int lineNumber, int linePosition, Exception? innerException) {
      if (innerException == null) return new JsonSerializationException(message);
      return new JsonSerializationException(message, innerException);
    }

    public static JsonSerializationException CreateSerializationException(this JsonReader reader, string message, Exception? innerException = null) {
      var builder = CreateStringBuilderWithSpaceAfter(message);

      builder.AppendFormat(CultureInfo.InvariantCulture, "Path '{0}'", reader.Path);

      var lineInfo = reader as IJsonLineInfo;
      int lineNumber = 0;
      int linePosition = 0;

      if (lineInfo?.HasLineInfo() == true) {
        lineNumber = lineInfo.LineNumber;
        linePosition = lineInfo.LinePosition;
        builder.AppendFormat(CultureInfo.InvariantCulture, ", line {0}, position {1}", lineNumber, linePosition);
      }

      builder.Append('.');

      return NewJsonSerializationException(
        builder.ToString(), reader.Path, lineNumber, linePosition, innerException);
    }

    public static JsonWriterException CreateWriterException(this JsonWriter writer, string message, Exception? innerException = null) {
      var builder = CreateStringBuilderWithSpaceAfter(message);

      builder.AppendFormat(CultureInfo.InvariantCulture, "Path '{0}'.", writer.Path);

      return new JsonWriterException(
        builder.ToString(), writer.Path, innerException);
    }

    private static StringBuilder CreateStringBuilderWithSpaceAfter(string message) {
      var builder = new StringBuilder(message);

      if (message.EndsWith("."))
        builder.Append(' ');
      else if (!message.EndsWith(". ")) builder.Append(". ");

      return builder;
    }

    public static T? ReadViaSerializer<T>(this JsonReader reader, JsonSerializer serializer) {
      reader.Read();
      return serializer.Deserialize<T>(reader);
    }

    public static float? ReadAsFloat(this JsonReader reader) {
      // https://github.com/jilleJr/Newtonsoft.Json-for-Unity.Converters/issues/46

      var str = reader.ReadAsString();

      if (string.IsNullOrEmpty(str)) return null;
      if (float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var valueParsed)) return valueParsed;
      return 0f;
    }

    public static byte? ReadAsInt8(this JsonReader reader) {
      return checked((byte)(reader.ReadAsInt32() ?? 0));
    }
  }

}
