using System.Text.RegularExpressions;

string strs = @"新增办理社保卡135条
查询社保卡信息59条
注销补卡16条
领取库存卡14条
删除失败数据1条


新增办理社保卡135条
查询社保卡信息59条
注销补卡16条
领取库存卡14条


新增办理社保卡135条
查询社保卡信息59条
注销补卡16条
修改医保密码1条
";

var list =new Dictionary<string,int>();
foreach (var str in strs.Replace("\r","").Split('\n').Where(a=>a.Length>0))
{
    var result = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", ""));
    var key = str.Replace(result.ToString(), "").Replace("条", "");
    if (list.ContainsKey(key))
    {
        list[key]+= result;
    }
    else
    {
        list.Add(key, result);
    }

}

foreach (var item in list.OrderBy(a=>a.Key))
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}