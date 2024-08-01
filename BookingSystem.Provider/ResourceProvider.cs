using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master;

namespace BookingSystem.Provider
{
    public class ResourceProvider
    {
        private BookingSystemDlsContext _context;

        public ResourceProvider(BookingSystemDlsContext _context) { 
            this._context = _context;
        }

        public IQueryable AllResource() { 
            return _context.MstResources.Where(a => !a.DelDate.HasValue);
        }
        public IQueryable<MstResourceCode> AllResourceCode()
        {
            return _context.MstResourceCodes.Where(a => !a.DelDate.HasValue);
        }
        private MstResource Get(int id)
        {
            return _context.MstResources.SingleOrDefault(a => a.Id == id);
        }
        private MstResourceCode GetResCod(int id)
        {
            return _context.MstResourceCodes.SingleOrDefault(a => a.Id == id);
        }

        public  void InsertResource(CreateEditResourceVM model) {
            var res = new MstResource
            {
                ResouceName = model.Name,
                Status = model.Status,
                ResourceIcon = model.Icon,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstResources.Add(res);
            _context.SaveChanges();
            foreach (var item in model.code)
            {
                if (item.Id > 0)
                {
                    UpdateResCod(item, model.Id);
                }
                else
                {
                    InsertResCod(item, model.Id);
                }
            }
        }

        public  void UpdateResource(CreateEditResourceVM model) {

           

            var resource = Get(model.Id);
            resource.ResouceName = model.Name;
           
            resource.Status = model.Status;
            resource.UpdatedBy = 2;
            resource.UpdateDate = DateTime.Now;
            resource.ResourceIcon = model.Icon;
            _context.SaveChanges();
            foreach (var item in model.code)
            {
                if (item.Id > 0)
                {
                    UpdateResCod(item, model.Id);
                }
                else
                {
                    InsertResCod(item, model.Id);
                }
            }
        }

        public void InsertResCod(CreateEditResCodVM model, int resourceId)
        {
            var resCod = new MstResourceCode
            {
                ResourceId = resourceId,
                ResourceCode = model.ResourceCode,
                Status = model.status,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstResourceCodes.Add(resCod);
            _context.SaveChanges();

        }
        public void UpdateResCod(CreateEditResCodVM model, int resourceId)
        {
            var resCod = GetResCod(model.Id);
            if (resCod != null)
            {
                resCod.ResourceCode = model.ResourceCode;
                resCod.ResourceId = resourceId;
                resCod.Status = model.status;
                resCod.UpdatedBy = 2;
                resCod.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteResource(int id) { 
            var resource = Get(id);
            resource.DelBy = 2;
            resource.DelDate = DateTime.Now;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", resource.ResourceIcon);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            resource.DelBy = 2;
            resource.DelDate = DateTime.Now;
            


            _context.SaveChanges();
        
        }

        public IndexResource GetIndex()
        {
            var indexResource = new IndexResource();

            var listResource = from a in _context.MstResources
                               where !a.DelDate.HasValue
                               select new RowResource
                               {
                                   Id = a.Id,
                                   Name = a.ResouceName,
                                   Status = a.Status,
                                   Icon = a.ResourceIcon
                               };
            indexResource.Rows = listResource.ToList();
            return indexResource;
        }

        public CreateEditResourceVM GetSingle(int id) {
            var model = new CreateEditResourceVM { Id = id };

            var entity =Get(id);
            model.Status = entity.Status;
            model.Name = entity.ResouceName;
            model.Icon = entity.ResourceIcon;
            model.code = GetListResCode(entity.Id);
            return model;

        }
        public List<CreateEditResCodVM> GetListResCode(int resourceId)
        {
            var list = from a in AllResourceCode()
                       where a.ResourceId == resourceId && !a.DelDate.HasValue
                       select new CreateEditResCodVM
                       {
                           Id = a.Id,
                           status = a.Status,
                           ResourceCode = a.ResourceCode
                       };
            return list.ToList();
        }

        public  CreateEditResourceVM ImageHandler(CreateEditResourceVM model)
        {

            if (model.IconFile != null && model.IconFile.Length > 0)
            {
                var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","resource","images");
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.IconFile.FileName}";
                var filePath = Path.Combine(folderpath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.IconFile.CopyToAsync(stream);
                }

                model.Icon = uniqueFileName;
                return model;
            }
            model.Icon = "Not Set";
            return model;
        }
    }
}
