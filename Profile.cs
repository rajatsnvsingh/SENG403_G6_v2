using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPSC_481_PROJECT
{
    class Profile
    {
        //sign-up instance variables
        private String email;
        private Dictionary<String, String> usernamePassword;
        private String battletag;

        //profile settings variables (with get/set methods)   
        public String Role { get; set; }
        public String Hero { get; set; }
        public String GameMode { get; set; }

        public static string[] RolesList = new string[] { "Offense", "Defense", "Tank", "Healer" };



        /// <summary>
        /// Profile constructor
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="battletag"></param>
        public Profile(String email, String username, String password, String battletag)
        {
            this.email = email;

            usernamePassword = new Dictionary<String, String>(1);
            usernamePassword.Add(username, password);

            this.battletag = battletag;


        }




    }
}
