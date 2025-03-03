namespace Byteron.software;

public class Filesystem
{
	public static readonly string RootPath = Path.Combine(Application.AppPath, "root");

	public static void Init()
	{
		if (!Directory.Exists(Application.AppPath))
		{
			Directory.CreateDirectory(RootPath);
		}
	}

	public static string ResolvePath(string path) => Path.Combine(RootPath, path);

	public static void CreateFolder(string path)
	{
		string fullPath = ResolvePath(path);
		string directory = Path.GetDirectoryName(fullPath) ?? RootPath;

		if (File.Exists(fullPath)) return;
		if (Directory.Exists(fullPath)) return;
		if (!Directory.Exists(directory))
			Directory.CreateDirectory(directory);
		
		Directory.CreateDirectory(fullPath);
	}

	public static string[] ListFiles(string path)
	{
		string fullPath = ResolvePath(path);

		string[] files = Directory.GetFiles(fullPath);
		List<string> names = [];
		foreach (var file in files)
		{
			names.Add(file.Split(Path.DirectorySeparatorChar)[^1]);
		}

		return names.ToArray();
	}

	public static string[] ListFolders(string path)
	{
		string fullPath = ResolvePath(path);

		string[] folders = Directory.GetDirectories(fullPath);
		List<string> names = [];
		foreach (var folder in folders)
		{
			names.Add(folder.Split(Path.DirectorySeparatorChar)[^1]);
		}

		return names.ToArray();
	}

	public static bool FolderExists(string path)
	{
		string fullPath = ResolvePath(path);
		return Directory.Exists(fullPath);
	}

	public static void RemoveFolder(string path, bool recursive)
	{
		string fullPath = ResolvePath(path);

		Directory.Delete(fullPath, recursive);
	}


	public static void WriteFile(string path, string content)
	{
		string fullPath = ResolvePath(path);
		string directory = Path.GetDirectoryName(fullPath) ?? RootPath;

		if (!Directory.Exists(directory))
			Directory.CreateDirectory(directory);

		File.WriteAllText(fullPath, content);
	}

	public static string ReadFile(string path)
	{
		string fullPath = ResolvePath(path);
		return (File.Exists(fullPath) ? File.ReadAllText(fullPath) : null) ?? "";
	}

	public static bool FileExists(string path)
	{
		string fullPath = ResolvePath(path);
		return File.Exists(fullPath);
	}

	public static void RemoveFile(string path)
	{
		string fullPath = ResolvePath(path);
		File.Delete(fullPath);
	}
}