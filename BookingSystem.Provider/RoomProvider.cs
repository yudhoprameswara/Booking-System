using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class RoomProvider
    {
        private BookingSystemDlsContext _context;

        public RoomProvider(BookingSystemDlsContext _context)
        {
            this._context = _context;
        }

        private IQueryable AllRoom()
        {
            return _context.MstRooms.Where(a => !a.DelDate.HasValue);
        }

        private MstRoom Get(int Id)
        {
            return _context.MstRooms.SingleOrDefault(a => a.RoomId == Id);
        }

        public void InsertRoom(CreateEditRoomVM model)
        {
            var newRoom = new MstRoom();
            newRoom.RoomId = model.Id;
            newRoom.RoomName = model.Name;
            newRoom.Floor = model.floor;
            newRoom.Description = model.description;
            newRoom.LocationOffice = model.location;
            newRoom.Capacity = model.capacity;
            newRoom.RoomColor = model.colour;
            newRoom.CreatedBy = 1;
            newRoom.CreatedDate = DateTime.Now;
            _context.MstRooms.Add(newRoom);
            _context.SaveChanges();
        }

        public void UpdateRoom (CreateEditRoomVM model)
        {
            var room = Get(model.Id);
            room.RoomId = model.Id;
            room.RoomName = model.Name; 
            room.Floor = model.floor;
            room.Description= model.description;
            room.LocationOffice = model.location;
            room.Capacity = model.capacity;
            room.RoomColor = model.colour;
            room.UpdatedBy = 2;
            room.UpdateDate = DateTime.Now;
            _context.SaveChanges();
        }

    }
}
