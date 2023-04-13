using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ExceptionHandling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionController : ControllerBase
    {
        
        private readonly ILogger<ExceptionController> _logger;

        public ExceptionController(ILogger<ExceptionController> logger)
        {
            _logger = logger;
        }

        public class ExceptionListObject
        {
            public int errorCode { get; set; }
            public string errorName { get; set; }
        }

        [HttpGet(Name = "GetExceptions")]
        public IActionResult Get()
        { 
            List<ExceptionListObject> exceptions = new() {
                new ExceptionListObject { errorCode = 1, errorName = "InvalidOperationException" },
                new ExceptionListObject { errorCode = 2, errorName = "ArgumentException" },
                new ExceptionListObject { errorCode = 3, errorName = "SystemException" },
                new ExceptionListObject { errorCode = 4, errorName = "ApplicationException" },
                new ExceptionListObject { errorCode = 5, errorName = "IndexOutOfRangeException" },
                new ExceptionListObject { errorCode = 6, errorName = "StackOverflowException" },
                new ExceptionListObject { errorCode = 7, errorName = "OutOfMemoryException" }
            };

            return Ok(JsonSerializer.Serialize(exceptions));
            
        }

        [HttpGet("{errorNumber:int}", Name ="GetExceptionByID")]
        public IActionResult GetByErrorNumber(int errorNumber)
        {
            
            switch (errorNumber)
            {
                case 1:
                    throw new InvalidOperationException("Something is wrong with the state of the object");
                case 2:
                    throw new ArgumentException("Invalid Argument", "CustomerId", new InvalidOperationException("The CustomerId entered is from an account with some issue."));
                case 3:
                    throw new SystemException("Do not throw base or system exceptions, unless you intend to rethrow. Only catch them at a global level.");
                case 4:
                    throw new ApplicationException("Do not throw or inherit from this error. It comes with a stack trace of inner details that we can get from logs.");
                case 5:
                    throw new IndexOutOfRangeException("Do not throw IndexOutOfRange or NullReference exceptions in your code. It gives info on the number of records or expected objects to clients calling your code.");
                case 6:
                    throw new StackOverflowException("Do not throw StackOverflow exceptions ever. Let the CLR throw this if it is ever encountered.");
                case 7:
                    throw new OutOfMemoryException("Do not ever throw an OutOfMemory exception. Let the CLR throw this if it is ever encountered.");
                default:
                    throw new Exception("Do not throw base exceptions, unless you intend to rethrow. Only catch them at a global level.");
            }

            return Ok();
        }
    }
}