var cancellationTokenSource = new CancellationTokenSource();
var task = new Task(() =>
{
    try
    {
        Console.WriteLine("Execute Some Code 1");
        Thread.Sleep(1500);//执行了2秒中代码
        cancellationTokenSource.Token.ThrowIfCancellationRequested();
        Console.WriteLine("Execute Some Code 2");
    }
    catch
    {
        Console.WriteLine("Cancel Execute");
    }
}, cancellationTokenSource.Token);

task.Start();//启动，等待调度执行

Console.WriteLine("Execute Some Code");
Thread.Sleep(1000);////一段时间后发现不对，可以取消task执行
Console.WriteLine("Task状态：" + task.Status);//Canceled
cancellationTokenSource.Cancel();
Console.WriteLine("Cancel in 1s");
Thread.Sleep(1000);//等待1秒
Console.WriteLine("Task状态：" + task.Status);//Canceled
Thread.Sleep(10000);//等待10秒
Console.WriteLine("Task状态：" + task.Status);//Canceled
