using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiMvcRestService.Models
{
    public class SomeTestModel
    {
        private bool _field = false;
        public bool Field = false;
        private bool _property { get; set; }
        public bool Property { get; set; }
    }
}