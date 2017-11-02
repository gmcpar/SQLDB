using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Class1
{
    static void Main()
    {
        SQLDB db = new SQLDB("Data Source=DESKTOP-P25B241;Initial Catalog=db1;Integrated Security=True");


        Customer c = new Customer();
        c.Name = "FREDO222aaa";
        //c.Telephone = "111111222";
        c.Id = 22112;
        c.Email = "hhhEmai22l";
            
            db.ExecuteSP("InsertCustomer", c);

    }
    


   
}
public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Telephone { get; set; }
}

