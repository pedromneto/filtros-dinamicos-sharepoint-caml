using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCamlex
{
    public abstract class Credenciais
    {
        static string _password = "asdsadfasdfasfdasdfasd";
        
        public const string UserName = "usuario@site.onmicrosoft.com";

        public static SecureString Password()
        {
            var password = new SecureString();
            _password.ToList().ForEach(c => password.AppendChar(c));
            return password;            
        }
    }
}
