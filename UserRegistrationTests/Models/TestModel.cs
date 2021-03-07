using System.Collections.Generic;

namespace UserRegistrationTests.Models
{
    public class TestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Disabled { get; set; }
        public TestListModel TestList { get; set; }
    }
    public class TestListModel
    {
        public string Team { get; set; }
        public string City { get; set; }
        public static List<string> ConvertToStringList(TestListModel orgStructure)
        {
            List<string> result = new List<string> { orgStructure.City, orgStructure.Team };
            return result;
        }
    }
}
