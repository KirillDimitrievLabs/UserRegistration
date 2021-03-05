using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UserRegistration.Components.Tests
{
    [TestClass()]
    public class YamlTests
    {
        [TestMethod()]
        public void YamlToModelTest_UserDestinationModel()
        {
            //Arrange
            string[] str = new string[] { "123", "123" };
            Models.UserDestinationModel expUserDest = new Models.UserDestinationModel
            {
                Avatar = "123",
                Login = "test_test",
                Fullname = "test test",
                Email = "test@test.test",
                Disabled = false,
                Groups = str
            };
            //Act
            Models.UserDestinationModel curUserDest = Yaml<Models.UserDestinationModel>.YamlToModel(@"Testing.yaml");
            bool isSameModel =
                expUserDest.Fullname == curUserDest.Fullname &&
                expUserDest.Login == curUserDest.Login &&
                expUserDest.Avatar == curUserDest.Avatar &&
                expUserDest.Disabled == curUserDest.Disabled &&
                expUserDest.Email == curUserDest.Email &&
                Enumerable.SequenceEqual(expUserDest.Groups, curUserDest.Groups);
            //Assert
            Assert.AreEqual(true, isSameModel);
        }

        [TestMethod()]
        public void ModelToYamlTest_Valid()
        {
            //Arrange
            string[] str = new string[] { "123", "123"};
            Models.UserDestinationModel userDestinationTest = new Models.UserDestinationModel
            {
                Avatar = "123",
                Login = "test_test",
                Fullname = "test test",
                Email = "test@test.test",
                Disabled = false,
                Groups = str
            };
            string expectedString = new StreamReader(Directory.GetCurrentDirectory() + $@"\ModelToYaml\expected.yaml").ReadToEnd().Trim();

            //Act
            Yaml<Models.UserDestinationModel>.ModelToYaml(userDestinationTest, @"\ModelToYaml\Testing.yaml");
            string actualString = new StreamReader(Directory.GetCurrentDirectory() + $@"\ModelToYaml\Testing.yaml").ReadToEnd().Trim();

            //Assert
            Assert.AreEqual(expectedString, actualString);
        }
    }
}