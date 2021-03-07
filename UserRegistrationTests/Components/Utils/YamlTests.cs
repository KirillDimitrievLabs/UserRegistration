using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UserRegistrationTests.Models;
using YamlDotNet.Core;

namespace UserRegistration.Components.Tests
{
    [TestClass()]
    public class YamlTests
    {
        [TestMethod()]
        public void YamlToModelTest_ToTestModel_ValidTestSource()
        {
            //Arrange
            TestModel expectedModel = new TestModel()
            {
                Name = "kirill",
                Disabled = false,
                Age = 100,
                TestList = new TestListModel()
                {
                    City = "TestCity",
                    Team = "TestTeam"
                }
            };

            //Act
            TestModel currentModel = Yaml<TestModel>.YamlToModel(@"ValidTestSource.yaml");
            bool isSameModel =
                expectedModel.Name == currentModel.Name &&
                expectedModel.Age == currentModel.Age &&
                expectedModel.Disabled == currentModel.Disabled &&
                Enumerable.SequenceEqual(TestListModel.ConvertToStringList(expectedModel.TestList), TestListModel.ConvertToStringList(expectedModel.TestList));
            //Assert
            Assert.IsTrue(isSameModel);
        }

        [TestMethod()]
        public void YamlToModelTest_Exception()
        {
            //Arrange
            TestModel expectedModel = new TestModel()
            {
                Name = "kirill",
                Disabled = false,
                Age = 100,
                TestList = new TestListModel()
                {
                    City = "TestCity",
                    Team = "TestTeam"
                }
            };

            var ex = Assert.ThrowsException<YamlException>(() => Yaml<TestModel>.YamlToModel(@"ValidTestSource.yaml"));

            Assert.AreSame(ex.Message, "");
            
        }

        //[TestMethod()]
        //public void ModelToYamlTest_Valid()
        //{
        //    //Arrange
        //    string writePath = @"C:\Users\user\Source\Repos\UserRegistration\UserRegistrationTests\bin\Debug\net5.0\TestSources\ValidTestSource.yaml";
        //    string expectedStr = @"";
        //    using (StreamReader sr = new StreamReader(writePath))
        //    {
        //        expectedStr = sr.ReadToEnd();
        //    }
        //    //Act
        //    TestModel testModel = new TestModel();
        //    TestModel curTestModel = Yaml<TestModel>.YamlToModel("WrongTestSource.yaml");
        //    //Assert

        //}

        [TestMethod()]
        public void GetPathTest_IsFileExsists()
        {
            //Arrange
            string fileName = "TestSource.txt";
            string expectedPath = @"C:\Users\user\Source\Repos\UserRegistration\UserRegistrationTests\bin\Debug\net5.0\TestSource.txt";


            //Act
            if (!File.Exists(expectedPath))
            {
                File.Create(expectedPath);
            }

            string currentPath = Yaml<string>.GetPath(fileName);

            //Assert
            Assert.AreEqual(expectedPath, currentPath);
        }

        [TestMethod()]
        public void GetPathTest_FileNotFoundException()
        {
            //Arrange
            string fileName = "TestSource.txt";

            //Act
            var expected = Assert.ThrowsException<FileNotFoundException>(() => Yaml<string>.GetPath(fileName));
            Assert.AreSame(expected.Message, "File not found");
        }
    }
}