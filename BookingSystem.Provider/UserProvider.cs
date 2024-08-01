using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class UserProvider
    {
        private BookingSystemDlsContext _context;

        public UserProvider(BookingSystemDlsContext _context)
        {
            this._context = _context;
        }

        private IQueryable AllUser()
        {
            return _context.MstUsers.Where(a => !a.DelDate.HasValue);
        }

        private MstUser Get(int id)
        {
            return _context.MstUsers.SingleOrDefault(a => a.Id == id);
        }

        public IndexUserVM GetIndex()
        {
            var indexUser = new IndexUserVM();

            var listUser = from a in _context.MstUsers
                           where !a.DelDate.HasValue
                           select new RowUserVM
                           {
                               id = a.Id,
                               LoginName = a.LoginName,
                               Role = a.Role.Name,
                               CreatedBy = a.CreatedBy.ToString(),// blum di gabung ke username
                               CreatedDate = a.CreatedDate.ToShortDateString(),
                               UpdatedBy = a.UpdatedBy.ToString(),// blum di gabung
                               UpdatedDate = a.UpdatedDate.ToString()

                           };
            indexUser.list = listUser.ToList();
            return indexUser;
        }


    }
}
