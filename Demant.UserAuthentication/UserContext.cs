using Bogus.DataSets;
using Bogus;
using Demant.UserAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demant.UserAuthentication
{
    internal class UserContext
    {
        private UserContext()
        {
        }

        /// <summary>
        /// Data that will use as Binding to UserScreen 
        /// </summary>
        public List<User> UsersData { get; set; } = new List<User>();

        private static readonly Lazy<UserContext> lazy = new Lazy<UserContext>(() => new UserContext());
        public static UserContext Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        ///  Initialize random data from Bogus (10 Users)
        /// </summary>
        /// <param name="loginName">This is the username of logged in user</param>
        internal void InitializeRandomData(string loginName)
        {
            var generatedPersons = new Faker<Person>().
                CustomInstantiator(f => new Person()).Generate(10);
            bool isUserLogin = true;
            foreach (var person in generatedPersons)
            {
                UsersData.Add(new User
                {                 
                    LoginName = isUserLogin?loginName : person.UserName ,
                    DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd"),
                    Country = GetRandomCountry(),
                    IsOnline = isUserLogin
                });

                isUserLogin = false;
            }
        }

        /// <summary>
        /// Reset UserData when logout
        /// </summary>
        internal void ResetData()
        {
            UsersData = new List<User>();
        }

        /// <summary>
        /// User generator from Bogus to generate random Country name. 
        /// We do another Country generator because Faker<Person> doesnt contains property of Country.
        /// </summary>
        /// <returns>Random Country</returns>
        private string GetRandomCountry()
        {
            return new Faker<Address>().CustomInstantiator(f => new Address())
                .Generate().Country();
        }
    }
}
