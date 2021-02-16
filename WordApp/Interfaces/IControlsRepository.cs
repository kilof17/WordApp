using System.Collections.Generic;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public interface IControlsRepository
    {
        IEnumerable<DBcontrol> GetUserControlsFromDb(int id);   
        IEnumerable<Control> GetControlsFromDb(List<Control> controls);
        bool SaveContols(List<Control> controls);
    }
}
