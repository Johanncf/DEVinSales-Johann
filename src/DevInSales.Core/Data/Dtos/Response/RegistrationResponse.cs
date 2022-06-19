using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class RegistrationResponse
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public RegistrationResponse(bool success = true)
        {
            Success = success; 
            Errors = new List<string>();
        }
        
        public void AddErrors(List<string> errors) => Errors.AddRange(errors);
    }
}
