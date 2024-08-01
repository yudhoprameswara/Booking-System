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

        public bool Status { get; set; }

        public string? Icon {  get; set; } 

        public IFormFile? IconFile {  get; set; }

        public List<CreateEditResCodVM>? code { get; set; } = new List<CreateEditResCodVM>();
    }
}
