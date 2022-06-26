using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class RegistrationResponse
    {
        public RegistrationResponse()
        {
            Errors = new List<string>();
        }

        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; set; }
        
        public void AddErrors(List<string> errors) => Errors.AddRange(errors);
    }
}
