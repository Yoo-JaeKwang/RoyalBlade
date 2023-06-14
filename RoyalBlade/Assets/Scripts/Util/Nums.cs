using Cysharp.Text;
using System.Collections.Generic;

namespace Util
{
    public static class Nums
    {
        private static readonly Dictionary<int, string> _container = new();
        private static string GetNumStringHelper(int num)
        {
            if (false == _container.ContainsKey(num))
            {
                _container.Add(num, $"{num}");
            }
            return _container[num];
        }
        private static readonly List<int> _nums = new();
        private static readonly string _comma = ",";
        public static string GetNumString(int num)
        {
            if (num == 0)
            {
                return GetNumStringHelper(num);
            }

            _nums.Clear();

            while (num != 0)
            {
                _nums.Add(num % 1000);
                num /= 1000;
            }

            using var sb = ZString.CreateStringBuilder();

            int count = _nums.Count;
            sb.Append(GetNumStringHelper(_nums[count - 1]));
            for (int i = count - 2; i > 0; --i)
            {
                sb.Append(_comma);
                sb.Append(GetNumStringHelper(_nums[i]));
            }

            return sb.ToString();
        }
    }
}
