using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            Errors = new List<string>();
        }

        public bool Success => Errors.Count == 0;
        public string Token { get; private set; }
        public List<string> Errors { get; private set; }

        public void SetToken(string token) => Token = token;    
        public void AddError(string error) => Errors.Add(error);
    }
}
