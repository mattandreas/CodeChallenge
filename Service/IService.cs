using System.Collections;

namespace Service
{
    public interface IService
    {
        IList Words { get; }
        void CheckWord(string input);
    }
}