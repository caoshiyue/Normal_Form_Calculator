using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace discrete_mathematics1
{
    class char_check
    {
        public const string NOT = "¬";
        public const string AND = "⋀";
        public const string OR = "⋁";
        public const string PREDICT = "→";
        public const string EQUAL = "↔";
        public void get_string(ref string _s)
        {
            s = _s;
        }
        public void recount()       //检查重计数
        {
            bracket_count = 0;
        }
        public bool syntactic_error()       //检查错误语法
        {
            int length = s.Length - 1;
            char last, next;
            for (int i = 0; i <= length; i++)
            {
                if (i > 0)
                    last = s[i - 1];
                else
                    last = ' ';
                if (i < length)
                    next = s[i + 1];
                else
                    next = ' ';
                if (
                    isletter(s[i], next)//字母
                    || ((i < length) && isNOT(s[i], next, last))//非
                    || (((i > 0) && (i < length)) && isbinary_operater(s[i], last, next))//连接词
                    || (((i > 0) && isfrontbracket(s[i], last)) || ((i == 0) && isfrontbracket(s[i])))//前括号
                    || (((i < length) && isbackbracket(s[i], next)) || ((i == length) && isbackbracket(s[i])))//后括号
                    )
                {
                    bracket_match(s[i]);
                    continue;
                }
                else
                {
                    recount();
                    MessageBox.Show("syntactic error");
                    return false;
                }
            }
            if (isbracket_match())
                return true;
            else
            {
                recount();
                MessageBox.Show("syntactic error");
                return false;
            }
        }
        private Int16 bracket_count = 0;
        protected string s;
        protected bool isletter(char c, char next)       //是字母且逻辑正确
        {
            return ((c <= 90 && c >= 65) || (c <= 122 && c >= 97) && !((next <= 90 && next >= 65) || (next <= 122 && next >= 97)));
        }
        protected bool isletter(char c)           //是否为字母
        {
            return (c <= 90 && c >= 65) || (c <= 122 && c >= 97);
        }
        protected bool isNOT(char c, char next, char last)           //是否为非且逻辑正确
        {
            return c == NOT[0] && (isletter(next) || isfrontbracket(next)) && !(isletter(last));
        }
        protected bool isNOT(char c)           //是否为非
        {
            return c == NOT[0];
        }
        protected bool isfrontbracket(char c, char last)           //是否为 ( 且逻辑正确
        {
            return c == '(' && !isletter(last);
        }
        protected bool isfrontbracket(char c)           //是否为 (
        {
            return c == '(';
        }
        protected bool isbackbracket(char c, char next)           //是否为 ) 且逻辑正确
        {
            return c == ')' && !isletter(next);
        }
        protected bool isbackbracket(char c)           //是否为 )
        {
            return c == ')';
        }
        protected bool isbinary_operater(char c, char last, char next)       //是否为双目连接词且逻辑正确
        {
            return (c == AND[0] || c == OR[0] || c == PREDICT[0] || c == EQUAL[0]) && (isletter(next) || isfrontbracket(next) || isNOT(next)) && (isletter(last) || isbackbracket(last));
        }
        protected void bracket_match(char c)       //检查括号匹配
        {
            if (isfrontbracket(c))
                bracket_count++;
            if (isbackbracket(c))
                bracket_count--;
        }
        protected bool isbracket_match()       //返回匹配结果
        {
            if (bracket_count == 0)
                return true;
            else
                bracket_count = 0;
            return false;
        }
    }
    class string_operate : char_check
    {
        private int find_another_bracket(int i)       //找到相匹配的括号
        {
            if (isfrontbracket(s[i]))
            {
                Int16 bracket_count = 1;
                for (int n = i + 1; n < s.Length; n++)
                {
                    if (isfrontbracket(s[n]))
                        bracket_count++;
                    if (isbackbracket(s[n]))
                        bracket_count--;
                    if (bracket_count == 0)
                        return n;
                }
            }
            if (isbackbracket(s[i]))
            {
                Int16 bracket_count = -1;
                for (int n = i - 1; n >= 0; n--)
                {
                    if (isbackbracket(s[n]))
                        bracket_count--;
                    if (isfrontbracket(s[n]))
                        bracket_count++;
                    if (bracket_count == 0)
                        return n;
                }
            }
            return -1;
        }
        private int find_another_bracket(int i, ref string _s)       //找到相匹配的括号
        {
            if (isfrontbracket(_s[i]))
            {
                Int16 bracket_count = 1;
                for (int n = i + 1; n < _s.Length; n++)
                {
                    if (isfrontbracket(_s[n]))
                        bracket_count++;
                    if (isbackbracket(_s[n]))
                        bracket_count--;
                    if (bracket_count == 0)
                        return n;
                }
            }
            if (isbackbracket(_s[i]))
            {
                Int16 bracket_count = -1;
                for (int n = i - 1; n >= 0; n--)
                {
                    if (isbackbracket(_s[n]))
                        bracket_count--;
                    if (isfrontbracket(_s[n]))
                        bracket_count++;
                    if (bracket_count == 0)
                        return n;
                }
            }
            return -1;
        }
        private int find_PREDICT_EQUAL()       //找到蕴含或等价
        {
            for (int i = 1; i < s.Length; i++)
                if (s[i] == '→' || s[i] == '↔')
                    return i;
            return 0;
        }
        private string divide(int i, bool order)       //将连接词前后拆分
        {
            string ds = null;
            if (order)
            {
                if (isletter(s[i]))
                    ds = s.Substring(i, 1);
                if (isNOT(s[i]))
                {
                    if (isletter(s[i + 1]))
                        ds = s.Substring(i, 2);
                    if (isfrontbracket(s[i + 1]))
                    {
                        int len = 0;
                        len = find_another_bracket(i + 1) - (i + 1) + 1;
                        ds = s.Substring(i, len);
                    }
                }
                if (isfrontbracket(s[i]))
                {
                    int len = 0;
                    len = find_another_bracket(i) - i + 1;
                    ds = s.Substring(i, len);
                }
            }
            else
            {
                if (isletter(s[i]))
                    if (i > 0)
                        if (isNOT(s[i - 1]))
                            ds = s.Substring(i - 1, 2);
                        else
                            ds = s.Substring(i, 1);
                    else
                        ds = s.Substring(i, 1);
                if (isbackbracket(s[i]))
                {
                    int len = 0, n = i;
                    len = n - find_another_bracket(n) + 1;
                    n = find_another_bracket(n);
                    if (n > 0)
                        if (isNOT(s[n - 1]))
                            ds = s.Substring(n - 1, len + 1);
                        else
                            ds = s.Substring(n, len);
                    else
                        ds = s.Substring(n, len);
                }
            }
            return ds;
        }
        private void NOT_correct()       //纠正 非 语法
        {
            while (true)
            {
                int i = s.IndexOf("¬(");
                if (i < 0)
                    break;
                string _s = s.Substring(i, find_another_bracket(i + 1) - i + 1);
                string news = _s.Substring(1, _s.Length - 1);
                for (i = 1; i < news.Length; i++)
                {
                    if (isletter(news[i]))
                    {
                        news = news.Insert(i, NOT);
                        i++;
                    }
                    else
                        if (isfrontbracket(news[i]))
                        {
                            news = news.Insert(i, NOT);
                            i++;
                            i = find_another_bracket(i, ref news);
                        }
                        else
                            if (news[i] == '⋀')
                            {
                                news = news.Remove(i, 1);
                                news = news.Insert(i, OR);
                            }
                            else
                                if (news[i] == '⋁')
                                {
                                    news = news.Remove(i, 1);
                                    news = news.Insert(i, AND);
                                }
                    if (news.IndexOf("¬¬") != -1)
                    {
                        news = news.Replace("¬¬", "");
                        i -= 2;
                    }
                }
                s = s.Replace(_s, news);
            }
        }
        private void bracket_correct()       //纠正括号语法
        {
            int i = -1;
            do
            {
                i = s.IndexOf("((", i + 1);
                if (i == -1)
                    break;
                if ((find_another_bracket(i) == (find_another_bracket(i + 1) + 1)))
                {
                    s = s.Remove(find_another_bracket(i), 1);
                    s = s.Remove(i, 1);
                }
            } while (true);
            if (s[0] == '(' && find_another_bracket(0) == s.Length - 1)
                s = s.Substring(1, s.Length - 2);
        }
        public string repalce()       //替换蕴含等价连接词
        {
            while (true)
            {
                int i = 0;
                string s1, s2, s3, news = "(";
                i = find_PREDICT_EQUAL();
                if (i == 0)
                    break;
                if (s[i] == '→')
                {
                    s2 = PREDICT;
                    s1 = divide(i - 1, false);
                    s3 = divide(i + 1, true);
                    news = news + NOT + s1 + OR + s3;
                }
                else
                {
                    s2 = EQUAL;
                    s1 = divide(i - 1, false);
                    s3 = divide(i + 1, true);
                    news = news + "(" + NOT + s1 + OR + s3 + ")" + AND + "(" + NOT + s3 + OR + s1 + ")";
                }
                news = news + ")";
                s = s.Replace(s1 + s2 + s3, news);
                s = s.Replace("¬¬", "");
                bracket_correct();
            }
            NOT_correct();
            bracket_correct();
            return s;
        }
    }
}
