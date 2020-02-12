using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AutoServiceLibrary
{
    public enum AccessRoles
    {
        Read,
        Write,
        Admin,
        Guest
    }
    public class Account
    {
        public Employee Employee { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccessRoles AccessRole { get; set; }
        public bool ActiveFlag { get; set; }
        public Account() { }
        public Account(Employee employee, List<Account> accounts)
        {
            Employee = employee;
            Login = CreateLogin(Employee, accounts);
            ActiveFlag = true;
            Password = CreatePassword();
        }

        private string CreateLogin(Employee emp, List<Account> accounts)
        {
            string login = emp.LastName + "_" +
                emp.FirstName.ToList<char>().First() + emp.SecondName.ToList<char>().First();
            int cntSameLogins = accounts.Select(a => a.Login.ToCharArray().
                                Where(n => !char.IsDigit(n)).ToArray()).
                                Where(s => new string(s) == login.ToUpper()).ToArray().Count();
            if (cntSameLogins != 0)
                login += cntSameLogins.ToString();
            return login.ToUpper();
        }
        private string CreatePassword(string password = "12345678") => EncryptPassword(password);
        private string EncryptPassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            return Encoding.UTF8.GetString(byteHash);
        }
        public void ChangePassword(string newPsw) => Password = EncryptPassword(newPsw);
        public bool VerifyUser(string password)
        {
            if (EncryptPassword(password).Equals(Password))
            {
                return true;
            }
            return false;
        }
        public void DeleteAccount()
        {
            ActiveFlag = false;
        }
    }
}
