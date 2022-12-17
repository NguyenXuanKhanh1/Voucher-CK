namespace VoucherCK.Utility
{
    public static class ListExtensionsHelper
    {
        public static List<T> FluentAddList<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> FluentClearList<T>(this List<T> list)
        {
            list.Clear();
            return list;
        }

        public static List<T> FluentForEachList<T>(this List<T> list, Action<T> action)
        {
            list.ForEach(action);
            return list;
        }

        public static List<T> FluentInsertList<T>(this List<T> list, int index, T item)
        {
            list.Insert(index, item);
            return list;
        }

        public static List<T> FluentRemoveAt<T>(this List<T> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public static List<T> FluentReverse<T>(this List<T> list)
        {
            list.Reverse();
            return list;
        }

        public static List<List<object>> SplitList(List<object> locations, int nSize)
        {
            var list = new List<List<object>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }


        public static IEnumerable<List<T>> Partition<T>(this List<T> values, int chunkSize)
        {
            for (int i = 0; i < values.Count; i += chunkSize)
            {
                yield return values.GetRange(i, Math.Min(chunkSize, values.Count - i));
            }
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> collection, int batchSize = 300)
        {
            var nextbatch = new List<T>();
            foreach (T item in collection)
            {
                nextbatch.Add(item);
                if (nextbatch.Count == batchSize)
                {
                    yield return nextbatch;
                    nextbatch = new List<T>(batchSize);
                }
            }

            if (nextbatch.Count > 0)
            {
                yield return nextbatch;
            }
        }
    }
}
