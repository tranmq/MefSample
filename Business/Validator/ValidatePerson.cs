using System;
using System.ComponentModel.Composition;
using Business.Entities;

namespace Business.Validator
{
    [Export("abc", typeof(IValidateEntity<Person>))] // export with name
    public class ValidatePerson : IValidateEntity<Person>
    {
        public bool Validate(Person entity)
        {
            return !String.IsNullOrWhiteSpace(entity.FirstName) && !String.IsNullOrWhiteSpace(entity.LastName);
        }
    }
}