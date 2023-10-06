using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace respapi.eshop.Helpers;
public class PagedListConverter<T> : JsonConverter<PagedList<T>> where T : class
{
    public override PagedList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;

            if (!root.TryGetProperty("items", out var itemsArray))
            {
                return new PagedList<T>(new List<T>(), 0, 1, 10);
            }

            List<T> items = new List<T>();
            
            if (itemsArray.ValueKind == JsonValueKind.Array)
            {
                items = JsonSerializer.Deserialize<List<T>>(itemsArray.GetRawText());
            }

            var count = root.GetProperty("totalCount").GetInt32();
            var pageNumber = root.GetProperty("currentPage").GetInt32();
            var pageSize = root.GetProperty("pageSize").GetInt32();
            
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
    public override void Write(Utf8JsonWriter writer, PagedList<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("items");
        JsonSerializer.Serialize(writer, value.ToArray(), options);

        writer.WriteNumber("totalCount", value.TotalCount);
        writer.WriteNumber("currentPage", value.CurrentPage);
        writer.WriteNumber("pageSize", value.PageSize);
        writer.WriteNumber("totalPages", value.TotalPages);

        writer.WriteEndObject();
    }
}

