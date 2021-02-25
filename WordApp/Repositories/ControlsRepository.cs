using System.Collections.Generic;
using System.Linq;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public class ControlsRepository : IControlsRepository
    {
        private readonly WordAppEntities _context;

        #region ctor

        public ControlsRepository(WordAppEntities wordAppEntities)
        {
            _context = wordAppEntities;
        }

        #endregion

        #region Get controls from Database 
        public IEnumerable<WordControl> GetControlsFromDb( List<WordControl> controls )
        {
            string nickname = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _context.Users.Where(x => x.Nickname == nickname).First();

            for (int i = 0; i < controls.Count; i++)
            {
                var title = controls[i].Title;
                List<DBcontrol> list = user.DBcontrols.ToList();
                var DBcontrol = user.DBcontrols.Where(x => x.Title == title).FirstOrDefault();
                if (DBcontrol != null)
                {
                    controls[i].Checked = DBcontrol.Checked;
                    controls[i].Text = DBcontrol.Text;
                }
            }
            return controls;
        }
        #endregion
     
        #region Save in Database    
        public bool SaveContols(List<WordControl> controls)
        {            
            string userNick = System.Web.HttpContext.Current.User.Identity.Name;

            var user = _context.Users.Where(x => x.Nickname == userNick).First();

            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Text != null || controls[i].Type == 8)
                {
                    var title = controls[i].Title;

                    var isExist = user.DBcontrols.Where(x => x.Title == title).FirstOrDefault();
                    if (isExist != null) //Update
                    {
                        isExist.Title = controls[i].Title;
                        isExist.Text = controls[i].Text;
                        isExist.Control_ID = controls[i].Control_ID;                        
                        isExist.Users.Id = user.Id;
                        isExist.Checked = controls[i].Checked;
                    }
                    else
                    {
                        DBcontrol dbc = new DBcontrol() //Create
                        {
                            Title = controls[i].Title,
                            Text = controls[i].Text,
                            Control_ID = controls[i].Control_ID,
                            Checked = controls[i].Checked,
                            Users = user
                        };
                        _context.DBcontrols.Add(dbc);
                    }
                    _context.SaveChanges();
                }
            }
            return true;
        }
        #endregion
    }
}