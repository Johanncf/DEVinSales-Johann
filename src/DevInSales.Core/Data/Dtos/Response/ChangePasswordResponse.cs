using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class ChangePasswordResponse
    {
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; set; } = new List<string>();
        public void AddError(string error) => Errors.Add(error);    
        public void AddError(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}
