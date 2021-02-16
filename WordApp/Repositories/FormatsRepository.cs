using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public class FormatsRepository : IFormatsRepository
    {
        private readonly WordAppEntities _context;

        public FormatsRepository(WordAppEntities context)
        {
            _context = context;
        }
        public bool AddFormat(Format add)
        {
            _context.Formats.Add(add);
            _context.SaveChanges();
            return true;
        }

        public bool EditFormat(Format edit)
        {
            _context.Entry(edit).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Format> GetAllFormats()
        {
            return _context.Formats.ToList();
        }

        public Format GetFormat(int? id)
        {
            return _context.Formats.Find(id);
        }

        public bool RemoveFormat(int id)
        {
            Format remove = _context.Formats.Find(id);
            _context.Formats.Remove(remove);
            _context.SaveChanges();
            return true;
        }
    }
}