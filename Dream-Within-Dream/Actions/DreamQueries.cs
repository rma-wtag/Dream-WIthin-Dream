using Dream_Within_Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream_Within_Dream.Actions
{

    public class DreamQueries
    {
        private readonly List<Dream> _dreamList;
        public DreamQueries(List<Dream> dreamList)
        {
            _dreamList = dreamList;
        }

        public List<Dream> DreamList {
            get { return _dreamList; }
        }

        public Dream? GetDataById(int id)
        {
            foreach (var dream in _dreamList)
            {
                var result = SearchById(dream, id);
                if (result != null) return result;
            }

            return null;
        }

        private Dream? SearchById(Dream dream, int id)
        {

            if (dream.Id == id) return dream;

            if (dream.InnerDreams != null)
            {
                foreach (var inner in dream.InnerDreams)
                {
                    var result = SearchById(inner, id);
                    if (result != null) return result;
                }
            }

            return null;
        }

        public async Task<Dream?> GetDataByIdAsync(int id)
        {
            using var cts = new CancellationTokenSource();
            var token = cts.Token;

            var tasks = _dreamList.Select(dream => Task.Run(() =>
            {
                return SearchByIdWithCancellation(dream, id, token);
            }, token)).ToList();

            while (tasks.Count > 0)
            {
                var finished = await Task.WhenAny(tasks);
                if (finished.Result != null)
                {
                    cts.Cancel();
                    return finished.Result;
                }
                tasks.Remove(finished);
            }
            return null;
        }

        private Dream? SearchByIdWithCancellation(Dream dream, int id, CancellationToken token)
        {
            if (token.IsCancellationRequested) return null;
            if (dream.Id == id) return dream;

            if (dream.InnerDreams != null)
            {
                foreach (var inner in dream.InnerDreams)
                {
                    var result = SearchByIdWithCancellation(inner, id, token);
                    if (result != null) return result;
                    if (token.IsCancellationRequested) return null;
                }
            }
            return null;
        }
    }
}
