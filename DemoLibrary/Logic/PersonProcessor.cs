﻿using DemoLibrary.Models;
using DemoLibrary.Utilities;

namespace DemoLibrary.Logic
{
    public class PersonProcessor : IPersonProcessor
    {
        ISqliteDataAccess _database;

        public PersonProcessor(ISqliteDataAccess database)
        {
            _database = database;
        }

        public PersonModel CreatePerson(string firstName,  string lastName, string heightText)
        {
            PersonModel output = new PersonModel();
            if (ValidateName(firstName) == true)
            {
                output.FirstName = firstName;
            } else
            {
                throw new ArgumentException("The value was not valid", "firstName");
            }
            if (ValidateName(lastName) == true)
            {
                output.LastName = lastName;
            } else
            {
                throw new ArgumentException("The value was not valid", "lastName");
            }
            var height = ConvertHeightTextToInches(heightText);
            if (height.isValid)
            {
                output.HeightInInches = height.heightInInches;
            } else
            {
                throw new ArgumentException("The value is not valid", "heightText");
            }
            return output;
        }

        public List<PersonModel> LoadPeople()
        {
            string sql = "SELECT * FROM Person";

            return _database.LoadData<PersonModel>(sql);
        }

        public void SavePerson(PersonModel person)
        {
            string sql = "INSERT INTO Person (FirstName, LastName, HeightInInches) VALUES (@FirstName, @LastName, @HeightInInches)";

            _database.SaveData(person, sql);
        }

        public void UpdatePerson(PersonModel person)
        {
            string sql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, HeightInInches = @HeightInInches WHERE Id = @Id";

            _database.SaveData(person, sql);
        }

        public (bool isValid, double heightInInches) ConvertHeightTextToInches(string heightText)
        {
            int feetMarkerLocation = heightText.IndexOf('\'');
            int inchesMarkerLocation = heightText.IndexOf('"');
            if (feetMarkerLocation < 0 || inchesMarkerLocation < 0 || inchesMarkerLocation < feetMarkerLocation)
            {
                return (false, 0);
            }

            // Split on both feet and inches
            string[] heightParts = heightText.Split(new char[] { '\'', '"' });

            if (int.TryParse(heightParts[0], out int feet) == false || int.TryParse(heightParts[1], out int inches) == false)
            {
                return (false, 0);
            }
            double heightInInches = feet * 12 + inches;
            return (true, heightInInches);
        }

        private bool ValidateName(string name)
        {
            bool output = true;
            char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
            if (name.Length < 2)
            {
                output = false;
            }
            if (name.IndexOfAny(invalidCharacters) >= 0)
            {
                output = false;
            }
            return output;
        }
    }
}
