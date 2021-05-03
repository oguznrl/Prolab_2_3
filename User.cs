using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab2_3Spo
{
    public class User
    {
       public int userId;
       public string userName;
       public string email;
       public string password;
       public Boolean isPremium;
       public string country;
       public Boolean paidPremiumPrice;
       public List<Songs> jazz = new List<Songs>();
       public List<Songs> pop = new List<Songs>();
       public List<Songs> classic = new List<Songs>();
       public List<User> following = new List<User>();

        public User(int userId, string userName, string email, string password, 
            bool isPremium,string country, bool paidPremiumPrice, List<Songs> jazz,
            List<Songs> pop, List<Songs> classic, List<User> following)
        {
            this.userId = userId;
            this.userName = userName;
            this.email = email;
            this.password = password;
            this.isPremium = isPremium;
            this.country = country;
            this.paidPremiumPrice = paidPremiumPrice;
            this.jazz = jazz;
            this.pop = pop;
            this.classic = classic;
            this.following = following;
        }

        public User()
        {

        }
        

        

    }
    
}
