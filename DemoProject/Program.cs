using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(new NhCustomerDal());
            customerManager.Save(new Customer());
            Console.ReadLine();

        }

    }
    //bir class çıplak kalmayacak
    class CustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added With Ef");
        }
    }
    class NhCustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added With Nh");
        }
    }

    internal interface ICustomerDal
    {
        void Save();
    }

    class CustomerManager : ICustomerService
    {
        public ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void Save(Customer customer)
        {
            //kurallar
            /*CustomerDal customerDal = new CustomerDal();*/  //bir sınıf başka bir sınıfı yani bağımlı olduğu başka bir sınıfı newleyemez!
            //customerDal.Save();
            _customerDal.Save();
            Logger logger = new Logger();
            logger.Log(LoggerType.Database);
        }
    }

    internal interface ICustomerService
    {
        void Save(Customer customer);
    }
    class Customer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }

    interface IEntity
    {
    }

    class Logger
    {
        public void Log(LoggerType loggerType)
        {
            if (loggerType == LoggerType.Database)
            {
                Console.WriteLine("logged to db");
            }
            else if(loggerType == LoggerType.File)
            {
                Console.WriteLine("logged to file");
            }
        }
    }

    enum LoggerType
    {
        Database, File
    }
}
