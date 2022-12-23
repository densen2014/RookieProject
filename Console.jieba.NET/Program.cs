// See https://aka.ms/new-console-template for more information
using JiebaNet.Analyser;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.Common;
using JiebaNet.Segmenter.PosSeg;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Hello, 请输入句子:");

do
{
    var str = Console.ReadLine();

    处理(str?? "一楼的空调,二楼的空调");

} while (true);

static void 处理(string str)
{
    Console.WriteLine("【原文】：{0}", str);

    var segmenter = new JiebaSegmenter();
    var segments = segmenter.Cut(str, cutAll: true);
    Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));

    segments = segmenter.Cut(str);  // 默认为精确模式
    Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

    segments = segmenter.Cut(str);  // 默认为精确模式，同时也使用HMM模型
    Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));

    segments = segmenter.CutForSearch(str); // 搜索引擎模式
    Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

    segments = segmenter.Cut(str);
    Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

    var tfidfExtractor = new TfidfExtractor();
    var tffe = tfidfExtractor.ExtractTags(str, 20, null);
    Console.WriteLine("【TF-IDF关键词提取】：{0}", string.Join("/ ", tffe));

    var tffe2 = tfidfExtractor.ExtractTagsWithWeight(str, 20, null);
    tffe2.ToList().ForEach(a => Console.WriteLine($"【TF-IDF关键词提取】：{a.Word}  权重{a.Weight} "));

    var textRankExtractor = new TextRankExtractor();
    var tre = textRankExtractor.ExtractTags(str, 20, null);
    Console.WriteLine("【TextRank关键词提取】：{0}", string.Join("/ ", tre));

    var tre2 = textRankExtractor.ExtractTagsWithWeight(str, 20, null);
    tre2.ToList().ForEach(a => Console.WriteLine($"【TextRank关键词提取】：{a.Word}  权重{a.Weight} "));

    var posSeg = new PosSegmenter();
    var tokens = posSeg.Cut(str);
    Console.WriteLine("【词性标注】：{0}", string.Join(" ", tokens.Select(token => string.Format("{0}/{1}", token.Word, token.Flag))));

    var tokens2 = segmenter.Tokenize(str);
    Console.WriteLine("【返回词语在原文的起止位置-默认】");
    tokens2.ToList().ForEach(token => Console.WriteLine("{0,-12} start: {1,-3} end: {2,-3}", token.Word, token.StartIndex, token.EndIndex));
    
    tokens2 = segmenter.Tokenize(str, TokenizerMode.Search);
    Console.WriteLine("【返回词语在原文的起止位置-搜索模式】");
    tokens2.ToList().ForEach(token => Console.WriteLine("{0,-12} start: {1,-3} end: {2,-3}", token.Word, token.StartIndex, token.EndIndex));

    //并行分词
    //使用如下方法：

    //JiebaSegmenter.CutInParallel()、JiebaSegmenter.CutForSearchInParallel()
    //PosSegmenter.CutInParallel()

    var freqs = new Counter<string>(segmenter.Cut(str));
    Console.WriteLine("【词频统计】");
    tokens2.ToList().ForEach(token => Console.WriteLine("{0,-12} start: {1,-3} end: {2,-3}", token.Word, token.StartIndex, token.EndIndex));

    Console.WriteLine("【KeywordProcessor】");
    str = "你需要通过cet-4考试，学习c语言、.NET core、网络 编程、JavaScript，掌握字典 tree的用法";
    Console.WriteLine("【原文】：{0}", str);
    var kp = new KeywordProcessor();
    kp.AddKeywords(new[] { ".NET Core", "Java", "C语言", "字典 tree", "CET-4", "网络 编程" });

    var keywords = kp.ExtractKeywords(str);

    // keywords 值为：
    // new List<string> { "CET-4", "C语言", ".NET Core", "网络 编程", "字典 tree"}
    Console.WriteLine("【关键词提取】：{0}", string.Join("/ ", keywords));


    // 可以看到，结果中的词与开始添加的关键词相同，与输入句子中的词则不尽相同。如果需要返回句中找到的原词，可以使用 `raw` 参数。

    var keywords2 = kp.ExtractKeywords(str, raw: true);

    // keywords2 值为：
    // new List<string> { "cet-4", "c语言", ".NET core", "网络 编程", "字典 tree"}
    Console.WriteLine("【关键词raw提取】：{0}", string.Join("/ ", keywords2));
}
