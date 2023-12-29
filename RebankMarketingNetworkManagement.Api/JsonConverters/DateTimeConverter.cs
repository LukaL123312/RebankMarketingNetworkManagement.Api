using System.Text.Json.Serialization;
using System.Text.Json;

namespace RebankMarketingNetworkManagement.Api.JsonConverters;

internal class DateTimeConverter
    : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
        reader.GetDateTime();

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options) =>
        writer?.WriteStringValue(
            value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value);
}

