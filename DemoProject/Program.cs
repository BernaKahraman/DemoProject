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
            CustomerManager customerManager = new CustomerManager(new NhCustomerDal(), new MainLoggerAdapter());
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
        private ICustomerDal _customerDal;
        private ILogger _logger;

        public CustomerManager(ICustomerDal customerDal, ILogger logger)
        {
            _customerDal = customerDal;
            _logger = logger;
        }

        public void Save(Customer customer)
        {
            //kurallar
            /*CustomerDal customerDal = new CustomerDal();*/  //bir sınıf başka bir sınıfı yani bağımlı olduğu başka bir sınıfı newleyemez!
            //customerDal.Save();
            _customerDal.Save();
            _logger.Log();
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
    interface ILogger
    {
        void Log();
    }
    class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("logged to db");
        }
    }
    class EmailLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("logged to email");
        }
    }

    
    class MainLoggerAdapter : ILogger
    {
        public void Log()
        {
            MainLogger mainLogger = new MainLogger();
            mainLogger.LogToMain();
        }
    }

    //microservice
    class MainLogger
    {
        public void LogToMain()
        {
            Console.WriteLine("logged to main");
        }
    }
}
