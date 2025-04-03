using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Curriculum.Common.Implementations;

/// <summary>
/// Static class responsible for YAML serialization and deserialization with enhanced reliability
/// </summary>
public static class YamlSerializer
{
    private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                              .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                              .IgnoreUnmatchedProperties()
                                              .Build();
    private static readonly ISerializer _serializer = new SerializerBuilder()
                                          .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                          .Build();
    
    // Optional logger instance that can be set for logging
    private static ILogger _logger = NullLogger.Instance;
    
    /// <summary>
    /// Sets a logger for YamlSerializer operations
    /// </summary>
    /// <param name="logger">The logger to use</param>
    public static void SetLogger(ILogger logger)
    {
        _logger = logger ?? NullLogger.Instance;
    }

    /// <summary>
    /// Deserialize a YAML string to the specified type with error handling
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    /// <param name="yaml">The YAML string to deserialize</param>
    /// <returns>The deserialized object</returns>
    public static T Deserialize<T>(string yaml)
    {
        try
        {
            _logger.LogDebug("Deserializing YAML content to type {Type}", typeof(T).Name);
            return _deserializer.Deserialize<T>(yaml);
        }
        catch (YamlException ex)
        {
            _logger.LogError(ex, "Failed to deserialize YAML content to type {Type}: {Message}", 
                typeof(T).Name, ex.Message);
            throw new YamlSerializationException($"Failed to deserialize YAML content to type {typeof(T).Name}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error deserializing YAML content to type {Type}: {Message}", 
                typeof(T).Name, ex.Message);
            throw new YamlSerializationException($"Unexpected error deserializing YAML to type {typeof(T).Name}", ex);
        }
    }

    /// <summary>
    /// Serialize an object to YAML string with error handling
    /// </summary>
    /// <typeparam name="T">The type to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <returns>The serialized YAML string</returns>
    public static string Serialize<T>(T obj)
    {
        try
        {
            _logger.LogDebug("Serializing object of type {Type} to YAML", typeof(T).Name);
            return _serializer.Serialize(obj);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to serialize object of type {Type} to YAML: {Message}", 
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