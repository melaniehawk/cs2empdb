using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace EmpDB
{

    [JsonConverter(typeof(JsonSubtypes), "EmployeeType")]
    [JsonSubtypes.KnownSubType(typeof(SalariedEmployee), "Salaried")]
    [JsonSubtypes.KnownSubType(typeof(HourlyEmployee), "Hourly")]
    [JsonSubtypes.KnownSubType(typeof(CommissionEmployee), "Commission")]
    [JsonSubtypes.KnownSubType(typeof(BasePlusCommissionEmployee), "BasePlusCommission")]

    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }

        public Employee(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        // return string representation of Employee object, using properties
        public override string ToString()
        {
            string display_str = "\n******** Employee Payroll Record ********\n";
            display_str += $"{FirstName} {LastName}\n";
            display_str += $"Social Security Number: {SocialSecurityNumber}";

            return display_str;
        }

        public abstract decimal Earnings();
    }
}