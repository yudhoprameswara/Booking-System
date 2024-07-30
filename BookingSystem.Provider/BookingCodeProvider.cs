
using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class BookingCodeProvider
    {
        private BookingSystemDlsContext _context;
        public BookingCodeProvider(BookingSystemDlsContext _context)
        {
            this._context = _context;
        }

        private IQueryable AllBookingCode()
        {

            return _context.MstBookings.Where(a => !a.DelDate.HasValue);
        }

        private MstBooking Get(int Id) {
            return _context.MstBookings.SingleOrDefault(a => a.Id == Id);
        }

        public void InsertBC(CreateEditBCVM model)
        {
            var newBC = new MstBooking();
            newBC.Id = model.Id;
            newBC.BookCode = model.BookingCode;
            newBC.Status = model.Status;
            newBC.CreatedBy = 1;
            newBC.CreatedDate = DateTime.Now;
            _context.MstBookings.Add(newBC);
            _context.SaveChanges();
        }

        public void UpdateBC(CreateEditBCVM model) { 
            var bc =  Get(model.Id);
            bc.BookCode = model.BookingCode;
            bc.Status = model.Status;
            bc.UpdatedBy = 2;
            bc.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public void DeleteBC(int Id)
        {
            var bc = Get(Id);
            bc.DelBy = 2;
            bc.DelDate = DateTime.Now;
            _context.SaveChanges();
        }

        public IndexBCVM GetIndex() { 
            var indexBC = new IndexBCVM();

            var listBC = from a in _context.MstBookings  where !a.DelDate.HasValue 
                         select new RowBCVM
                         {
                             Id = a.Id,
                             BookCode = a.BookCode,
                             Status = a.Status,
                         };
            indexBC.list= listBC.ToList();
            return indexBC;
        }

        public CreateEditBCVM GetSingle(int id) { 
            var model = new CreateEditBCVM {Id = id};

            var entity = _context.MstBookings.SingleOrDefault(x => x.Id == id);
            model.BookingCode = entity.BookCode;
            model.Status = entity.Status;

            return model;
        }
    }
}