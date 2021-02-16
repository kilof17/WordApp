using System.Collections.Generic;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public interface IFilesRepository
    {
        List<File> GetFiles(string folderPath);
        List<File> GetFiles(string folderPath, string username);
        List<string> GetFilesNames(string folderPath);
        IEnumerable<Format> GetAllFormats();
    }
}
