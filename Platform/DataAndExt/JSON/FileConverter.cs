// Ignore Spelling: JSON Loc

namespace BestChat.Platform.DataAndExt.JSON;

public class FileThatMightBeNullConverter : System.Text.Json.Serialization.JsonConverter<System.IO.FileInfo?>
{
	public override System.IO.FileInfo? Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, 
			System.Text.Json.JsonSerializerOptions options)
		=> reader.GetString() is string strVal && strVal != ""
			? new System.IO.FileInfo(strVal)
			: null;

	public override void Write(System.Text.Json.Utf8JsonWriter writer, System.IO.FileInfo? fileVal, System.Text.Json
			.JsonSerializerOptions options)
		=> writer.WriteStringValue(fileVal == null
			? ""
			: fileVal.FullName);
}

public class FileConverter : System.Text.Json.Serialization.JsonConverter<System.IO.FileInfo>
{
	public class InvalidLocException() : System.Exception;

	public override System.IO.FileInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, 
			System.Text.Json.JsonSerializerOptions options)
		=> reader.GetString() is string strVal && strVal != ""
			? new System.IO.FileInfo(strVal)
			: throw new InvalidLocException();

	public override void Write(System.Text.Json.Utf8JsonWriter writer, System.IO.FileInfo fileVal, System.Text.Json
			.JsonSerializerOptions options)
		=> writer.WriteStringValue(fileVal.FullName);
}