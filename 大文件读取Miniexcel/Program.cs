// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using MiniExcelLibs;
using System.Diagnostics;
using System.IO;

var sw = new Stopwatch ();
sw.Start ();
Console.WriteLine("Hello, World!");
var row = MiniExcel.Query(@"C:\Users\Alex\Desktop\RawData.csv").Skip(200000).Take(1).ToList();

Console.WriteLine(row[0].A);
//row.ToList().ForEach(a=> Console.WriteLine(a.A));

sw.Stop();
Console.WriteLine($"Cost {sw.ElapsedMilliseconds} ms");
