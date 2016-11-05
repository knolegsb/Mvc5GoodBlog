using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebMatrix.WebData;

namespace Mvc5GoodBlog.Filters
{
    public class CustomAdminMembershipProvider : SimpleMembershipProvider
    {
        public CustomAdminMembershipProvider()
        {

        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Argument cannot be null or empty", "username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Argument cannot be null or empty", "password");
            }

            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                User user = db.Users.Where(u => u.Username == username).FirstOrDefault();

                if (user == null)
                {
                    return false;
                }

                HashAlgorithm hash = null;
                hash = SHA1.Create();

                byte[] passBits = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(passBits).Replace("-", "").ToLower();

                return user.Password == hashedPassword;
            }
            //return base.ValidateUser(username, password);
        }
    }
}