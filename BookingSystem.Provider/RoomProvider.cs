using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master;
using BookingSystem.DataModel.Master.Dropdown;
using BookingSystem.DataModel.Master.Room;
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


        public IndexRoom GetIndex()
        {
            var indexRoom = new IndexRoom();

            var listRoom = from a in _context.MstRooms
                         where !a.DelDate.HasValue
                         select new RowRoomVM
                         {
                             roomId = a.RoomId,
                             name = a.RoomName,
                             floor = a.Floor,
                             capacity = a.Capacity,
                             description = a.Description,
                             roomColour = a.RoomColor,
                             location = a.LocationOffice

                         };
            indexRoom.list = listRoom.ToList();
            return indexRoom;
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


        public void DeleteRoom(int Id)
        {
            var room = Get(Id);
            room.DelBy = 2;
            room.DelDate = DateTime.Now;
            _context.SaveChanges();
        }

        public CreateEditRoomVM GetSingle(int id)
        {
            var model = new CreateEditRoomVM { Id = id };

            var entity = _context.MstRooms.SingleOrDefault(x => x.RoomId == id);
            model.Id = entity.RoomId;
            model.Name = entity.RoomName;
            model.floor = entity.Floor;
            model.description = entity.Description;
            model.location = entity.LocationOffice;
            model.capacity = entity.Capacity;
            model.colour = entity.RoomColor;

            return model;
        }

        public ListDropdown LocationDropdown()
        {
            var list = new ListDropdown();

            var listLocation = from a in _context.MstLocations
                           where !a.DelDate.HasValue
                           select new LocationDropdown
                           {
                                id = a.Id,
                                location = a.Name
                           };
            list.locationDropdowns = listLocation.ToList();
            return list;

        }
    }
}
