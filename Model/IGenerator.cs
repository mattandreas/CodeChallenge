using System.Collections;

namespace Model
{
    public interface IGenerator
    {
        IList CheckInput(string input);
    }
}