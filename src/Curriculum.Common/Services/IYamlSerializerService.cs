namespace Curriculum.Common.Services;

/// <summary>
/// Interface for YAML serialization and deserialization services
/// </summary>
public interface IYamlSerializerService
{
    /// <summary>
    /// Deserialize a YAML string to the specified type
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    /// <param name="yaml">The YAML string to deserialize</param>
    /// <returns>The deserialized object</returns>
    T Deserialize<T>(string yaml);
    
    /// <summary>
    /// Serialize an object to YAML string
    /// </summary>
    /// <typeparam name="T">The type to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <returns>The serialized YAML string</returns>
    string Serialize<T>(T obj);
}