using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wpf_practical
{
    public static class Parser
    {
        public static bool Date(string inp)
        {
            Regex r = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[012])\.((19|20)\d\d)$");
            return r.IsMatch(inp);
        }

        public static bool Time(string inp)
        {
            Regex r = new Regex(@"^Дней:\d{1,2},Часов:\d{1,2},Минут:\d{1,2}\.$");
            return r.IsMatch(inp);
        }

        public static bool Cost(string inp)
        {
            Regex r = new Regex(@"^\d+(?:[\,\.]\d+)?$");
            return r.IsMatch(inp);
        }

        public static bool File(string inp)
        {
            Regex r = new Regex(@"^\w+(\.csv)?$");
            return r.IsMatch(inp);
        }
        public static bool Name(string inp)
        {
            Regex r = new Regex(@"^[^\;]+$");
            return r.IsMatch(inp);
        }

    }
}
