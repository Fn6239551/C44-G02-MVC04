using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.AttachementsService
{


    public interface IAttachementService
    {
        public string Upload(IFormFile file);
        string Upload(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
    }
