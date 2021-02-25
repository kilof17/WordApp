using System.Collections.Generic;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public interface IControlsRepository
    {        
        IEnumerable<WordControl> GetControlsFromDb(List<WordControl> controls);
        bool SaveContols(List<WordControl> controls);
    }
}
