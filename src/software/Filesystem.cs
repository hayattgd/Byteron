namespace Byteron.software;

public class Content
{
	protected Content(string name)
	{
		Name = name;
		parent = Application.rootfolder;
	}

	public string path
	{
		get
		{
			string result = "/" + Name;
			Folder current = GetParent();
			while (current.parent != current)
			{
				result = "/" + current.Name + result;
				current = current.parent;
			}
			
			return result;
		}
	}
	public string Name { get; protected set; } = "";

	protected Folder parent;

	public virtual Folder GetParent() => parent;
}

public class Folder : Content
{
	internal Folder(string name, bool root) : base(name)
	{
		isRoot = root;
	}

	private bool isRoot = false;

	private List<Folder> folders = new();
	private List<File> files = new();

	public override Folder GetParent()
	{
		return isRoot ? this : parent;
	}

	public void CreateFile(string name, string content)
	{
		files.Add(new File(name, content));
	}

	public void DeleteFile(string name)
	{
		files.RemoveAll(x => x.Name == name);
	}

	public string GetFile(string name)
	{
		return files.First(x => x.Name == name).Content;
	}
}

public class File : Content
{
	internal File(string name, string content) : base(name)
	{
		Content = content;
	}

	public string Content { get; private set; } = "";
}