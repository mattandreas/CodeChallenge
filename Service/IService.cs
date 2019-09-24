using System.Collections;

namespace Service
{
    public interface IService
    {
        Speed Speed { get; set; }
        IList Words { get; }
        void CheckWord(string input);
        void SwitchGen(int speed);
    }
}