using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOS.Department
{
    public class CreatedDepartmentDto
    {
        [Required (ErrorMessage ="Name Is Required !!")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Is Required !!")]
        [Range (100,int.MaxValue)]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        
    }
}
