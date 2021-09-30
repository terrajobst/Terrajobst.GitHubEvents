
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Terrajobst.GitHubEvents;

internal sealed class GitHubDateTimeConverter : DateTimeConverterBase
{
    private IsoDateTimeConverter _isoDateTimeConverter = new IsoDateTimeConverter();
    private UnixDateTimeConverter _unixDateTimeConverter = new UnixDateTimeConverter();

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Integer)
            return _unixDateTimeConverter.ReadJson(reader, objectType, existingValue, serializer);
        else
            return _isoDateTimeConverter.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
