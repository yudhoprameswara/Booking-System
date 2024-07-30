using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingSystem.DataModel.Master
{
    public class CreateEditResourceVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Boolean Status { get; set; }

        public String? Icon {  get; set; } 

        public IFormFile? IconFile {  get; set; }

        public List<CreateEditResCodVM>? code { get; set; } = new List<CreateEditResCodVM>();
    }
}
