using UserRegistration.Models;

namespace UserRegistration.Components
{
    class Loader : LoaderModel
    {
        public Loader()
        {
            Config config = new Config();
            Convertor source = new Convertor();
            config.Write();
            this.Destination = config.Connectioncode;
            this.Source = source;
        }
        public void Read()
        {
            
        }
    }
}