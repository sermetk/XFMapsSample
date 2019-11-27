using System;
namespace XFMapsSample.Services
{
    public class ApiParameter    {        public string Name { get; set; }        public string Value { get; set; }        public ApiParameter()        {        }        public ApiParameter(string name, string value)        {            Name = name;            Value = value;        }        public override string ToString()        {            return Name + "=" + Value;        }    }
}