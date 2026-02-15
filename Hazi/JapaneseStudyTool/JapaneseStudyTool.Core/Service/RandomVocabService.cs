using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Service
{
    internal class RandomVocabService
    {
        private readonly List<VocabWord> _vocab;
        private int index;

        public RandomVocabService()
        {
            var allVocab = VocabHandler.LoadVocab();

            if(allVocab.Count < 10)
            {
                _vocab = allVocab;
                return;
            }

            index = 0;

            var orderedVocab = allVocab
                .OrderBy(word => word.Mastery)
                .ToList();

            var lowMasteryVocab = orderedVocab
                .Take(7)
                .ToList();

            var randomVocab = orderedVocab
                .Skip(7)
                .OrderBy(word => Random.Shared.Next())
                .Take(3)
                .ToList();

            _vocab = lowMasteryVocab
                .Concat(randomVocab)
                .OrderBy(word => Random.Shared.Next())
                .Take(10)
                .ToList();
        }

        public VocabWord GetNextWord()
        {
            return _vocab.ElementAt(index++);
        }

        public int GetMaxScore()
        {
            return _vocab.Count;
        }
    }
}
