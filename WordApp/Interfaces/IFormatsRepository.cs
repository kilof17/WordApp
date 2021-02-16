using System.Collections.Generic;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public interface IFormatsRepository
    {
        IEnumerable<Format >GetAllFormats();
        Format GetFormat(int? id);
        bool AddFormat(Format add);
        bool EditFormat(Format edit);
        bool RemoveFormat(int id);


    }
}
