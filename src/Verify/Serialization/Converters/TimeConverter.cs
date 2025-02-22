﻿#if NET6_0_OR_GREATER

using Newtonsoft.Json;
using VerifyTests;

class TimeConverter :
    WriteOnlyJsonConverter<TimeOnly>
{
    public override void WriteJson(
        JsonWriter writer,
        TimeOnly value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WriteValue(value.ToString("h:mm tt", serializer.Culture));
    }
}
#endif