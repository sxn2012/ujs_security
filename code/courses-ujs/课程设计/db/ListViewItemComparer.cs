using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineOrdering
{
    class ListViewItemComparer:IComparer
    {
        public bool sort_b;
        public SortOrder order = SortOrder.Ascending;
        private int col;
        public  ListViewItemComparer()//无参构造函数
        {
            col = 0;
        }

        public  ListViewItemComparer(int column, bool sort)//有参构造函数
        {
            col = column;
            sort_b = sort;
        }

        public int Compare(object x, object y)//实现比较函数
        {
            if (Regex.IsMatch(((ListViewItem)x).SubItems[col].Text, @"^\d+(\.\d+)?$") && Regex.IsMatch(((ListViewItem)y).SubItems[col].Text, @"^\d+(\.\d+)?$"))//选择的对应列为数字类型
            {
                //按照数字大小进行比较
                decimal a = Convert.ToDecimal(((ListViewItem)x).SubItems[col].Text);
                decimal b = Convert.ToDecimal(((ListViewItem)y).SubItems[col].Text);
                if (sort_b)
                {
                    if (a > b) return 1;
                    else if (a == b) return 0;
                    else if (a < b) return -1;
                }
                else
                {
                    if (b > a) return 1;
                    else if (b == a) return 0;
                    else if (b < a) return -1;
                }
            }
            //选择的对应列为字符串
            //按照字符串比较函数进行比较
            if (sort_b)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
            else
            {
                return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }
    }
}
