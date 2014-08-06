using System;
using Business.Entities;

namespace Business.Validator
{
    public class ValidatePerson : IValidateEntity<Person>
    {
        public bool Validate(Person entity)
        {
            return !String.IsNullOrWhiteSpace(entity.FirstName) && !String.IsNullOrWhiteSpace(entity.LastName);
        }
    }
}