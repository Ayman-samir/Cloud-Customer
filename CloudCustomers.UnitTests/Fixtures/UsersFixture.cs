using CloudCustomers.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User>GetTestUsers() {
            return new List<User>
            {
               new User{ Id = 1,
                    Name="User 1",
                    Address=new Address()
                    {   City="Damietta_1",
                        street="Abo Esa_1",
                        ZipCode="1105"
                    },
                    Email="User_1@gamil.com"
                },

               new User{ Id = 2,
                    Name="User 2",
                    Address=new Address()
                    {   City="Damietta_2",
                        street="Abo Esa_2",
                        ZipCode="1105"
                    },
                    Email="User_2@gamil.com"
                },

               new User{ Id = 3,
                    Name="User 3",
                    Address=new Address()
                    {   City="Damietta_3",
                        street="Abo Esa_3",
                        ZipCode="1105"
                    },
                    Email="User_3@gamil.com"
                },

               new User{ Id = 4,
                    Name="User 4",
                    Address=new Address()
                    {   City="Damietta_4",
                        street="Abo Esa_4",
                        ZipCode="1105"
                    },
                    Email="User_4@gamil.com"
                },



            };
        }
    }
}
