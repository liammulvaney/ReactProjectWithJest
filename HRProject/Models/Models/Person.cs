using System;

namespace Models.Models
{
    public abstract class Person
    {
        public Guid Id { get; set; }
        public Guid PartitionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class Employee : Person {
        
    }

    public class ContactPerson : Person
    {

    }
}
