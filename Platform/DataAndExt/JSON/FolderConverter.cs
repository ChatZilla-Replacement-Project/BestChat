// Ignore Spelling: JSON Loc

namespace BestChat.Platform.DataAndExt.JSON;

public class FolderThatMightBeNullConverter : System.Text.Json.Serialization.JsonConverter<System.IO
	.DirectoryInfo?>
{
	public override System.IO.DirectoryInfo? Read(ref System.Text.Json.Utf8JsonReader reader, System.Type
		typeToConvert, System.Text.Json.JsonSerializerOptions options)
		=> reader.GetString() is string strVal && strVal != ""
			? new System.IO.DirectoryInfo(strVal)
			: null;

	public override void Write(System.Text.Json.Utf8JsonWriter writer, System.IO.DirectoryInfo? dirVal, System.Text
			.Json.JsonSerializerOptions options)
		=> writer.WriteStringValue(dirVal == null
			? ""
			: dirVal.FullName);
}

public class FolderConverter : System.Text.Json.Serialization.JsonConverter<System.IO.DirectoryInfo>
{
	public class InvalidLocException() : System.Exception;

	public override System.IO.DirectoryInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type
			typeToConvert, System.Text.Json.JsonSerializerOptions options)
		=> reader.GetString() is string strVal && strVal != ""
			? new System.IO.DirectoryInfo(strVal)
			: throw new InvalidLocException();

	public override void Write(System.Text.Json.Utf8JsonWriter writer, System.IO.DirectoryInfo dirVal, System.Text
			.Json.JsonSerializerOptions options)
		=> writer.WriteStringValue(dirVal.FullName);
}