using Curriculum.Common.Services;
using Microsoft.Extensions.Logging;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Curriculum.Common.Implementations;

/// <summary>
/// Service responsible for YAML serialization and deserialization with enhanced reliability and logging
/// </summary>
public class YamlSerializer(ILogger<YamlSerializer> logger) : IYamlSerializer
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
                                                  .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                                  .IgnoreUnmatchedProperties()
                                                  .Build();
    private readonly ISerializer _serializer = new SerializerBuilder()
                                              .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                              .Build();

    /// <summary>
    /// Deserialize a YAML string to the specified type with error handling and logging
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    /// <param name="yaml">The YAML string to deserialize</param>
    /// <returns>The deserialized object or default(T) if deserialization fails</returns>
    public T Deserialize<T>(string yaml)
    {
        try
        {
            logger.LogDebug("Deserializing YAML content to type {Type}", typeof(T).Name);
            return _deserializer.Deserialize<T>(yaml);
        }
        catch (YamlException ex)
        {
            logger.LogError(ex, "Failed to deserialize YAML content to type {Type}: {Message}", 
                typeof(T).Name, ex.Message);
            throw new YamlSerializationException($"Failed to deserialize YAML content to type {typeof(T).Name}", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error deserializing YAML content to type {Type}: {Message}", 
                typeof(T).Name, ex.Message);
            throw new YamlSerializationException($"Unexpected error deserializing YAML to type {typeof(T).Name}", ex);
        }
    }

    /// <summary>
    /// Serialize an object to YAML string with error handling and logging
    /// </summary>
    /// <typeparam name="T">The type to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <returns>The serialized YAML string</returns>
    public string Serialize<T>(T obj)
    {
        try
        {
            logger.LogDebug("Serializing object of type {Type} to YAML", typeof(T).Name);
            return _serializer.Serialize(obj);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to serialize object of type {Type} to YAML: {Message}", 
                typeof(T).Name, ex.Message);
            throw new YamlSerializationException($"Failed to serialize object of type {typeof(T).Name} to YAML", ex);
        }
    }
}

/// <summary>
/// Custom exception for YAML serialization/deserialization errors
/// </summary>
public class YamlSerializationException : Exception
{
    public YamlSerializationException(string message) : base(message)
    {
    }

    public YamlSerializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}