using System.Collections.Generic;
using System.Linq;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public class FilesRepository : IFilesRepository
    {
        private WordAppEntities _context { get; }
    
        public FilesRepository(WordAppEntities wordAppEntities)
        {
            _context = wordAppEntities;
        }           

        public List<File> GetFiles(string folderPath)
        {
            List<File> filesList =new List<File>();

            foreach (var path in System.IO.Directory.GetFiles(folderPath))
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(path);
                string extension = System.IO.Path.GetExtension(path);

                if (filename[0] != 126)
                {
                    filesList.Add(new File
                    {
                        FileName = filename,
                        Extension = extension,
                        FilePaht = path,
                        Hash = HashingFunctions.InfoHash(filename+extension),
                        
                    });                    
                }
            }
            return filesList;
        }
        public List<File> GetFiles(string folderPath, string username)
        {
            List<File> userFiles = new List<File>();

            foreach (var path in System.IO.Directory.GetFiles(folderPath))
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(path);
                string extension = System.IO.Path.GetExtension(path);

                if (filename[0] != 126 && filename.Contains(username))//  is not temporary file and contains user nick
                {
                    userFiles.Add(new File
                    {
                        FileName = filename,
                        Extension = extension,
                        FilePaht = path,
                        Hash = HashingFunctions.InfoHash(filename+extension)                        
                    });
                }
            }
            return userFiles;
        }

        public IEnumerable<Format> GetAllFormats()
        {
            List<Format> formatsList = _context.Formats.ToList();            
            return formatsList;
        }

        public List<string> GetFilesNames(string folderPath)
        {
            List<string> files = new List<string>();
            foreach (var item in System.IO.Directory.GetFiles(folderPath))
            {
                files.Add(System.IO.Path.GetFileNameWithoutExtension(item));
            }          
            return files;
        }      
    }
}