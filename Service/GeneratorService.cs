using System;
using System.Collections;
using System.Collections.ObjectModel;
using Model;
using static Service.Speed;

namespace Service
{
    public class GeneratorService : IService
    {
        public IList Words { get; } = new ObservableCollection<string>();

        private Speed _speed;
        public Speed Speed
        {
            get => _speed;

            set
            {
                switch (value)
                {
                    case Fast:
                        _inUseGen = _fastGen;
                        _speed = Fast;
                        break;
                    case Slow:
                        _inUseGen = _slowGen;
                        _speed = Slow;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        private readonly IGenerator _fastGen;

        private readonly IGenerator _slowGen;

        private IGenerator _inUseGen;

        public GeneratorService()
        {
            _slowGen = new Generator();
            _fastGen = new FastGenerator();
            _inUseGen = _slowGen;
        }

        public void CheckWord(string input)
        {
            Words.Clear();
            var newWords = _inUseGen.CheckInput(input);
            foreach (string word in newWords)
            {
                Words.Add(word);
            }
        }

        public void SwitchGen(int speed)
        {
            switch (speed)
            {
                case (int) Fast:
                    Speed = Fast;
                    break;
                case (int) Slow:
                    Speed = Slow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(speed), speed, null);
            }
        }
    }
}
