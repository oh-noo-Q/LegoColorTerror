using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestAlgorithm : MonoBehaviour
{
    //Test Kongstudios
    public Button testBtn;
    public List<int> testNumber;
    public List<int> testNumber2;
    public List<List<int>> chessResult;
    public int testChess;

    private void Awake()
    {
        testBtn.onClick.AddListener(TestOnclick);
    }
    void TestOnclick()
    {
        //testNumber = plusOne(testNumber);
        //testNumber = Merge(testNumber, testNumber.Count - testNumber2.Count, testNumber2, testNumber2.Count);
        chessResult = QueenResult(testChess);
        for (int i = 0; i < testChess; i++)
        {
            Debug.Log($"{i} {chessResult[0][i]}");
        }
        //foreach (List<int> result in chessResult)
        //{
        //    foreach(int queen in result)
        //    {
        //        Debug.Log($"{result.IndexOf(queen)} {queen}");
        //    }
        //}
    }

    public static string reverseVowels(string s)
    {
        StringBuilder result = new StringBuilder();
        Stack<char> vowelStack = new Stack<char>();
        foreach (char x in s)
        {
            if (CheckVowel(x))
            {
                vowelStack.Push(x);
            }
        }

        foreach (char x in s)
        {
            if (CheckVowel(x))
            {
                result.Append(vowelStack.Pop());
            }
            else result.Append(x);
        }

        return result.ToString();
    }

    static bool CheckVowel(char x)
    {
        if (x == 'a' || x == 'A' || x == 'e' || x == 'E' || x == 'i' || x == 'I' || x == 'o' || x == 'O' || x == 'u' || x == 'U')
        {
            return true;
        }
        else return false;
    }

    public static List<int> plusOne(List<int> digits)
    {
        bool bonus = false;
        for (int i = digits.Count - 1; i >= 0; i--)
        {
            if (digits[i] == 9)
            {
                digits[i] = 0;
            }
            else
            {
                digits[i] = digits[i] + 1;
                bonus = true;
                break;
            }

        }
        if (!bonus)
        {
            digits.Insert(0, 1);
        }
        return digits;
    }

    public List<int> Merge(List<int> nums1, int m, List<int> nums2, int n)
    {
        List<int> result = new List<int>();
        int start1 = 0;
        int start2 = 0;
        for (int i = 0; i < m + n; i++)
        {
            if (start1 < n && start2 < m)
            {
                if (nums1[start1] < nums2[start2])
                {
                    result.Add(nums1[start1]);
                    start1++;
                }
                else
                {
                    result.Add(nums2[start2]);
                    start2++;
                }
            }
            else if (start1 >= n)
            {
                result.Add(nums2[start2]);
                start2++;
            }
            else if (start2 >= m)
            {
                result.Add(nums1[start1]);
                start1++;
            }
        }
        return result;
    }

    public bool IsHappy(int n)
    {
        HashSet<int> set = new HashSet<int>();
        while (n != 1 && !set.Contains(n))
        {
            set.Add(n);
            n = Square(n);
        }
        return n == 1;
    }

    public int Square(int number)
    {
        int sum = 0;
        while (number > 0)
        {
            int unit = number % 10;
            sum += unit * unit;
            number /= 10;
        }
        return sum;
    }

    public IList<IList<string>> SolveNQueens(int n)
    {
        int[][] board = new int[n][];
        List<List<int>> result = QueenResult(n);
        IList<IList<string>> res = new List<IList<string>>();
        foreach (List<int> chess in result)
        {
            List<string> boardResult = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string row = "";
                for (int j = 0; j < n; j++)
                {
                    if (chess[j] == 1) row += "Q";
                    else row += ".";
                }
                boardResult.Add(row);
            }
            res.Add(boardResult);
        }
        return res;
    }

    static List<List<int>> QueenResult(int n)
    {
        int[,] board = new int[n, n];
        List<List<int>> result = new List<List<int>>();
        AddQueenBoard(n, board, 0, result);
        return result;
    }

    static void AddQueenBoard(int size, int[,] board, int iRow, List<List<int>> list)
    {
        if (iRow == size)
        {
            list.Add(AddPositionQueen(size, board));
            return;
        }
        for (int i = 0; i < size; i++)
        {
            if (CheckSafeQueen(size, board, iRow, i))
            {
                board[iRow, i] = 1;
                AddQueenBoard(size, board, iRow + 1, list);
                board[iRow, i] = 0;
            }
        }
    }

    static bool CheckSafeQueen(int size, int[,] board, int iRow, int iCol)
    {
        for (int i = 0; i < iRow; i++)
        {
            if (board[i, iCol] == 1)
            {
                return false;
            }
        }

        for (int i = iRow - 1, j = iCol - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (board[i, j] == 1)
            {
                return false;
            }
        }

        for (int i = iRow - 1, j = iCol + 1; i >= 0 && j < size; i--, j++)
        {
            if (board[i, j] == 1)
            {
                return false;
            }
        }

        return true;
    }

    static List<int> AddPositionQueen(int size, int[,] board)
    {
        List<int> positionResult = new List<int>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == 1)
                {
                    positionResult.Add(j);
                }
            }
        }
        return positionResult;
    }
}
