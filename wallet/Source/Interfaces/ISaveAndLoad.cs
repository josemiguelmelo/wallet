using System;
using System.Threading.Tasks;

namespace wallet
{
	public interface ISaveAndLoad
	{
		Task SaveTextAsync (string filename, string text);
		Task<string> LoadTextAsync (string filename);
		bool FileExists (string filename);
	}
}
