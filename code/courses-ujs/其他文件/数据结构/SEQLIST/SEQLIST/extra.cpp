#include"seqlist.h"
/*



第  1  题



*/
int Delmin(SeqList<int> &sa)
{
	int min, a, b;
	int j = 1;
	sa.GetElem(1, min);
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		if (a < min)
		{
			min = a;
			j = i;
		}
	}
	sa.GetElem(sa.GetLength(), b);
	sa.SetElem(j, b);
	return min;
}


void Delel(SeqList<int> &sa, int e)
{
	int a;
	
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		if (a == e)
		{
			sa.DeleteElem(i, a);
			i--;
		}
	}
}

void Delrep(SeqList<int> &sa)
{
	int a, b,flag;
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		if (sa.GetElem(i, a)) return;
		for (int j = i + 1; j <= sa.GetLength(); j++)
			{
				if (sa.GetElem(j, b)) return;
				if (a == b)
				{
					flag=sa.DeleteElem(j, b);
					j--;
					if (flag) return;
				}
			}
	}
}



/*


第   2   题


*/

void Insertnum(SeqList<int> &sa, int e)
{
	int a;
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		if (e < a)
		{
			sa.InsertElem(i, e);
			break;
		}
	}
}

void Deletenum(SeqList<int> &sa, int e)
{
	int a;
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		if (e == a)
		{
			sa.DeleteElem(i, a);
			i--;
		}
	}

}

SeqList<int>  Toge(SeqList<int> &sa, SeqList<int> &sb)
{
	int a, b;
	SeqList<int> s(sa.GetLength()+sb.GetLength());
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		for (int j = 1; j <= sb.GetLength(); j++)
		{
			sb.GetElem(j, b);
			if (a>b) s.InsertElem(b);
			else s.InsertElem(a);
		}
	}
	return s;
}

void Deleteseq(SeqList<int> &sa, int s, int t)
{
	if (s >= t) return;
	if (sa.IsEmpty()) return;
	int a;
	for (int i = 1; i <= sa.GetLength(); i++)
	{
		sa.GetElem(i, a);
		if (a < s) continue;
		else if (a>t) break;
		else
		{
			sa.DeleteElem(i, a);
			i--;
		}
	}
}